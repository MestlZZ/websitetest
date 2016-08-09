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