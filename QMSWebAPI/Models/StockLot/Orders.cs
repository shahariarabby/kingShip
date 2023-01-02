using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QMSWebAPI.Models.StockLot
{
    public class Orders
    {

        public decimal NetTotalPrice { get; set; }
        public string UserId { get; set; }
        public List<PurchesOrder> PurchesOrders { get; set; }

    }


}