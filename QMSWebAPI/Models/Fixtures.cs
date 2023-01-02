using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QMSWebAPI.Models
{
    public class Fixtures
    {
        public int Id { get; set; }
        public string Home { get; set; }
        public string Away { get; set; }
        public string Matchs { get; set; }
        public string MatchDate { get; set; }
        public string MatchTime { get; set; }
        public string Venue { get; set; }
        public string TeamGroup { get; set; }
        public string HomeShort { get; set; }
        public string AwayShort { get; set; }
        public string HomeImageUrl { get; set; }
        public string AwayImageUrl { get; set; }
        public bool Status { get; set; }
        public DateTime MatchFullDate { get; set; }


     


    }


    public class FixturesObj
    {

        public List<Fixtures> FixturesList = new List<Fixtures>();

    

    }


    public class QuizAsnObj
    {

        public List<QuizAsnList> FixturesList = new List<QuizAsnList>();



    }


}