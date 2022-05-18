using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TBSLogistics.WebApp.Models;

namespace TBSLogistics.WebApp.DTO
{
    public class OrderItem
    {
        public int OrderID { get; set; }
        public IEnumerable<OrderProduct> OrderProducts
        {
            get;
            set;
        }
        public double? TotalPrice
        {
            get;
            set;
        }
        public int? TotalQty
        {
            get;
            set;
        }
        public int? Status
        {
            get;
            set;
        }
        public DateTime? OrderDate { get; set; }
        public bool? IsCancel { get; set; }
        public string CancelBy { get; set; }
        public DateTime? CancelDate { get; set; }
        public string CancelReason { get; set; }
    }
    public class OrderProduct
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int? Qty { get; set; }
        public double? Price { get; set; }
      
    }
    
}