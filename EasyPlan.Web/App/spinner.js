define(['durandal/app'], function (app) {
    
    var spinner = $('.spinner');
    
    return {
        show,
        hide,
        initialize
    }

    function show() {
        spinner.removeClass('spinner-off');
        spinner.addClass('spinner-on');
    }

    function hide() {
        spinner.removeClass('spinner-on');
        spinner.addClass('spinner-off');
    }

    function initialize() {
        app.on('storageHttpWrapper:post-begin', function () {
            show();
        })

        app.on('storageHttpWrapper:post-end', function () {
            hide();
        })
    }

})