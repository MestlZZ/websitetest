using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Web.Mvc;
using mySite.DomainModel.Repositories;
using mySite.Web.ViewModels;

namespace mySite.Web.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentRepository _studentRepository;

        public StudentsController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public ActionResult Index()
        {
            var students = _studentRepository.GetCollection()
                    .OrderBy(student => student.Name)
                    .Select(student => new StudentViewModel()
                    {
                        Name = student.Name,
                        Surname = student.Surname,
                        University = student.University
                    });

            return View(students);
        }

        [Route("students/get-data")]
        public JsonResult GetData()
        {
            var students = _studentRepository.GetCollection()
                    .OrderBy(student => student.Name)
                    .Select(student => new StudentViewModel()
                    {
                        Name = student.Name,
                        Surname = student.Surname,
                        University = student.University
                    });

            return Json( new{ Data = students }, JsonRequestBehavior.AllowGet);
        }

        
    }
}