using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QMSWebAPI.Models
{
    public class QuizParticipantDetails
    {
        public int Id { get; set; }
        public int? QuizParticipantMasterId { get; set; }
        public int? QuizId { get; set; }
        public string QuizName { get; set; }
        public int? QuizAnswear { get; set; }
        public DateTime CreateDate { get; set; }
        public int Home { get; set; }
        public int Away { get; set; }

    }
}