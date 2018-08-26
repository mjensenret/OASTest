using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace OASTest.Models
{
    //[DataContract]
    public class PLCTags
    {
        public string path { get; set; }
        
        public string TagName { get; set; }
        public float value { get; set; }
        //public List<Parameters> parameters { get; set; }
    }

    //public class Parameters
    //{
    //    public List<Value> value { get; set; }
    //}

    //public class Value
    //{
    //    public string Desc { get; set; }
    //    public string Units { get; set; } 
    //}


}