using OASTest.Models;
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

namespace OASTest.Controllers
{
    public class OASTestController : Controller
    {
        string baseUrl = "http://fieldvision.savageservices.com:58725/OASREST/v2/";
        string clientId = "";
        string token = "";


        // GET: OASTest
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> WsTest()
        {
            
            
            var tagModel = getMultipleTagValues("HarringtonStation.Amps");

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

        public async Task<ActionResult> TankLevels()
        {
            //if(setAuth("",""))
            //{
            //    //var tagChart = getSpecificTagValue();

                
            //}
            return View();
        }

        public AuthorizationModel setAuth(string user, string pass)
        {
            var client = new RestClient(baseUrl + "authenticate");
            var request = new RestRequest(Method.POST);
            request.RequestFormat = DataFormat.Json;
            //request.AddHeader("Postman-Token", "5eea52d2-ffc7-4ff4-a915-82be2e7fd562");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", "{\n\t\"username\":\"\",\n\t\"password\":\"\"\n}", ParameterType.RequestBody);
            var response = client.Execute<AuthorizationModel>(request);

            //JObject obj = JObject.Parse(response.Content);
            //JObject ojObject = (JObject)obj["data"];

            //try
            //{
            //    clientId = (String)ojObject["clientid"];
            //    token = (String)ojObject["token"];
            //    return true;
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("Something is not happy here: " + e);

            //    return false;
            //}

            AuthorizationModel authModel = response.Data;

            return authModel;

        }

        public List<TagList> getMultipleTagValues(string folder)
        {
            
            var auth = setAuth("", "");
            var client = new RestClient(baseUrl + "tags?params=true&ref="+ folder + "&recurse=true");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Authorization", "Basic Og==");
            request.AddHeader("token", auth.data.token);
            request.AddHeader("clientid", auth.data.clientid);
            request.AddHeader("Content-Type", "application/json");
            var response = client.Execute<List<TagList>>(request);

            //JObject tags = JObject.Parse(response.Content);
            List<TagList> tags = response.Data;

            

            return tags;
        }

        public List<SingleTagWithParams> getSpecificTagValue(String path)
        {
            var auth = setAuth("", "");
            var client = new RestClient(baseUrl + "tags?path=" + path);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Authorization", "Basic Og==");
            request.AddHeader("token", auth.data.token);
            request.AddHeader("clientid", auth.data.clientid);
            request.AddHeader("Content-Type", "application/json");
            IRestResponse response = client.Execute(request);
            var response2 = client.Execute<List<SingleTagWithParams>>(request);

            List<SingleTagWithParams> tags = response2.Data;

            return tags;
        }
    }
}