using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TBSLogistics.WebApp.Models
{
    public class CategoryViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool isActive { get; set; }
        public HttpPostedFileBase File { get; set; }
    }
    
}