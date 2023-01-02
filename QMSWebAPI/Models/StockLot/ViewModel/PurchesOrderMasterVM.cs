using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QMSWebAPI.Models.StockLot
{
    public class PurchesOrderMasterVM
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public string UserId { get; set; }
        public int Status { get; set; }
        public string Remarks { get; set; }
        public decimal NetPrice { get; set; }
        public string CreateDate { get; set; }
        public string CreateTime { get; set; }
      
    }

   
}