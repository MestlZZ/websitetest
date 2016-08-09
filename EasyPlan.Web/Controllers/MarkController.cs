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
    public class MarkController : DefaultController
    {
        private readonly IMarkRepository _markRepository;

        public MarkController(IMarkRepository markRepository)
        {
            _markRepository = markRepository;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserRole(RoleName.Admin, RoleName.Editor)]
        public ActionResult CreateMark(Item item, Criterion criterion, int value)
        {
            var mark = _markRepository.FindByItemAndCriterionId(item.Id, criterion.Id);

            if (mark == null)
            {
                mark = new Mark(item, criterion, value);

                _markRepository.Add(mark);
            }
            else
            {
                mark.SetValue(value);
            }

            return JsonSuccess(MarkMapper.Map(mark));
        }
    }
}