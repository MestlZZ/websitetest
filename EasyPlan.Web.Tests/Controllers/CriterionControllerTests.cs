using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using EasyPlan.Web.Controllers;
using Moq;
using System.Linq;
using EasyPlan.DomainModel.Repositories;
using EasyPlan.DomainModel.Entities;
using System.Web.Mvc;

namespace EasyPlan.Web.Tests
{
    [TestClass]
    public class CriterionControllerTests
    {
        private CriterionController _controller;
        private ICriterionRepository _criterionRepository;

        [TestInitialize]
        public void Initialize()
        {
            _criterionRepository = Substitute.For<ICriterionRepository>();

            _controller = new CriterionController(_criterionRepository);
        }

        [TestMethod]
        public void CriterionController_SetCriterionWeight()
        {
            //arrange
            var criterion = Substitute.For<Criterion>();

            //act
            _controller.SetCriterionWeight(15, criterion);

            //assert
            criterion.Received().SetWeight(15);        
        }

        [TestMethod]
        public void CriterionController_SetCriterionTitle()
        {
            //arrange
            var criterion = Substitute.For<Criterion>();

            //act
            _controller.SetCriterionTitle("my title", criterion);

            //assert
            criterion.Received().SetTitle("my title");
        }

        [TestMethod]
        public void CriterionController_RemoveCriterion()
        {
            //arrange
            var criterion = Substitute.For<Criterion>();

            //act
            _controller.RemoveCriterion(criterion);

            //assert
            _criterionRepository.Received().Remove(Arg.Any<Criterion>());
        }

        [TestMethod]
        public void CriterionController_CreateCriterion()
        {
            //arrange
            var board = Substitute.For<Board>();

            //act
            object jsonResult = _controller.CreateCriterion(true, board);

            //assert
            _criterionRepository.Received().Add(Arg.Any<Criterion>());
            Assert.IsTrue(jsonResult is JsonResult);
        }
    }
}
