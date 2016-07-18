define(['repositories/boardRepository', 'repositories/itemRepository','repositories/markRepository',
    'durandal/app', 'mappers/boardMapper', 'mappers/itemMapper', 'constants', 'services/boardService',
    'repositories/criterionRepository'],
    function (boardRepository, itemRepository, markRepository, app, boardMapper, itemMapper, constants, boardService,
        criterionRepository) {
        var board;

        return {
            board: {},
            columnCount: ko.observable(),
            order: ko.observable(),
            benefitCriterions: ko.observableArray([]),
            costCriterions: ko.observableArray([]),
            activate: function (boardId) {
                var self = this;
                boardId = boardId || "ccef5cf6-5184-4a5a-8234-c2df683cbfba";

                return boardRepository.getBoard(boardId).then(function (data) {
                    var b = boardMapper.map(data);

                    boardRepository.updateOpenedBoard(b);

                    board = boardMapper.mapToViewModel(b);

                    self.board = board;

                    ko.computed(function () {
                        _.each(board.criterions(), function (criterion) {
                            if (criterion.isBenefit) {
                                self.benefitCriterions.push(criterion);
                            } else {
                                self.costCriterions.push(criterion);
                            }
                        })
                    });

                    self.columnCount = ko.computed(function () {
                        return self.board.criterions().length + 3;
                    })

                    self.sortByRank();

                    app.on('board:item-changed', function () {
                        self.order('');
                    });

                    boardService.boardChanged(board);
                });
            },
            sortByRank: function(isReverse){
                board.items(_.sortBy(board.items(), function (item) {
                    return item.score();
                }));

                if (!isReverse)
                    board.items.reverse();
            },
        updateItemTitle: function (item) {
            item.title(item.title().trim());

            itemRepository.setTitle(item.title(), item.id);
        },
        deleteItem: function (item, event) {
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
        },
        addItem: function () {
            itemRepository.getNewItem().then(function(item){
                item = itemMapper.mapToViewModel(item);

                board.items.unshift(item);

                boardService.boardChanged(board);
            });
        },
        setMark: function (mark) {
            if (_.isUndefined(mark.id)) {
                markRepository.createMark(mark.itemId, mark.criterionId).then(function (newMark) {
                    mark.id = newMark.id;

                   return markRepository.setValue(+mark.value(), mark.id, mark.itemId, mark.criterionId);
                });
            } else {
                markRepository.setValue(+mark.value(), mark.id, mark.itemId, mark.criterionId);
            }

            boardService.boardChanged(board);
        },
        setWeight: function (criterion) {
            criterionRepository.setWeight(criterion.weight(), criterion.id);

            boardService.criterionChanged(criterion);

            boardService.boardChanged(board);
        },
        updateCriterionTitle: function (criterion) {
            criterionRepository.setTitle(criterion.title, criterion.id);

            boardService.criterionChanged(criterion);

            boardService.boardChanged(board);
        }
    }
});