define(['repositories/boardRepository', 'repositories/pointRepository'], function (boardRepository, pointRepository) {
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
        points: ko.observableArray(),
        activate: function () {
            this.title = board.title;

            for (var i = 0; i < board.points().length; i++) {
                board.points()[i].title.extend({
                    validTitle: 'Title'
                });
            }

            this.points = board.points;
        },
        setItemTitle: function (data, context) {
            data.title(data.title().trim());

            pointRepository.setTitle(data.title(), data.id(), board.id());
        },
        deleteItem: function (item) {
            pointRepository.remove(item, board.id());
            board.points.remove(item);

            console.log(board.points());
        }
    }
});