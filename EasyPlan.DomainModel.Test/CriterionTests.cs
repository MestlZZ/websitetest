using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyPlan.DomainModel.Entities;

namespace EasyPlan.DomainModel.Test
{
    [TestClass]
    public class CriterionTests
    {
        private Board board = new Board();
        
        [TestMethod]
        public void Criterion_Create_Success_WithoutExtraValues()
        {
            var criterion = new Criterion(board, true);

            Assert.AreEqual(board.Id, criterion.Board.Id, message: "board is invalid");
            Assert.IsTrue(criterion.IsBenefit, message: "isBenefit are not setted");
            Assert.AreEqual(criterion.Weight, 20, message: "default weight are not 20");
            Assert.AreEqual(criterion.Title, "New criteria", "default title isn't default");
        }

        [TestMethod]
        public void Criterion_Create_Success_WithExtraValues()
        {
            //act
            var criterion = new Criterion(board, false, "criteria", 10);

            //assert
            Assert.AreEqual(criterion.Weight, 10, message: "Weight arn't setted");
            Assert.AreEqual(criterion.Title, "criteria", "Title aren't setted");
        }

        [TestMethod]
        public void Criterion_Create_Failed_BoardAreNull()
        {
            //arrange
            bool isCatched = false;

            //act
            try
            {
                var criterion = new Criterion(null, true);
            }
            catch
            {
                isCatched = true;
            }

            //assert
            Assert.IsTrue(isCatched, message: "Exception isn't called");
        }

        [TestMethod]
        public void Criterion_Create_Failed_TitleAreNull()
        {
            //arrange
            bool isCatched = false;


            //act
            try
            {
                var criterion = new Criterion(board, true, null);
            }
            catch
            {
                isCatched = true;
            }
            
            //assert
            Assert.IsTrue(isCatched, message: "Exception isn't called");
        }

        [TestMethod]
        public void Criterion_Create_Failed_TitleAreWhiteSpace()
        {
            //arrange
            bool isCatched = false;

            //act
            try
            {
                var criterion = new Criterion(board, true, "       ");
            }
            catch
            {
                isCatched = true;
            }

            //assert
            Assert.IsTrue(isCatched, message: "Exception isn't called");
        }

        [TestMethod]
        public void Criterion_Create_Failed_TitleAreLongerThan255()
        {
            //arrange
            bool isCatched = false;

            //act
            try
            {
                var criterion = new Criterion(board, true, "asd asd as a sa sad as as sda sa as asasd asd as a sa sad as as sda sa as asasd asd as a sa sad as as sda sa as asasd asd as a sa sad as as sda sa as asasd asd as a sa sad as as sda sa as asasd asd as a sa sad as as sda sa as asasd asd as a sa sad as as sda");
            }
            catch
            {
                isCatched = true;
            }

            //assert
            Assert.IsTrue(isCatched, message: "Exception isn't called");
        }

        [TestMethod]
        public void Criterion_Create_Failed_WeightAreNegative()
        {
            //arrange
            bool isCatched = false;

            //act
            try
            {
                var criterion = new Criterion(board, true, weight: -3);
            }
            catch
            {
                isCatched = true;
            }

            //assert
            Assert.IsTrue(isCatched, message: "Exception isn't called");
        }

        [TestMethod]
        public void Criterion_Create_Failed_WeightAreEqualToZero()
        {
            //arrange
            bool isCatched = false;

            //act
            try
            {
                var criterion = new Criterion(board, true, weight: 0);
            }
            catch
            {
                isCatched = true;
            }

            //assert
            Assert.IsTrue(isCatched, message: "Exception isn't called");
        }

        [TestMethod]
        public void Criterion_Create_Failed_WeightGreaterThan20()
        {
            //arrange
            bool isCatched = false;

            //act
            try
            {
                var criterion = new Criterion(board, true, weight: 21);
            }
            catch
            {
                isCatched = true;
            }

            //assert
            Assert.IsTrue(isCatched, message: "Exception isn't called");
        }

        [TestMethod]
        public void Criterion_SetWeight_Success()
        {
            //arrange
            var criterion = new Criterion(board, true, weight: 3);

            //act
            criterion.SetWeight(13);

            //assert
            Assert.AreEqual(criterion.Weight, 13, message: "Weight isn't setted");
        }

        [TestMethod]
        public void Criterion_SetWeight_Failed_WeightGreaterThan20()
        {
            //arrange
            var criterion = new Criterion(board, true, weight: 5);

            bool isCatched = false;

            //act
            try
            {
                criterion.SetWeight(21);
            }
            catch
            {
                isCatched = true;
            }

            //assert
            Assert.IsTrue(isCatched, message: "Exception isn't called");
        }

        [TestMethod]
        public void Criterion_SetWeight_Failed_WeightEqualTo0()
        {
            //arrange
            var criterion = new Criterion(board, true, weight: 5);

            bool isCatched = false;

            //act
            try
            {
                criterion.SetWeight(0);
            }
            catch
            {
                isCatched = true;
            }

            //assert
            Assert.IsTrue(isCatched, message: "Exception isn't called");
        }

        [TestMethod]
        public void Criterion_SetWeight_Failed_WeightAreNegative()
        {
            //arrange
            var criterion = new Criterion(board, true, weight: 5);

            bool isCatched = false;

            //act
            try
            {
                criterion.SetWeight(-5);
            }
            catch
            {
                isCatched = true;
            }

            //assert
            Assert.IsTrue(isCatched, message: "Exception isn't called");
        }

        [TestMethod]
        public void Criterion_SetTitle_Success()
        {
            //arrange
            var criterion = new Criterion(board, true, weight: 3);

            //act
            criterion.SetTitle("132");

            //assert
            Assert.AreEqual(criterion.Title, "132", message: "Title isn't setted");
        }

        [TestMethod]
        public void Criterion_SetTitle_Failed_TitleAreWhiteSpace()
        {
            //arrange
            var criterion = new Criterion(board, true, weight: 5);

            bool isCatched = false;

            //act
            try
            {
                criterion.SetTitle("     ");
            }
            catch
            {
                isCatched = true;
            }

            //assert
            Assert.IsTrue(isCatched, message: "Exception isn't called");
        }

        [TestMethod]
        public void Criterion_SetTitle_Failed_TitleAreNull()
        {
            //arrange
            var criterion = new Criterion(board, true, weight: 5);

            bool isCatched = false;

            //act
            try
            {
                criterion.SetTitle(null);
            }
            catch
            {
                isCatched = true;
            }
            
            //assert
            Assert.IsTrue(isCatched, message: "Exception isn't called");
        }

        [TestMethod]
        public void Criterion_SetTitle_Failed_TitleAreLongerThan255()
        {
            //arrange
            var criterion = new Criterion(board, true, weight: 5);

            bool isCatched = false;

            //act
            try
            {
                criterion.SetTitle("asd asd as a sa sad as as sda sa as asasd asd as a sa sad as as sda sa as asasd asd as a sa sad as as sda sa as asasd asd as a sa sad as as sda sa as asasd asd as a sa sad as as sda sa as asasd asd as a sa sad as as sda sa as asasd asd as a sa sad as as sda");
            }
            catch
            {
                isCatched = true;
            }

            //assert
            Assert.IsTrue(isCatched, message: "Exception isn't called");
        }
    }
}
