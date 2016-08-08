using System.Linq;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using EasyPlan.DomainModel.Entities;
using EasyPlan.DomainModel.Repositories;
using EasyPlan.Infrastructure;
using EasyPlan.Web.Components.Mapper;
using EasyPlan.Web.Components;
using EasyPlan.Web.Components.ActionFilters.Premission;

namespace EasyPlan.Web.Controllers
{
    [Authorize]
    public class ItemController : DefaultController
    {
        private readonly IItemRepository _itemRepository;

        public ItemController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserRole(RoleName.Admin, RoleName.Editor)]
        public void SetItemTitle(string title, Item item)
        {
            item.SetTitle(title);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserRole(RoleName.Admin, RoleName.Editor)]
        public void RemoveItem(Item item)
        {
            _itemRepository.Remove(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserRole(RoleName.Admin, RoleName.Editor)]
        public ActionResult CreateItem(Board board)
        {
            var item = new Item("New item", board);

            _itemRepository.Add(item);

            return JsonSuccess(ItemMapper.Map(item));
        }
    }
}