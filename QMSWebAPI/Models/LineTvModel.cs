using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QMSWebAPI.Models
{
    public class LineTvModel
    {
        public int LineTvId { get; set; }
        public string IP { get; set; }
        public string Line { get; set; }
        public string BusinessUnitName { get; set; }
        public int BusinessUnitId { get; set; }
        public string PunitName { get; set; }
        public string FloorModuleName { get; set; }
        public string TotalProduction { get; set; }
        public string TotalDefect { get; set; }
        public string DefectivePieces { get; set; }
        public double DefectPercentage { get; set; }
        public double DHU { get; set; }
        public double SMV { get; set; }
        public double Efficiency { get; set; }
        public int MachineNumber { get; set; }

    }
}