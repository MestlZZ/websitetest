using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mySite.DomainModel.Entities;

namespace mySite.Web.Components.Mapper
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
                id = entity.Id,
                title = entity.Title,
                width = entity.Width,
                isBenefit = entity.IsBenefit,
                marks = entity.Marks.Select(e => _mapper.Map(e))
            };
        }
    }
}