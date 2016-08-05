using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyPlan.DomainModel.Repositories;
using System.Web.Security;
using EasyPlan.DomainModel.Entities;
using EasyPlan.Infrastructure;

namespace EasyPlan.Web.Components.Mapper
{
    public class BoardMapper
    {
        public static object Map(Board entity, RoleName userRole)
        {
            return new
            {
                id = entity.Id,
                title = entity.Title,
                items = entity.Items.Select(e => ItemMapper.Map(e)),
                criterions = entity.Criterions.Select(e => CriterionMapper.Map(e)),
                userRole = userRole
            };
        }

        public static object MapToShortInfo(Board entity, RoleName userRole)
        {
            return new
            {
                id = entity.Id,
                title = entity.Title,
                createdOn = entity.CreatedOn.ToString(),
                createdBy = entity.CreatedBy,
                userRole = userRole
            };
        }

        public static object MapToUsersInfo(Board entity, RoleName userRole, string email)
        {
            return new
            {
                createdOn = entity.CreatedOn.ToString(),
                createdBy = entity.CreatedBy,
                userRole = userRole,
                email = email,
                usersInRoles = entity.Roles.Select(e => RoleMapper.Map(e))
            };
        }
    }
}