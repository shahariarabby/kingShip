using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QMSWebAPI.Models.SustainabilityModels.SustainabilityViewModels
{
    public class DailyTaskListDetailsViewModel
    {
        public long Id { get; set; }
        public long DailyTaskListId { get; set; }
        public string Title { get; set; }
        public string Note { get; set; }
        public string TitleBangla { get; set; }
        public string NoteBangla { get; set; }
        public bool Islink { get; set; }
    }
}