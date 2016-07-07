using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mySite.DomainModel.Repositories;
using mySite.DomainModel.Entities;

namespace mySite.Web.Components.Mapper
{
    public class BoardMapper : EntityModelMapper<Board>
    {
        private readonly IEntityMapper _mapper;

        public BoardMapper(IEntityMapper mapper)
        {
            _mapper = mapper;
        }

        public override dynamic Map(Board entity)
        {
            return new {
                id = entity.Id,
                title = entity.Title,
                points = entity.Points.Select(point => _mapper.Map(point)),
                criterion = entity.Criterions.Select(criterion => _mapper.Map(criterion))
            };
        }
    }
}