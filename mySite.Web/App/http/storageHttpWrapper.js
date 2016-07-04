define(['http/httpRequestSender', 'durandal/app'], function (httpRequestSender, app) {
    "use strict";

    return {
        post: post,
        get: get
    };

    function post(url, data) {
        app.trigger('storageHttpWrapper:post-begin');

        return httpRequestSender.post(url, data, '').fin(function () {
            app.trigger('storageHttpWrapper:post-end');
        });
    }

    function get(url, query) {
        app.trigger('storageHttpWrapper:get-begin');

        return httpRequestSender.get(url, query, '').fin(function () {
            app.trigger('storageHttpWrapper:get-end');
        });
    }
});