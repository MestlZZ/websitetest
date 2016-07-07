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
            var tmp = _boardRepository.GetCollection();
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