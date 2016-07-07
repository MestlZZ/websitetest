ko.bindingHandlers.dblclickEdit = {
    init: function (element, valueAccessor, allBindings, viewModel, context) {
        var title = ko.utils.unwrapObservable(valueAccessor());

        $(element).on('dblclick', function (event) {
            var elem = $(element).find('input');
            elem.removeClass('invisible');
            elem.focus();
            $('html').on('click', function (event) {
                $()
            });
            
            var elem = $(element).find('span');
            elem.addClass('invisible');
        });
    }
};