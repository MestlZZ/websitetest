using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyPlan.DomainModel.Entities;

namespace EasyPlan.Web.Components.Mapper
{
    public static class ItemMapper
    {
        public static object Map(Item entity)
        {
            return new
            {
                id = entity.Id,
                title = entity.Title,
                marks = entity.Marks.Select(e => MarkMapper.Map(e))
            };
        }
    }
}