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
            ViewBag.TagGroups = svc.GetTagGroupsDropdown("");

            return View();
        }
        [HttpPost]
        public async Task<ActionResult> TankLevels(FormCollection form)
        {
            //var taggroups = svc.GetTagGroups();
            //var folders= taggroups.Select(x => x.data);
            //List<String> data = folders.First();

            //List<SelectListItem> result = new List<SelectListItem>();

            //for (var i = 0; i < data.Count(); i++)
            //{
            //    result.Add(new SelectListItem { Text = data[i], Value = data[i] });
            //}
            string strDDLValue = form["TagGroup"].ToString();
            ViewBag.TagGroups = svc.GetTagGroupsDropdown(strDDLValue);


            return View();
        }


        //[HttpGet]
        //public ActionResult GetGroups(DataSourceLoadOptions loadOptions)
        //{
        //    return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(svc.GetTagGroups(), loadOptions)), "application/json");
        //}


    }
}