using System.Linq;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using EasyPlan.DomainModel.Entities;
using EasyPlan.DomainModel.Repositories;
using EasyPlan.Infrastructure;
using EasyPlan.Web.Components.Mapper;

namespace EasyPlan.Web.Controllers
{

    [RoutePrefix("boards")]
    public class BoardController : Controller
    {
        private readonly IBoardRepository _boardRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityMapper _mapper;
        private readonly IItemRepository _itemRepository;

        public BoardController(IBoardRepository boardRepository, IUnitOfWork unitOfWork,
            IEntityMapper mapper, IItemRepository itemRepository)
        {
            _boardRepository = boardRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _itemRepository = itemRepository;
        }

        [HttpPost]
        [Route("get-data")]
        public JsonResult GetData()
        {
            var tmp = _boardRepository.GetCollection();
            var boards = tmp.Select(board => _mapper.Map(board));

            return Json(boards, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("item/set-title")]
        public void SetTitle(string title, string id)
        {
            _itemRepository.SetTitle(title, id);
            _unitOfWork.Save();
        }

        [HttpPost]
        [Route("item/remove")]
        public void Remove(string id)
        {
            _itemRepository.Remove(_itemRepository.Get(Guid.Parse(id)));
            _unitOfWork.Save();
        }
    }
}