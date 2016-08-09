using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using EasyPlan.DomainModel.Repositories;
using EasyPlan.Web.Components.Mapper;
using EasyPlan.Web.Components;
using Newtonsoft.Json;

namespace EasyPlan.Web.Hubs
{
    public class BoardHub : Hub
    {    
        public Task OpenBoard(string id)
        {
            return Groups.Add(Context.ConnectionId, id);
        }

        public Task CloseBoard(string id)
        {
            return Groups.Remove(Context.ConnectionId, id);
        }

        public void UpdateItemTitle(string boardId, string id, string title)
        {
            Clients.Group(boardId).updateItemTitle(id, title);
        }

        public void DeleteItem(string boardId, string id)
        {
            Clients.Group(boardId).deleteItem(id);
        }

        public void AddItem(string boardId, object item)
        {            
            Clients.Group(boardId).addItem(item);
        }

        public void SetMark(string boardId, object mark)
        {
            Clients.Group(boardId).setMark(mark);
        }

        public void SetCriterionWeight(string boardId, string id, int weight)
        {
            Clients.Group(boardId).setCriterionWeight(id, weight);
        }

        public void UpdateCriterionTitle(string boardId, string id, string title)
        {
            Clients.Group(boardId).updateCriterionTitle(id, title);
        }

        public void UpdateBoardTitle(string boardId, string title)
        {
            Clients.Group(boardId).updateBoardTitle(title);
        }

        public void DeleteCriterion(string boardId, string id)
        {
            Clients.Group(boardId).deleteCriterion(id);
        }

        public void AddCriterion(string boardId, object criterion)
        {
            Clients.Group(boardId).addCriterion(criterion);
        }

        public void AddCollaborator(string boardId, object collaborator)
        {
            Clients.All.addCollaborator(collaborator, boardId);
        }

        public void RemoveCollaborator(string boardId, string email)
        {
            Clients.All.removeCollaborator(email, boardId);
        }

        public void ChangeCollaboratorRole(string boardId, string email, int role)
        {
            Clients.All.collaboratorChangeRole(email, role, boardId);
        }
    }
}