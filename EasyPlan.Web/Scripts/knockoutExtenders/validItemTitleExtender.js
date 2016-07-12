ko.extenders.validItemTitle = function (target, fieldName) {
    target.hasError = ko.observable(false);
    target.validationMessage = ko.observable();

    function validate(newValue) {
        var text = newValue.trim();

        if (!text) {
            target.hasError(true);
            target.validationMessage(fieldName + " field can't be clear.");
        } else if (text.length > 254) {
            target.hasError(true);
            target.validationMessage('Text in ' + fieldName + ' field must have less than 254 symbols.');
        } else {
            target.hasError(false);
        }
    }

    validate(target());
    target.subscribe(validate);
    return target;
};