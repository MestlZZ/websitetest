define(function () {

    var model = {
        buttons: ko.observableArray([]),
        title: ko.observable(''),
        enabled: ko.observable(false),
        activate: activate,
        show: show,
        hide: hide
    };

    return model;

    function activate(settings) {
        this.enabled(false);
    }

    function show(settings) {        
        var $body = $('.popup-content > .popup-body');

        $body.load(settings.templatePath);

        var source = $body.html();
        var template = Handlebars.compile(source);
        var htmlResult = template(settings.data);

        $body.html(htmlResult);

        model.buttons(_.map(settings.buttons, mapButton));

        model.enabled(true);
    }

    function mapButton(data) {
        var button = {};

        button.title = data.title;
        button.class = data.class;
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