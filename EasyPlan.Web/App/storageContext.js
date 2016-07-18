define(['http/storageHttpWrapper', 'constants'],
function (storageHttpWrapper, constants, boardMapper) {

    var storage = {
        boards: [],
        openedBoard: {},
        initialize: initialize
    };

    function initialize()
    {
        return storageHttpWrapper.post(constants.storage.boardsInfoUrl).then(function (boards) {
            storage.boards = boards;
        });
    }

    return storage
});