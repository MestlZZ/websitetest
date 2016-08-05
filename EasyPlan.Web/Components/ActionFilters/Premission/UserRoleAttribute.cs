using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyPlan.DomainModel.Repositories;
using EasyPlan.DomainModel.Entities;
using EasyPlan.Infrastructure;
using System.Web.Security;

namespace EasyPlan.Web.Components.ActionFilters.Premission
{
    public class UserRoleAttribute : AuthorizeAttribute, IAuthorizationFilter
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
        protected Guid BoardId { get; set; }
        protected bool Result { get; set; }

        public UserRoleAttribute(params RoleName[] role)
        {
            SelectedRoles = role;
        }

        protected override bool AuthorizeCore(HttpContextBase filterContext)
        {

            var user = UserRepository.FindUserByEmail(filterContext.User.Identity.Name);

            if (user == null || !CheckAccess(BoardId, user))
            {
                return false;
            }

            return true;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var entityId = filterContext.Controller.ValueProvider.GetValue("boardId");

            if (entityId.AttemptedValue == null)
            {
                throw new InvalidOperationException();
            }

            BoardId = Guid.Parse(entityId.AttemptedValue);

            base.OnAuthorization(filterContext);
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

        /*private Entity GetEntity(Guid id)
        {

            var serviceType = typeof(IQueryableRepository<>).MakeGenericType(typeof(Board));
            var service = DependencyResolver.Current.GetService(serviceType);
            var method = serviceType.GetMethod("Get");

            var callParams = new object[] { id };

            return (Entity)method.Invoke(service, callParams);
        }*/
    }
}