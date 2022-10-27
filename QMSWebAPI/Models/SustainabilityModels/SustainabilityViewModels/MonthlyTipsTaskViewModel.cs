using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QMSWebAPI.Models.SustainabilityModels
{
    public class MonthlyTipsTaskViewModel
    {
        public long Id { get; set; }
        public string CreateDate { get; set; }
        public string CreateTime { get; set; }
        public string OriginalLink { get; set; }
        public string LinkCode { get; set; }
        public string Type { get; set; }
        public long BusinessUnitId { get; set; }
    }
}