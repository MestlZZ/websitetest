using System.Linq;
using EasyPlan.DomainModel.Entities;

namespace EasyPlan.Web.Components.Mapper
{
    public static class BoardMapper
    {
        public static object Map(Board entity)
        {
            return new
            {
                id = entity.Id,
                title = entity.Title,
                items = entity.Items.Select(e => ItemMapper.Map(e)),
                criterions = entity.Criterions.Select(e => CriterionMapper.Map(e)),
            };
        }

        public static object MapToShortInfo(Board entity)
        {
            return new
            {
                id = entity.Id,
                title = entity.Title,
                createdOn = entity.CreatedOn.ToString(),
                createdBy = entity.CreatedBy,
            };
        }

        public static object MapToUsersInfo(Board entity)
        {
            return new
            {
                createdOn = entity.CreatedOn.ToString(),
                createdBy = entity.CreatedBy,
                usersInRoles = entity.Rights.Select(e => RoleMapper.Map(e))
            };
        }
    }
}