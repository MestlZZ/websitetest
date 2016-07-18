﻿using System;
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
                Id = entity.Id,
                Title = entity.Title,
                Weight = entity.Weight,
                IsBenefit = entity.IsBenefit
            };
        }
    }
}