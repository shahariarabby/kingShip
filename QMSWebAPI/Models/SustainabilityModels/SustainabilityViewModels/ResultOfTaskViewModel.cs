using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QMSWebAPI.Models.SustainabilityModels
{
    public class ResultOfTaskViewModel
    {
        
        //public string CreateDate { get; set; }
        //public string CreateTime { get; set; }
        //public string UserId { get; set; }
        public long Action { get; set; }
        public long Score { get; set; }
        public int levelUp { get; set; }
        public bool levlUpStatus { get; set; }
    }
}