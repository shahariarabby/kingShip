using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QMSWebAPI.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        public int FixtureId { get; set; }
        public string QuizName { get; set; }
        public int Score { get; set; }
        public DateTime AvailableTime { get; set; }
        public int SLNo { get; set; }

       // public List<QuizAsnList> FixturesList = new List<QuizAsnList>();
    }
}