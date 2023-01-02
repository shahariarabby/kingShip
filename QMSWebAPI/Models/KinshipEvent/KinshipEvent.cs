using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QMSWebAPI.Models.StockLot
{
    public class KinshipEvent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }        
        public Boolean IsActive { get; set; }
        public int Type { get; set; }
        public string Title { get; set; }
        public string BannerUrl { get; set; }
        public string AltBannerUrl { get; set; }
        //public DateTime CreateDateTime { get; set; }
        //public DateTime ModifiDateTime { get; set; }
      
    }

   
}