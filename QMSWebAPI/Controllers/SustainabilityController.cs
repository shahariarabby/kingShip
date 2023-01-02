using ExcelDataReader;
using Newtonsoft.Json.Linq;
using QMSWebAPI.DAL;
using QMSWebAPI.Models;
using QMSWebAPI.Models.SustainabilityModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
//using System.Web.Mvc;


namespace QMSWebAPI.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]

    [RoutePrefix("api/Sustainability")]
    public class SustainabilityController : ApiController
    {
        SustainabilityDAL SustainabilityDAL = new SustainabilityDAL();

        #region Sustainability Survey
        [HttpPost]
        [Route("SaveSustainabilitySurvey")]
        public IHttpActionResult SaveSustainabilitySurvey(List<SustainabilitySurvey> sustainabilitySurvey)
        {
            try
            {
                return base.Json(this.SustainabilityDAL.SaveSustainabilitySurvey(sustainabilitySurvey));
            }
            catch (Exception e)
            {

                return base.Json(e.Message);
                //throw e;
            }

        }

        [HttpGet]
        [Route("GetSustainabilitySurveyByUser")]
        public IHttpActionResult GetSustainabilitySurveyByUser(string UserId)
        {
            try
            {
                return base.Json(this.SustainabilityDAL.GetSustainabilitySurveyByUser(UserId));
            }
            catch (Exception e)
            {

                return base.Json(e.Message);
                //throw e;
            }

        }

        #endregion

        #region Daily Task

        [HttpGet]
        [Route("GetDailyTaskListAndDetails")]
        public IHttpActionResult GetDailyTaskListAndDetails()
        {
            try
            {
                return Json(this.SustainabilityDAL.GetDailyTaskListAndDetails());
            }
            catch (Exception e)
            {
                return base.Json(e.Message);
            }
        }

        [HttpGet]
        [Route("GetDailyTaskListAndDetailsSimple")]
        public IHttpActionResult GetDailyTaskListAndDetailsSimple()
        {
            try
            {
                //return Json(this.SustainabilityDAL.GetDailyTaskListAndDetailsSimple());
                var type = "S";
                return Json(this.SustainabilityDAL.GetDailyTaskListAndDetailsAllType(type));
            }
            catch (Exception e)
            {
                return base.Json(e.Message);
            }
        }

        [HttpGet]
        [Route("GetDailyTaskListAndDetailsMedium")]
        public IHttpActionResult GetDailyTaskListAndDetailsMedium()
        {
            try
            {
                //return Json(this.SustainabilityDAL.GetDailyTaskListAndDetailsMedium());
                var type = "M";
                return Json(this.SustainabilityDAL.GetDailyTaskListAndDetailsAllType(type));
            }
            catch (Exception e)
            {
                return base.Json(e.Message);
            }
        }

        [HttpGet]
        [Route("GetDailyTaskListAndDetailsHard")]
        public IHttpActionResult GetDailyTaskListAndDetailsHard()
        {
            try
            {

                //return Json(this.SustainabilityDAL.GetDailyTaskListAndDetailsHard());
                var type = "H";
                return Json(this.SustainabilityDAL.GetDailyTaskListAndDetailsAllType(type));
            }
            catch (Exception e)
            {
                return base.Json(e.Message);
            }
        }

        [HttpPost]
        [Route("SaveDailyTask")]
        public IHttpActionResult SaveDailyTask(DailyTask dailyTask)
        {
            try
            {
                return base.Json(this.SustainabilityDAL.SaveDailyTask(dailyTask));
            }
            catch (Exception e)
            {

                return base.Json(e.Message);
                //throw e;
            }

        }


        [HttpPost]
        [Route("SaveDailyTaskList")]
        public IHttpActionResult SaveDailyTaskList(DailyTaskList dailyTaskList)
        {
            try
            {
                return base.Json(this.SustainabilityDAL.SaveDailyTaskList(dailyTaskList));
            }
            catch (Exception e)
            {

                return base.Json(e.Message);
                //throw e;
            }

        }

        [HttpPost]
        [Route("SaveDailyTaskListDetails")]
        public IHttpActionResult SaveDailyTaskListDetails(DailyTaskListDetails dailyTaskListDetails)
        {
            try
            {
                return base.Json(this.SustainabilityDAL.SaveDailyTaskListDetails(dailyTaskListDetails));
            }
            catch (Exception e)
            {

                return base.Json(e.Message);
                //throw e;
            }

        }


        #endregion

        #region Monthly Task

        [HttpGet]
        [Route("GetMonthlyTaskList")]
        public IHttpActionResult GetMonthlyTaskList()
        {
            try
            {
                return Json(this.SustainabilityDAL.GetMonthlyTaskList());
            }
            catch (Exception e)
            {
                return base.Json(e.Message);
            }
        }

        [HttpGet]
        [Route("GetMonthlyTaskListAndDetails")]
        public IHttpActionResult GetMonthlyTaskListAndDetails(string userId)
        {
            try
            {
                return Json(this.SustainabilityDAL.GetMonthlyTaskListAndDetailsRandomlyNew(userId));
            }
            catch (Exception e)
            {
                return base.Json(e.Message);
            }
        }

        [HttpPost]
        [Route("SaveMonthlyTask")]
        public IHttpActionResult SaveMonthlyTask(MonthlyTask monthlyTask)
        {
            try
            {
                return base.Json(this.SustainabilityDAL.SaveMonthlyTaskNew(monthlyTask));
            }
            catch (Exception e)
            {

                return base.Json(e.Message);
                //throw e;
            }

        }


        [HttpPost]
        [Route("SaveMonthlyTaskList")]
        public IHttpActionResult SaveMonthlyTaskList(MonthlyTaskList monthlyTaskList)
        {
            try
            {
                return base.Json(this.SustainabilityDAL.SaveMonthlyTaskList(monthlyTaskList));
            }
            catch (Exception e)
            {

                return base.Json(e.Message);
                //throw e;
            }

        }

        [HttpPost]
        [Route("SaveMonthlyTaskListDetails")]
        public IHttpActionResult SaveMonthlyTaskListDetails(MonthlyTaskListDetails monthlyTaskListDetails)
        {
            try
            {
                return base.Json(this.SustainabilityDAL.SaveMonthlyTaskListDetails(monthlyTaskListDetails));
            }
            catch (Exception e)
            {

                return base.Json(e.Message);
                //throw e;
            }

        }

        [HttpGet]
        [Route("GetTaskdisableorNot")]
        public IHttpActionResult GetTaskdisableorNot(long monthlyTaskId, string userId)
        {
            try
            {
                return Json(this.SustainabilityDAL.GetTaskdisableorNot(monthlyTaskId,userId));
            }
            catch (Exception e)
            {
                return base.Json(e.Message);
            }
        }

        #endregion

        #region Sustainability ContactUs

        [HttpPost]
        [Route("SaveSustainabilityContactUs")]
        public IHttpActionResult SaveSustainabilityContactUs()
        {
            try
            {
                return base.Json(this.SustainabilityDAL.SaveSustainabilityContactUs());
            }
            catch (Exception e)
            {

                return base.Json(e.Message);
                //throw e;
            }

        }

        #endregion


        #region Daily Tips Task

        [HttpGet]
        [Route("GetDailyTipsTask")]
        public IHttpActionResult GetDailyTipsTask()
        {
            try
            {
                return Json(this.SustainabilityDAL.GetDailyTipsTask());
            }
            catch (Exception e)
            {
                return base.Json(e.Message);
            }
        }

        [HttpPost]
        [Route("SaveDailyTips")]
        public IHttpActionResult SaveDailyTips(DailyTips dailyTips)
        {
            try
            {
                return base.Json(this.SustainabilityDAL.SaveDailyTips(dailyTips));
            }
            catch (Exception e)
            {

                return base.Json(e.Message);
                //throw e;
            }

        }

        [HttpPost]
        [Route("SaveDailyTipsTask")]
        public IHttpActionResult SaveDailyTipsTask(DailyTipsTask dailyTipsTask)
        {
            try
            {
                return base.Json(this.SustainabilityDAL.SaveDailyTipsTask(dailyTipsTask));
            }
            catch (Exception e)
            {

                return base.Json(e.Message);
                //throw e;
            }

        }

        #endregion

        #region Monthly Tips Task

        [HttpGet]
        [Route("GetMonthlyTipsTaskListByeType")]
        public IHttpActionResult GetMonthlyTipsTaskListByeType(string type,string userId)
        {
            try
            {
                return Json(this.SustainabilityDAL.GetMonthlyTipsTaskListByType(type,userId));
            }
            catch (Exception e)
            {
                return base.Json(e.Message);
            }
        }

        [HttpGet]
        [Route("GetMonthlyTipsTask")]
        public IHttpActionResult GetMonthlyTipsTask()
        {
            try
            {
                return Json(this.SustainabilityDAL.GetMonthlyTipsTask());
            }
            catch (Exception e)
            {
                return base.Json(e.Message);
            }
        }

        [HttpPost]
        [Route("SaveMonthlyTips")]
        public IHttpActionResult SaveMonthlyTips(MonthlyTips monthlyTips)
        {
            try
            {
                return base.Json(this.SustainabilityDAL.SaveMonthlyTips(monthlyTips));
            }
            catch (Exception e)
            {

                return base.Json(e.Message);
                //throw e;
            }

        }

        [HttpPost]
        [Route("SaveMonthlyTipsTask")]
        public IHttpActionResult SaveMonthlyTipsTask(MonthlyTipsTask monthlyTipsTask)
        {
            try
            {
                return base.Json(this.SustainabilityDAL.SaveMonthlyTipsTask(monthlyTipsTask));
            }
            catch (Exception e)
            {

                return base.Json(e.Message);
                //throw e;
            }

        }

        #endregion

        #region Task Result

        [HttpGet]
        [Route("GetResultOfTaskByUser")]
        public IHttpActionResult GetResultOfTaskByUser(string userId)
        {
            try
            {
                return Json(this.SustainabilityDAL.GetResultOfTaskByUser(userId));
            }
            catch (Exception e)
            {
                return base.Json(e.Message);
            }
        }

        #endregion

        #region Excel Data Upload


        [Route("ExcelUploadForDailyTaskList")]
        [HttpPost]
        public IHttpActionResult ExcelUploadForDailyTaskList()
        {
            try
            {
                return base.Json(this.SustainabilityDAL.ExcelUploadForDailyTaskList());
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Route("ExcelUploadForDailyTaskListDetails")]
        [HttpPost]
        public IHttpActionResult ExcelUploadForDailyTaskListDetails()
        {
            try
            {
                return base.Json(this.SustainabilityDAL.ExcelUploadForDailyTaskListDetails());
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Route("ExcelUploadForMonthlyTaskList")]
        [HttpPost]
        public IHttpActionResult ExcelUploadForMonthlyTaskList()
        {
            try
            {
                return base.Json(this.SustainabilityDAL.ExcelUploadForMonthlyTaskList());
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Route("ExcelUploadForMonthlyTaskListDetails")]
        [HttpPost]
        public IHttpActionResult ExcelUploadForMonthlyTaskListDetails()
        {
            try
            {
                return base.Json(this.SustainabilityDAL.ExcelUploadForMonthlyTaskListDetails());
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Route("ExcelUploadForDailyTips")]
        [HttpPost]
        public IHttpActionResult ExcelUploadForDailyTips()
        {
            try
            {
                return base.Json(this.SustainabilityDAL.ExcelUploadForDailyTips());
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Route("ExcelUploadForMonthlyTips")]
        [HttpPost]
        public IHttpActionResult ExcelUploadForMonthlyTips()
        {
            try
            {
                return base.Json(this.SustainabilityDAL.ExcelUploadForMonthlyTips());
            }
            catch (Exception)
            {

                throw;
            }
        }


        #endregion
    }
}
