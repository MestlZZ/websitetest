requirejs.config({
    paths: {
        'text': '../Scripts/text',
        'durandal': '../Scripts/durandal',
        'plugins': '../Scripts/durandal/plugins',
        'transitions': '../Scripts/durandal/transitions'
    }
});

define('jquery', function () { return jQuery; });
define('knockout', ko);

define(['durandal/system', 'durandal/app',
    'durandal/viewLocator', 'storageContext',
    'spinner'],
    function (system, app, viewLocator, storage, spinner) {
    //>>excludeStart("build", true);
    system.debug(true);
    //>>excludeEnd("build");

    app.title = 'EasyPlan';

    app.configurePlugins({
        router: true,
        dialog: true
    });

    app.start().then(function() {
        viewLocator.useConvention();

        storage.initialize().then(function () {
            spinner.initialize();
            spinner.stop();

            app.setRoot('viewmodels/shell', 'entrance');
        });
    });
});