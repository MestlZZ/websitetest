define(['repositories/boardRepository', 'repositories/itemRepository','repositories/markRepository',
    'durandal/app', 'mappers/boardMapper', 'mappers/itemMapper', 'constants', 'services/boardService'],
    function (boardRepository, itemRepository, markRepository, app, boardMapper, itemMapper, constants, boardService) {
        var board;

    return {
        title: ko.observable(),
        items: ko.observableArray(),
        criterions: ko.observableArray(),
        marks: ko.observable(),
        activate: function () {
            var self = this;

            return boardRepository.getOpenedBoard().then(function (data) {
                board = boardMapper.mapToObservable(data);

                self.title = board.title;

                self.items = board.items;
                self.criterions = board.criterions;
                self.marks = board.marks;

                boardService.itemsChanged(board.items);
            });
        },
        updateItemTitle: function (item) {
            item.title(item.title().trim());

            itemRepository.setTitle(item.title(), item.id);
        },
        deleteItem: function (item, event) {
            $(constants.popupTemplatesId.confirmation).popup({ title: item.title(), body: 'You realy want remove this item?' })
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
            console.log(mark);
            markRepository.setValue(+mark.value(), mark.id);

            boardService.itemsChanged(board.items);
        },
        selectValue: function () {
            document.execCommand('selectAll', false, null);
        }
    }
});