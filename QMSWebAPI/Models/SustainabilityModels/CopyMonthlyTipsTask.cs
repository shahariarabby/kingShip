using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QMSWebAPI.Models.SustainabilityModels
{
    public class CopyMonthlyTipsTask
    {
        public long Id { get; set; }
        public long MonthlyTipsTaskId { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public DateTime? ModifyDateTime { get; set; }
        public string OriginalLink { get; set; }
        public string LinkCode { get; set; }
        public string Type { get; set; }
        public long BusinessUnitId { get; set; }
    }
}