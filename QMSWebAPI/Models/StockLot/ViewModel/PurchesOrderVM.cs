using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QMSWebAPI.Models.StockLot
{
    public class PurchesOrderVM
    {
        public int Id { get; set; }
        public string StyleName { get; set; }
        public string Gender { get; set; }
        public string ProductType { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public string OrderNumber { get; set; }
        public string UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public string CreateDate { get; set; }
        public string CreateTime { get; set; }
        public int PurchesOrderMasterId { get; set; }

    }

   
}