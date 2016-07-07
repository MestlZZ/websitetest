define(['http/storageHttpWrapper', 'constants'],
function (storageHttpWrapper, constants) {

    var storage = {
        boards: []
    };

    function initialize()
    {
        return storageHttpWrapper.post(constants.storage.host + constants.storage.boardsUrl).then(function (students) {
            storage[constants.storage.keys.boards] = JSON.parse(students);
        });
    }

    function set(key, value) {
        if (!(key in storage)) {
            throw 'Invalid key';
        }

        storage[key] = value;
        return value;
    };

    function get(key) {
        if (!(key in storage)) {
            throw 'Invalid key';
        }

        return storage[key];
    };

    function remove(key) {
        if (!(key in storage)) {
            throw 'Invalid key';
        }

        delete storage[key];
    };

    return {
        set: set,
        get: get,
        remove: remove,
        initialize: initialize
    }
});