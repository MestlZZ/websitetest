define(function () {
    var storage = {};

    function set(key, value) {
        if (_.isNull(key) ||
            _.isUndefined(key) ||
                _.isEmpty(key) ||
                _.isUndefined(value)) {
            throw 'Invalid arguments';
        }

        storage[key] = value;
        return value;
    };

    function get(key) {
        if (_.isNull(key) ||
            _.isUndefined(key) ||
            _.isEmpty(key)) {
            throw 'Invalid arguments';
        }

        return storage[key];
    };

    function remove(key) {
        if (_.isNull(key) ||
            _.isUndefined(key) ||
            _.isEmpty(key)) {
            throw 'Invalid arguments';
        }

        delete storage[key];
    };

    return {
        set: set,
        get: get,
        remove: remove
    }

});