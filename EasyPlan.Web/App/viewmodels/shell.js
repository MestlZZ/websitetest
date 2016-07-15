define(['plugins/router', 'durandal/app'], function (router, app) {
    return {
        router: router,
        activate: function () {
            var self = this;

//router.navigate('#board/ccef5cf6-5184-4a5a-8234-c2df683cbfba', false);

            router.map([
                { route: '', title: 'Board', moduleId: 'viewmodels/board', nav: true },
                { route: 'board/:id', title: 'Board', moduleId: 'viewmodels/board', nav: true }
            ]).buildNavigationModel();

            return router.activate();
        }
    };
});