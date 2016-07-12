(function ($) {
    $.fn.popup = function (context) {
        var deferred = Q.defer();

        var source = $(this).html();
        var template = Handlebars.compile(source);
        var htmlResut = template(context);
        var popup = $('<div />').html(htmlResut).find('div').first();

        $('.' + popup.attr('class')).remove();

        $('body').append(popup);

        popup.dialog({
            dialogClass: "no-close",
            modal: true,
            hide: { effect: "fade", duration: 200 },
            show: { effect: "fade", duration: 200 },
            buttons: [
              {
                  text: "OK",
                  click: function () {
                      $(this).dialog("option", "hide", { effect: "explode", duration: 1000 });
                      $(this).dialog("close");
                      deferred.resolve(true);
                  }
              },
              {
                  text: "Cancel",
                  click: function () {
                      $(this).dialog("close");
                      deferred.resolve(false);
                  }
              }
            ]
        });
        
        return deferred.promise;
    };
})(jQuery);