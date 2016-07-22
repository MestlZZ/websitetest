using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyPlan.DomainModel.Entities;

namespace EasyPlan.Web.Components.Mapper
{
    public static class UserMapper
    {
        public static object Map(User entity)
        {
            return new
            {
                id = entity.Id,
                name = entity.Email,
                boardsShortInfo = entity.UserRoles.Select(e => BoardMapper.MapToShortInfo(e.Board))
            };
        }
    }
}