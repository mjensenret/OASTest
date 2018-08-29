using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OASTest.Models
{
    public class TagList
    {
        public string path { get; set; }
        public List<Parameters> parameters { get; set; }

    }

    public class Parameters
    {
       public List<Value> Value { get; set; }
    }

    public class Value
    {
        [DeserializeAs(Name = "Value")]
        public float Reading {get;set;}
        public string Desc { get; set; }
        public string Units { get; set;}
        public int HighRange { get; set; }
        public int LowRange { get; set; }
    }
}