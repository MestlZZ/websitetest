define(['http/httpRequestSender', 'durandal/app'], function (httpRequestSender, app) {
    "use strict";

    return {
        post: post,
        get: get
    };

    function post(url, data) {
        return httpRequestSender.post(url, data, '');
    }

    function get(url, query) {        
        return httpRequestSender.get(url, query, '');
    }
});