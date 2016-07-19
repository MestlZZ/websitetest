ko.bindingHandlers.sort = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        var data = ko.utils.unwrapObservable(valueAccessor());
        var order = ko.utils.unwrapObservable(data.order);
        var sort = ko.unwrap(data.function);
        var $element = $(element);

        $element.on('click', function () {
            sort();
            data.order($element.text());

            if ($element.hasClass('sorted-ascending')) {
                $element.removeClass('sorted-ascending');
                $element.addClass('sorted-descending');

                    sort(true);
                } else {
                $element.removeClass('sorted-descending');
                $element.addClass('sorted-ascending');
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