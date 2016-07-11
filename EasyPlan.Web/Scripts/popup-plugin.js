(function ($) {
    $.fn.popup = function (context, element) {
        var deferred = Q.defer();
        
        $('.confirmation-popup').remove();
        var source = $(this).html();
        var template = Handlebars.compile(source);
        var htmlResut = template(context);

        $('body').append(htmlResut);

        $('.confirmation-popup').dialog({
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

        /*$('.popup-button.button-cancel').click(function cl(e) {
            $('.confirmation-popup').remove();
            $(this).unbind('click', cl);
            
            deferred.resolve(false);
            e.preventDefault();
        })

        $('.popup-button.button-ok').click(function cl(e) {
            $('.confirmation-popup').remove();
            $(this).unbind('click', cl);

            deferred.resolve(true);
            e.preventDefault();
        })

        $('html').click(function cl(e) {
            if (e.target != $('.popup-button')[0])
            {
                console.log(e.target);
                console.log($('.popup-button')[0]);

               // $('html').unbind('click', cl);

                e.preventDefault();
                e.stopPropagation();
            }
        })
        */
        return deferred.promise;
    };
})(jQuery);