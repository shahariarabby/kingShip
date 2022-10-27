using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QMSWebAPI.Models.SustainabilityModels
{
    public class SustainabilitySurvey
    {
        public long Id { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public DateTime? ModifyDateTime { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public long Score { get; set; }
        public string UserId { get; set; }
        public long BusinessUnitId { get; set; }        
        public bool? Status { get; set; }
    }
}