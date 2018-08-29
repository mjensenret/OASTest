using DevExpress.Web.Mvc;
using OASTest.Models;
using OASTest.Service;
using OASTest.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DevExtreme.AspNet.Mvc;
using DevExtreme.AspNet.Data;

namespace OASTest.Controllers
{
    public class OASTestController : Controller
    {
        OASRestService svc = new OASRestService();


        // GET: OASTest
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> WsTest()
        {
            ViewBag.TagGroups = svc.GetTagGroupsDropdown("");

            var tagModel = svc.GetMultipleTagValues("HarringtonStation");

            var model = (tagModel
                .OrderBy(n => n.path)
                )
                .Select(x => new
                {
                    x.path,
                    Desc = x.parameters[0].Value.Select(y => y.Desc),
                    Reading = x.parameters[0].Value.Select(y => y.Reading),
                    Units = x.parameters[0].Value.Select(y => y.Units)

                })
                .ToList()
                .Select(y => new TagViewModel()
                {
                    Path = y.path,
                    Name = y.Desc.First(),
                    Reading = y.Reading.First(),
                    Units = y.Units.First()                         
                });

            //tagModel = getSpecificTagValue();
            return View(model);

        }

        [HttpPost]
        public async Task<ActionResult> WsTest(FormCollection form)
        {
            string strDDLValue = form["TagGroup"].ToString();
            

            var tagModel = svc.GetMultipleTagValues(strDDLValue);

            var model = (tagModel
                .OrderBy(n => n.path)
                )
                .Select(x => new
                {
                    x.path,
                    Desc = x.parameters[0].Value.Select(y => y.Desc),
                    Reading = x.parameters[0].Value.Select(y => y.Reading),
                    Units = x.parameters[0].Value.Select(y => y.Units)

                })
                .ToList()
                .Select(y => new TagViewModel()
                {
                    Path = y.path,
                    Name = y.Desc.First(),
                    Reading = y.Reading.First(),
                    Units = y.Units.First()
                });

            ViewBag.TagGroups = svc.GetTagGroupsDropdown(strDDLValue);

            return View(model);
        }

        public async Task<ActionResult> TankLevels()
        {
            var tankLevelTags = svc.GetMultipleTagValues("Trenton");

            var model = (tankLevelTags
                .OrderBy(n => n.path)
                )
                .Select(x => new
                {
                    TagName = x.parameters[0].Value.Select(y => y.Desc),
                    Reading = x.parameters[0].Value.Select(y => y.Reading),
                    HighLevel = x.parameters[0].Value.Select(y => y.HighRange),
                    LowLevel = x.parameters[0].Value.Select(y => y.LowRange)
                })
                .ToList()
                .Select(y => new GaugeViewModel()
                {
                    tagName = y.TagName.First(),
                    tagValue = y.Reading.First(),
                    maxValue = y.HighLevel.First(),
                    minValue = y.LowLevel.First()

                });

            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> TankLevels(FormCollection form)
        {
            var tankLevelTags = svc.GetMultipleTagValues("Trenton");

            //string strDDLValue = form["TagGroup"].ToString();
            //ViewBag.TagGroups = svc.GetTagGroupsDropdown(strDDLValue);


            return View();
        }





        public ActionResult _tankChart()
        {
            var tankLevelTags = svc.GetMultipleTagValues("Trenton");

            var model = (tankLevelTags
                .OrderBy(n => n.path)
                )
                .Select(x => new
                {
                    TagName = x.parameters[0].Value.Select(y => y.Desc),
                    Reading = x.parameters[0].Value.Select(y => y.Reading),
                    HighLevel = x.parameters[0].Value.Select(y => y.HighRange),
                    LowLevel = x.parameters[0].Value.Select(y => y.LowRange)
                })
                .ToList()
                .Select(y => new GaugeViewModel()
                {
                    tagName = y.TagName.First(),
                    tagValue = y.Reading.First(),
                    maxValue = y.HighLevel.First(),
                    minValue = y.LowLevel.First()

                });

            return PartialView("_tankChart", model);
        }

        public ActionResult _tankGauges()
        {
            var tankLevelTags = svc.GetMultipleTagValues("Trenton");

            var model = (tankLevelTags
                .OrderBy(n => n.path)
                )
                .Select(x => new
                {
                    TagName = x.parameters[0].Value.Select(y => y.Desc),
                    Reading = x.parameters[0].Value.Select(y => y.Reading),
                    HighLevel = x.parameters[0].Value.Select(y => y.HighRange),
                    LowLevel = x.parameters[0].Value.Select(y => y.LowRange)
                })
                .ToList()
                .Select(y => new GaugeViewModel()
                {
                    tagName = y.TagName.First(),
                    tagValue = y.Reading.First(),
                    maxValue = y.HighLevel.First(),
                    minValue = y.LowLevel.First()

                });

            return PartialView("_tankChart", model);
        }

        public ActionResult HarringtonDashboard()
        {
            return View();
        }
        public ActionResult amps()
        {
            var amperageTags = svc.GetMultipleTagValues("HarringtonStation.Amps");

            var model = (amperageTags
                .OrderBy(n => n.path)
                )
                .Select(x => new
                {
                    TagPath = x.path,
                    TagName = x.parameters[0].Value.Select(y => y.Desc),
                    Reading = x.parameters[0].Value.Select(y => y.Reading),
                    HighLevel = x.parameters[0].Value.Select(y => y.HighRange),
                    LowLevel = x.parameters[0].Value.Select(y => y.LowRange)
                })
                .ToList()
                .Select(y => new GaugeViewModel()
                {
                    tagPath = y.TagPath,
                    tagName = y.TagName.First(),
                    tagValue = y.Reading.First(),
                    maxValue = y.HighLevel.First(),
                    minValue = y.LowLevel.First()

                });

            return PartialView("_ampsPartialView", model);
        }

        public ActionResult tons()
        {
            var tankLevelTags = svc.GetMultipleTagValues("HarringtonStation.Weights");

            var model = (tankLevelTags
                .OrderBy(n => n.path)
                )
                .Select(x => new
                {
                    TagName = x.parameters[0].Value.Select(y => y.Desc),
                    Reading = x.parameters[0].Value.Select(y => y.Reading),
                    HighLevel = x.parameters[0].Value.Select(y => y.HighRange),
                    LowLevel = x.parameters[0].Value.Select(y => y.LowRange)
                })
                .ToList()
                .Select(y => new GaugeViewModel()
                {
                    tagName = y.TagName.First(),
                    tagValue = y.Reading.First(),
                    maxValue = y.HighLevel.First(),
                    minValue = y.LowLevel.First()

                });
            return PartialView("_tonsPartialView", model);
        }

        public void ampTrend(string tagPath)
        {
            var name = svc.GetSpecificTagValue(tagPath);




        }
    }
}