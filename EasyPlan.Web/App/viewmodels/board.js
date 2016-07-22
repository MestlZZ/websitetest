define(['repositories/boardRepository', 'repositories/itemRepository','mappers/markMapper','repositories/markRepository',
    'durandal/app', 'mappers/boardMapper', 'mappers/itemMapper', 'constants', 'services/boardService',
    'repositories/criterionRepository', 'mappers/criterionMapper', 'spinner', 'services/validators'],
    function (boardRepository, itemRepository,markMapper, markRepository, app, boardMapper, itemMapper, constants, boardService,
        criterionRepository, criterionMapper, spinner, validators) {

        var mappedCriterions = [];


        return viewModel = {
            board: {},
            columnCount: ko.observable(),
            sorted: ko.observable(),
            sortAscending: ko.observable(),
            benefitCriterions: ko.observableArray([]),
            costCriterions: ko.observableArray([]),

            filterValue: ko.observable("").extend({
                validate: validators.validateObservableFilterValue
            }),

            activate,
            sortByRank,
            updateItemTitle,
            deleteItem,
            addItem,
            setMark,
            setWeight,
            updateCriterionTitle,
            updateBoardTitle,
            deleteCriterion,
            addCostCriterion,
            addBenefitCriterion,
            applyFilter
        }

        function activate(boardId) {
            var self = this;

            spinner.show();
            return boardRepository.getBoard(boardId).then(function (data) {
                var board = boardMapper.map(data);

                self.board = mapBoard(board);

                _.each(self.board.criterions(), function (criterion) {
                    if (criterion.isBenefit) {
                        self.benefitCriterions.push(criterion);
                    } else {
                        self.costCriterions.push(criterion);
                    }
                })

                self.sortByRank();

                app.on('board:item-changed', function () {
                    self.sorted(false);
                });

                boardService.boardChanged(self.board);

                self.sortAscending(true);
                self.sorted(true);

                self.filterValue.subscribe(function () { applyFilter(); });
                self.board.items.subscribe(function () { applyFilter(); });
                spinner.hide();
            });
        };

        function sortByRank(){
            viewModel.board.items(_.sortBy(viewModel.board.items(), function (item) {
                return item.score();
            }));

            if (!viewModel.sortAscending())
                viewModel.board.items.reverse();

            viewModel.sorted(true);
            viewModel.sortAscending(!viewModel.sortAscending());
        };

        function updateItemTitle (item) {
            item.title(item.title().trim());

            itemRepository.setTitle(item.title(), item.id);
        };

        function updateBoardTitle(context) {
            context.board.title(context.board.title().trim());

            boardRepository.setTitle(context.board.title(), context.board.id);
        };

        function deleteItem(item, event) {
            var title = item.title();

            if (title.length > 50)
                title = title.slice(0, 50) + "...";


            $(constants.popupTemplatesId.confirmation).popup({ title: "Delete", body: 'delete "' + title + '"' })
            .then(function (s) { 
                if (s) {
                    itemRepository.remove(item.id);
                    viewModel.board.items.remove(item);

                    boardService.boardChanged(viewModel.board);
                }
            });            
        };

        function addItem() {
            spinner.show();

            itemRepository.getNewItem(viewModel.board.id).then(function(item){
                item = mapItem(item);

                viewModel.board.items.unshift(item);

                boardService.boardChanged(viewModel.board);

                spinner.hide();
            });
        };

        function setMark (mark) {
            if (_.isNull(mark.id)) {
                markRepository.createMark(mark.itemId, mark.criterionId).then(function (newMark) {
                    mark.id = newMark.id;

                    return markRepository.setValue(+mark.value(), mark.id, mark.itemId, mark.criterionId);
                });
            } else {
                markRepository.setValue(+mark.value(), mark.id, mark.itemId, mark.criterionId);
            }

            boardService.boardChanged(viewModel.board);
        };

        function setWeight (criterion) {
            criterionRepository.setWeight(criterion.weight(), criterion.id);

            boardService.criterionChanged(criterion);

            boardService.boardChanged(viewModel.board);
        };

        function updateCriterionTitle (criterion) {
            criterionRepository.setTitle(criterion.title, criterion.id);

            boardService.criterionChanged(criterion);

            boardService.boardChanged(viewModel.board);
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
                    viewModel.board.criterions.remove(criterion);
                    
                    _.each(viewModel.board.items(), function (item) {
                        delete item.marks[criterion.id];
                    });
                    
                    if (criterion.isBenefit)
                        viewModel.benefitCriterions.remove(criterion);
                    else
                        viewModel.costCriterions.remove(criterion);

                    boardService.criterionChanged(criterion);

                    boardService.boardChanged(viewModel.board);
                }
            });
        };

        function addCriterion(isBenefit) {
            spinner.show();

            criterionRepository.getNewCriterion(isBenefit, viewModel.board.id).then(function (criterion) {
                criterion = mapCriterion(criterion);

                _.each(viewModel.board.items(), function (item) {
                    item.marks[criterion.id] = mapMark({
                        id: null,
                        value: 0,
                        criterionId: criterion.id,
                        itemId: item.id
                    }, criterion);
                    
                    item.score = ko.computed(function () {
                        return boardService.computeScore(item.marks);
                    });
                });

                viewModel.board.criterions.unshift(criterion);

                if (isBenefit)
                    viewModel.benefitCriterions.push(criterion);
                else
                    viewModel.costCriterions.push(criterion);                

                boardService.criterionChanged(criterion);

                boardService.boardChanged(viewModel.board);

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

            _.each(viewModel.board.items(), function (item) {
                var itemLowerValue = item.title().toLowerCase();
                var filterLowerValue = viewModel.filterValue().toLowerCase();

                if (itemLowerValue.indexOf(filterLowerValue) + 1) {
                    item.visible(true);
                    countVisible++;
                } else {
                    item.visible(false);
                }
            });

            viewModel.board.items.countVisible(countVisible);
        }

            function mapBoard(board) {
                var boardViewModel = {};

                boardViewModel.id = board.id;
                boardViewModel.title = ko.observable(board.title).extend({
                    validate: validators.validateObservableBoardTitle
                });
                boardViewModel.criterions = ko.observableArray(_.map(board.criterions, mapCriterion));

                mappedCriterions = boardViewModel.criterions;

                boardViewModel.items = ko.observableArray(_.map(board.items, mapItem));

                boardViewModel.items.countVisible = ko.observable(board.items.length);

                return boardViewModel;
            }

            function mapCriterion(criterion) {
                var criterionViewModel = {};

                criterionViewModel.id = criterion.id;
                criterionViewModel.isBenefit = criterion.isBenefit;
                criterionViewModel.title = ko.observable(criterion.title).extend({
                    validate: validators.validateObservableTitle
                });
                criterionViewModel.weight = ko.observable(criterion.weight).extend({
                    validate: validators.validateObservableWeightValue
                });

                return criterionViewModel;
            }

            function mapItem(item) {
                var itemViewModel = {};

                itemViewModel.id = item.id;
                itemViewModel.title = ko.observable(item.title).extend({
                    validate: validators.validateObservableTitle
                });
                itemViewModel.marks = {};

                _.map(mappedCriterions(), function (criterion) {
                    var mark = _.find(item.marks, function(mark){return mark.criterionId == criterion.id});

                    if (_.isUndefined(mark)){
                        itemViewModel.marks[criterion.id] = mapMark({id: null, criterionId: criterion.id, itemId: item.id, value: 0 }, criterion);
                    } else {
                        itemViewModel.marks[criterion.id] = mapMark(mark, criterion);
                    }                    
                })

                itemViewModel.rank = ko.observable();
                itemViewModel.score = ko.computed(function () {
                    return boardService.computeScore(itemViewModel.marks);
                });
                itemViewModel.visible = ko.observable(true);

                return itemViewModel;
            }

            function mapMark(mark, criterion) {
                var markViewModel = {};

                markViewModel.id = mark.id;
                markViewModel.value = ko.observable(mark.value).extend({
                    validate: validators.validateObservableMarkValue
                })
                markViewModel.criterionId = mark.criterionId;
                markViewModel.itemId = mark.itemId;
                markViewModel.isBenefit = criterion.isBenefit;
                markViewModel.weight = criterion.weight;

                return markViewModel;
            }   
});