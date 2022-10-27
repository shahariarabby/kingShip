using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QMSWebAPI.Models
{
    public class OperationModel
    {
        public int OperationID { get; set; }
        public string OperationName { get; set; }
        public string ProductionFloor { get; set; }
        public string LineNumber { get; set; }
        public string BusinessUnit { get; set; }
      
    }
}