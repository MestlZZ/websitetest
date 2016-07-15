requirejs.config({
    paths: {
        'text': '../Scripts/text',
        'durandal': 'core',
        'plugins': 'core/plugins',
        'transitions': 'core/transitions',
        'packages': 'core/packages',
        'bindings': 'core/packages/knockoutBindings',
        'extenders': 'core/packages/knockoutExtenders'

    }
});

define('jquery', function () { return jQuery; });
define('knockout', ko);

define(['durandal/system', 'durandal/app',
    'durandal/viewLocator', 'storageContext',
    'spinner', 'bootstrapper'],
    function (system, app, viewLocator, storage, spinner, bootstrapper) {
    //>>excludeStart("build", true);
    system.debug(true);
    //>>excludeEnd("build");

    app.title = 'EasyPlan';

    app.configurePlugins({
        router: true,
        dialog: true
    });

    bootstrapper.initialize().then(function () {
        return app.start();
    })
    .then(function () {
        viewLocator.useConvention();

        storage.initialize().then(function () {
            spinner.initialize();
            spinner.hide();

            app.setRoot('viewmodels/shell', 'entrance', 'application');
        });
    });
});