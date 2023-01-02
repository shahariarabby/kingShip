using ExcelDataReader;
using Newtonsoft.Json.Linq;
using QMSWebAPI.DAL;
using QMSWebAPI.Models;
using QMSWebAPI.Models.AttendanceModels;
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

    [RoutePrefix("api/Attendance")]
    public class AttendanceController : ApiController
    {
        AttendanceDAL AttendanceDAL = new AttendanceDAL();


        [HttpGet]
        [Route("GetAttendanceInfoFromKormee")]
        public IHttpActionResult AttendanceInfoFromKormee(string EmpoyeeCode, string Date)
        {

            //AttendanceInfoFromKormee attenInfo = new AttendanceInfoFromKormee();

            return base.Json<IEnumerable<AttendanceInfoModel>>(AttendanceDAL.RetriveDataForReportModel(EmpoyeeCode, Date));
        }
    }
}
