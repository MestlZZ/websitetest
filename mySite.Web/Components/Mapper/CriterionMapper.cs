using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mySite.DomainModel.Entities;

namespace mySite.Web.Components.Mapper
{
    public class CriterionMapper : EntityModelMapper<Criterion>
    {
        public override dynamic Map(Criterion entity)
        {
            return new
            {
                id = entity.Id,
                title = entity.Title,
                width = entity.Width,
                isBenefit = entity.IsBenefit
            };
        }
    }
}