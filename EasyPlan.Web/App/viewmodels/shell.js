define(['plugins/router', 'durandal/app'], function (router, app) {
    return {
        router: router,
        spinner: ko.observable(false),
        activate: function () {
            var self = this;

            router.map([
                { route: '', title:'Board', moduleId: 'viewmodels/board', nav: true },
            ]).buildNavigationModel();            

            return router.activate();
        }
    };
});