using OASTest.Models;
using OASTest.ViewModels;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OASTest.Service
{
    public class OASRestService
    {
        string baseUrl = "http://ops-dev.savageservices.com:58725/OASREST/v2/";
        string clientId = "";
        string token = "";

        public OASRestService() { }

        public AuthorizationModel setAuth(string user, string pass)
        {
            var client = new RestClient(baseUrl + "authenticate");
            var request = new RestRequest(Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", "{\n\t\"username\":\"\",\n\t\"password\":\"\"\n}", ParameterType.RequestBody);
            var response = client.Execute<AuthorizationModel>(request);

            AuthorizationModel authModel = response.Data;

            return authModel;

        }

        public List<TagList> GetMultipleTagValues(string folder)
        {

            var auth = setAuth("", "");
            var client = new RestClient(baseUrl + "tags?params=true&ref=" + folder + "&recurse=true");
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

        public List<TagList> GetSpecificTagValue(String path)
        {
            var auth = setAuth("", "");
            var client = new RestClient(baseUrl + "tags?path=" + path + "&params=true");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Authorization", "Basic Og==");
            request.AddHeader("token", auth.data.token);
            request.AddHeader("clientid", auth.data.clientid);
            request.AddHeader("Content-Type", "application/json");
            IRestResponse response = client.Execute(request);
            var response2 = client.Execute<List<TagList>>(request);

            List<TagList> tags = response2.Data;

            return tags;
        }

        public List<TagGroups> GetTagGroups()
        {
            var auth = setAuth("", "");
            var client = new RestClient(baseUrl + "taggroups");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("token", auth.data.token);
            request.AddHeader("clientid", auth.data.clientid);
            request.AddHeader("Content-Type", "application/json");

            var response = client.Execute<List<TagGroups>>(request);
            List<TagGroups> groups = response.Data;

            return groups;
        }

        public List<SelectListItem> GetTagGroupsDropdown(string selectedValue)
        {
            var auth = setAuth("", "");
            var client = new RestClient(baseUrl + "taggroups");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("token", auth.data.token);
            request.AddHeader("clientid", auth.data.clientid);
            request.AddHeader("Content-Type", "application/json");

            var response = client.Execute<List<TagGroups>>(request);
            List<TagGroups> groups = response.Data;

            List<SelectListItem> groupList = new List<SelectListItem>();

            var folders = groups.Select(x => x.data);
            List<String> data = folders.First();


            for (var i = 0; i < data.Count(); i++)
            {
                bool selected = false;
                if(data[i] == selectedValue)
                {
                    selected = true;
                }
                groupList.Add(new SelectListItem
                {
                    Text = data[i],
                    Value = data[i],
                    Selected = selected
                });
            }

            return groupList;
        }
    }
}