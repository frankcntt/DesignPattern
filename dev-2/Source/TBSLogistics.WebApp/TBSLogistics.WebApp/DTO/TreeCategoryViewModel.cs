using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TBSLogistics.WebApp.Models
{
    public class TreeCategoryViewModel
    {
        public int id { get; set; } 
        public string parent { get; set; }
        public string text { get; set; }
    }
}