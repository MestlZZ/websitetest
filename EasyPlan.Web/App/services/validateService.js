define(['constants'], function (constants) {
    return {
        validateObservableTitle,
        validateObservableMarkValue,
        validateObservableWeightValue,
        validateObservableFilterValue
    }

    function validateObservableTitle(target) {
        var text = target().trim();

        if (!text) {
            target.hasError(true);
            target.validationMessage("Title field can't be clear.");
        } else if (text.length > 255) {
            target.hasError(true);
            target.validationMessage("Text in title field can't be longer than 255 symbols.");
        } else {
            target.hasError(false);
        }
    }

    function validateObservableMarkValue(target) {
        var value = +target();
        var stringValue = target().toString();

        if (stringValue.length == 0) {
            target.hasError(true);
            target.validationMessage("Mark field can't be clear");
        } else if (!Number.isInteger(value)) {
            target.hasError(true);
            target.validationMessage("Value in mark field must be a number.");
        } else if (value > 5) {
            target.hasError(true);
            target.validationMessage('Number in mark field must to be less than 6.');
        } else if (value < 0) {
            target.hasError(true);
            target.validationMessage("Number in mark field can't be negative.");
        } else {
            target.hasError(false);
        }
    }

    function validateObservableWeightValue(target) {
        var value = +target();
        var stringValue = target().toString();

        if (stringValue.length == 0) {
            target.hasError(true);
            target.validationMessage("Weight field can't be clear");
        } else if (!Number.isInteger(value)) {
            target.hasError(true);
            target.validationMessage("Value in weight field must be a number.");
        } else if (value > 20) {
            target.hasError(true);
            target.validationMessage('Number in weight field can\'t be greater than 20.');
        } else if (value < 1) {
            target.hasError(true);
            target.validationMessage("Number in weight field can't be negative or equal to zero.");
        } else {
            target.hasError(false);
        }
    }

    function validateObservableFilterValue(target) {
        var text = target().trim();

        if (text.length > 255) {
            target.hasError(true);
            target.validationMessage("text in filter field can't be longer than 255 symbols.");
        } else {
            target.hasError(false);
        }
    }
})