define(['http/httpRequestSender', 'durandal/app', 'error'], function (httpRequestSender, app, errorHandler) {
    return {
        post: post,
        get: get
    };

    function post(url, data) {
        if (_.isUndefined(data))
            data = {};

        var tokenField = $("input[type='hidden'][name$='__RequestVerificationToken']");
        data[tokenField[0].name] = tokenField[0].value;

        return httpRequestSender.post(url, data, '')
                .fail(function (response) {
                    errorHandler.throw(response.message, response.status);
                });
    }

    function get(url, query) {        
        return httpRequestSender.get(url, query, '');
    }
});