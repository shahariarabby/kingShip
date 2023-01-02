using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QMSWebAPI.Models.StockLot
{
    public class Registration
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }        
        public string Location { get; set; }
        public string Designation { get; set; }
        public int BusinessUnitId { get; set; }        
        public string BusinessUnit { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime ModifiDateTime { get; set; }
      
    }

   
}