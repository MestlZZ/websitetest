ko.extenders.validate = function (target, validateFunction) {
    target.hasError = ko.observable(false);
    target.validationMessage = ko.observable();

    validateFunction(target);
    target.subscribe(function () { validateFunction(target) });
    return target;
};