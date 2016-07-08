define(['http/storageHttpWrapper', 'constants'],
function (storageHttpWrapper, constants) {

    var storage = {
        boards: [],
        initialize: initialize
    };

    function initialize()
    {
        return storageHttpWrapper.post(constants.storage.host + constants.storage.boardsUrl).then(function (students) {
            storage.boards = JSON.parse(students);
        });
    }

    return storage
});