ko.bindingHandlers.editableText = {
    init: function (element, valueAccessor, allBindings, viewModel, context) {
        var setTitle = ko.utils.unwrapObservable(valueAccessor());
        var input = $(element);
        var title = '';
               
        input.focus(function focus(event) {
            clearBind();

            title = viewModel.title();

            input.keyup(keyUp);
            input.keypress(keyPress);

            input.focusout(function fout(e) {
                clearBind();

                if (!viewModel.title.hasError()) {
                    if (viewModel.title() != title)
                        setTitle(viewModel, context.$root);
                } else
                    input.select().focus();

                input.unbind('focusout', fout);
            });
        });

        function clearBind() {
            input.unbind('keypress', keyPress);
            input.unbind('keyup', keyUp);
        }

        function keyUp(e) {
            viewModel.title(input.text());

            if (e.keyCode == 27) {
                viewModel.title(title);

                $(input).focusout().blur();
                return false;
            }
        }

        function keyPress(e) {
            if (e.keyCode == 13) {              
                if (!viewModel.title.hasError()) $(input).focusout().blur();

                return false;
            }
        }    
    }
};