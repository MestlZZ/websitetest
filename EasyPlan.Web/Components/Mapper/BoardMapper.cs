using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyPlan.DomainModel.Repositories;
using EasyPlan.DomainModel.Entities;

namespace EasyPlan.Web.Components.Mapper
{
    public class BoardMapper
    {
        public static object Map(Board entity)
        {
            return new
            {
                id = entity.Id,
                title = entity.Title,
                items = entity.Items.Select(e => ItemMapper.Map(e)),
                criterions = entity.Criterions.Select(e => CriterionMapper.Map(e))
            };
        }

        public static object MapToShortInfo(Board entity)
        {
            return new
            {
                id = entity.Id,
                title = entity.Title
            };
        }
    }
}