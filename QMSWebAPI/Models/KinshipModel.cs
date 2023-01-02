using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QMSWebAPI.Models
{
    public class KinshipModel
    {
        public int BusinessUnitId { get; set; }
        public string BusinessUnitName { get; set; }
        public int RequestorId { get; set; }
        public int CategoryId { get; set; }
        public string DateOfRequest { get; set; }
        public string Image { get; set; }
        public string ImagePath { get; set; }
        public string CreateDate { get; set; }
        public string UpdateDate { get; set; }
        public string UserName { get; set; }
        public string CategoryName { get; set; }
        public string CategoryNameBangla { get; set; }
        public string SupervisorId { get; set; }
        public int SubCategoryId { get; set; }
        public string UserId { get; set; }
        public string BusinessUnit { get; set; }
        public int GRVRequesMastertId { get; set; }
        public string SupervisoName { get; set; }
        public string SubCategoryName { get; set; }
        public string SubCategoryNameBangla { get; set; }
        public string Status { get; set; }
        public int IsApproved { get; set; }
        public string ApproverName { get; set; }
        public string Issue { get; set; }
        public string ReviewComment { get; set; }
        public string Location { get; set; }
        public string Reply { get; set; }
        public int IsVisible { get; set; }
        public string GRVFileName { get; set; }
        public string GRVFilePath { get; set; }

        public string Name { get; set; }
        public string UserEmail { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public string ImmidiateSuperviosor { get; set; }
        public string ResponsedLevel { get; set; }
        public string Approved { get; set; }
        public string CloseStatus { get; set; }
        public string SBU { get; set; }
        public string LogDescrition { get; set; }
    }
}