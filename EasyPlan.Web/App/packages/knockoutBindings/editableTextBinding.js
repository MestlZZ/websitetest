ko.bindingHandlers.editableText = {
    init: function (element, valueAccessor, allBindings, viewModel, context) {
        var data = ko.unwrap(valueAccessor());
        var handler = data.handler;
        var observValue = data.value;
        var $input = $(element);
        var lastValue = '';
        var hasFocus = false;

        observValue.subscribe(function () {
            if (!hasFocus) {
                $input.text(observValue());
            }
        });

        $input.attr('contenteditable', 'true');
        $input.text(observValue());
               
        $input.focus(function focus(event) {
            hasFocus = true;

            clearBind();

            lastValue = observValue();

            $input.keyup(keyUp);
            $input.keypress(keyPress);

            $input.focusout(function fout(e) {
                hasFocus = false;

                clearBind();

                if (!observValue.hasError()) {
                    if (observValue() != lastValue) {
                        handler(viewModel);
                    }
                } else {
                    observValue(lastValue);
                    $input.text(lastValue);
                }
                
                window.getSelection().removeAllRanges();

                $input.unbind('focusout', fout);
            });
        });

        function clearBind() {
            $input.unbind('keypress', keyPress);
            $input.unbind('keyup', keyUp);
        }

        function keyUp(e) {
            observValue($input.text());

            if (e.keyCode == 27) {
                observValue(value);
                $input.text(value);

                $input.focusout().blur();
                return false;
            }
        }

        function keyPress(e) {
            if (e.keyCode == 13) {              
                if (!observValue.hasError()) {
                    $input.focusout().blur();
                }

                return false;
            }
        }    
    }
};