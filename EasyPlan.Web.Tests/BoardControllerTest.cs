using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyPlan.Web.Controllers;
using EasyPlan.DataAccess;
using EasyPlan.DomainModel.Entities;
using EasyPlan.DomainModel.Repositories;
using EasyPlan.Infrastructure;
using EasyPlan.Web.Components.Mapper;
using EasyPlan.Web.Components;
using EasyPlan.DataAccess.Repositories;
using Newtonsoft.Json;

namespace EasyPlan.Web.Tests
{
    [TestClass]
    public class BoardControllerTest
    {
        private IBoardRepository _boardRepository;
        private IUnitOfWork _unitOfWork;
        private IItemRepository _itemRepository;
        private IMarkRepository _markRepository;
        private ICriterionRepository _criterionRepository;
        private BoardController _boardController;
        private Board TestBoard;

        [TestInitialize]
        public void ContextSetup()
        {
            var context = new DatabaseContext();

            _unitOfWork = context;
            _boardRepository = new BoardRepository(context);
            _markRepository = new MarkRepository(context);
            _itemRepository = new ItemRepository(context);
            _criterionRepository = new CriterionRepository(context);

            _boardController = new BoardController(_boardRepository, _unitOfWork, _itemRepository, _markRepository, _criterionRepository);

            var items = new List<Item>();
            var criterions = new List<Criterion>();

            items.Add(new Item() { Title = "TestItem1" });
            items.Add(new Item() { Title = "TestItem2" });

            criterions.Add(new Criterion() { Title = "TestCriterion1", IsBenefit = true, Weight = 20 });
            criterions.Add(new Criterion() { Title = "TestCriterion2" , IsBenefit = false, Weight = 0 });

            var board = new Board() { Title = "TestBoard", Criterions = criterions, Items = items };
            _boardRepository.Add(board);

            _markRepository.Add(new Mark() { Value = 1, Criterion = criterions[0], Item = items[0] });
            _markRepository.Add(new Mark() { Value = 2, Criterion = criterions[0], Item = items[1] });
            _markRepository.Add(new Mark() { Value = 3, Criterion = criterions[1], Item = items[0] });
            _markRepository.Add(new Mark() { Value = 4, Criterion = criterions[1], Item = items[1] });

            _unitOfWork.Save();

            TestBoard = _boardRepository.Get(board.Id);
        }

        [TestCleanup]
        public void ContextCelanup()
        {
            var board = _boardRepository.Get(TestBoard.Id);
            _boardRepository.Remove(board);

            _unitOfWork.Save();
        }

        [TestMethod]
        public void GetBoardsInfoTest()
        {
            var info = _boardController.GetBoardsInfo();

            Assert.IsNotNull(info, "boards info isn't goted");
        }

        [TestMethod]
        public void GetBoardData()
        {
            var data = _boardController.GetBoardData(TestBoard.Id.ToString());

            Assert.IsNotNull(data, "board isn't gotted");
        }

        [TestMethod]
        public void SetItemTitleTest()
        {
            var item = TestBoard.Items[0];

            _boardController.SetItemTitle("NewTitle", item.Id.ToString());

            var itemTest = _itemRepository.Get(item.Id);

            Assert.IsNotNull(itemTest, "Item is not in the database");
            Assert.AreEqual("NewTitle", itemTest.Title, message: "item name isn't changed");
        }

        [TestMethod]
        public void RemoveItemTest()
        {
            var item = TestBoard.Items[0];

            _boardController.RemoveItem(item.Id.ToString());

            var itemTest = _itemRepository.Get(item.Id);

            Assert.IsNull(itemTest, "Item isn't removed from database");
        }

        [TestMethod]
        public void CreateItemTest()
        {
            var countItemsBefore = _itemRepository.GetCollection().Count;
            var item = _boardController.CreateItem(TestBoard.Id.ToString());

            var countItemsAfter = _itemRepository.GetCollection().Count;

            Assert.AreNotEqual(countItemsAfter, countItemsBefore, message: "Item wasn't added to database");
            Assert.IsNotNull(item, "Method don't send new item");
        }

        [TestMethod]
        public void SetMarkValue()
        {
            var item = TestBoard.Items[1];

            var mark = item.Marks[0];

            _boardController.SetMarkValue("5", mark.Id.ToString());

            var TestMark = _markRepository.Get(mark.Id);

            Assert.IsNotNull(TestMark, "Mark removed from database");
            Assert.AreEqual(TestMark.Value, 5, message: "Mark value don't set in database");
        }
    }
}
