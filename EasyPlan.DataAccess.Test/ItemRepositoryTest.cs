using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyPlan.DomainModel.Repositories;
using EasyPlan.DomainModel.Entities;
using EasyPlan.DataAccess.Repositories;
using EasyPlan.Infrastructure;

namespace EasyPlan.DataAccess.Test
{
    [TestClass]
    public class ItemRepositoryTest
    {
        private IItemRepository _itemRepository;
        private IBoardRepository _boardRepository;
        private IUnitOfWork _unitOfWork;
        private Board DefaultBoard = new Board() { Title = "TestBoard" };
        private Item DefaultItem = new Item() { Title = "TestTitle" };

        [TestInitialize]
        public void SetupContext()
        {
            var context = new DatabaseContext();

            _unitOfWork = context;
            _itemRepository = new ItemRepository(context);
            _boardRepository = new BoardRepository(context);

            DefaultItem.Board = DefaultBoard;

            _boardRepository.Add(DefaultBoard);
            _unitOfWork.Save();
        }

        [TestCleanup]
        public void ClearContext()
        {
            var board = _boardRepository.Get(DefaultBoard.Id);

            _boardRepository.Remove(board);
            _unitOfWork.Save();
        }

        [TestMethod]
        public void AddItem_ChangeTitle_RemoveItem()
        {
            _itemRepository.Add(DefaultItem);
            _unitOfWork.Save();

            var item = _itemRepository.Get(DefaultItem.Id);

            Assert.IsNotNull(item, "Item don't added to database");
            Assert.AreEqual(item.Title, DefaultItem.Title, "Item title is invalid");

            _itemRepository.SetTitle("NewTitle", item.Id.ToString());
            _unitOfWork.Save();

            item = _itemRepository.Get(DefaultItem.Id);

            Assert.IsNotNull(item, "Item destroyed in database");
            Assert.AreEqual("NewTitle", item.Title, "Item title is't changed");

            _itemRepository.Remove(item);
            _unitOfWork.Save();
            
            item = _itemRepository.Get(DefaultItem.Id);

            Assert.IsNull(item, "Item is't removed from database");
        }        
    }
}
