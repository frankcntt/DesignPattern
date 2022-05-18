using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TBSLogistics.WebApp.Models
{
    public class ProductViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<double> Price { get; set; }
        public string SKU { get; set; }
        public Nullable<int> Inventory { get; set; }
        public Nullable<int> VAT { get; set; }
        public Nullable<bool> IsFreeShip { get; set; }
        public Nullable<bool> IsActve { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public HttpPostedFileBase File { get; set; }
    }
    
}