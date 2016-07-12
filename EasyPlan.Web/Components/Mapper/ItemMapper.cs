using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyPlan.DomainModel.Entities;

namespace EasyPlan.Web.Components.Mapper
{
    public class ItemMapper
    {
        public static dynamic Map(Item entity)
        {
            return new
            {
                Id = entity.Id,
                Title = entity.Title,
                Marks = entity.Marks.Select(e => MarkMapper.Map(e))
            };
        }
    }
}