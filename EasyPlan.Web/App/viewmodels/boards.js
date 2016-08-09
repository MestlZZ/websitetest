define(['repositories/userRepository', 'repositories/boardRepository', 'spinner', 'constants', 'durandal/app'],
    function (userRepository, boardRepository, spinner, constants, app) {

        var boardHub = $.connection.boardHub;

        /*Hub events*/
        app.on(constants.EVENT.BOARD.COLLABORATOR.ADDED, function (collaborator, boardId) {
            if(collaborator.user.email == profileViewModel.user.email) {
                getShortBoardData(boardId);
            }
        });
        
        app.on(constants.EVENT.BOARD.COLLABORATOR.REMOVED, function (email, boardId) {
            if (email == profileViewModel.user.email) {
                var entity = _.find(profileViewModel.user.boardsShortInfo(), function (entity) { return entity.board.id == boardId; })

                profileViewModel.user.boardsShortInfo.remove(entity);
            }
        });

        app.on(constants.EVENT.BOARD.REMOVED, function (boardId) {
            var entity = _.find(profileViewModel.user.boardsShortInfo(), function (entity) { return entity.board.id == boardId; })

            profileViewModel.user.boardsShortInfo.remove(entity);
        });
        /*end*/

        var profileViewModel = {
            user: {},
            activate: activate,
            createBoard: createBoard,
            removeBoard: removeBoard
        };

        return profileViewModel;

        function activate() {
            var self = this;

            self.user = {};

            if (!spinner.enabled)
                spinner.show();

            return userRepository.getCurrentUser()
                .then(function (user) {
                    self.user = user;

                    self.user.boardsShortInfo = ko.observableArray(user.boardsShortInfo);

                    spinner.hide();
                });
        }

        function getShortBoardData(id) {
            boardRepository.getShortBoard(id).then(function (boardShortInfo) {
                profileViewModel.user.boardsShortInfo.push(boardShortInfo);

                spinner.hide();
            });
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

            $(constants.popupTemplatesId.confirmation).popup({ title: 'Delete', body: 'delete "' + board.title + '"' })
                .then(function (response) {
                    if (response) {
                        boardRepository.removeBoard(board.id);

                        if (board.createdBy == profileViewModel.user.email) {
                            boardHub.server.removeBoard(board.id);
                        } else {
                            boardHub.server.removeCollaborator(board.id, profileViewModel.user.email);
                        }
                    }
                });
        }
    });