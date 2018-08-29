using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OASTest.ViewModels
{
    public class GaugeViewModel
    {
        public string tagPath { get; set; }
        public string tagName { get; set; }
        public int maxValue { get; set; }
        public int minValue { get; set; }
        public float tagValue { get; set; }
    }
}