define(['plugins/router', 'durandal/app'], function (router, app) {

    return viewModel = {
        user: {},
        router: router,
        activate: function () {
            var self = this;

            router.map([
                { route: '', title: 'User panel', moduleId: 'viewmodels/profile', nav: true },
                { route: 'board/:id', title: 'Board', moduleId: 'viewmodels/board', nav: true },
            ]).buildNavigationModel();

            return router.activate();
        }
    };
});