ko.bindingHandlers.editableText = {
    init: function (element, valueAccessor, allBindings, viewModel, context) {
        var data = ko.utils.unwrapObservable(valueAccessor());
        var handler = data.handler;
        var observValue = data.value;
        var input = $(element);
        var value = '';

        input.attr("contenteditable", "true");
        input.text(observValue());
               
        input.focus(function focus(event) {
            clearBind();

            value = observValue();

            input.keyup(keyUp);
            input.keypress(keyPress);

            input.focusout(function fout(e) {
                clearBind();

                if (!observValue.hasError()) {
                    if (observValue() != value)
                        handler(viewModel);
                } else {
                    observValue(value);
                    input.text(value);
                }

                window.getSelection().empty();
                input.unbind('focusout', fout);
            });
        });

        function clearBind() {
            input.unbind('keypress', keyPress);
            input.unbind('keyup', keyUp);
        }

        function keyUp(e) {
            observValue(input.text());

            if (e.keyCode == 27) {
                observValue(value);
                input.text(value);

                $(input).focusout().blur();
                return false;
            }
        }

        function keyPress(e) {
            if (e.keyCode == 13) {              
                if (!observValue.hasError()) $(input).focusout().blur();

                return false;
            }
        }    
    }
};