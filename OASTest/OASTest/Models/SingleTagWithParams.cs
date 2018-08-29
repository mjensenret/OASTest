using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace OASTest.Models
{
    //[DataContract]
    public class SingleTagWithParams
    {
        public string path { get; set; }
        
        public string TagName { get; set; }
        public float value { get; set; }
        public List<ParameterContainer> parameters { get; set; }
    }

    public class ParameterContainer
    {
        public List<ParameterValue> value { get; set; }
    }

    public class ParameterValue
    {
        public string Desc { get; set; }
        public string Units { get; set; }
        public int HighRange { get; set; }
        public int LowRange { get; set; }
    }


}