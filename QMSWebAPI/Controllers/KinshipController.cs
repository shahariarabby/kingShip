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
    public class KinshipController : ApiController
    {
        KinshipDAL KinshipDAL = new KinshipDAL();

        [HttpGet]
        //[Route("login")]
        public IHttpActionResult GetLogin(string UserName, string UserPassword)
        {
            LoginModel data = this.KinshipDAL.CheckLoging(UserName, UserPassword);
            bool isSuccess = false;
            string msg = string.Empty;

            List<LoginModel> loginModels = new List<LoginModel>();
            LoginModel incentiveModel = null;

            if (data != null)
            {
                incentiveModel = new LoginModel()
                {
                    SQId = data.SQId,
                    UserName = data.UserName,
                    BusinessUnitId=data.BusinessUnitId
                };
                loginModels.Add(incentiveModel);
            }

            if (data == null)
            {
                isSuccess = false;
                msg = "Invalid Password and UserName";

            }
            else
            {
                isSuccess = true;
                msg = "Login Successfully";
            }


            return base.Json(new { isSuccess = isSuccess, msg = msg, userdata = incentiveModel });
        }
        [HttpGet]
        // [Route("RetriveDataForCuttingAQLReport")]
        public IHttpActionResult Category(int BusinessUnitId)
        {
            dynamic productionList = null;
           
            productionList = this.KinshipDAL.Category(BusinessUnitId);
            dynamic Data = (List<CategoryModel>)productionList;
            bool isSuccess = false;
            string msg = string.Empty;
            string Status = string.Empty;

            if (productionList == null)
            {
                isSuccess = false;
                msg = "Failed";
                Status = "500";

            }
            else
            {
                isSuccess = true;
                msg = "Success";
                Status = "200";
            }


            return base.Json(new { isSuccess = isSuccess, msg = msg, Status= Status, Data });
        }
        [HttpGet]
        // [Route("RetriveDataForCuttingAQLReport")]
        public IHttpActionResult Subcategory(int BusinessUnitId, int CategoryId)
        {
            dynamic productionList = null;

            productionList = this.KinshipDAL.Subcategory(BusinessUnitId, CategoryId);
            dynamic Data = (List<CategoryModel>)productionList;
            bool isSuccess = false;
            string msg = string.Empty;
            string Status = string.Empty;

            if (productionList == null)
            {
                isSuccess = false;
                msg = "Failed";
                Status = "500";

            }
            else
            {
                isSuccess = true;
                msg = "Success";
                Status = "200";
            }


            return base.Json(new { isSuccess = isSuccess, msg = msg, Status = Status, Data });
        }
        [HttpGet]
        //[Route("login")]
        public IHttpActionResult GetUserWiseSupervisor(string UserId)
    {
            CategoryModel data = this.KinshipDAL.GetUserWiseSupervisor(UserId);
            bool isSuccess = false;
            string msg = string.Empty;

            List<CategoryModel> loginModels = new List<CategoryModel>();
            CategoryModel incentiveModel = null;

            if (data != null)
            {
                incentiveModel = new CategoryModel()
                {
                    SupervisorId = data.SupervisorId,
                    UserId = data.UserId,
                    Name = data.Name
                 };
                loginModels.Add(incentiveModel);
            }

            if (data == null)
            {
                isSuccess = false;
                msg = "Data Not Found";

            }
            else
            {
                isSuccess = true;
                msg = "Data Found";
            }


            return base.Json(new { isSuccess = isSuccess, msg = msg, userdata = incentiveModel });
        }
        [HttpPost]
        public IHttpActionResult SaveGrivanceData()
        {
            CommonModel data = new CommonModel();
            List<CommonModel> CommonModels = new List<CommonModel>();
            try
            {

                var ctx = HttpContext.Current.Request;

                // HTTP Respons
                HttpResponseMessage responseMessage = Request.CreateResponse(HttpStatusCode.OK);

                var UserId = ctx.Params["UserId"];
                var CategoryId = ctx.Params["CategoryId"];
                var SubCategoryId = ctx.Params["SubCategoryId"];
                var BusinessUnitId = ctx.Params["BusinessUnitId"];
                var SupervisorId = ctx.Params["SupervisorId"];
                var Issue = ctx.Params["Issue"];


                if (HttpContext.Current.Request.Files.Count == 0)
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }

                //read the file
                HttpPostedFile postedFile = HttpContext.Current.Request.Files[0];
                string filename = Path.GetFileName(postedFile.FileName);

                //string filePath = @"D:\PUBLISH PROJECTS\EnergyApi\Upload" + filename;
                string filePath = @"D:\PUBLISH PROJECTS\EnergyApi\GrivanceUpload\" + filename;

                string urlPath = @"/GrivanceUpload/" + filename;

                postedFile.SaveAs(filePath);


                CommonModel commonModel = new CommonModel()
                {
                    UserId = UserId,
                    CategoryId = Int32.Parse(CategoryId),
                    SubCategoryId = Int32.Parse(SubCategoryId),
                    BusinessUnitId = Int32.Parse(BusinessUnitId),
                    SupervisorId = SupervisorId,
                    Issue=Issue,
                    Image = filePath,
                    ImagePath = urlPath
                };
                var Image = commonModel.Image;
                var ImagePath = commonModel.ImagePath;
                ResultResponse list = this.KinshipDAL.DataSaveToDatabase(UserId, CategoryId, SubCategoryId, BusinessUnitId, SupervisorId, Issue, Image, ImagePath);
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
        public IHttpActionResult GetReport(string UserId)
        {
            dynamic productionList = null;

            productionList = this.KinshipDAL.RetriveDataForReportModel(UserId);
            dynamic Data = (List<KinshipModel>)productionList;
            bool isSuccess = false;
            string msg = string.Empty;
            string Status = string.Empty;
            // return base.Json<IEnumerable<KinshipModel>>(this.KinshipDAL.RetriveDataForReportModel(UserId));
            // return base.Json<IEnumerable<KinshipModel>>(this.KinshipDAL.RetriveDataForReportModel(UserId));
            if (productionList == null)
            {
                isSuccess = false;
                msg = "Failed";
                Status = "500";

            }
            else
            {
                isSuccess = true;
                msg = "Success";
                Status = "200";
            }
            return base.Json(new { isSuccess = isSuccess, msg = msg, Status = Status, Data });
        }
        [HttpGet]
        public IHttpActionResult GetComment(int GRVRequesMastertId)
        {
            dynamic productionList = null;

            productionList = this.KinshipDAL.GetComment(GRVRequesMastertId);
            dynamic Data = (List<KinshipModel>)productionList;
            bool isSuccess = false;
            string msg = string.Empty;
            string Status = string.Empty;
            // return base.Json<IEnumerable<KinshipModel>>(this.KinshipDAL.RetriveDataForReportModel(UserId));
            // return base.Json<IEnumerable<KinshipModel>>(this.KinshipDAL.RetriveDataForReportModel(UserId));
            if (productionList == null)
            {
                isSuccess = false;
                msg = "Failed";
                Status = "500";

            }
            else
            {
                isSuccess = true;
                msg = "Success";
                Status = "200";
            }
            return base.Json(new { isSuccess = isSuccess, msg = msg, Status = Status, Data });
        }
    }
}
