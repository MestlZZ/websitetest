﻿define(['repositories/boardRepository', 'spinner'], function (boardRepository, spinner) {
    return viewModel1 = {
        role: ko.observable(),
        email: ko.observable(),
        info: ko.observable(),
        boardId: ko.observable(),
        activate: function (settings) {
            var self = this;

            self.role(3);
            self.email('');
            self.boardId(settings.boardId);

            return boardRepository.getBoardUsersInfo(settings.boardId).then(function (info) {
                info.usersInRoles = ko.observableArray(_.map(info.usersInRoles, MapUserRole));
                self.info(info);
            });
        },
        invite: function (model) {
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
        },
        removeUser: function (userRole) {
            console.log(userRole);

            boardRepository.removeUser(viewModel1.boardId(), userRole.user.email).then(function () {
                viewModel1.info().usersInRoles.remove(userRole);
            })
        }
    }

    function MapUserRole(userRole)
    {
        var result = {};

        result.user = userRole.user;
        result.accessLevel = ko.observable(userRole.accessLevel);

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
})