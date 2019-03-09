using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OASTest.ViewModels
{
    public enum NetworkNodes
    {
        [Display(Name = "localhost")]
        localhost,
        [Display(Name = "Development Server")]
        opsdev,
        [Display(Name = "Texas City")]
        texasCity

    }

    public class TagViewModel
    {

        public string Path { get; set; }
        public string Name { get; set; }
        public float Reading { get; set; }
        public string Units { get; set; }
        public string Node { get; set; }
        public string OASVersion { get; set; }
        public NetworkNodes NodeList { get; set; }
    }

    public class TagGroups
    {
        public string status { get; set; }
        public List<String> data { get; set; }

        public IEnumerable<SelectListItem> tagGroups { get; set; }
    }

    
}