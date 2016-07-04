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
    'durandal/viewLocator', 'http/storageHttpWrapper',
    'constants', 'storageContext'],
    function (system, app, viewLocator, storageHttpWrapper,
                constants, storage) {
    //>>excludeStart("build", true);
    system.debug(true);
    //>>excludeEnd("build");

    app.title = 'Durandal Starter Kit';

    app.configurePlugins({
        router: true,
        dialog: true
    });

    app.start().then(function() {
        //Replace 'viewmodels' in the moduleId with 'views' to locate the view.
        //Look for partial views in a 'views' folder in the root.
        viewLocator.useConvention();

        //Show the app by setting the root view model for our application with a transition.
        storageHttpWrapper.post(constants.storage.host + constants.storage.studentsUrl).then(function (students) {
            storage.set('students', students);
        });

        app.setRoot('viewmodels/shell', 'entrance');
    });
});