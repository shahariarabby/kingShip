using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QMSWebAPI.Models
{
    public class QuizAsnList
    {
        public int Id { get; set; }
        public int QuizId { get; set; }
        public string PlayerName { get; set; }
        public string PlayerCountry { get; set; }
        public int SLNo { get; set; }
     
    }


    public class QuizList
    {
     
            public List<QuizAsnList> ChappionList = new List<QuizAsnList>();
            public List<QuizAsnList> GoldenBollList = new List<QuizAsnList>();
            public List<QuizAsnList> GoldenBootlList = new List<QuizAsnList>();
            public List<QuizAsnList> GoldenGloveslList = new List<QuizAsnList>();

    

    }
}