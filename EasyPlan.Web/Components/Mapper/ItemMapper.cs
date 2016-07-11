using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyPlan.DomainModel.Entities;

namespace EasyPlan.Web.Components.Mapper
{
    public class ItemMapper : EntityModelMapper<Item>
    {
        private readonly IEntityMapper _mapper;

        public ItemMapper(IEntityMapper mapper)
        {
            _mapper = mapper;
        }

        public override dynamic Map(Item entity)
        {
            return new
            {
                Id = entity.Id,
                Title = entity.Title,
                Marks = entity.Marks.Select(e => _mapper.Map(e))
            };
        }
    }
}