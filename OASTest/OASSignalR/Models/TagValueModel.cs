using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OASSignalR.Models
{
    public class TagValueModel
    {
        public string TagName { get; set; }
        public DateTime ReadTime { get; set; }
        public string Value { get; set; }
    }
}