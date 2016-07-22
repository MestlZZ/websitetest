define(['constants', 'http/storageHttpWrapper'],
    function (constants, storageHttpWrapper) {

    return {
        getBoard,
        createBoard,
        setTitle,
        removeBoard
    }

    function getBoard(boardId) {        
        return storageHttpWrapper.post(constants.storage.boardDataUrl, { boardId: boardId })
    }

    function createBoard() {
        return storageHttpWrapper.post(constants.storage.boardCreateUrl)
    }

    function setTitle(title, id) {
        return storageHttpWrapper.post(constants.storage.boardSetTitleUrl, { boardId: id, title: title });
    }

    function removeBoard(id) {
        return storageHttpWrapper.post(constants.storage.boardRemoveUrl, { boardId: id });
    }
});