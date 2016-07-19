using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using EasyPlan.Web.Controllers;
using System.Linq;
using EasyPlan.DomainModel.Repositories;
using EasyPlan.DomainModel.Entities;
using System.Web.Mvc;

namespace EasyPlan.Web.Tests.Controllers
{
    [TestClass]
    public class ItemControllerTest
    {
        private ItemController _controller;
        private IItemRepository _repository;

        [TestInitialize]
        public void Initialize()
        {
            _repository = Substitute.For<IItemRepository>();

            _controller = new ItemController(_repository);
        }

        [TestMethod]
        public void ItemController_SetItemTitle()
        {
            //arrange
            var item = Substitute.For<Item>();

            //act
            _controller.SetItemTitle("some title", item);

            //assert
            item.Received().SetTitle("some title");
        }

        [TestMethod]
        public void ItemController_RemoveItem()
        {
            //arrange
            var item = Substitute.For<Item>();

            //act
            _controller.RemoveItem(item);

            //assert
            _repository.Received().Remove(item);
        }

        [TestMethod]
        public void ItemController_CreateItem()
        {
            //arrange
            var board = Substitute.For<Board>();

            //act
            object jsonResult = _controller.CreateItem(board);

            //assert
            _repository.Received().Add(Arg.Any<Item>());
            Assert.IsTrue(jsonResult is JsonResult);
        }
    }
}
