using OAS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OASTest.Controllers
{
    public class OpsDevController : Controller
    {
        // GET: OpsDev
        public ActionResult Index()
        {
            var versionNumber = ModuleNetworkNode.OASSystemComponent.GetVersion();
            return View();
        }

        public PartialViewResult AmpGauge()
        {
            ViewBag.InitValue = 15;
            return PartialView("_ampGauge");
        }

        [HttpPost]
        public int getGaugeValue()
        {
            var value = 420;
            return value;
        }
    }
}