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
        private readonly IItemRepository _itemRepository;
        private readonly IMarkRepository _markRepository;
        private readonly ICriterionRepository _criterionRepository;

        public BoardController(IBoardRepository boardRepository, IUnitOfWork unitOfWork,
            IItemRepository itemRepository, IMarkRepository markRepository,
            ICriterionRepository criterionRepository)
        {
            _boardRepository = boardRepository;
            _unitOfWork = unitOfWork;
            _itemRepository = itemRepository;
            _markRepository = markRepository;
            _criterionRepository = criterionRepository;
        }

        [HttpPost]
        [Route("get-data")]
        public JsonResult GetBoardData(string id)
        {
            ArgumentValidation.ThrowIfNullOrEmpty(id, "board id");

            var tmp = _boardRepository.Get(Guid.Parse(id));
            var boards = BoardMapper.Map(tmp);

            return Json(boards, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("get-info")]
        public JsonResult GetBoardsInfo()
        {
            var tmp = _boardRepository.GetCollection();
            var boards = tmp.Select(e => BoardMapper.MapToView(e));

            return Json(boards, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("item/set-title")]
        public void SetItemTitle(string title, string id)
        {
            ArgumentValidation.ThrowIfNullOrEmpty(id, "item id");

            var item = _itemRepository.Get(Guid.Parse(id));

            item.SetTitle(title);
        }

        [HttpPost]
        [Route("item/remove")]
        public void RemoveItem(string id)
        {
            ArgumentValidation.ThrowIfNullOrEmpty(id, "item id");

            _itemRepository.Remove(_itemRepository.Get(Guid.Parse(id)));
        }

        [HttpPost]
        [Route("item/create")]
        public JsonResult CreateItem(string id)
        {
            ArgumentValidation.ThrowIfNullOrEmpty(id, "board id");

            var board = _boardRepository.Get(Guid.Parse(id));

            var itemId = _itemRepository.CreateItem("New item", board);

            _unitOfWork.Save();

            return Json(ItemMapper.Map(_itemRepository.Get(itemId)), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("mark/set-value")]
        public void SetMarkValue(int value, string id)
        {
            ArgumentValidation.ThrowIfNullOrEmpty(id, "mark id");

            var mark = _markRepository.Get(Guid.Parse(id));

            mark.SetValue(value);
        }
    }
}