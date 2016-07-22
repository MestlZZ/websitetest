using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyPlan.DomainModel.Entities;
using EasyPlan.DomainModel.Repositories;
using EasyPlan.Infrastructure;
using System.Web.Security;

namespace EasyPlan.Web.Components
{
    public class RoleProvider : IRoleProvider
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RoleProvider(IRoleRepository roleRepository, IUnitOfWork unitOfWork)
        {
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
        }

        public void SetUserRole(Board board, User user, RoleName roleName)
        {
            var currentRole = GetRoleForUser(board, user);

            if (currentRole == null)
            {
                var userRole = new Role(board, user, roleName);
                _roleRepository.Add(userRole);
            }
            else
            {
                currentRole.SetRole(roleName);
            }

            _unitOfWork.Save();
        }

        public IEnumerable<User> FindUsersInRole(Board board, RoleName roleName)
        {
            var userRolesDependencesInBoard = _roleRepository.GetCollection().ToList().FindAll(e => e.Board == board);
            var users = userRolesDependencesInBoard.FindAll(e => e.Name == roleName).Select(e => e.User);
            return users;
        }

        public IEnumerable<Role> GetRolesForUser(User user)
        {
            var roles = _roleRepository.GetCollection().ToList().FindAll(e => e.User == user);

            return roles;
        }

        public Role GetRoleForUser(Board board, User user)
        {
            var roles = _roleRepository.GetCollection().ToList().FindAll(e => e.User == user);
            var role = roles.Find(e => e.Board == board);

            return role;
        }

        public bool IsUserInRole(Board board, string email, RoleName roleName)
        {
            var users = FindUsersInRole(board, roleName);

            var user = users.First(e => e.Email.Equals(email));

            return user != null;
        }

        public void RemoveUserFromRole(Board board, User user)
        {
            var role = GetRoleForUser(board, user);

            ArgumentValidation.ThrowIfNull(role, argumentName: "role for user in board");

            _roleRepository.Remove(role);
            _unitOfWork.Save();
        }
    }
}