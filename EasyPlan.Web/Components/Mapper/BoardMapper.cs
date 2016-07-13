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
        public static dynamic Map(Board entity)
        {
            return new
            {
                Id = entity.Id,
                Title = entity.Title,
                Items = entity.Items.Select(e => ItemMapper.Map(e))
            };
        }

        public static dynamic MapToView(Board entity)
        {
            return new
            {
                Id = entity.Id,
                Title = entity.Title
            };
        }
    }
}