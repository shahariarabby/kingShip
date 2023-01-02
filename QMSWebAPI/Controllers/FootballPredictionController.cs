using Newtonsoft.Json.Linq;
using QMSWebAPI.DAL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QMSWebAPI.Controllers
{
    public class FootballPredictionController : ApiController
    {

        FootballPredictionDAL fixtures = new FootballPredictionDAL();

        ////FootballPredictionDAL toast = await fixtures.GetTodayMatch(BarcodeNumber);

        ////String strTestDate = "02-11-2007";
        ////DateTime coverdate = DateTime.ParseExact(strTestDate, "dd-MM-yyyy", null);
        ////string s = coverdate.ToString(" MMMM yyyy");
        ////string t = string.Format("{0}{1}", coverdate.Day);
        ////string result = t + s;

        //var date = DateTime.Parse("06:45 AM");
        //// Console.WriteLine(date);
        //string times = "06:45 AM";
        //DateTime dt;
        //    if (DateTime.TryParseExact(times, "HH:mm tt", CultureInfo.InvariantCulture,
        //                              DateTimeStyles.None, out dt))
        //    { }
        //    TimeSpan theTime = DateTime.Parse("06:45 AM").TimeOfDay;

        //Date someDate = new DateTime();
        //string timeOfDay = someDate.ToString("hh:mm tt");

        [HttpGet]
        public IHttpActionResult GetTodayMatchInformation()
        {
            

            return Json(fixtures.GetTodayMatch());
        }


        [HttpGet]
        public IHttpActionResult GetAllQuizInformation(string MatchTime)
        {

            return Json(fixtures.GetAllQuiz(MatchTime));
        }


        [HttpGet]
        public IHttpActionResult GetAllQuizOneTimeInformation()
        {

            return Json(fixtures.GetAllQuizOneTime());
        }


        [HttpGet]
        public IHttpActionResult GetAllQuizOneTimeOption()
        {

            return Json(fixtures.GetAllQuizOneTimeOption());
        }

        [HttpPost]
        public IHttpActionResult SaveQuizOneTimeInformation(JArray qMSDataModels)
        {
            return Json(fixtures.QmsDataSaveToDatabase(qMSDataModels.ToString()));
        }

        //[HttpPost]
        //public IHttpActionResult SaveToQmsMasterTable(int UserId, int MasterGenarationId, string TabId, float OperationSMV)
        //{
        //    return Json(fixtures.SaveToQmsMasterTable(UserId, MasterGenarationId, TabId, OperationSMV));
        //}


        [HttpPost]
        public IHttpActionResult QmsDefectEntryDetails(JObject qmsDetailsData)
        {
            return Json(fixtures.SaveDetailsToQms(qmsDetailsData));
        }

        [HttpPost]
        public IHttpActionResult SaveToQuizParticipantMaster(string UserId, string MatchDate, int FixtureId, int QuizIdWin, string QuizAnswearWin, int QuizIdScore, int Home, int Away)
        {
            return Json(fixtures.SaveToQuizParticipantMaster(UserId, MatchDate, FixtureId, QuizIdWin, QuizAnswearWin, QuizIdScore, Home, Away));
        }


        //[HttpPost]
        //public IHttpActionResult SaveToQuizParticipantMaster()
        //{
        //    return Json(fixtures.SaveToQuizParticipantMaster("", "", 12, 2, "", 2, 2, 3));
        //}

        [HttpPost]
       public IHttpActionResult SaveToQuizParticipantOneTimeMaster(string UserId, int QuizNameChampionId, string QuizAnswearChampion, int QuizNameBallId, string QuizAnswearBall, int QuizNameBootId, string QuizAnswearBoot, int QuizNameGlovesId, string QuizAnswearGloves)
     
        {
           return Json(fixtures.SaveToQuizParticipantOneTimeMaster( UserId,  QuizNameChampionId,  QuizAnswearChampion,  QuizNameBallId,  QuizAnswearBall,  QuizNameBootId,  QuizAnswearBoot,  QuizNameGlovesId,  QuizAnswearGloves));
    
        }


        [HttpPost]
        public IHttpActionResult SaveToChangePassword(string UserId, string PassWord, string NewPassWord)      
        {
            return Json(fixtures.SaveToChangePassword( UserId,  PassWord,  NewPassWord));           

        }


        [HttpGet]
        public IHttpActionResult GetAllScoringPolicy()
        {

            return Json(fixtures.GetAllScoringPolicy());
        }


        [HttpGet]
        public IHttpActionResult GetAllGiftPolicy()
        {

            return Json(fixtures.GetAllGiftPolicy());
        }


        [HttpGet]
        public IHttpActionResult GetAllLeaderDashBoardData()
        {

            return Json(fixtures.GetAllLeaderDashBoardData());
        }


        [HttpGet]
        public IHttpActionResult GetAllScoreByUserId(string UserId)
        {

            return Json(fixtures.GetAllScoreByUserId(UserId));
        }
    }
}
