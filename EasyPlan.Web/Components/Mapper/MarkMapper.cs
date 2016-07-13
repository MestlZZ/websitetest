using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyPlan.DomainModel.Entities;

namespace EasyPlan.Web.Components.Mapper
{
    public class MarkMapper
    {
        public static dynamic Map(Mark entity)
        {
            return new
            {
                Id = entity.Id,
                Value = entity.Value,
                IsBenefit = entity.Criterion.IsBenefit
            };
        }
    }
}