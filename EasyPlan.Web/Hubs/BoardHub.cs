using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using EasyPlan.DomainModel.Repositories;
using EasyPlan.Web.Components.Mapper;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace EasyPlan.Web.Hubs
{
    public class BoardHub : Hub
    {
        private string boardId;

        private readonly IItemRepository _itemRepository;
        private readonly ICriterionRepository _criterionRepository;
        private readonly IMarkRepository _markRepository;

        public BoardHub()
        {
            _markRepository = DependencyResolver.Current.GetService<IMarkRepository>();
            _itemRepository = DependencyResolver.Current.GetService<IItemRepository>();
            _criterionRepository = DependencyResolver.Current.GetService<ICriterionRepository>();
        }

        public Task OpenBoard(string id)
        {
            boardId = id;

            return Groups.Add(Context.ConnectionId, id);
        }

        public Task CloseBoard()
        {
            return Groups.Remove(Context.ConnectionId, boardId);
        }

        public void UpdateItemTitle(string id, string title)
        {
            Clients.OthersInGroup(boardId).updateItemTitle(id, title);
        }

        public void DeleteItem(string id)
        {
            Clients.OthersInGroup(boardId).deleteItem(id);
        }

        public void AddItem(string id)
        {
            var item = ItemMapper.Map(_itemRepository.Get(Guid.Parse(id)));
            
            Clients.OthersInGroup(boardId).addItem(JsonConvert.SerializeObject(item));
        }

        public void SetMark(string id)
        {
            var mark = MarkMapper.Map(_markRepository.Get(Guid.Parse(id)));

            Clients.OthersInGroup(boardId).setMark(JsonConvert.SerializeObject(mark));
        }

        public void SetCriterionWeight(string id, int weight)
        {
            Clients.OthersInGroup(boardId).setCriterionWeight(id, weight);
        }

        public void UpdateCriterionTitle(string id, string title)
        {
            Clients.OthersInGroup(boardId).updateCriterionTitle(id, title);
        }

        public void UpdateBoardTitle(string title)
        {
            Clients.OthersInGroup(boardId).updateBoardTitle(title);
        }

        public void DeleteCriterion(string id)
        {
            Clients.OthersInGroup(boardId).deleteCriterion(id);
        }

        public void AddCriterion(string id)
        {
            var criterion = CriterionMapper.Map(_criterionRepository.Get(Guid.Parse(id)));

            Clients.OthersInGroup(boardId).addCriterion(JsonConvert.SerializeObject(criterion));
        }
    }
}