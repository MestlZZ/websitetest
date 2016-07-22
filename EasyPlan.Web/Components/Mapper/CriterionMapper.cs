using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyPlan.DomainModel.Entities;

namespace EasyPlan.Web.Components.Mapper
{
    public class CriterionMapper
    {
        public static object Map(Criterion entity)
        {
            return new
            {
                id = entity.Id,
                title = entity.Title,
                weight = entity.Weight,
                isBenefit = entity.IsBenefit
            };
        }
    }
}