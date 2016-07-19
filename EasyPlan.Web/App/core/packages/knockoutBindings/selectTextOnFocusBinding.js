ko.bindingHandlers.selectTextOnFocus = {
    update: function (element, valueAccessor, allBindings, viewModel, context) {
        var isSwitchOn = ko.unwrap(valueAccessor());
        var $input = $(element);

        if (isSwitchOn)
            $input.focus(select);
        else
            $input.unbind('focus', select);


        function select() {
            if ($input.prop("tagName") === "INPUT")
                $input.select();
            else
                $input.selectText();
        }
    }
}