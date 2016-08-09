define(['plugins/router', 'durandal/app'], function (router, app) {

    return viewModel = {
        user: {},
        router: router,
        activate: function () {
            var self = this;

            router.map([
                { route: '', title: 'Boards List', moduleId: 'viewmodels/boards', nav: true },
                { route: 'board/:id', title: 'Board', moduleId: 'viewmodels/board', nav: true },
                { route: 'error', title: 'Board', moduleId: 'viewmodels/error', nav: true }
            ]).buildNavigationModel();

            return router.activate();
        }
    };
});