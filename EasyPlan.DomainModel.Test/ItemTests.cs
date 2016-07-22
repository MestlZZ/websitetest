using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyPlan.DomainModel.Entities;
using NSubstitute;

namespace EasyPlan.DomainModel.Test
{
    [TestClass]
    public class ItemTests
    {
        private Board board = Substitute.For<Board>();
        
        [TestMethod]
        public void Item_Create_Success()
        {
            //act
            var item = new Item("Item title", board);

            //assert
            Assert.IsNotNull(item, message: "item is null");
            Assert.AreEqual(item.Title, "Item title", message: "title is unequal");
            Assert.IsNotNull(item.Board, message: "board is unsetted");
            Assert.IsNotNull(item.Id, message: "item id is not setted");
            Assert.AreEqual(item.Board.Id, board.Id, message: "Board setted as invalid");
        }

        [TestMethod]
        public void Item_Create_IncorectBoard()
        {
            //arrange
            bool isCatched = false;

            //act
            try
            {
                var item = new Item("Item title", null);
            }
            catch
            {
                isCatched = true;
            }

            //assert
            Assert.IsTrue(isCatched, message: "Exception isn't called");
        }

        [TestMethod]
        public void Item_SetTitle_Success()
        {
            //arrange
            var item = new Item("Item title", board);

            //act
            item.SetTitle("new title");

            //assert
            Assert.AreEqual(item.Title, "new title", message: "title is unequal");
        }

        [TestMethod]
        public void Item_SetTitle_Invalid_EmptyString()
        {
            //arrange
            var item = new Item("Item title", board);

            bool isCatched = false;

            //act
            try
            {
                item.SetTitle("     ");
            }
            catch
            {
                isCatched = true;
            }
            
            //assert
            Assert.IsTrue(isCatched, message: "Exception isn't called");
        }

        [TestMethod]
        public void Item_SetTitle_Invalid_Null()
        {
            //arrange
            var item = new Item("Item title", board);

            bool isCatched = false;

            //act
            try
            {
                item.SetTitle(null);
            }
            catch
            {
                isCatched = true;
            }

            //assert
            Assert.IsTrue(isCatched, message: "Exception isn't called");
        }

        [TestMethod]
        public void Item_SetTitle_Invalid_LongerThan255()
        {
            //arrange
            var item = new Item("Item title", board);

            bool isCatched = false;

            //act
            try
            {
                item.SetTitle("asd asd as a sa sad as as sda sa as asasd asd as a sa sad as as sda sa as asasd asd as a sa sad as as sda sa as asasd asd as a sa sad as as sda sa as asasd asd as a sa sad as as sda sa as asasd asd as a sa sad as as sda sa as asasd asd as a sa sad as as sda");
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
