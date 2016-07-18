define(['constants', 'storageContext', 'http/storageHttpWrapper'],
    function (constants, storage, storageHttpWrapper) {

    return {
        getCollection,
        getBoard,
        getOpenedBoard,
        updateOpenedBoard
    }

    function getCollection() {
        return storage.boards;
    }

    function getBoard(boardId) {        
        return storageHttpWrapper.post(constants.storage.boardDataUrl, { id: boardId }).then(function (board) {
            storage.openedBoard = board;

            return board;
        });
    }

    function getOpenedBoard() {
        return storage.openedBoard;
    }

    function updateOpenedBoard(board) {
        storage.openedBoard = board;
    }
});