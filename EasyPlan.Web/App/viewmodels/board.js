define(['repositories/boardRepository', 'repositories/itemRepository', 'durandal/app'],
    function (boardRepository, itemRepository, app) {
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

    var board = ko.mapping.fromJS(boardRepository.getCollection()[0]);

    return {
        title: ko.observable(),
        items: ko.observableArray(),
        activate: function () {
            this.title = board.title;

            for (var i = 0; i < board.items().length; i++) {
                board.items()[i].title.extend({
                    validTitle: 'Title'
                });
            }

            this.items = board.items;
        },
        updateItemTitle: function (item, context) {
            item.title(item.title().trim());

            itemRepository.setTitle(item.title(), item.id(), item.id());
        },
        deleteItem: function (item) {
            if (!confirm("Remove?")) return;

            itemRepository.remove(item.id(), board.id());
            board.points.remove(item);
        }
    }
});