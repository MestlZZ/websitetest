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
                Id = entity.Id,
                Title = entity.Title,
                Items = entity.Items.Select(e => _mapper.Map(e)),
                Criterions = entity.Criterions.Select(e => _mapper.Map(e))
            };
        }
    }
}