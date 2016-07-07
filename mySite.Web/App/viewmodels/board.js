define(['repositories/boardRepository', 'repositories/pointRepository'], function (boardRepository, pointRepository) {
    ko.extenders.validTitle = function (target, fieldName) {
        target.hasError = ko.observable(false);
        target.validationMessage = ko.observable();
        target.isMustBeEdit = ko.observable(false);

        function validate(newValue) {
            var text = newValue.trim();

            if (!text) {
                target.hasError(true);
                target.validationMessage("Field " + fieldName + " can't be clear");
            } else if (text.length > 255) {
                target.hasError(true);
                target.validationMessage('Text in ' + fieldName + ' field must have less than 255 symbols');
            } else {
                target.hasError(false);
                target.validationMessage('');
            }
        }

        validate(target());
        target.subscribe(validate);
        return target;
    };

    return {
        board: ko.observable(),
        title: ko.observable(),
        points: ko.observableArray(),
        activate: function () {
            var board = ko.mapping.fromJS(boardRepository.getCollection()[0]);

            this.board(board);
            this.title = board.title;

            for (var i = 0; i < board.points().length; i++) {
                board.points()[i].title.extend({
                    validTitle: 'point title'
                });
            }

            this.points = board.points;

            
        },
        sendTitle: function (data, context) {
            pointRepository.setTitle(data.title(), data.id(), context.board().id());
        }
    }
});