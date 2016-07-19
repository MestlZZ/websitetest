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
    public class BoardController : DefaultController
    {
        private readonly IBoardRepository _boardRepository;

        public BoardController(IBoardRepository boardRepository)
        {
            _boardRepository = boardRepository;
        }

        [HttpPost]
        public ActionResult GetBoardData(Board board)
        {
            return JsonSuccess(BoardMapper.Map(board));
        }

        [HttpPost]
        public ActionResult GetBoardsInfo()
        {
            var tmp = _boardRepository.GetCollection();
            var boards = tmp.Select(e => BoardMapper.MapToShortInfo(e));

            return JsonSuccess(boards);
        }
    }
}