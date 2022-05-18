using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TBSLogistics.WebApp.Models;

namespace TBSLogistics.WebApp.DTO
{
    public class CartViewModel
    {
        public CartViewModel()
        {
            CartItems = new List<CartItemViewModel>();
        }
        public int CartID
        {
            get;
            set;
        }
        public List<CartItemViewModel> CartItems
        {
            get;
            set;
        }
       
        public int TotalQty
        {
            get;
            set;
        }
        public double TotalPrice
        {
            get;
            set;
        }

    }
    public class CartItemViewModel
    {
        public CartItemViewModel()
        {
            Product = new Tbs_Product();
        }
        public Tbs_Product Product
        {
            get;
            set;
        }
        public double? HistoricalCost
        {
            get;
            set;
        }
        public int Qty
        {
            get;set;
        }
    }
}