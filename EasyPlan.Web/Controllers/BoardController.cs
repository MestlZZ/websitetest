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
        private readonly IMarkRepository _markRepository;

        public BoardController(IBoardRepository boardRepository, IUnitOfWork unitOfWork,
            IEntityMapper mapper, IItemRepository itemRepository, IMarkRepository markRepository)
        {
            _boardRepository = boardRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _itemRepository = itemRepository;
            _markRepository = markRepository;
        }

        [HttpPost]
        [Route("get-data")]
        public JsonResult GetData()
        {
            /*set data
            var items = new List<Item>();
            var criterions = new List<Criterion>();
            var marks = new List<Mark>();

            items.Add(new Item() { Title = "First item" });
            items.Add(new Item() { Title = "Second item" });
            items.Add(new Item() { Title = "Third item" });

            criterions.Add(new Criterion() { Title = "First criterion", Width = 1, IsBenefit = true });

            _markRepository.Add(new Mark() { Value = 1, Criterion = criterions[0], Item = items[0] });
            _markRepository.Add(new Mark() { Value = 4, Criterion = criterions[0], Item = items[1] });
            _markRepository.Add(new Mark() { Value = 3, Criterion = criterions[0], Item = items[2] });

            _boardRepository.Add(new Board() { Criterions = criterions, Items = items, Title = "First board!" });
                        
            _unitOfWork.Save();
            /*end*/

            var tmp = _boardRepository.GetCollection();
            var boards = tmp.Select(board => _mapper.Map(board));

            return Json(boards, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("item/set-title")]
        public void SetItemTitle(string title, string id)
        {
            _itemRepository.SetTitle(title, id);
            _unitOfWork.Save();
        }

        [HttpPost]
        [Route("item/remove")]
        public void RemoveItem(string id)
        {
            _itemRepository.Remove(_itemRepository.Get(Guid.Parse(id)));
            _unitOfWork.Save();
        }

        [HttpPost]
        [Route("item/create")]
        public JsonResult CreateItem(string id)
        {
            var board = _boardRepository.Get(Guid.Parse(id));
            var criterions = board.Criterions;
            var marks = new List<Mark>();
            var item = new Item() { Title = "New item" };

            foreach(var criteria in criterions)
            {
                _markRepository.Add(new Mark() { Item = item, Criterion = criteria, Value = 0 });
            }

            board.Items.Add(item);

            _unitOfWork.Save();

            return Json(_mapper.Map(_itemRepository.Get(item.Id)), JsonRequestBehavior.AllowGet);
        }
    }
}