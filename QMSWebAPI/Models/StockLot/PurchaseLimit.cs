using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QMSWebAPI.Models.StockLot
{
    public class PurchaseLimit
    {
        public int ID { get; set; }
        public string ProductType { get; set; }
        public int UnitLimit { get; set; }
        public int UnitPrice { get; set; }     
      
    }

   
}