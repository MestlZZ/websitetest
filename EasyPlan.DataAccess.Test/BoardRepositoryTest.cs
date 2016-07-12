using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyPlan.DomainModel.Repositories;
using EasyPlan.DomainModel.Entities;
using EasyPlan.DataAccess.Repositories;
using EasyPlan.Infrastructure;

namespace EasyPlan.DataAccess.Test
{
    [TestClass]
    public class BoardRepositoryTest
    {
        private IBoardRepository _boardRepository;
        private IUnitOfWork _unitOfWork;
        private Board DefaultBoard = new Board() { Title = "BoardTest" };

        [TestInitialize]
        public void SetupContext()
        {
            var context = new DatabaseContext();

            _unitOfWork = context;
            _boardRepository = new BoardRepository(context);
        }

        [TestMethod]
        public void GetCollection()
        {
            var boards = _boardRepository.GetCollection();

            Assert.IsNotNull(boards);
        }

        [TestMethod]
        public void AddRemoveBoard()
        {
            _boardRepository.Add(DefaultBoard);
            _unitOfWork.Save();

            var board = _boardRepository.Get(DefaultBoard.Id);

            Assert.IsNotNull(board, "board is't added to database");
            Assert.AreEqual(board.Title, DefaultBoard.Title, "board is invalid");

            _boardRepository.Remove(board);
            _unitOfWork.Save();

            board = _boardRepository.Get(DefaultBoard.Id);

            Assert.IsNull(board, "board don't removed from database");
        }        
    }
}
