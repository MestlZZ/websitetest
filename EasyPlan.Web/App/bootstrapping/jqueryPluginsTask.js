define(function (require) {
    return {
        initialize
    }

    function initialize() {
        return Q.all([
            require('packages/jqueryPlugins/jquery-ui'),
            require('packages/jqueryPlugins/jquery.popup'),
            require('packages/jqueryPlugins/jquery.selectText')
        ]);
    }
})