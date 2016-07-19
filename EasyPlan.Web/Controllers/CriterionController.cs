using System.Linq;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using EasyPlan.DomainModel.Entities;
using EasyPlan.DomainModel.Repositories;
using EasyPlan.Infrastructure;
using EasyPlan.Web.Components.Mapper;
using EasyPlan.Web.Components;

namespace EasyPlan.Web.Controllers
{
    public class CriterionController : DefaultController
    {
        private readonly ICriterionRepository _criterionRepository;

        public CriterionController(ICriterionRepository criterionRepository)
        {
            _criterionRepository = criterionRepository;
        }

        [HttpPost]
        public void SetCriterionWeight(int weight, Criterion criterion)
        {
            criterion.SetWeight(weight);
        }

        [HttpPost]
        public void SetCriterionTitle(string title, Criterion criterion)
        {
            criterion.SetTitle(title);
        }

        [HttpPost]
        public void RemoveCriterion(Criterion criterion)
        {
            _criterionRepository.Remove(criterion);
        }

        [HttpPost]
        public ActionResult CreateCriterion(bool isBenefit, Board board)
        {
            var criterion = new Criterion(board, isBenefit);

            _criterionRepository.Add(criterion);

            return JsonSuccess(CriterionMapper.Map(criterion));
        }
    }
}