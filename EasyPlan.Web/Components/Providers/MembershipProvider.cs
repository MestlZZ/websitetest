using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyPlan.DomainModel.Entities;
using EasyPlan.DomainModel.Repositories;
using EasyPlan.Infrastructure;

namespace EasyPlan.Web.Components.Providers
{
    public class MembershipProvider : IMembershipProvider
    {      
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MembershipProvider(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public bool ChangePassword(Guid id, string oldPassword, string newPassword)
        {
            var user = _userRepository.Get(id);

            if(ValidateUser(user, oldPassword))
            {
                user.SetPassword(newPassword);
                _unitOfWork.Save();

                return true;
            }

            return false;
        }

        public User CreateUser(string email, string password, string fullName)
        {
            var existUser = FindUserByEmail(email);

            if(existUser == null)
            {
                var user = new User(fullName, email, password);

                _userRepository.Add(user);
                _unitOfWork.Save();
            
                return user;
            }

            return null; 
        }

        public bool DeleteUser(Guid id)
        {
            ArgumentValidation.ThrowIfNull(id, argumentName: "user id");

            var user = _userRepository.Get(id);

            ArgumentValidation.ThrowIfNull(user, argumentName: "user");

            _userRepository.Remove(user);

            return true;
        }

        public User FindUserByEmail(string email)
        {
            ArgumentValidation.ThrowIfNullOrWhiteSpace(email, argumentName: "user email");

            var users = _userRepository.GetCollection();

            return users.FirstOrDefault(e => e.Email.Equals(email));
        }

        public bool ValidateUser(Guid id, string password)
        {
            ArgumentValidation.ThrowIfNull(id, argumentName:"user id");

            var user = _userRepository.Get(id);

            return ValidateUser(user, password);
        }

        public bool ValidateUser(User user, string password)
        {
            ArgumentValidation.ThrowIfNull(user, argumentName: "user");
            ArgumentValidation.ThrowIfNullOrWhiteSpace(password, argumentName: "user password");

            return Cryptography.VerifyHash(password, user.HashPassword);
        }

        public bool ValidateUser(string email, string password)
        {
            ArgumentValidation.ThrowIfNullOrWhiteSpace(password, argumentName: "user password");

            var user = FindUserByEmail(email);

            if (user != null)
            {
                return Cryptography.VerifyHash(password, user.HashPassword);
            }

            return false;
        }
    }
}