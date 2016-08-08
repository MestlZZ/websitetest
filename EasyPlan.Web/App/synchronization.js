define(['durandal/app', 'constants'], function (app, constants) {

    var boardHub = $.connection.boardHub;
    var boardId = '';
    var isConnectedToBoard = false;

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


    return {
        boardHub: boardHub,
        connect: connect,
        disconnect: disconnect,
        openBoard: openBoard,
        closeBoard: closeBoard
    };


    function openBoard(id) {
        isConnectedToBoard = true;
        boardId = id;

        boardHub.server.openBoard(id);
    }

    function closeBoard() {
        if (_.isEmpty(boardId))
            throw 'Can\'t disconect from board because board id is clear';

        boardHub.server.closeBoard(boardId);

        isConnectedToBoard = false;
        boardId = '';
    }

    function connect() {
        return $.connection.hub.start();
    }

    function disconnect() {
        return $.connection.hub.stop();
    }
});