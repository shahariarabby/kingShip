using QMSWebAPI.DAL;
using QMSWebAPI.Models.StockLot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QMSWebAPI.Controllers
{
    [RoutePrefix("api/KinshipEvent")]
    public class KinshipEventController : ApiController
    {

        KinshipEventDAL kinshipEventDAL = new KinshipEventDAL();


        [HttpGet]
        [Route("GetAllEvent")]
        public IHttpActionResult GetAllEvent()
        {

            return Json(kinshipEventDAL.GetAllEvent());
        }

        [HttpGet]
        [Route("GetInstructions")]
        public IHttpActionResult GetInstructions(int type)
        {

            return Json(kinshipEventDAL.GetInstructions(type));
        }



        [HttpPost]
        [Route("Save")]
        public IHttpActionResult SaveOrder(Registration registration)
        {
            try
            {
                return Json(kinshipEventDAL.Save(registration));
            }
            catch (Exception e)
            {

                return base.Json(e.Message);
                //throw e;
            }

        }




    }
}
