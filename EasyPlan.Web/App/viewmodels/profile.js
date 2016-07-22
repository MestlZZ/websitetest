define(['repositories/userRepository', 'repositories/boardRepository', 'spinner'], function (userRepository, boardRepository, spinner) {
    return viewModel = {
        user: {},
        activate,
        createBoard,
        removeBoard
    };
    
    function activate() {
        return userRepository.getCurrentUser().then(function (user) {
            viewModel.user = user;

            viewModel.user.boardsShortInfo = ko.observableArray(user.boardsShortInfo);

            spinner.hide();
        })
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