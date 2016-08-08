define(function (require) {
    return {
        initialize: initialize
    }

    function initialize() {
        return Q.all([
            require('extenders/validateExtender'),
        ]);
    }
})