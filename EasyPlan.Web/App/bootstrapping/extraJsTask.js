define(function (reqiure) {
    return {
        initialize
    }

    function initialize() {
        return Q.all([
            require('packages/handlebars-4.0.5'),
            require('packages/underscore')
        ])
    }
})