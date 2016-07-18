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
                Id = entity.Id,
                Title = entity.Title,
                Items = entity.Items.Select(e => ItemMapper.Map(e)),
                Criterions = entity.Criterions.Select(e => CriterionMapper.Map(e))
            };
        }

        public static object MapToShortInfo(Board entity)
        {
            return new
            {
                Id = entity.Id,
                Title = entity.Title
            };
        }
    }
}