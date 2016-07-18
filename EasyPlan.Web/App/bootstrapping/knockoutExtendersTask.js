define(function (require) {
    return {
        initialize
    }

    function initialize() {
        return Q.all([
            require('extenders/validateExtender'),
        ]);
    }
})