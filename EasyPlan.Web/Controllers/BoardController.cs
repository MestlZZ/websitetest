using System.Linq;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using EasyPlan.DomainModel.Entities;
using EasyPlan.DomainModel.Repositories;
using EasyPlan.Infrastructure;
using EasyPlan.Web.Components.Mapper;
using EasyPlan.Web.Components;

namespace EasyPlan.Web.Controllers
{

    [RoutePrefix("boards")]
    public class BoardController : DefaultController
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
        public ActionResult GetBoardData(Board board)
        {
            return JsonSuccess(BoardMapper.Map(board));
        }

        [HttpPost]
        [Route("get-info")]
        public ActionResult GetBoardsInfo()
        {
            var tmp = _boardRepository.GetCollection();
            var boards = tmp.Select(e => BoardMapper.MapToShortInfo(e));

            return JsonSuccess(boards);
        }

        [HttpPost]
        [Route("item/set-title")]
        public void SetItemTitle(string title, Item item)
        {
            item.SetTitle(title);
        }

        [HttpPost]
        [Route("item/remove")]
        public void RemoveItem(Item item)
        {
            _itemRepository.Remove(item);
        }

        [HttpPost]
        [Route("item/create")]
        public ActionResult CreateItem(Board board)
        {
            var item = new Item("New item", board);

            _itemRepository.Add(item);

            return JsonSuccess(ItemMapper.Map(item));
        }

        [HttpPost]
        [Route("mark/set-value")]
        public void SetMarkValue(int value, Mark mark)
        {
            mark.SetValue(value);
        }

        [HttpPost]
        [Route("mark/create")]
        public ActionResult CreateMark(Item item, Criterion criterion)
        {
            var mark = new Mark(item, criterion, 0);

            _markRepository.Add(mark);

            return JsonSuccess(MarkMapper.Map(mark));
        }

        [HttpPost]
        [Route("criterion/set-weight")]
        public void SetCriterionWeight(int weight, Criterion criterion)
        {
            criterion.SetWeight(weight);
        }

        [HttpPost]
        [Route("criterion/set-title")]
        public void SetCriterionTitle(string title, Criterion criterion)
        {
            criterion.SetTitle(title);
        }

        [HttpPost]
        [Route("criterion/remove")]
        public void RemoveCriterion(Criterion criterion)
        {
            _criterionRepository.Remove(criterion);
        }

        [HttpPost]
        [Route("criterion/create")]
        public ActionResult CreateCriterion(bool isBenefit, Board board)
        {
            var criterion = new Criterion(board, isBenefit);

            _criterionRepository.Add(criterion);

            return JsonSuccess(CriterionMapper.Map(criterion));
        }
    }
}