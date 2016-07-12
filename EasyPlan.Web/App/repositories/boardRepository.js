define(['constants', 'storageContext', 'mappers/boardMapper', 'http/storageHttpWrapper'],
    function (constants, storage, boardMapper, storageHttpWrapper) {

    return {
        getCollection: getCollection,
        getFirstBoard: getFirstBoard
    }

    function getCollection()
    {
        return storage.boards;
    }

    function getFirstBoard()
    {
        var boardInfo = storage.boards[0];
        if (!boardInfo)
            throw "Failed to load board";

        return storageHttpWrapper.post(constants.storage.host + constants.storage.boardDataUrl, { id: boardInfo.id });
    }
});