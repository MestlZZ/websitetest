ko.bindingHandlers.dblclickEditTitle = {
    init: function (element, valueAccessor, allBindings, viewModel, context) {
        var func = ko.utils.unwrapObservable(valueAccessor());
        
        $(element).on('dblclick', function (event) {
            var input = $(element).find('.point__title_input');
            var span = $(element).find('.point__title_value');
            var title = viewModel.title();

            viewModel.title.isMustBeEdit(true);

            input.focus();

            input.keyup(function keyup(e) {
                if(!viewModel.title.hasError()){
                    if(e.keyCode == 13)
                    {
                        input.unbind('keyup', keyup);
                        viewModel.title.isMustBeEdit(false);

                        func(viewModel, context.$root);

                        $('html').off('click');
                    }
                }
                if(e.keyCode == 27)
                {
                    input.unbind('keyup', keyup);

                    viewModel.title(title);

                    viewModel.title.isMustBeEdit(false);

                    $('html').off('click');
                }                
            });

            $('html').on('click', function (event) {
                if (event.target != input[0] && !viewModel.title.hasError())
                {
                    input.unbind('keyup');

                    viewModel.title.isMustBeEdit(false);

                    $('html').off('click');

                    func(viewModel, context.$root);
                }
                else
                {
                    input.focus();
                }

                event.stopPropagation();
            });        
        });
    }
};