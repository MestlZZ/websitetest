using System;
using EasyPlan.DomainModel.Repositories;
using EasyPlan.DataAccess.Repositories;
using EasyPlan.DomainModel.Entities;
using EasyPlan.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPlan.DataAccess.Test
{
    [TestClass]
    public class BoardRepositoryTest
    {
        private const string DefaultTitle = "Board";
        private IBoardRepository _boardRepository;
        private IUnitOfWork _unitOfWork;
        private Guid id;
        
        [TestInitialize]
        public void testInit()
        {
            _boardRepository = new BoardRepository(new DatabaseContext());
            _unitOfWork = unitOfWork;
        }

        [TestMethod]
        public void AddBoard()
        {
            var b = new Board() { Title = DefaultTitle };
            _boardRepository.Add(b);
            _unitOfWork.Save();

            var board = _boardRepository.Get(b.Id);

            id = b.Id;

            Assert.AreEqual(b.Title, board.Title);
        }

        [TestMethod]
        public void GetCollection()
        {
            var boards = _boardRepository.GetCollection();
            var board = _boardRepository.Get(id);

            Assert.IsTrue(boards.Contains(board));

            _boardRepository.Remove(board);
            _unitOfWork.Save();

            boards = _boardRepository.GetCollection();

            Assert.IsFalse(boards.Contains(board));
        }
    }
}
