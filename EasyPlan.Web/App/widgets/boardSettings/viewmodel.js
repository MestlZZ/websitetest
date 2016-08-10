define(['repositories/boardRepository', 'spinner', 'constants', 'error', 'durandal/app', 'widgets/popup/viewmodel'],
    function (boardRepository, spinner, constants, errorHandler, app, popup) {
        
        var boardHub = $.connection.boardHub;

        /*Hub events*/
        app.on(constants.EVENT.BOARD.COLLABORATOR.ADDED, function (collaborator) {
            settingsViewModel.info().usersInRoles.unshift(MapUserRole(collaborator));
        });

        app.on(constants.EVENT.BOARD.COLLABORATOR.REMOVED, function (email) {
            var userInRole = _.find(settingsViewModel.info().usersInRoles(), function (userInRole) { return userInRole.user.email == email; });

            settingsViewModel.info().usersInRoles.remove(userInRole);
        });

        app.on(constants.EVENT.BOARD.COLLABORATOR.ROLE_CHANGED, function (email, role) {
            var userInRole = _.find(settingsViewModel.info().usersInRoles(), function (userInRole) { return userInRole.user.email == email; });

            userInRole.accessLevel._latestValue = role;
        });
        /*end*/

        var settingsViewModel = {
            role: ko.observable(0),
            roles: [],
            email: ko.observable(''),
            info: ko.observable({}),
            boardId: ko.observable(''),
            clientEmail: ko.observable(''),
            clientRole: ko.observable(0),
            ROLE: constants.ROLE,
            activate: activate,
            invite: invite,
            removeUser: removeUser
        };

        return settingsViewModel;

        function removeUser(userRole) {
            popup.showConfirmation('Remove', 'remove user "' + userRole.user.email + '" from board', function () {
                boardRepository.removeUser(settingsViewModel.boardId(), userRole.user.email).then(function () {
                    settingsViewModel.info().usersInRoles.remove(userRole);

                    boardHub.server.removeCollaborator(settingsViewModel.boardId(), userRole.user.email);
                });
            });
        }

        function invite(model) {
            var boardId = ko.unwrap(model.boardId);
            var email = ko.unwrap(model.email);
            var role = ko.unwrap(model.role);

            for (var i = 0; i < settingsViewModel.info().usersInRoles().length; i++)
            {
                if(settingsViewModel.info().usersInRoles()[i].user.email == email)
                {
                    return errorHandler.throw('User with email: ' + email + ' has already exist in board.', 400);
                }
            }

            setRole(boardId, email, role).then(function (data) {
                model.email('');
                model.role(3);

                boardHub.server.addCollaborator(settingsViewModel.boardId(), data);
            })
        }

        function activate(settings) {
            var self = this;

            self.roles = [
                { name: 'Admin', accessLevel: constants.ROLE.ADMIN },
                { name: 'Editor', accessLevel: constants.ROLE.EDITOR },
                { name: 'Viewer', accessLevel: constants.ROLE.VIEWER }
            ];

            self.role(3);
            self.email('');
            self.boardId(settings.boardId);

            return boardRepository.getBoardUsersInfo(settings.boardId)
                .then(function (info) {
                    info.board.usersInRoles = ko.observableArray(_.map(info.board.usersInRoles, MapUserRole));

                    self.info(info.board);
                    self.clientEmail(info.clientEmail);
                    self.clientRole(info.clientRole);
                });
        }

        function MapUserRole(userRole) {
            var result = {};

            result.user = userRole.user;
            result.accessLevel = ko.observable(userRole.accessLevel);

            window.userRole = result;
            window.viewModel = settingsViewModel;

            result.accessLevel.subscribe(function () {
                setRole(settingsViewModel.boardId(), result.user.email, result.accessLevel()).then(function () {
                    boardHub.server.changeCollaboratorRole(settingsViewModel.boardId(), result.user.email, result.accessLevel());
                });
            });

            return result;
        }

        function setRole(boardId, email, role) {
            spinner.show();

            return boardRepository.inviteUser(boardId, email, role).then(function(data) {
                spinner.hide();

                return data;
            });
        }
    });