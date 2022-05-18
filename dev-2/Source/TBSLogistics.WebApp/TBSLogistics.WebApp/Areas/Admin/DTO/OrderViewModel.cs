using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TBSLogistics.WebApp.DTO;

namespace TBSLogistics.WebApp.Areas.Admin.DTO
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public string OrderRequestBy { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? OrderStatus { get; set; }
        public int? TotalQty { get; set; }
        public double? TotalPrice { get; set; } 
        public bool? IsCancel { get; set; }
        public string CancelBy { get; set; }
        public DateTime? CancelDate { get; set; }
        public string CancelReason { get; set; }    
    }
    public class OrderDetailViewModel: OrderViewModel
    {
       public string NumberPhone { get; set; }
        public string Address { get; set; } 
        public List<OrderProduct> OrderProducts { get; set; }
    }
}