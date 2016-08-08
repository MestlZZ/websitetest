define(['repositories/userRepository', 'repositories/boardRepository', 'spinner', 'constants'], function (userRepository, boardRepository, spinner, constants) {
    return profileViewModel = {
        user: {},
        activate: activate,
        createBoard: createBoard,
        removeBoard: removeBoard,
        getBoardColor: getBoardColor
    };
    
    function activate() {
        var self = this;

        self.user = {};

        if (!spinner.enabled)
            spinner.show();

        return userRepository.getCurrentUser().then(function (user) {
            self.user = user;

            self.user.boardsShortInfo = ko.observableArray(user.boardsShortInfo);

            spinner.hide();
        })
    }

    function getBoardColor() {
        return Math.floor(Math.random() * 16777215).toString(16);
    }

    function createBoard() {
        spinner.show();

        boardRepository.createBoard().then(function (boardShortInfo) {
            profileViewModel.user.boardsShortInfo.push(boardShortInfo);

            spinner.hide();
        })
    }

    function removeBoard(entity) {
        var board = entity.board;

        $(constants.popupTemplatesId.confirmation).popup({ title: "Delete", body: 'delete "' + board.title + '"' })
            .then(function (s) {
                if (s) {
                    boardRepository.removeBoard(board.id);

                    profileViewModel.user.boardsShortInfo.remove(board);
                }
            });
    }
});