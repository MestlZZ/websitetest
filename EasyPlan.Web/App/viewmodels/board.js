define(['repositories/boardRepository', 'repositories/itemRepository', 'durandal/app', 'mappers/boardMapper', 'mappers/itemMapper'],
    function (boardRepository, itemRepository, app, boardMapper, itemMapper) {
    ko.extenders.validTitle = function (target, fieldName) {
        target.hasError = ko.observable(false);
        target.validationMessage = ko.observable();
        target.isMustBeEdit = ko.observable(false);

        function validate(newValue) {
            var text = newValue.trim();

            if (!text) {
                target.hasError(true);
                target.validationMessage(fieldName + " field can't be clear.");
            } else if (text.length > 255) {
                target.hasError(true);
                target.validationMessage('Text in ' + fieldName + ' field must have less than 255 symbols.');
            } else {
                target.hasError(false);
            }
        }

        validate(target());
        target.subscribe(validate);
        return target;
    };

    var board = boardMapper.mapToObservable(boardRepository.getFirstBoard());

    return {
        title: ko.observable(),
        items: ko.observableArray(),
        criterions: ko.observableArray(),
        activate: function () {
            this.title = board.title;

            for (var i = 0; i < board.items().length; i++) {
                board.items()[i].title.extend({
                    validTitle: 'Title'
                });
            }

            this.items = board.items;
            this.criterions = board.criterions;
        },
        updateItemTitle: function (item, context) {
            item.title(item.title().trim());

            itemRepository.setTitle(item.title(), item.id, board.id);
        },
        deleteItem: function (item, event) {
            $('#confirmation-popup-template').popup({ title: item.title(), body: 'You realy want remove this item?' }, event.toElement)
            .then(function (s) { 
                if (s) {
                    itemRepository.remove(item.id, board.id);
                    board.items.remove(item);
                }
            });            
        },
        addItem: function () {
            itemRepository.getNewItem(board.id).then(function(item){
                item = itemMapper.mapToObservable(item);

                item.title.extend({
                    validTitle: 'Title'
                });

                board.items.unshift(item);
            });
        }
    }
});