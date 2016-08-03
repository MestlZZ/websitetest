define(['durandal/app'], function (app) {
    
    var spinner = $('.spinner');
    
    return spinnerModel = {
        enabled: false,
        show,
        hide
    }

    function show() {
        spinnerModel.enabled = true;
        spinner.removeClass('spinner-off');
        spinner.addClass('spinner-on');
    }

    function hide() {
        spinnerModel.enabled = false;
        spinner.removeClass('spinner-on');
        spinner.addClass('spinner-off');
    }
})