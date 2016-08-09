using System;
using System.Web.Mvc;
using EasyPlan.DomainModel.Repositories;
using EasyPlan.DomainModel.Entities;
using EasyPlan.Infrastructure;

namespace EasyPlan.Web.Components.ActionFilters.Premission
{
    public class UserRoleAttribute : FilterAttribute, IAuthorizationFilter
    {
        public IUserRepository UserRepository
        {
            get
            {
                return DependencyResolver.Current.GetService<IUserRepository>();
            }
        }

        public IBoardRepository BoardRepository
        {
            get
            {
                return DependencyResolver.Current.GetService<IBoardRepository>();
            }
        }

        public RoleName[] SelectedRoles { get; set; }

        public UserRoleAttribute(params RoleName[] role)
        {
            SelectedRoles = role;
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var entityId = filterContext.Controller.ValueProvider.GetValue("boardId");

            if (entityId.AttemptedValue == null)
            {
                throw new InvalidOperationException();
            }

            var user = UserRepository.FindUserByEmail(filterContext.HttpContext.User.Identity.Name);

            var boardId = Guid.Parse(entityId.AttemptedValue);

            if (!CheckAccess(boardId, user))
            {
                throw new ArgumentValidationException("Access denied", statusCode: 403);
            }
        }

        protected bool CheckAccess(Guid boardId, User user)
        { 
            var board = BoardRepository.Get(boardId);

            if (board == null)
                return false;

            foreach(var role in SelectedRoles)
            {
                if(board.GetRole(user).Name == role)
                {
                    return true;
                }
            }

            return false;
        }
    }
}