(function ($) {
    $.fn.popup = function (context) {
        var deferred = Q.defer();

        var source = $(this).html();
        var template = Handlebars.compile(source);
        var htmlResut = template(context);
        var $popup = $('<div />').html(htmlResut).find('div').first();

        $('.' + $popup.attr('class')).remove();

        $('body').append($popup);

        var title = "";

        $popup.dialog({
            title: context.title,
            draggable: true,
            closeText: '✖',
            resizable: true,
            width: 400,
            height: 'auto',
            modal: true,
            hide: { effect: "fade", duration: 200 },
            show: { effect: "fade", duration: 200 },
            buttons: [
              {
                  text: "Confirm",
                  click: function () {
                      $(this).dialog("option", "hide", { effect: "explode", duration: 600 });
                      $(this).dialog("close");
                      deferred.resolve(true);
                  },
                  'class': 'confirm-button'
              },
              {
                  text: "Cancel",
                  click: function () {
                      $(this).dialog("close");
                      deferred.resolve(false);
                  },
                  'class': 'cancel-button'
              }
            ]
        });
        return deferred.promise;
    };
})(jQuery);