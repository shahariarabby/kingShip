using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QMSWebAPI.Models.SustainabilityModels.SustainabilityViewModels
{
    public class MonthlyTaskListViewModel
    {
        public long Id { get; set; }
        public string CreateDate { get; set; }
        public string CreateTime { get; set; }
        public string BanglaName { get; set; }
        public string EnglishName { get; set; }
        public string Type { get; set; }
        public long BusinessUnitId { get; set; }
        public string ImageName { get; set; }
        public string ImageUrl { get; set; }
        public string ImagePath { get; set; }
        public string InfoLink { get; set; }
        public long SustainabilityCategoryId { get; set; }
        //public long Action { get; set; }
        public string Co2 { get; set; }
        public string Water { get; set; }
        public string Energy { get; set; }
        public string Kindness { get; set; }
        public bool DisableStatus { get; set; }


        public IList<MonthlyTaskListDetailsViewModel> MonthlyTaskListDetailsViewModels { get; set; }
        
        

    }
}