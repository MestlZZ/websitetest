﻿using System.Linq;
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
        [UserRole(RoleName.Admin, RoleName.Editor)]
        public void SetMarkValue(int value, Mark mark)
        {
            mark.SetValue(value);
        }

        [HttpPost]
        [UserRole(RoleName.Admin, RoleName.Editor)]
        public ActionResult CreateMark(Item item, Criterion criterion)
        {
            var mark = new Mark(item, criterion, 0);

            _markRepository.Add(mark);

            return JsonSuccess(MarkMapper.Map(mark));
        }
    }
}