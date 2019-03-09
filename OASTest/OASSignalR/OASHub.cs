using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using OAS;
using OASSignalR.Models;

namespace OASSignalR
{
    public class OASHub : Hub
    {
        private readonly OASTags _oasTags;

        public OASHub() : this(OASTags.Instance) { }

        public OASHub(OASTags oasTags)
        {
            _oasTags = oasTags;
        }

        public void Send(string name, string message)
        {
            Clients.All.broadcastMessage(name, message);
        }

        public void testMessage(string name, string message)
        {
            Clients.All.broadcastMessage(name, message);

        }

        public void getVersion()
        {
            var versionNumber = ModuleNetworkNode.OASSystemComponent.GetVersion().ToString();
            Clients.All.returnVersion(versionNumber);
        }

        public void getTagValue()
        {
            string[] Tags = new string[3];
            Tags[0] = "Ramp.Value";
            Tags[1] = "Sine.Value";
            Tags[2] = "Random.Value";
            Int32[] Errors = null;
            // The Errors array will be sized to the same length as your Tags array.
            // The following values will be returned to you in the Int32 array you pass.
            // 0 = Good Quality
            // 1 = Bad Quality
            // 2 = Timeout
            string values = null;
            values = OAS.DataConnector.ReadTagValues(Tags);
            Clients.All.returnTags(values);
        }

        public string GetTagValues()
        {
            return _oasTags.GetTagValues();
        }
    }
}