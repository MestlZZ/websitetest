﻿using System.Web.Mvc;
using EasyPlan.DomainModel.Entities;
using EasyPlan.DomainModel.Repositories;
using EasyPlan.Infrastructure;
using EasyPlan.Web.Components.Mapper;
using EasyPlan.Web.Components;
using EasyPlan.Web.Components.ActionFilters.Premission;

namespace EasyPlan.Web.Controllers
{
    [Authorize]
    public class CriterionController : DefaultController
    {
        private readonly ICriterionRepository _criterionRepository;
        private readonly IMarkRepository _markRepository;

        public CriterionController(ICriterionRepository criterionRepository, IMarkRepository markRepository)
        {
            _criterionRepository = criterionRepository;
            _markRepository = markRepository;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserRole(RoleName.Admin, RoleName.Editor)]
        public void SetCriterionWeight(int weight, Criterion criterion)
        {
            criterion.SetWeight(weight);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserRole(RoleName.Admin, RoleName.Editor)]
        public void SetCriterionTitle(string title, Criterion criterion)
        {
            criterion.SetTitle(title);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserRole(RoleName.Admin, RoleName.Editor)]
        public void RemoveCriterion(Criterion criterion)
        {
            criterion.Marks.Clear();

            _criterionRepository.Remove(criterion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserRole(RoleName.Admin, RoleName.Editor)]
        public ActionResult CreateCriterion(bool isBenefit, Board board)
        {
            var criterion = new Criterion(board, isBenefit);

            _criterionRepository.Add(criterion);

            return JsonSuccess(CriterionMapper.Map(criterion));
        }
    }
}