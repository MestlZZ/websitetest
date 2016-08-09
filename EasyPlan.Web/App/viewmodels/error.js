define(['durandal/app', 'constants', 'plugins/router'], function (app, constants, router) {

    return errorViewModel = {
        statusCode: ko.observable(200),
        reason: ko.observable(''),
        activate: activate,
        deactivate: deactivate
    };

    function deactivate() {
        this.statusCode(200);
        this.reason('');
    }

    function activate() {
        var self = this;

        if (self.statusCode() == 200) {
            router.navigate('#');
        }
    }
})