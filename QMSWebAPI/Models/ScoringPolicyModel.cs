using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QMSWebAPI.Models
{
    public class ScoringPolicyModel
    {
        public int Id { get; set; }
        public string PolicyName { get; set; }
        public int Point { get; set; }
    }
}