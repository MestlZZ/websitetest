using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mySite.DomainModel.Entities;

namespace mySite.Web.Components.Mapper
{
    public class MarkMapper : EntityModelMapper<Mark>
    {
        public override dynamic Map(Mark entity)
        {
            return new
            {
                id = entity.Id,
                value = entity.Value
                /*criterionId = entity.CriterionId,
                pointId = entity.PointId*/
            };
        }
    }
}