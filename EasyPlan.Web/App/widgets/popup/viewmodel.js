define(function () {

    var model = {
        buttons: ko.observableArray([]),
        title: ko.observable(''),
        enabled: ko.observable(false),
        activate: activate,
        show: show,
        hide: hide,
        showConfirmation: showConfirmation
    };

    return model;

    function activate(settings) {
        this.enabled(false);
    }

    function show(settings) {        
        var $body = $('.popup-content > .popup-body');

        $body.load(settings.templatePath, function () {
            var source = $body.html();
            var template = Handlebars.compile(source);
            var htmlResult = template(settings.data);

            $body.html(htmlResult);

            model.buttons(_.map(settings.buttons, mapButton));
            model.title(settings.data.title);

            model.enabled(true);
        });
    }

    function showConfirmation(title, body, callback) {
        var popupSettings = {
            templatePath: '../App/templates/confirmation-popup-template.html',
            buttons: [
                {
                    title: 'Confirm',
                    'class': 'confirm-button',
                    callback: callback
                },
                {
                    title: 'Cancel',
                    'class': 'cancel-button',
                    callback: function () {}
                }
            ],
            data: {
                title: title,
                body: body
            }
        }

        show(popupSettings);
    }

    function mapButton(data) {
        var button = {};

        button.title = data.title;
        button.buttonClass = data.class;
        button.callback = function () {
            data.callback();
            hide();
        };

        return button;
    }

    function hide() {
        model.enabled(false);
    }
})