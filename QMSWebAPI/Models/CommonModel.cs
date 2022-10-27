using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QMSWebAPI.Models
{
    public class CommonModel
    {
        public int CommonId { get; set; }
        public string CommonName { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public int BusinessUnitId { get; set; }
        public string BusinessUnitName { get; set; }
        public int MeterId { get; set; }
        public string MeterName { get; set; }
        public int BWLId { get; set; }
        public int LWMId { get; set; }
        public int EnergyRequesMastertId { get; set; }
        public int RequestorId { get; set; }
        public int CategoryId { get; set; }
        public string DateOfRequest { get; set; }
        public string Image { get; set; }
        public string ImagePath { get; set; }
        public string CreateDate { get; set; }
        public string UpdateDate { get; set; }
        public string UserName { get; set; }
        public string CategoryName { get; set; }
        public string SupervisorId { get; set; }
        public string Issue { get; set; }
        public int SubCategoryId { get; set; }
        public string UserId { get; set; }
        public int OperationID { get; set; }
        public string OperationName { get; set; }
        public string ProductionFloor { get; set; }
        public string LineNumber { get; set; }
        public string BusinessUnit { get; set; }
        public int GRVRequesMastertId { get; set; }
        public string SupervisoName { get; set; }
        public string SubCategoryName { get; set; }
        public string Status { get; set; }
        public int IsApproved { get; set; }
    }
}