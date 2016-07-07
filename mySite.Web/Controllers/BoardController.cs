using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using mySite.DomainModel.Entities;
using mySite.DomainModel.Repositories;
using mySite.Infrastructure;
using mySite.Infrastructure.Serialization.Providers;
using mySite.Web.Components.Mapper;

namespace mySite.Web.Controllers
{

    [RoutePrefix("boards")]
    public class BoardController : Controller
    {
        private readonly IBoardRepository _boardRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISerializationProvider<dynamic> _serializationProvider;
        private readonly IEntityMapper _mapper;
        private readonly IPointRepository _pointRepository;
        private readonly ICriterionRepository _criterionRepository;
        private readonly IMarkRepository _markRepository;

        public BoardController(IBoardRepository boardRepository, IUnitOfWork unitOfWork, ISerializationProvider<dynamic> serializationProvider,
            IEntityMapper mapper, IPointRepository pointRepository, IMarkRepository markRepository, ICriterionRepository criterioRepository)
        {
            _boardRepository = boardRepository;
            _unitOfWork = unitOfWork;
            _serializationProvider = serializationProvider;
            _mapper = mapper;
            _pointRepository = pointRepository;
            _markRepository = markRepository;
            _criterionRepository = criterioRepository;
        }

        [HttpPost]
        [Route("get-data")]
        public string GetData()
        {
            /*set data
            var criterias = new List<Criterion>();
            var points = new List<Point>();
            var marks = new List<Mark>();

            criterias.Add(new Criterion() { IsBenefit = true, Title = "criteria1", Width = 20 });
            criterias.Add(new Criterion() { IsBenefit = true, Title = "criteria2", Width = 20 });


            points.Add(new Point() { Title = "point1" });
            points.Add(new Point() { Title = "point2" });


            _boardRepository.Add(new Board() { Title = "First board", Criterions = criterias, Points = points });
            _unitOfWork.Save();

            _markRepository.Add(new Mark() { Value = 3, Criterion = criterias[0], Point = points[0] });
            _markRepository.Add(new Mark() { Value = 2, Criterion = criterias[1], Point = points[0] });
            _markRepository.Add(new Mark() { Value = 2, Criterion = criterias[0], Point = points[1] });
            _markRepository.Add(new Mark() { Value = 5, Criterion = criterias[1], Point = points[1] });

            _unitOfWork.Save();
            /*end*/

            var tmp = _boardRepository.GetCollection();
            var point = tmp.First().Points;
            var board1 = tmp.FirstOrDefault();
            var newBoard = _mapper.Map(board1);

            var boards = tmp.Select(board => _mapper.Map(board));

            return _serializationProvider.Serialize(boards);
        }

        [HttpPost]
        [Route("point/set-title")]
        public void SetTitle(string title, string id)
        {
            _pointRepository.SetTitle(title, id);
            _unitOfWork.Save();
        }
    }
}