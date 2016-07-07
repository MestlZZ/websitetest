ko.bindingHandlers.dblclickEdit = {
    init: function (element, valueAccessor, allBindings, viewModel, context) {
        var title = ko.utils.unwrapObservable(valueAccessor());
        var funcOnEnter = ko.utils.unwrapObservable(allBindings.get('onEnterKeyDown'));

        $(element).on('dblclick', function (event) {
            var elem = $(element).find('input');
            elem.removeClass('invisible');
            elem.focus();
            $('html').on('click', function (event) {
                $()
            });
            elem.on("keypress", function (event) {
                if(event.keyCode == 13)
                {
                    var elem = $(event.target);
                    title = elem.val();
                    elem.addClass('invisible');
                    elem.off("keypress");

                    var elem = $(element).find('span');
                    elem.removeClass('invisible');
                    elem.text(title);

                    funcOnEnter(viewModel, context.$root);
                }
            });

            var elem = $(element).find('span');
            elem.addClass('invisible');
        });
    }
};