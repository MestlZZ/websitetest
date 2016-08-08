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
                name = entity.FullName,
                email = entity.Email,
                boardsShortInfo = entity.Roles.Select(e => new { board = BoardMapper.MapToShortInfo(e.Board), clientRole = e.Name })
            };
        }

        public static object MapToShortInfo(User entity)
        {
            return new
            {
                name = entity.FullName,
                email = entity.Email,
            };
        }
    }
}