define(['constants'], function (constants) {

    return {
        validateItemTitle: validateItemTitle,
        validateMarkValue: validateMarkValue,
        validateWeightValue: validateWeightValue,
        validateFilterValue: validateFilterValue,
        validateBoardTitle: validateBoardTitle
    };

    function validateItemTitle(target) {
        validateTitle(target, 255);
    }

    function validateBoardTitle(target) {
        validateTitle(target, 50);
    }

    function validateFilterValue(target) {
        validateTitle(target, 255);
    }

    function validateMarkValue(target) {
        validateValue(target, 0, 5);
    }

    function validateWeightValue(target) {
        validateValue(target, 1, 20);
    }

    function validateTitle(target, maxLength) {
        var text = target().trim();

        if (!text) {
            target.hasError(true);
            target.validationMessage('Text in field can\'t be clear.');
        } else if (text.length > maxLength) {
            target.hasError(true);
            target.validationMessage('Text in field can\'t be longer than ' + maxLength + 'symbols.');
        } else {
            target.hasError(false);
        }
    }

    function validateValue(target, min, max) {
        var value = +target();
        var stringValue = target().toString();

        if (stringValue.length == 0) {
            target.hasError(true);
            target.validationMessage('Field can\'t be clear.');
        } else if (!_.isNumber(value)) {
            target.hasError(true);
            target.validationMessage('Value must be a number.');
        } else if (value > 5) {
            target.hasError(true);
            target.validationMessage('Value must to be less or equal ' + max + '.');
        } else if (value < 0) {
            target.hasError(true);
            target.validationMessage('Value must be greater than ' + min + '.');
        } else {
            target.hasError(false);
        }
    }
});