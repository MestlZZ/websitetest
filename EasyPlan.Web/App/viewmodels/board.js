define(['repositories/boardRepository', 'repositories/itemRepository','repositories/markRepository',
    'durandal/app', 'mappers/boardMapper', 'mappers/itemMapper', 'constants'],
    function (boardRepository, itemRepository, markRepository, app, boardMapper, itemMapper, constants) {
        var board;

    return {
        title: ko.observable(),
        items: ko.observableArray(),
        criterions: ko.observableArray(),
        activate: function () {
            var self = this;

            return boardRepository.getOpenedBoard().then(function (data) {
                board = boardMapper.mapToObservable(data);

                self.title = board.title;

                self.items = board.items;
                self.criterions = board.criterions;
            });            
        },
        updateItemTitle: function (item) {
            item.title(item.title().trim());

            itemRepository.setTitle(item.title(), item.id, board.id);
        },
        deleteItem: function (item, event) {
            $(constants.popupTemplatesId.confirmation).popup({ title: item.title(), body: 'You realy want remove this item?' })
            .then(function (s) { 
                if (s) {
                    itemRepository.remove(item.id, board.id);
                    board.items.remove(item);
                    console.log(s);
                }
            });            
        },
        addItem: function () {
            itemRepository.getNewItem(board.id).then(function(item){
                item = itemMapper.mapToObservable(item);

                board.items.unshift(item);
            });
        },
        setMark: function (mark) {
            markRepository.setValue(+mark.value(), mark.id, board.id);
        }
    }
});