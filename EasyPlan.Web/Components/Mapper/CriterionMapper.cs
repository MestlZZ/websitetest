﻿using EasyPlan.DomainModel.Entities;

namespace EasyPlan.Web.Components.Mapper
{
    public static class CriterionMapper
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