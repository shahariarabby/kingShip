using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QMSWebAPI.Models.StockLot
{
    public class PurchesOrderMaster
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public string UserId { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public decimal NetPrice { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime ModifiDateTime { get; set; }
      
    }

   
}