define(['constants', 'storageContext', 'mappers/boardMapper', 'http/storageHttpWrapper'],
    function (constants, storage, boardMapper, storageHttpWrapper) {

    return {
        getCollection,
        getBoard
    }

    function getCollection()
    {
        return storage.boards;
    }

    function getBoard(boardId)
    {        
        return storageHttpWrapper.post(constants.storage.host + constants.storage.boardDataUrl, { id: boardId });
    }
});