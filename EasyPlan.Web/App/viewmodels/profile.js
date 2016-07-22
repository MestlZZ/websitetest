define(['repositories/userRepository', 'repositories/boardRepository', 'spinner'], function (userRepository, boardRepository, spinner) {
    return viewModel = {
        user: {},
        activate,
        createBoard,
        removeBoard,
        getBoardColor
    };
    
    function activate() {
        return userRepository.getCurrentUser().then(function (user) {
            viewModel.user = user;

            viewModel.user.boardsShortInfo = ko.observableArray(user.boardsShortInfo);

            spinner.hide();
        })
    }

    function getBoardColor() {
        return Math.floor(Math.random() * 16777215).toString(16);
    }

    function createBoard() {
        spinner.show();

        boardRepository.createBoard().then(function (boardShortInfo) {
            viewModel.user.boardsShortInfo.push(boardShortInfo);

            spinner.hide();
        })
    }

    function removeBoard(board) {
        boardRepository.removeBoard(board.id);

        viewModel.user.boardsShortInfo.remove(board);
    }
});