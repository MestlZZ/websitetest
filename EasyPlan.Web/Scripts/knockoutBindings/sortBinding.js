ko.bindingHandlers.sort = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        var data = ko.utils.unwrapObservable(valueAccessor());
        var order = ko.utils.unwrapObservable(data.order);
        var sort = ko.unwrap(data.function);
        var elem = $(element);

        elem.on('click', function () {
            sort();
            data.order(elem.text());

                if (elem.hasClass('sorted-ascending')) {
                    elem.removeClass('sorted-ascending');
                    elem.addClass('sorted-descending');

                    sort(true);
                } else {
                    elem.removeClass('sorted-descending');
                    elem.addClass('sorted-ascending');
                }
        });


    },
    update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        var data = ko.utils.unwrapObservable(valueAccessor());
        var sort = ko.unwrap(data.function);
        var elem = $(element);

        if (elem.text() != data.order()) {
            elem.removeClass('sorted-ascending');
            elem.removeClass('sorted-descending');

            data.order("");
        }
    }
};