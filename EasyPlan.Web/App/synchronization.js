define(['durandal/app', 'constants'], function (app, constants) {

    var boardHub = $.connection.boardHub;
    var boardId = '';

    boardHub.client.updateItemTitle = function (id, title) {
        app.trigger(constants.EVENT.BOARD.ITEM.TITLE_CHANGED, id, title);
    };
    boardHub.client.deleteItem = function (id) {
        app.trigger(constants.EVENT.BOARD.ITEM.REMOVED, id);
    };
    boardHub.client.addItem = function (item) {
        app.trigger(constants.EVENT.BOARD.ITEM.ADDED, item);
    };
    boardHub.client.setMark = function (mark) {
        app.trigger(constants.EVENT.BOARD.MARK.VALUE_CHANGED, mark);
    };
    boardHub.client.setCriterionWeight = function (id, weight) {
        app.trigger(constants.EVENT.BOARD.CRITERION.WEIGHT_CHANGED, id, weight);
    };
    boardHub.client.updateCriterionTitle = function (id, title) {
        app.trigger(constants.EVENT.BOARD.CRITERION.TITLE_CHANGED, id, title);
    };
    boardHub.client.updateBoardTitle = function (title) {
        app.trigger(constants.EVENT.BOARD.TITLE_CHANGED, title);
    };
    boardHub.client.deleteCriterion = function (id) {
        app.trigger(constants.EVENT.BOARD.CRITERION.REMOVED, id);
    };
    boardHub.client.addCriterion = function (criterion) {
        app.trigger(constants.EVENT.BOARD.CRITERION.ADDED, criterion);
    };
    boardHub.client.addCollaborator = function (collaborator, boardId) {
        app.trigger(constants.EVENT.BOARD.COLLABORATOR.ADDED, collaborator, boardId);
    };
    boardHub.client.removeCollaborator = function (email, boardId) {
        app.trigger(constants.EVENT.BOARD.COLLABORATOR.REMOVED, email, boardId);
    };
    boardHub.client.collaboratorChangeRole = function (email, role, boardId) {
        app.trigger(constants.EVENT.BOARD.COLLABORATOR.ROLE_CHANGED, email, role, boardId);
    };
    boardHub.client.removeBoard = function (boardId) {
        app.trigger(constants.EVENT.BOARD.REMOVED, boardId);
    };


    var model = {
        boardHub: boardHub,
        isBoardOpened: false,
        connect: connect,
        disconnect: disconnect,
        openBoard: openBoard,
        closeBoard: closeBoard
    };

    return model;

    function openBoard(id) {        
        model.isBoardOpened = true;
        boardId = id;

        boardHub.server.openBoard(id);
    }

    function closeBoard() {
        if (!_.isEmpty(boardId)) {
            boardHub.server.closeBoard(boardId);

            model.isBoardOpened = false;
            boardId = '';
        }
    }

    function connect() {
        model.isBoardOpened = false;
        return $.connection.hub.start();
    }

    function disconnect() {
        model.isBoardOpened = false;
        return $.connection.hub.stop();
    }
});