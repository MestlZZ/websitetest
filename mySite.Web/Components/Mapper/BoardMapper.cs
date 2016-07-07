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
        private readonly IPointRepository _pointRepository;
        private readonly IMarkRepository _markRepository;
        private readonly ICriterionRepository _criterionRepository;

        public BoardMapper(IEntityMapper mapper, IPointRepository pointRepository,
            IMarkRepository markRepository, ICriterionRepository criterionRepository)
        {
            _mapper = mapper;
            _pointRepository = pointRepository;
            _markRepository = markRepository;
            _criterionRepository = criterionRepository;
        }

        public override dynamic Map(Board entity)
        {
            /*var points = _pointRepository.GetCollection(p => p.BoardId == entity.Id);
            var criterions = _criterionRepository.GetCollection(c => c.BoardId == entity.Id);
            var marks = _markRepository.GetCollection(m => m.Criterion.BoardId == entity.Id);*/

            /*return new {
                id = entity.Id,
                title = entity.Title,
                points = points.Select(point => _mapper.Map(point)),
                criterion = criterions.Select(criterion => _mapper.Map(criterion)),
                marks = marks.Select(mark => _mapper.Map(mark))
            };*/
            return null;
        }
    }
}