using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QMSWebAPI.Models
{
    public class LeaderDashBoardModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public string Cluster { get; set; }
        public string Score { get; set; }
    }
}