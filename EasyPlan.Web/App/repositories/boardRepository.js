define(['constants', 'storageContext', 'mappers/boardMapper', 'http/storageHttpWrapper'],
    function (constants, storage, boardMapper, storageHttpWrapper) {

    return {
        getCollection,
        getOpenedBoard
    }

    function getCollection()
    {
        return storage.boards;
    }

    function getOpenedBoard()
    {
        var boardInfo = storage.boards[0];

        storage.openedBoardId = boardInfo.id;

        if (!boardInfo)
            throw "Failed to load board";

        return storageHttpWrapper.post(constants.storage.host + constants.storage.boardDataUrl, { id: boardInfo.id });
    }
});