using Newtonsoft.Json.Linq;
using QMSWebAPI.DAL;
using QMSWebAPI.Models;
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

    [RoutePrefix("api/{Controller}")]
    public class EnergyController : ApiController
    {
        EnergyDAL EnergyDAL = new EnergyDAL();

        //[HttpGet]
        //[Route("login")]
        public IHttpActionResult GetLogin(string UserName, string UserPassword)
        {
            LoginModel data = this.EnergyDAL.CheckLoging(UserName, UserPassword);
            bool isSuccess = false;
            string msg = string.Empty;
           
            List<LoginModel> loginModels = new List<LoginModel>();
            LoginModel loginModel = null;
          

            if (data == null)
            {
                isSuccess = false;
                msg = "Please Entry Correct UserName Or Password";
            }
            else
            {
                isSuccess = true;
                msg = "Login Successfully";
            }


            return base.Json(new { isSuccess = isSuccess, msg = msg,UserId=data.UserId, UserName=data.UserName,CreateDate=data.CreateDate,Password=data.Password,Email=data.Email });
        }
        [HttpGet]
       // [Route("RetriveDataForCuttingAQLReport")]
        public IHttpActionResult RetriveBusinessUnit()
        {
            return base.Json<DataTable>(this.EnergyDAL.RetriveBusinessUnit());
        }
        [HttpGet]
        // [Route("RetriveDataForCuttingAQLReport")]
        public IHttpActionResult BusinessUnitWiseLocation(int BusinessUnitId, int CategoryId)
        {
            return base.Json<DataTable>(this.EnergyDAL.BusinessUnitWiseLocation(BusinessUnitId, CategoryId));
        }
        [HttpGet]
        // [Route("RetriveDataForCuttingAQLReport")]
        public IHttpActionResult LocationWiseMeter(int LocationId, int CategoryId)
        {
            return base.Json<DataTable>(this.EnergyDAL.LocationWiseMeter(LocationId, CategoryId));
        }
        [HttpPost]
        public IHttpActionResult SaveEnergyData()
        {
            CommonModel data = new CommonModel();
            List<CommonModel> CommonModels = new List<CommonModel>();
            try
            {

                var ctx = HttpContext.Current.Request;

                // HTTP Respons
                HttpResponseMessage responseMessage = Request.CreateResponse(HttpStatusCode.OK);

                var RequestorId = ctx.Params["RequestorId"];
                var CategoryId = ctx.Params["CategoryId"];
                var BusinessUnitId = ctx.Params["BusinessUnitId"];
                var LocationId = ctx.Params["LocationId"];
                var MeterId = ctx.Params["MeterId"];
               

                if (HttpContext.Current.Request.Files.Count == 0 )
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }

                //read the file
                HttpPostedFile postedFile = HttpContext.Current.Request.Files[0];
                string filename = Path.GetFileName(postedFile.FileName);

                //string filePath = @"D:\PUBLISH PROJECTS\EnergyApi\Upload" + filename;
                string filePath = @"D:\PUBLISH PROJECTS\EnergyApi\Upload\" + filename;

                string urlPath = @"/Upload/" + filename;

                postedFile.SaveAs(filePath);


                CommonModel commonModel = new CommonModel()
                {
                    RequestorId = Int32.Parse(RequestorId),
                    CategoryId = Int32.Parse(CategoryId),
                    BusinessUnitId = Int32.Parse(BusinessUnitId),
                    LocationId = Int32.Parse(LocationId),
                    MeterId = Int32.Parse(MeterId),
                    Image = filePath,
                    ImagePath = urlPath
                };
                var Image = commonModel.Image;
                var ImagePath =commonModel.ImagePath;
                ResultResponse list = this.EnergyDAL.DataSaveToDatabase(RequestorId, CategoryId, BusinessUnitId, LocationId, MeterId, Image, ImagePath);
                return base.Json<ResultResponse>(list);
                #region old
                //var ctx = HttpContext.Current;

                //var root = ctx.Server.MapPath("~/Images/Energy/");
                //var provider = new MultipartFormDataStreamProvider(root);

                //await  Request.Content.ReadAsMultipartAsync(provider);
                //foreach (var file in provider.FileData)
                //{
                //    var name = file.Headers.ContentDisposition.FileName;
                //    name = name.Trim('"');
                //    var localfilename = file.LocalFileName;
                //    var filePath = Path.Combine(root,name);
                //    File.Move(localfilename,filePath);

                //}
                #endregion
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }
        [HttpGet]
        public IHttpActionResult GetReport()
        {
           return base.Json<IEnumerable<CommonModel>>(this.EnergyDAL.RetriveDataForReportModel());
        }
    }
}
