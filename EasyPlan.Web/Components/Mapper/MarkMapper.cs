using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyPlan.DomainModel.Entities;

namespace EasyPlan.Web.Components.Mapper
{
    public class MarkMapper : EntityModelMapper<Mark>
    {
        public override dynamic Map(Mark entity)
        {
            return new
            {
                Id = entity.Id,
                Value = entity.Value
            };
        }
    }
}