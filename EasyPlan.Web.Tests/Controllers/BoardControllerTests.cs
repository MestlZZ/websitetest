using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using EasyPlan.Web.Controllers;
using System.Linq;
using EasyPlan.DomainModel.Repositories;
using EasyPlan.DomainModel.Entities;

namespace EasyPlan.Web.Tests
{
    [TestClass]
    public class BoardControllerTests
    { 
        private BoardController _controller;
        private IBoardRepository _boardRepository;

        [TestInitialize]
        public void Initialize()
        {
            _boardRepository = Substitute.For<IBoardRepository>();

            _controller = new BoardController(_boardRepository);
        }

        [TestMethod]
        public void BoardController_GetBoardsInfo()
        {
            //arrange
            var collection = new List<Board>();
            collection.Add(Substitute.For<Board>());
            collection.Add(Substitute.For<Board>());

            _boardRepository.GetCollection().Returns(collection);

            //act
            dynamic jsonResult = _controller.GetBoardsInfo();

            IEnumerable<dynamic> enumerble = jsonResult.Data;

            //assert
            Assert.AreEqual(enumerble.Count(), 2, message: "Array length isn't 2");
        }
    }
}
