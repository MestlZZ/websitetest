define(['durandal/app'], function (app) {
    
    var spinner = $('.spinner');
    
    return {
        show,
        hide
    }

    function show() {
        spinner.removeClass('spinner-off');
        spinner.addClass('spinner-on');
    }

    function hide() {
        spinner.removeClass('spinner-on');
        spinner.addClass('spinner-off');
    }
})