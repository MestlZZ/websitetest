define(function (require) {
    return {
        getTasks
    }

    function getTasks()
    {
        return [
            require('bootstrapping/jqueryPluginsTask'),
            require('bootstrapping/knockoutBindingTask'),
            require('bootstrapping/knockoutExtendersTask'),
            require('bootstrapping/popupTemplatesTask')
        ]
    }
})