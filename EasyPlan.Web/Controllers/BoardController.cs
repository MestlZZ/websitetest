using System.Linq;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using EasyPlan.DomainModel.Entities;
using EasyPlan.DomainModel.Repositories;
using EasyPlan.Infrastructure;
using EasyPlan.Web.Components.Mapper;
using EasyPlan.Web.Components;
using EasyPlan.Web.Components.Providers;
using EasyPlan.Web.Components.ActionFilters.Premission;


namespace EasyPlan.Web.Controllers
{
    [Authorize]
    public class BoardController : DefaultController
    {
        private readonly IBoardRepository _boardRepository;
        private readonly IUserRepository _userRepository;

        public BoardController(IBoardRepository boardRepository, IUserRepository userRepository)
        {
            _boardRepository = boardRepository;
            _userRepository = userRepository;
        }

        [HttpPost]
        public ActionResult GetBoardData(Board board)
        {
            if(board == null)
                return HttpNotFound();

            var user = _userRepository.FindUserByEmail(HttpContext.User.Identity.Name);

            var role = board.GetRole(user);

            if (role == null)
                return HttpNotFound();

            return JsonSuccess(BoardMapper.Map(board, role.Name));
        }

        [HttpPost]
        [UserRole(RoleName.Admin)]
        public void SetTitle(Board board, string title)
        {
            board.SetTitle(title);
        }

        [HttpPost]
        public ActionResult Create()
        {
            var user = _userRepository.FindUserByEmail(HttpContext.User.Identity.Name);

            var board = new Board(user);

            _boardRepository.Add(board);

            return JsonSuccess(BoardMapper.MapToShortInfo(board, RoleName.Admin));
        }

        [HttpPost]
        public void Remove(Board board)
        {
            var user = _userRepository.FindUserByEmail(HttpContext.User.Identity.Name);

            if (!user.RemoveFromBoard(board)) {
                _boardRepository.Remove(board);
            };
        }

        [HttpPost]
        [UserRole(RoleName.Admin)]
        public ActionResult InviteUser(Board board, User user, int role)
        {
            board.SetRole(user, (RoleName)role);

            return JsonSuccess(RoleMapper.Map(board.GetRole(user)));
        }

        [HttpPost]
        [UserRole(RoleName.Admin, RoleName.Editor)]
        public ActionResult GetBoardUserInfo(Board board)
        {
            var user = _userRepository.FindUserByEmail(User.Identity.Name);

            var role = board.GetRole(user);

            return JsonSuccess(BoardMapper.MapToUsersInfo(board, role.Name, User.Identity.Name));
        }

        [HttpPost]
        [UserRole(RoleName.Admin)]
        public void RemoveUser(Board board, User user)
        {
            board.RemoveUser(user);
        }
    }
}