﻿ko.bindingHandlers.editableInput = {
    init: function (element, valueAccessor, allBindings, viewModel, context) {
        var data = ko.utils.unwrapObservable(valueAccessor());
        var setTitle = data.function;
        var observValue = data.value;
        var input = $(element);
        var value = '';

        input.focus(function focus(event) {
            clearBind();

            value = observValue();

            input.keyup(keyUp);
            input.keypress(keyPress);

            input.focusout(function fout(e) {
                clearBind();

                if (!observValue.hasError()) {
                    if (observValue() != value)
                        setTitle(viewModel);
                } else {
                    observValue(value);
                }

                input.unbind('focusout', fout);
            });
        });

        function clearBind() {
            input.unbind('keypress', keyPress);
            input.unbind('keyup', keyUp);
        }

        function keyUp(e) {
            if (e.keyCode == 27) {
                observValue(value);

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
}