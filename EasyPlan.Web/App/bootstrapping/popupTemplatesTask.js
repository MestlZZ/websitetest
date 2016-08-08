define(function (require) {
    return {
        initialize: initialize
    }

    function initialize() {
        initializeConfirmTemplate();
    }

    function initializeConfirmTemplate()
    {
        $('<script/>', {
            id: 'confirmation-popup-body-template',
            type: 'text/x-handlebars-template'
        }).appendTo('body');

        var $form = $('#confirmation-popup-body-template');

        $form.load('../App/templates/confirmation-popup-template.html');
    }
});