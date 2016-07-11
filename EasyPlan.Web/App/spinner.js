define(['durandal/app'], function (app) {
    
    var spinner = $('.spinner');
    
    return {
        start,
        stop,
        initialize
    }

    function start() {
        spinner.removeClass('spinner-off');
        spinner.addClass('spinner-on');
    }

    function stop() {
        spinner.removeClass('spinner-on');
        spinner.addClass('spinner-off');
    }

    function initialize() {
        app.on('storageHttpWrapper:post-begin', function () {
            start();
        })

        app.on('storageHttpWrapper:post-end', function () {
            stop();
        })
    }

})