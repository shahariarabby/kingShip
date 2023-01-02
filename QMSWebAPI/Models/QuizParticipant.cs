using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QMSWebAPI.Models
{
    public class QuizParticipant
    {

        public int UserId { get; set; }
        public int? FixtureId { get; set; }
        public DateTime CreareDate { get; set; }
        public string QuizNameWin { get; set; }
        public string QuizAnswearWin { get; set; }
        public string QuizNameScore { get; set; }
        public int? QuizAnswearScore { get; set; }
        public int? MatchDate { get; set; }
        public int? QuizScoreHome { get; set; }
        public int? QuizScoreAway { get; set; }
        public int? QuizIdWin { get; set; }
        public int? QuizIdScore { get; set; }
      
        public List<QuizParticipantDetails> QmsDetailsInformation { get; set; }
    }
}