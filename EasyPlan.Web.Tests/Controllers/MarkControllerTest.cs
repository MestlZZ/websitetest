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
    public class MarkControllerTest
    {
        private MarkController _controller;
        private IMarkRepository _repository;

        [TestInitialize]
        public void Initialize()
        {
            _repository = Substitute.For<IMarkRepository>();

            _controller = new MarkController(_repository);
        }

        [TestMethod]
        public void MarkController_SetMarkValue()
        {
            //arrange
            var mark = Substitute.For<Mark>();

            //act
            _controller.SetMarkValue(3, mark);

            //assert
            mark.Received().SetValue(3);
        }

        [TestMethod]
        public void MarkController_CreateMark()
        {
            //arrange
            var item = Substitute.For<Item>();
            var criterion = Substitute.For<Criterion>();

            //act
            object jsonResult = _controller.CreateMark(item, criterion);

            //assert
            _repository.Received().Add(Arg.Any<Mark>());
            Assert.IsTrue(jsonResult is JsonResult);
        }
    }
}
