using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OASTest.Models
{
    public class AuthorizationModel
    {
        public string status { get; set; }
        public string messages { get; set; }

        public data data { get; set; }
    }

    public class data
    {
        public string clientid { get; set; }
        public string token { get; set; }
    }
}