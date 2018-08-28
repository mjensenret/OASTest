using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OASTest.ViewModels
{
    public class TagViewModel
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public float Reading { get; set; }
        public string Units { get; set; }
    }

    public class TagGroups
    {
        public string status { get; set; }
        public List<String> data { get; set; }

        public IEnumerable<SelectListItem> tagGroups { get; set; }
    }

    
}