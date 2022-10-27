using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QMSWebAPI.Models
{
    public class IncentiveModel
    {
        public int ID { get; set; }
        public int EmpBarcodeNo { get; set; }
        public string EmployeeCode { get; set; }
        public int WorkerID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string SectionInfo { get; set; }
        public string SubSection { get; set; }
        public string LineInfo { get; set; }
        public string Floor { get; set; }
        public int OperationID { get; set; }
        public string OperationName { get; set; }
        public int BarcodeNo { get; set; }
        public string BarcoadCount { get; set; }
        public string BusinessUnit { get; set; }
        public string ProductionFloor { get; set; }
        public string LineNumber { get; set; }
        //public string UserName { get; set; }
        //public string CategoryName { get; set; }
    }
}