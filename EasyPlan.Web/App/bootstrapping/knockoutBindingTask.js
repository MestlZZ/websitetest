define(function (require) {

    return {
        initialize: initialize
    };

    function initialize() {
        return Q.all([
            require('bindings/editableInputBinding'),
            require('bindings/editableTextBinding'),
            require('bindings/selectTextOnFocusBinding')
        ]);
    }
})