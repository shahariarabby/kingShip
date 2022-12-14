using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QMSWebAPI.Models.SustainabilityModels
{
    public class SustainabilityContactUs
    {
        public long Id { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public DateTime? ModifyDateTime { get; set; }
        public long Score { get; set; }
        public string UserId { get; set; }
        public long BusinessUnitId { get; set; }        
        public bool? Status { get; set; }
        public string Type { get; set; }
        public long Action { get; set; }
        public string Discription { get; set; }
        public string ImageName { get; set; }
        public string ImageUrl { get; set; }
        public string ImagePath { get; set; }
        public int LevelUp { get; set; }

        
    }
}