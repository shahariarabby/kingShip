using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QMSWebAPI.Models.StockLot
{
    public class Product
    {
        public int Id { get; set; }
        public string StyleName { get; set; }
        public string Gender { get; set; }
        public string ProductType { get; set; }
        //public string Color { get; set; }
        //public string Size { get; set; }
        public string Description { get; set; }
        public int StockQuantity { get; set; }
        public int UnitPrice { get; set; }
        public string Image { get; set; }
        public string ImageUrl { get; set; }
      
    }

   
}