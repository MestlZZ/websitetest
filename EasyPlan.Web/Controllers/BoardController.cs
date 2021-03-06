﻿using System.Web.Mvc;
using EasyPlan.DomainModel.Entities;
using EasyPlan.DomainModel.Repositories;
using EasyPlan.Infrastructure;
using EasyPlan.Web.Components.Mapper;
using EasyPlan.Web.Components;
using EasyPlan.Web.Components.ActionFilters.Premission;
using EasyPlan.DomainModel.ObjectMothers;


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
        [UserRole(RoleName.Admin, RoleName.Editor, RoleName.Viewer)]
        [ValidateAntiForgeryToken]
        public ActionResult GetBoardData(Board board)
        {
            var user = _userRepository.FindUserByEmail(HttpContext.User.Identity.Name);

            var role = board.GetRole(user);

            return JsonSuccess(new { board = BoardMapper.Map(board), clientRole = role.Name });
        }

        [HttpPost]
        [UserRole(RoleName.Admin, RoleName.Editor, RoleName.Viewer)]
        [ValidateAntiForgeryToken]
        public ActionResult GetBoardShortData(Board board)
        {
            var user = _userRepository.FindUserByEmail(HttpContext.User.Identity.Name);

            var role = board.GetRole(user);

            return JsonSuccess(new { board = BoardMapper.MapToShortInfo(board), clientRole = role.Name });
        }        

       [HttpPost]
        [ValidateAntiForgeryToken]
        [UserRole(RoleName.Admin)]
        public void SetTitle(Board board, string title)
        {
            board.SetTitle(title);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create()
        {
            var user = _userRepository.FindUserByEmail(HttpContext.User.Identity.Name);

            var board = BoardObjectMother.Create(user);

            _boardRepository.Add(board);

            return JsonSuccess(new { board = BoardMapper.MapToShortInfo(board), clientRole = RoleName.Admin });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void Remove(Board board)
        {
            var user = _userRepository.FindUserByEmail(HttpContext.User.Identity.Name);

            if (!user.RemoveFromBoard(board)) {
                _boardRepository.Remove(board);
            };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserRole(RoleName.Admin)]
        public ActionResult InviteUser(Board board, User user, int role)
        {
            if(board.CreatedBy == user.Email)
            {
                throw new ArgumentValidationException("You can't change board creator role", statusCode: 403);
            }
            board.SetRole(user, (RoleName)role);

            return JsonSuccess(RoleMapper.Map(board.GetRole(user)));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserRole(RoleName.Admin, RoleName.Editor, RoleName.Viewer)]
        public ActionResult GetBoardUserInfo(Board board)
        {
            var user = _userRepository.FindUserByEmail(User.Identity.Name);

            var role = board.GetRole(user);

            return JsonSuccess( new { board = BoardMapper.MapToUsersInfo(board), clientRole = role.Name, clientEmail = User.Identity.Name });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserRole(RoleName.Admin)]
        public void RemoveUser(Board board, User user)
        {
            board.RemoveUser(user);
        }
    }
}