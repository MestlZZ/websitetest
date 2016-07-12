define(['http/storageHttpWrapper', 'constants', 'mappers/boardMapper'],
function (storageHttpWrapper, constants, boardMapper) {

    var storage = {
        boards: [],
        openedBoardId: '',
        initialize: initialize
    };

    function initialize()
    {
        return storageHttpWrapper.post(constants.storage.host + constants.storage.boardsInfoUrl).then(function (boards) {
            storage.boards = _.map(boards, boardMapper.mapInfo);
        });
    }

    return storage
});