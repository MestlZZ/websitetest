﻿define(['repositories/boardRepository', 'repositories/itemRepository','mappers/markMapper','repositories/markRepository',
    'durandal/app', 'mappers/boardMapper', 'mappers/itemMapper', 'constants', 'services/boardService',
    'repositories/criterionRepository', 'mappers/criterionMapper', 'spinner', 'services/validators', 'synchronization'],
    function (boardRepository, itemRepository,markMapper, markRepository, app, boardMapper, itemMapper, constants, boardService,
        criterionRepository, criterionMapper, spinner, validators, sync) {

        var boardHub = $.connection.boardHub;

        return viewModel = {
            board: {},
            ROLE: constants.ROLE,
            settingsVisible: ko.observable(false),
            sorted: ko.observable(),
            sortAscending: ko.observable(),
            benefitCriterions: ko.observableArray([]),
            costCriterions: ko.observableArray([]),
            userRole: ko.observable(),

            filterValue: ko.observable("").extend({
                validate: validators.validateFilterValue
            }),

            activate: activate,
            deactivate: deactivate,
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
            applyFilter: applyFilter
        };

        function deactivate() {
            sync.closeBoard();
        }

        function activate(boardId) {
            spinner.show();

            var self = this;

            sync.openBoard(boardId);

            self.board = {};
            self.sorted = ko.observable();
            self.sortAscending = ko.observable();
            self.benefitCriterions = ko.observableArray([]);
            self.costCriterions = ko.observableArray([]);
            self.userRole = ko.observable();

            self.filterValue = ko.observable("").extend({
                validate: validators.validateFilterValue
            });

            /*Hub events*/
            app.on(constants.EVENT.BOARD.ITEM.TITLE_CHANGED, function (id, title) {
                var item = _.find(self.board.items(), function (item) { return id == item.id; });

                item.title(title);
            });

            app.on(constants.EVENT.BOARD.ITEM.REMOVED, function (id) {
                var item = _.find(self.board.items(), function (item) { return id == item.id; });

                self.board.items.remove(item);
                self.sorted(false);

                boardService.setRanks(self.board.items());
            });

            app.on(constants.EVENT.BOARD.ITEM.ADDED, function (item) {
                item = mapItem(item, viewModel.board.criterions);

                self.board.items.unshift(item);
                self.sorted(false);

                boardService.setRanks(self.board.items());
            });

            app.on(constants.EVENT.BOARD.MARK.VALUE_CHANGED, function (mark) {
                var item = _.find(self.board.items(), function (item) { return mark.itemId == item.id; });
                var criterion = _.find(self.board.criterions(), function (criterion) { return mark.criterionId == criterion.id; });

                item.marks()[mark.criterionId].id = mark.id;
                item.marks()[mark.criterionId].value(mark.value);

                self.sorted(false);

                boardService.setRanks(self.board.items());
            });

            app.on(constants.EVENT.BOARD.CRITERION.WEIGHT_CHANGED, function (id, weight) {
                var criterion = _.find(self.board.criterions(), function (criterion) { return id == criterion.id; });

                criterion.weight(weight);
                self.sorted(false);
            });

            app.on(constants.EVENT.BOARD.CRITERION.TITLE_CHANGED, function (id, title) {
                var criterion = _.find(self.board.criterions(), function (criterion) { return id == criterion.id; });

                criterion.title(title);
            });

            app.on(constants.EVENT.BOARD.TITLE_CHANGED, function (title) {
                self.board.title(title);
            });

            app.on(constants.EVENT.BOARD.CRITERION.REMOVED, function (id) {
                var criterion = _.find(self.board.criterions(), function (criterion) { return id == criterion.id; });

                self.board.criterions.remove(criterion);

                if (criterion.isBenefit)
                    viewModel.benefitCriterions.remove(criterion);
                else
                    viewModel.costCriterions.remove(criterion);

                _.each(viewModel.board.items(), function (item) {
                    delete item.marks()[criterion.id];

                    item.marks.notifySubscribers();
                });

                self.sorted(false);

                boardService.setRanks(self.board.items());
            });

            app.on(constants.EVENT.BOARD.CRITERION.ADDED, function (criterion) {
                var criterion = mapCriterion(criterion);

                _.each(viewModel.board.items(), function (item) {
                    item.marks()[criterion.id] = mapMark({
                        id: null,
                        value: 0,
                        criterionId: criterion.id,
                        itemId: item.id
                    }, criterion);

                    item.marks.notifySubscribers();
                });

                viewModel.board.criterions.unshift(criterion);

                if (criterion.isBenefit)
                    viewModel.benefitCriterions.push(criterion);
                else
                    viewModel.costCriterions.push(criterion);
            });
            /*End*/

            return boardRepository.getBoard(boardId)
                .then(function (data) {
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

                    self.sortAscending(true);
                    self.sorted(true);

                    self.filterValue.subscribe(function () { applyFilter(); });
                    self.board.items.subscribe(function () { applyFilter(); });

                    spinner.hide();
                });
        }

        function sortByRank() {
            viewModel.board.items(_.sortBy(viewModel.board.items(), function (item) {
                return item.score();
            }));

            if (!viewModel.sortAscending())
                viewModel.board.items.reverse();

            viewModel.sorted(true);
            viewModel.sortAscending(!viewModel.sortAscending());
        }

        function updateItemTitle(item) {
            spinner.show();
            itemRepository.setTitle(viewModel.board.id, item.title(), item.id)
                .then(function () {
                    boardHub.server.updateItemTitle(viewModel.board.id, item.id, item.title());

                    spinner.hide();
            });
        }

        function updateBoardTitle(context) {
            spinner.show();

            boardRepository.setTitle(context.board.title(), context.board.id)
                .then(function () {
                    boardHub.server.updateBoardTitle(viewModel.board.id, context.board.title());

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

                    itemRepository.remove(item.id, viewModel.board.id).then(function () {

                        boardHub.server.deleteItem(viewModel.board.id, item.id);
                        spinner.hide();
                    });
                }
            });            
        }

        function addItem() {
            spinner.show();

            itemRepository.getNewItem(viewModel.board.id).then(function (item) {
                boardHub.server.addItem(viewModel.board.id, item);

                spinner.hide();
            });
        }

        function setMark(mark) {
            spinner.show();

            if (_.isNull(mark.id)) {
                markRepository.createMark(mark.itemId, mark.criterionId, viewModel.board.id)
                    .then(function (newMark) {
                        mark.id = newMark.id;

                        var newMark = {
                            id: mark.id,
                            value: mark.value(),
                            criterionId: mark.criterionId,
                            itemId: mark.itemId
                        };

                        markRepository.setValue(+mark.value(), mark.id, viewModel.board.id).then(function () {
                            boardHub.server.setMark(viewModel.board.id, newMark);

                            spinner.hide();
                        });
                    });
            } else {
                var newMark = {
                    id: mark.id,
                    value: mark.value(),
                    criterionId: mark.criterionId,
                    itemId: mark.itemId
                };

                markRepository.setValue(+mark.value(), mark.id, viewModel.board.id).then(function () {
                    boardHub.server.setMark(viewModel.board.id, newMark);

                    spinner.hide();
                });
            }
        }

        function setWeight(criterion) {
            spinner.show();

            criterionRepository.setWeight(criterion.weight(), criterion.id, viewModel.board.id).then(function () {
                boardHub.server.setCriterionWeight(viewModel.board.id, criterion.id, criterion.weight());

                spinner.hide();
            });
        }

        function updateCriterionTitle(criterion) {
            spinner.show();

            criterionRepository.setTitle(criterion.title, criterion.id, viewModel.board.id).then(function () {
                boardHub.server.updateCriterionTitle(viewModel.board.id, criterion.id, criterion.title());

                spinner.hide();
            });
        }

        function deleteCriterion(criterion) {
            if (criterion.isBenefit && viewModel.benefitCriterions().length <= 1) { 
                return;
            } else if (!criterion.isBenefit && viewModel.costCriterions().length <= 1) {
                return;
            }

            var title = criterion.title();

            if (criterion.length > 50)
                criterion = criterion.slice(0, 50) + '...';

            $(constants.popupTemplatesId.confirmation).popup({ title: 'Delete', body: 'delete "' + title + '"' })
                .then(function (response) {
                    if (response) {
                        spinner.show();

                        criterionRepository.remove(criterion.id, viewModel.board.id).then(function () {
                            boardHub.server.deleteCriterion(viewModel.board.id, criterion.id);

                            spinner.hide();
                        });
                    }
                });
        }

        function addCriterion(isBenefit) {
            spinner.show();

            criterionRepository.getNewCriterion(isBenefit, viewModel.board.id)
                .then(function (criterion) {
                    boardHub.server.addCriterion(viewModel.board.id, criterion);

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
            boardViewModel.criterions = ko.observableArray(_.map(board.criterions, mapCriterion));
            boardViewModel.title = ko.observable(board.title).extend({
                validate: validators.validateBoardTitle
            });

            boardViewModel.items = ko.observableArray(_.map(board.items, function (item) { return mapItem(item, boardViewModel.criterions) }));

            boardViewModel.items.countVisible = ko.observable(board.items.length);

            return boardViewModel;
        }

        function mapCriterion(criterion) {
            var criterionViewModel = {};

            criterionViewModel.id = criterion.id;
            criterionViewModel.isBenefit = criterion.isBenefit;
            criterionViewModel.title = ko.observable(criterion.title).extend({
                validate: validators.validateItemTitle
            });
            criterionViewModel.weight = ko.observable(criterion.weight).extend({
                validate: validators.validateWeightValue
            });

            return criterionViewModel;
        }

        function mapItem(item, mappedCriterions) {
            var itemViewModel = {};

            itemViewModel.id = item.id;
            itemViewModel.title = ko.observable(item.title).extend({
                validate: validators.validateItemTitle
            });
            itemViewModel.marks = ko.observable({});

            _.map(mappedCriterions(), function (criterion) {
                var mark = _.find(item.marks, function(mark){return mark.criterionId == criterion.id});

                if (_.isUndefined(mark)){
                    itemViewModel.marks()[criterion.id] = mapMark({id: null, criterionId: criterion.id, itemId: item.id, value: 0 }, criterion);
                } else {
                    itemViewModel.marks()[criterion.id] = mapMark(mark, criterion);
                }                    
            })

            itemViewModel.rank = ko.observable();
            itemViewModel.score = ko.computed(function () {
                return boardService.computeScore(itemViewModel.marks());
            });
            itemViewModel.visible = ko.observable(true);

            return itemViewModel;
        }

        function mapMark(mark, criterion) {
            var markViewModel = {};

            markViewModel.id = mark.id;
            markViewModel.value = ko.observable(mark.value).extend({
                validate: validators.validateMarkValue
            });
            markViewModel.criterionId = mark.criterionId;
            markViewModel.itemId = mark.itemId;
            markViewModel.isBenefit = criterion.isBenefit;
            markViewModel.weight = criterion.weight;

            return markViewModel;
        }   
});