define(['repositories/boardRepository', 'repositories/itemRepository','mappers/markMapper','repositories/markRepository',
    'durandal/app', 'mappers/boardMapper', 'mappers/itemMapper', 'constants', 'services/boardService',
    'repositories/criterionRepository', 'mappers/criterionMapper', 'spinner', 'services/validateService'],
    function (boardRepository, itemRepository,markMapper, markRepository, app, boardMapper, itemMapper, constants, boardService,
        criterionRepository, criterionMapper, spinner, validateService) {
        var board;

        return viewModel = {
            board: {},
            columnCount: ko.observable(),
            sorted: ko.observable(),
            sortAscending: ko.observable(),
            benefitCriterions: ko.observableArray([]),
            costCriterions: ko.observableArray([]),

            filterValue: ko.observable("").extend({
                validate: validateService.validateObservableFilterValue
            }),

            activate,
            sortByRank,
            updateItemTitle,
            deleteItem,
            addItem,
            setMark,
            setWeight,
            updateCriterionTitle,
            deleteCriterion,
            addCostCriterion,
            addBenefitCriterion,
            applyFilter
        }

        function activate(boardId) {
            var self = this;
            boardId = boardId || "ccef5cf6-5184-4a5a-8234-c2df683cbfba";

            spinner.show();
            return boardRepository.getBoard(boardId).then(function (data) {
                var b = boardMapper.map(data);

                boardRepository.updateOpenedBoard(b);

                board = boardMapper.mapToViewModel(b);

                self.board = board;

                _.each(board.criterions(), function (criterion) {
                    if (criterion.isBenefit) {
                        self.benefitCriterions.push(criterion);
                    } else {
                        self.costCriterions.push(criterion);
                    }
                })

                self.columnCount = ko.computed(function () {
                    return self.board.criterions().length + 3;
                })

                self.sortByRank();

                app.on('board:item-changed', function () {
                    self.sorted(false);
                });

                self.sortAscending(true);
                self.sorted(true);

                boardService.boardChanged(board);

                self.sortAscending(true);
                self.sorted(true);

                self.filterValue.subscribe(function () { applyFilter(); });

                spinner.hide();
            });
        };

        function sortByRank(){
            board.items(_.sortBy(board.items(), function (item) {
                return item.score();
            }));

            if (!viewModel.sortAscending())
                board.items.reverse();

            viewModel.sorted(true);
            viewModel.sortAscending(!viewModel.sortAscending());
        };

        function updateItemTitle (item) {
            item.title(item.title().trim());

            itemRepository.setTitle(item.title(), item.id);
        };

        function deleteItem(item, event) {
            var title = item.title();

            if (title.length > 50)
                title = title.slice(0, 50) + "...";


            $(constants.popupTemplatesId.confirmation).popup({ title: "Delete", body: 'delete "' + title + '"' })
            .then(function (s) { 
                if (s) {
                    itemRepository.remove(item.id);
                    board.items.remove(item);

                    boardService.boardChanged(board);
                }
            });            
        };

        function addItem() {
            spinner.show();

            itemRepository.getNewItem().then(function(item){
                item = itemMapper.mapToViewModel(item);

                board.items.unshift(item);

                boardService.boardChanged(board);

                spinner.hide();
            });
        };

        function setMark (mark) {
            if (_.isUndefined(mark.id)) {
                markRepository.createMark(mark.itemId, mark.criterionId).then(function (newMark) {
                    mark.id = newMark.id;

                    return markRepository.setValue(+mark.value(), mark.id, mark.itemId, mark.criterionId);
                });
            } else {
                markRepository.setValue(+mark.value(), mark.id, mark.itemId, mark.criterionId);
            }

            boardService.boardChanged(board);
        };

        function setWeight (criterion) {
            criterionRepository.setWeight(criterion.weight(), criterion.id);

            boardService.criterionChanged(criterion);

            boardService.boardChanged(board);
        };

        function updateCriterionTitle (criterion) {
            criterionRepository.setTitle(criterion.title, criterion.id);

            boardService.criterionChanged(criterion);

            boardService.boardChanged(board);
        };

        function deleteCriterion(criterion) {
            if (criterion.isBenefit){                
                if (viewModel.benefitCriterions().length <= 1)
                    return;
            } else if (viewModel.costCriterions().length <= 1) {
                return;
            }
            var title = criterion.title();

            if (criterion.length > 50)
                criterion = criterion.slice(0, 50) + "...";


            $(constants.popupTemplatesId.confirmation).popup({ title: "Delete", body: 'delete "' + title + '"' })
            .then(function (s) {
                if (s) {
                    criterionRepository.remove(criterion.id);
                    board.criterions.remove(criterion);
                    
                    _.each(board.items(), function (item) {
                        delete item.marks[criterion.id];
                    });
                    
                    if (criterion.isBenefit)
                        viewModel.benefitCriterions.remove(criterion);
                    else
                        viewModel.costCriterions.remove(criterion);

                    boardService.criterionChanged(criterion);

                    boardService.boardChanged(board);
                }
            });
        };

        function addCriterion(isBenefit) {
            spinner.show();

            criterionRepository.getNewCriterion(isBenefit).then(function (criterion) {
                criterion = criterionMapper.mapToViewModel(criterion);

                _.each(board.items(), function (item) {
                    item.marks[criterion.id] = markMapper.mapToViewModel({
                        Value: 0,
                        CriterionId: criterion.id,
                        ItemId: item.id
                    });
                });

                board.criterions.unshift(criterion);

                if (isBenefit)
                    viewModel.benefitCriterions.push(criterion);
                else
                    viewModel.costCriterions.push(criterion);                

                boardService.criterionChanged(criterion);

                boardService.boardChanged(board);

                spinner.hide();
            });
        };

        function addBenefitCriterion() {
            addCriterion(true);
        };

        function addCostCriterion () {
            addCriterion(false);
        };

        function applyFilter() {
            var countVisible = 0;

            _.each(board.items(), function (item) {
                var itemLowerValue = item.title().toLowerCase();
                var filterLowerValue = viewModel.filterValue().toLowerCase();

                if (itemLowerValue.indexOf(filterLowerValue) + 1) {
                    item.visible(true);
                    countVisible++;
                } else {
                    item.visible(false);
                }
            });

            board.items.countVisible(countVisible);
        }
});