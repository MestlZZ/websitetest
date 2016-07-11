define(['plugins/router', 'durandal/app'], function (router, app) {
    return {
        router: router,
        activate: function () {
            router.map([
                { route: '', title:'Home', moduleId: 'viewmodels/board', nav: true },
            ]).buildNavigationModel();
            
            return router.activate();
        }
    };
});