define(['http/storageHttpWrapper', 'constants'],
function (storageHttpWrapper, constants) {

    var storage = {
        boards: [],
        initialize: initialize
    };

    function initialize()
    {
        return storageHttpWrapper.post(constants.storage.host + constants.storage.boardsUrl).then(function (boards) {
            storage.boards = boards;
        });
    }

    return storage
});