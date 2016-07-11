using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyPlan.DomainModel.Repositories;
using EasyPlan.DomainModel.Entities;

namespace EasyPlan.Web.Components.Mapper
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
                items = entity.Items.Select(e => _mapper.Map(e)),
                criterion = entity.Criterions.Select(e => _mapper.Map(e))
            };
        }
    }
}