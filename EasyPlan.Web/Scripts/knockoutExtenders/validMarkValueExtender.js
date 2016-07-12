ko.extenders.validMarkValue = function (target, fieldName) {
    target.hasError = ko.observable(false);
    target.validationMessage = ko.observable();

    function validate(newValue) {
        var value = +newValue;
        var stringValue = newValue.toString();

        if (stringValue.length == 0) {
            target.hasError(true);
            target.validationMessage(fieldName + " field can't be clear");
        } else if (!Number.isInteger(value)) {
            target.hasError(true);
            target.validationMessage("Value in " + fieldName + " field must be a number.");
        } else if (value > 5) {
            target.hasError(true);
            target.validationMessage('Number in ' + fieldName + ' field must to be less than 6.');
        } else if (value < 0) {
            target.hasError(true);
            target.validationMessage("Number in " + fieldName + " field can't be negative.");
        } else {
            target.hasError(false);
        }
    }

    validate(target());
    target.subscribe(validate);
    return target;
};