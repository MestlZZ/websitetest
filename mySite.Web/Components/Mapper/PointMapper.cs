using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mySite.DomainModel.Entities;

namespace mySite.Web.Components.Mapper
{
    public class PointMapper : EntityModelMapper<Point>
    {
        private readonly IEntityMapper _mapper;

        public PointMapper(IEntityMapper mapper)
        {
            _mapper = mapper;
        }

        public override dynamic Map(Point entity)
        {
            return new
            {
                id = entity.Id,
                title = entity.Title,
                marks = entity.Marks.Select(e => _mapper.Map(e))
            };
        }
    }
}