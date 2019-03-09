using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OASTest.Controllers
{
    public class ClickPLCController : Controller
    {
        // GET: ClickPLC
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult getOpcConfig()
        {
             
            var js = new
            {
                token = "2d1288cf-5ac3-4851-b2bd-9c17e263dea5",
                //TEST SERVER CONFIGURATION
                serverURL= "http://fieldvision.savageservices.com:58725"
            };

            return Json(js, JsonRequestBehavior.AllowGet);
        }
    }
}