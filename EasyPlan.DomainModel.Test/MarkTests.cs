using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyPlan.DomainModel.Entities;

namespace EasyPlan.DomainModel.Test
{
    [TestClass]
    public class MarkTests
    {
        private Board board = new Board();
        private Item item;
        private Criterion criterion;

        [TestInitialize]
        public void SetUpTests()
        {
            item = new Item("item", board);
            criterion = new Criterion(board, true);
        }

        [TestMethod]
        public void Mark_Create_Success_UsedEntities()
        {
            //act
            var mark = new Mark(item, criterion, 4);

            //assert
            Assert.AreEqual(mark.Value, 4, message: "Value is unsetted");
            Assert.AreEqual(mark.Item.Id, item.Id, message: "Item is invalid");
            Assert.AreEqual(mark.Criterion.Id, criterion.Id, message: "Criterion is invalid");
        }

        [TestMethod]
        public void Mark_Create_Success_UsedEntitiesId()
        {
            //act
            var mark = new Mark(item.Id, criterion.Id, 4);

            //assert
            Assert.AreEqual(mark.Value, 4, message: "Value is unsetted");
            Assert.AreEqual(mark.ItemId, item.Id, message: "Item id is invalid");
            Assert.AreEqual(mark.CriterionId, criterion.Id, message: "Criterion id is invalid");
        }

        [TestMethod]
        public void Mark_Create_Failed_ItemIsNull()
        {
            //arrange
            bool isCatched = false;

            //act
            try
            {
                var mark = new Mark(null, criterion, 4);
            }
            catch
            {
                isCatched = true;
            }

            //assert
            Assert.IsTrue(isCatched, message: "Exception isn't called");
        }

        [TestMethod]
        public void Mark_Create_Failed_CriterionIsNull()
        {
            //arrage
            bool isCatched = false;

            //act
            try
            {
                var mark = new Mark(item, null, 4);
            }
            catch
            {
                isCatched = true;
            }

            //assert
            Assert.IsTrue(isCatched, message: "Exception isn't called");
        }

        [TestMethod]
        public void Mark_Create_Failed_ValueIsNegative()
        {
            //arrange
            bool isCatched = false;

            //act
            try
            {
                var mark = new Mark(item, criterion, -2);
            }
            catch
            {
                isCatched = true;
            }

            //assert
            Assert.IsTrue(isCatched, message: "Exception isn't called");
        }

        [TestMethod]
        public void Mark_Create_Failed_ValueIsMoreThan5()
        {
            //arrange
            bool isCatched = false;

            //act
            try
            {
                var mark = new Mark(item, criterion, 7);
            }
            catch
            {
                isCatched = true;
            }

            //assert
            Assert.IsTrue(isCatched, message: "Exception isn't called");
        }

        [TestMethod]
        public void Mark_SetValue_Success()
        {           
            //arrange 
            var mark = new Mark(item, criterion, 3);

            //act
            mark.SetValue(5);

            //assert
            Assert.AreEqual(mark.Value, 5, message: "value isn't setted");
        }

        [TestMethod]
        public void Mark_SetValue_Invalid_ValueIsNegative()
        {
            //arrange
            var mark = new Mark(item, criterion, 3);

            bool isCatched = false;

            //act
            try
            {
                mark.SetValue(-3);
            }
            catch
            {
                isCatched = true;
            }

            //assert
            Assert.IsTrue(isCatched, message: "Exception isn't called");
        }

        [TestMethod]
        public void Mark_SetValue_Invalid_ValueIsMoreThan5()
        {
            //arrange
            var mark = new Mark(item, criterion, 3);

            bool isCatched = false;

            //act
            try
            {
                mark.SetValue(10);
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
