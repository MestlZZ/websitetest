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
    'durandal/viewLocator', 'repositories/studentsRepository'],
    function (system, app, viewLocator, studentRepository) {
    //>>excludeStart("build", true);
    system.debug(true);
    //>>excludeEnd("build");

    app.title = 'Test website';

    app.configurePlugins({
        router: true,
        dialog: true
    });

    app.start().then(function() {
        viewLocator.useConvention();

        studentRepository.initialize().then(function(){
            app.setRoot('viewmodels/shell', 'entrance');
        });
    });
});