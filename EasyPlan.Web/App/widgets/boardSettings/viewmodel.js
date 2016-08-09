define(['repositories/boardRepository', 'spinner', 'constants'],
    function (boardRepository, spinner, constants) {

        return viewModel1 = {
            role: ko.observable(),
            roles: [],
            email: ko.observable(),
            info: ko.observable(),
            boardId: ko.observable(),
            clientEmail: ko.observable(),
            clientRole: ko.observable(),
            ROLE: constants.ROLE,
            activate: activate,
            invite: invite,
            removeUser: removeUser
        };

        function removeUser(userRole) {
            $(constants.popupTemplatesId.confirmation).popup({ title: 'Remove', body: 'remove user "' + userRole.user.email + '" from board' })
                .then(function (response) {
                    if (response) {
                        boardRepository.removeUser(viewModel1.boardId(), userRole.user.email).then(function () {
                            viewModel1.info().usersInRoles.remove(userRole);
                        });
                    }
                });
        }

        function invite(model) {
            var boardId = ko.unwrap(model.boardId);
            var email = ko.unwrap(model.email);
            var role = ko.unwrap(model.role);

            setRole(boardId, email, role).then(function (data) {
                if (_.isUndefined(data.message)) {

                    model.info().usersInRoles.unshift(MapUserRole(data));

                    model.email('');
                    model.role(3);
                } else {
                }
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
            window.viewModel = viewModel1;

            result.accessLevel.subscribe(function () {
                setRole(viewModel1.boardId, result.user.email, result.accessLevel);
            });

            return result;
        }

        function setRole(boardId, email, role) {
            spinner.show();

            return boardRepository.inviteUser(boardId, email, role).then(function (result) {
                spinner.hide();

                return result;
            });
        }
    });