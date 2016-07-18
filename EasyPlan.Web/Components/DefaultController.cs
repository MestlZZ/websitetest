using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyPlan.Web.Components
{
    public class DefaultController : Controller
    {
        protected ActionResult JsonSuccess(object data)
        {
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}