requirejs.config({
    paths: {
        'text': '../Scripts/text',
        'durandal': 'core',
        'plugins': 'core/plugins',
        'transitions': 'core/transitions',
        'bindings': 'packages/knockoutBindings',
        'extenders': 'packages/knockoutExtenders'
    }
});

define('jquery', function () { return jQuery; });
define('knockout', ko);

define(['durandal/system', 'durandal/app', 'synchronization',
    'durandal/viewLocator','spinner', 'bootstrapper'],
    function (system, app, sync, viewLocator, spinner, bootstrapper) {

    //>>excludeStart("build", true);
    system.debug(true);
    //>>excludeEnd("build");

    app.title = 'EasyPlan';

    app.configurePlugins({
        router: true,
        dialog: true,
        widget: {
            kinds: ['boardSettings']
        }
    });

    bootstrapper.initialize()
        .then(function () { return sync.connect(); })
        .then(function () { return app.start(); })
        .then(function () {
            viewLocator.useConvention();
            app.setRoot('viewmodels/shell', 'entrance', 'application');
        });
});