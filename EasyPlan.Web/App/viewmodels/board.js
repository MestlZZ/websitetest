define(['repositories/boardRepository', 'repositories/itemRepository','repositories/markRepository',
    'durandal/app', 'mappers/boardMapper', 'mappers/itemMapper', 'constants', 'services/boardService'],
    function (boardRepository, itemRepository, markRepository, app, boardMapper, itemMapper, constants, boardService) {
        var board;

        return {
            title: ko.observable(),
            items: ko.observableArray(),
            columnCount: ko.observable(),
            criterions: ko.observableArray(),
            order: ko.observable(),
            activate: function () {
                var self = this;

                return boardRepository.getOpenedBoard().then(function (data) {
                    board = boardMapper.mapToObservable(data);

                    self.title = board.title;

                    self.items = board.items;
                    self.criterions = board.criterions;

                    self.columnCount = ko.computed(function () {
                        return self.criterions().length + 3;
                    })

                    self.sortByRank();

                    app.on('board:item-changed', function () {
                        self.order('');
                    });

                    boardService.itemsChanged(board.items);
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
                    
                    boardService.itemsChanged(board.items);
                }
            });            
        },
        addItem: function () {
            itemRepository.getNewItem(board.id).then(function(item){
                item = itemMapper.mapToObservable(item);

                board.items.unshift(item);
                
                boardService.itemsChanged(board.items);
            });
        },
        setMark: function (mark) {
            markRepository.setValue(+mark.value(), mark.id);

            boardService.itemsChanged(board.items);
        },
        selectValue: function () {
            document.execCommand('selectAll', false, null);
        }
    }
});