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
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RoleProvider(IRoleRepository roleRepository, IUserRoleRepository userRoleRepository, IUnitOfWork unitOfWork)
        {
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
            _unitOfWork = unitOfWork;
        }

        public void SetUserRole(Board board, User user, string roleName)
        {
            var role = FindRoleByName(roleName);

            var currentRole = GetRoleForUser(board, user);

            if (currentRole == null)
            {
                var userRole = new UserRole(board, user, role);
                _userRoleRepository.Add(userRole);
            }
            else
            {
                currentRole.SetRole(role);
            }

            _unitOfWork.Save();
        }

        public void CreateRole(string roleName)
        {
            var role = new Role(roleName);

            _roleRepository.Add(role);

            _unitOfWork.Save();   
        }

        public bool DeleteRole(string roleName)
        {
            var role = FindRoleByName(roleName);

            if(role != null)
            {
                _roleRepository.Remove(role);

                return true;
            }

            return false;
        }

        public IEnumerable<User> FindUsersInRole(Board board, string roleName)
        {
            var role = FindRoleByName(roleName);

            var userRolesInBoard = _userRoleRepository.GetCollection().ToList().FindAll(e => e.Board == board);
            var userRoles = userRolesInBoard.FindAll(e => e.Role == role);
            return userRoles.Select(e => e.User);
        }

        public IEnumerable<string> GetAllRoles()
        {
            var roles = _roleRepository.GetCollection();

            return roles.Select(e => e.Name);
        }

        public IEnumerable<UserRole> GetUserRolesForUser(User user)
        {
            var userRoles = _userRoleRepository.GetCollection().ToList().FindAll(e => e.User == user);

            return userRoles;
        }

        public UserRole GetRoleForUser(Board board, User user)
        {
            var userRoles = _userRoleRepository.GetCollection().ToList().FindAll(e => e.User == user);
            var role = userRoles.Find(e => e.Board == board);

            return role;
        }

        public bool IsUserInRole(Board board, string email, string roleName)
        {
            var users = FindUsersInRole(board, roleName);

            var user = users.First(e => e.Email.Equals(email));

            return user == null ? false : true;
        }

        public void RemoveUserFromRole(Board board, User user, string roleName)
        {
            var role = GetRoleForUser(board, user);

            ArgumentValidation.ThrowIfNull(role, argumentName: "role for user in board");

            _userRoleRepository.Remove(role);
            _unitOfWork.Save();
        }

        public bool RoleExists(string roleName)
        {
            var role = FindRoleByName(roleName);

            return role == null ? false : true;
        }

        public Role FindRoleByName(string roleName)
        {
            ArgumentValidation.ThrowIfNullOrWhiteSpace(roleName);

            var roles = _roleRepository.GetCollection();

            return roles.First(e => e.Name.Equals(roleName));
        }
    }
}