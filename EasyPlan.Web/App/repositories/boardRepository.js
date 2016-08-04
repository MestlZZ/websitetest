define(['constants', 'http/storageHttpWrapper'],
    function (constants, storageHttpWrapper) {

    return {
        getBoard,
        createBoard,
        setTitle,
        removeBoard,
        inviteUser,
        getBoardUsersInfo,
        removeUser
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

    function inviteUser(boardId, email, role) {
        return storageHttpWrapper.post(constants.storage.boardInviteUrl, { boardId: boardId, email, role });
    }

    function getBoardUsersInfo(boardId) {
        return storageHttpWrapper.post(constants.storage.getBoardUsersInfoUrl, { boardId: boardId });
    }

    function removeUser(boardId, email) {
        return storageHttpWrapper.post(constants.storage.removeUserFromBoardUrl, { boardId: boardId, email: email });
    }
});