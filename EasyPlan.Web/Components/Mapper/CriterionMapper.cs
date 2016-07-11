using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyPlan.DomainModel.Entities;

namespace EasyPlan.Web.Components.Mapper
{
    public class CriterionMapper : EntityModelMapper<Criterion>
    {
        private readonly IEntityMapper _mapper;

        public CriterionMapper(IEntityMapper mapper)
        {
            _mapper = mapper;
        }

        public override dynamic Map(Criterion entity)
        {
            return new
            {
                Id = entity.Id,
                Title = entity.Title,
                Weight = entity.Weight,
                IsBenefit = entity.IsBenefit,
                Marks = entity.Marks.Select(e => _mapper.Map(e))
            };
        }
    }
}