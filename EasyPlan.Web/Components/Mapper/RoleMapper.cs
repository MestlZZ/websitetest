using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyPlan.DomainModel.Entities;

namespace EasyPlan.Web.Components.Mapper
{
    public static class RoleMapper
    {
        public static object Map(Right entity)
        {
            return new
            {
                user = UserMapper.MapToShortInfo(entity.User),
                accessLevel = entity.Name
            };
        }
    }
}