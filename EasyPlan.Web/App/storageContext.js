define(['http/storageHttpWrapper', 'constants', 'mappers/boardMapper'],
function (storageHttpWrapper, constants, boardMapper) {

    var storage = {
        boards: [],
        initialize: initialize
    };

    function initialize()
    {
        return storageHttpWrapper.post(constants.storage.host + constants.storage.boardsUrl).then(function (boards) {
            storage.boards = _.map(boards, boardMapper.map);
        });
    }

    return storage
});