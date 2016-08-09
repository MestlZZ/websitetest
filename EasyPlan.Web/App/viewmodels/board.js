define(['plugins/router', 'repositories/boardRepository', 'repositories/itemRepository','mappers/markMapper','repositories/markRepository',
    'durandal/app', 'mappers/boardMapper', 'mappers/itemMapper', 'constants', 'services/boardService',
    'repositories/criterionRepository', 'mappers/criterionMapper', 'spinner', 'services/validators', 'synchronization', 'viewmodels/boards', 'error', 'widgets/boardSettings/viewModel'],
    function (router, boardRepository, itemRepository,markMapper, markRepository, app, boardMapper, itemMapper, constants, boardService,
        criterionRepository, criterionMapper, spinner, validators, sync, boards, error, boardSettings) {

        var boardHub = $.connection.boardHub;

        /*Hub events*/
        app.on(constants.EVENT.BOARD.COLLABORATOR.ROLE_CHANGED, function (email, role, boardId) {
            if (boardId == boardViewModel.board.id) {
                if (boards.user.email == email) {
                    boardSettings.clientRole(role);
                    boardViewModel.userRole(role);
                }
            }
        });

        app.on(constants.EVENT.BOARD.REMOVED, function (boardId) {            
            if (boardId == boardViewModel.board.id && sync.isBoardOpened) {
                error.throw('Board was deleted by creator', 404);
            }
        });

        app.on(constants.EVENT.BOARD.COLLABORATOR.REMOVED, function (email, boardId) {
            if (boardId == boardViewModel.board.id && sync.isBoardOpened) {
                if (boards.user.email == email) {
                    error.throw('You was removed from this board by admin', 401);
                }
            }
        })

        app.on(constants.EVENT.BOARD.ITEM.TITLE_CHANGED, function (id, title) {
            var item = _.find(boardViewModel.board.items(), function (item) { return id == item.id; });

            boardViewModel.isFocusEnabled(false);

            item.title(title);
        });

        app.on(constants.EVENT.BOARD.ITEM.REMOVED, function (id) {
            var item = _.find(boardViewModel.board.items(), function (item) { return id == item.id; });

            boardViewModel.board.items.remove(item);
            boardViewModel.sorted(false);

            boardService.setRanks(boardViewModel.board.items());
        });

        app.on(constants.EVENT.BOARD.ITEM.ADDED, function (item) {
            item = mapItem(item, boardViewModel.board.criterions);

            boardViewModel.board.items.unshift(item);
            boardViewModel.sorted(false);

            boardService.setRanks(boardViewModel.board.items());
        });

        app.on(constants.EVENT.BOARD.MARK.VALUE_CHANGED, function (mark) {
            var item = _.find(boardViewModel.board.items(), function (item) { return mark.itemId == item.id; });
            var criterion = _.find(boardViewModel.board.criterions(), function (criterion) { return mark.criterionId == criterion.id; });

            item.marks()[mark.criterionId].id = mark.id;
            item.marks()[mark.criterionId].value(mark.value);

            boardViewModel.sorted(false);

            boardService.setRanks(boardViewModel.board.items());
        });

        app.on(constants.EVENT.BOARD.CRITERION.WEIGHT_CHANGED, function (id, weight) {
            var criterion = _.find(boardViewModel.board.criterions(), function (criterion) { return id == criterion.id; });

            criterion.weight(weight);
            boardViewModel.sorted(false);
        });

        app.on(constants.EVENT.BOARD.CRITERION.TITLE_CHANGED, function (id, title) {
            var criterion = _.find(boardViewModel.board.criterions(), function (criterion) { return id == criterion.id; });

            boardViewModel.isFocusEnabled(false);

            criterion.title(title);
        });

        app.on(constants.EVENT.BOARD.TITLE_CHANGED, function (title) {
            boardViewModel.board.title(title);
        });

        app.on(constants.EVENT.BOARD.CRITERION.REMOVED, function (id) {
            var criterion = _.find(boardViewModel.board.criterions(), function (criterion) { return id == criterion.id; });

            boardViewModel.board.criterions.remove(criterion);

            if (criterion.isBenefit)
                boardViewModel.benefitCriterions.remove(criterion);
            else
                boardViewModel.costCriterions.remove(criterion);

            _.each(boardViewModel.board.items(), function (item) {
                delete item.marks()[criterion.id];

                item.marks.notifySubscribers();
            });

            boardViewModel.sorted(false);

            boardService.setRanks(boardViewModel.board.items());
        });

        app.on(constants.EVENT.BOARD.CRITERION.ADDED, function (criterion) {
            var criterion = mapCriterion(criterion);

            _.each(boardViewModel.board.items(), function (item) {
                item.marks()[criterion.id] = mapMark({
                    id: null,
                    value: 0,
                    criterionId: criterion.id,
                    itemId: item.id
                }, criterion);

                item.marks.notifySubscribers();
            });

            boardViewModel.board.criterions.unshift(criterion);

            if (criterion.isBenefit)
                boardViewModel.benefitCriterions.push(criterion);
            else
                boardViewModel.costCriterions.push(criterion);
        });
        /*End*/

        var boardViewModel = {
            board: {},
            ROLE: constants.ROLE,
            settingsVisible: ko.observable(false),
            sorted: ko.observable(true),
            isFocusEnabled: ko.observable(false),
            sortAscending: ko.observable(true),
            benefitCriterions: ko.observableArray([]),
            costCriterions: ko.observableArray([]),
            userRole: ko.observable(0),

            filterValue: ko.observable('').extend({
                validate: validators.validateFilterValue
            }),

            activate: activate,
            sortByRank: sortByRank,
            updateItemTitle: updateItemTitle,
            deleteItem: deleteItem,
            addItem: addItem,
            setMark: setMark,
            setWeight: setWeight,
            updateCriterionTitle: updateCriterionTitle,
            updateBoardTitle: updateBoardTitle,
            deleteCriterion: deleteCriterion,
            addCostCriterion: addCostCriterion,
            addBenefitCriterion: addBenefitCriterion,
            applyFilter: applyFilter,
            detached: detached
        };

        return boardViewModel;

        function detached() {
            sync.closeBoard();
        }

        function activate(boardId) {
            spinner.show();

            var self = this;

            sync.openBoard(boardId);

            self.board = {};
            self.sorted(true);
            self.sortAscending(true);
            self.benefitCriterions([]);
            self.costCriterions([]);
            self.userRole('');
            self.settingsVisible(false);
            self.filterValue('');            

            return boardRepository.getBoard(boardId)
                .then(function (data) {
                    if (_.isUndefined(data))
                    {
                        return;
                    }

                    var board = boardMapper.map(data.board);

                    self.userRole(data.clientRole);
                    self.board = mapBoard(board);

                    _.each(self.board.criterions(), function (criterion) {
                        if (criterion.isBenefit) {
                            self.benefitCriterions.push(criterion);
                        } else {
                            self.costCriterions.push(criterion);
                        }
                    });

                    boardService.setRanks(self.board.items());

                    self.sortByRank();

                    self.filterValue.subscribe(function () { applyFilter(); });
                    self.board.items.subscribe(function () { applyFilter(); });

                    spinner.hide();

                    return boardSettings.activate({ boardId: boardId });
                });
        }

        function sortByRank() {
            boardViewModel.board.items(_.sortBy(boardViewModel.board.items(), function (item) {
                return item.score();
            }));

            if (!boardViewModel.sortAscending())
                boardViewModel.board.items.reverse();

            boardViewModel.sorted(true);
            boardViewModel.sortAscending(!boardViewModel.sortAscending());
        }

        function updateItemTitle(item) {
            spinner.show();
            itemRepository.setTitle(boardViewModel.board.id, item.title(), item.id)
                .then(function () {
                    boardHub.server.updateItemTitle(boardViewModel.board.id, item.id, item.title());

                    spinner.hide();
                });
        }

        function updateBoardTitle(context) {
            spinner.show();

            boardRepository.setTitle(context.board.title(), context.board.id)
                .then(function () {
                    boardHub.server.updateBoardTitle(boardViewModel.board.id, context.board.title());

                    spinner.hide();
            });
        }

        function deleteItem(item, event) {
            var title = item.title();

            if (title.length > 50)
                title = title.slice(0, 50) + '...';

            $(constants.popupTemplatesId.confirmation).popup({ title: 'Delete', body: 'delete "' + title + '"' })
            .then(function (response) { 
                if (response) {
                    spinner.show();

                    itemRepository.remove(item.id, boardViewModel.board.id).then(function () {

                        boardHub.server.deleteItem(boardViewModel.board.id, item.id);
                        spinner.hide();
                    });
                }
            });            
        }

        function addItem() {
            spinner.show();

            itemRepository.getNewItem(boardViewModel.board.id).then(function (item) {
                boardViewModel.isFocusEnabled(true);

                boardHub.server.addItem(boardViewModel.board.id, item);
                spinner.hide();
            });
        }

        function setMark(mark) {
            spinner.show();

            var newMark = {
                id: mark.id,
                value: mark.value(),
                criterionId: mark.criterionId,
                itemId: mark.itemId
            };

            markRepository.setValue(mark.itemId, mark.criterionId, boardViewModel.board.id, +mark.value()).then(function () {
                boardHub.server.setMark(boardViewModel.board.id, newMark);

                spinner.hide();
            });
        }

        function setWeight(criterion) {
            spinner.show();

            criterionRepository.setWeight(criterion.weight(), criterion.id, boardViewModel.board.id).then(function () {
                boardHub.server.setCriterionWeight(boardViewModel.board.id, criterion.id, criterion.weight());

                spinner.hide();
            });
        }

        function updateCriterionTitle(criterion) {
            spinner.show();

            criterionRepository.setTitle(criterion.title, criterion.id, boardViewModel.board.id).then(function () {
                boardHub.server.updateCriterionTitle(boardViewModel.board.id, criterion.id, criterion.title());

                spinner.hide();
            });
        }

        function deleteCriterion(criterion) {
            if (criterion.isBenefit && boardViewModel.benefitCriterions().length <= 1) { 
                return;
            } else if (!criterion.isBenefit && boardViewModel.costCriterions().length <= 1) {
                return;
            }

            var title = criterion.title();

            if (criterion.length > 50)
                criterion = criterion.slice(0, 50) + '...';

            $(constants.popupTemplatesId.confirmation).popup({ title: 'Delete', body: 'delete "' + title + '"' })
                .then(function (response) {
                    if (response) {
                        spinner.show();

                        criterionRepository.remove(criterion.id, boardViewModel.board.id).then(function () {
                            boardHub.server.deleteCriterion(boardViewModel.board.id, criterion.id);

                            spinner.hide();
                        });
                    }
                });
        }

        function addCriterion(isBenefit) {
            spinner.show();

            criterionRepository.getNewCriterion(isBenefit, boardViewModel.board.id)
                .then(function (criterion) {
                    boardViewModel.isFocusEnabled(true);

                    boardHub.server.addCriterion(boardViewModel.board.id, criterion);

                    spinner.hide();
                });
        }

        function addBenefitCriterion() {
            addCriterion(true);
        }

        function addCostCriterion () {
            addCriterion(false);
        }

        function applyFilter() {
            var countVisible = 0;

            _.each(boardViewModel.board.items(), function (item) {
                var itemLowerValue = item.title().toLowerCase();
                var filterLowerValue = boardViewModel.filterValue().toLowerCase();

                if (itemLowerValue.indexOf(filterLowerValue) + 1) {
                    item.visible(true);
                    countVisible++;
                } else {
                    item.visible(false);
                }
            });

            boardViewModel.board.items.countVisible(countVisible);
        }

        function mapBoard(board) {
            var boardboardViewModel = {};

            boardboardViewModel.id = board.id;
            boardboardViewModel.criterions = ko.observableArray(_.map(board.criterions, mapCriterion));
            boardboardViewModel.title = ko.observable(board.title).extend({
                validate: validators.validateBoardTitle
            });

            boardboardViewModel.items = ko.observableArray(_.map(board.items, function (item) { return mapItem(item, boardboardViewModel.criterions) }));

            boardboardViewModel.items.countVisible = ko.observable(board.items.length);

            return boardboardViewModel;
        }

        function mapCriterion(criterion) {
            var criterionboardViewModel = {};

            criterionboardViewModel.id = criterion.id;
            criterionboardViewModel.isBenefit = criterion.isBenefit;
            criterionboardViewModel.title = ko.observable(criterion.title).extend({
                validate: validators.validateItemTitle
            });
            criterionboardViewModel.weight = ko.observable(criterion.weight).extend({
                validate: validators.validateWeightValue
            });

            return criterionboardViewModel;
        }

        function mapItem(item, mappedCriterions) {
            var itemboardViewModel = {};

            itemboardViewModel.id = item.id;
            itemboardViewModel.title = ko.observable(item.title).extend({
                validate: validators.validateItemTitle
            });
            itemboardViewModel.marks = ko.observable({});

            _.map(mappedCriterions(), function (criterion) {
                var mark = _.find(item.marks, function(mark){return mark.criterionId == criterion.id});

                if (_.isUndefined(mark)){
                    itemboardViewModel.marks()[criterion.id] = mapMark({id: null, criterionId: criterion.id, itemId: item.id, value: 0 }, criterion);
                } else {
                    itemboardViewModel.marks()[criterion.id] = mapMark(mark, criterion);
                }                    
            })

            itemboardViewModel.rank = ko.observable();
            itemboardViewModel.score = ko.computed(function () {
                return boardService.computeScore(itemboardViewModel.marks());
            });
            itemboardViewModel.visible = ko.observable(true);

            return itemboardViewModel;
        }

        function mapMark(mark, criterion) {
            var markboardViewModel = {};

            markboardViewModel.id = mark.id;
            markboardViewModel.value = ko.observable(mark.value).extend({
                validate: validators.validateMarkValue
            });
            markboardViewModel.criterionId = mark.criterionId;
            markboardViewModel.itemId = mark.itemId;
            markboardViewModel.isBenefit = criterion.isBenefit;
            markboardViewModel.weight = criterion.weight;

            return markboardViewModel;
        }   
});