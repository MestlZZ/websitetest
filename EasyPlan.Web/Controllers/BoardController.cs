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
    [Authorize]
    public class BoardController : DefaultController
    {
        private readonly IBoardRepository _boardRepository;
        private readonly IMembershipProvider _membershipProvider;
        private readonly IRoleProvider _roleProvider;

        public BoardController(IBoardRepository boardRepository, IMembershipProvider membershipProvider, IRoleProvider roleProvider)
        {
            _boardRepository = boardRepository;
            _membershipProvider = membershipProvider;
            _roleProvider = roleProvider;
        }

        [HttpPost]
        public ActionResult GetBoardData(Board board)
        {
            var user = _membershipProvider.FindUserByEmail(HttpContext.User.Identity.Name);

            var role = _roleProvider.GetRoleForUser(board, user);

            if (role == null)
                return HttpNotFound();

            return JsonSuccess(BoardMapper.Map(board));
        }

        [HttpPost]
        public void SetTitle(Board board, string title)
        {
            board.SetTitle(title);
        }

        [HttpPost]
        public ActionResult Create()
        {
            var user = _membershipProvider.FindUserByEmail(HttpContext.User.Identity.Name);

            var board = new Board(user);

            _boardRepository.Add(board);

            return JsonSuccess(BoardMapper.MapToShortInfo(board));
        }

        [HttpPost]
        public void Remove(Board board)
        {
            _boardRepository.Remove(board);
        }
    }
}