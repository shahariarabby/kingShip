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
    //[EnableCors(origins:"*",headers:"*",methods:"*")]

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
        //[Route("login")]
        public IHttpActionResult GetLoginManagement(string UserName, string UserPassword)
        {
            LoginModel data = this.KinshipDAL.CheckLogingManagement(UserName, UserPassword);
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
                    BusinessUnitId = data.BusinessUnitId,
                    BusinessUnit = data.BusinessUnit,
                    Designation = data.Designation,
                    Department = data.Department,
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
        public IHttpActionResult Category(int BusinessUnitId, string Language)
        {
            dynamic productionList = null;
           
            productionList = this.KinshipDAL.Category(BusinessUnitId, Language);
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
        public IHttpActionResult CategoryM(int BusinessUnitId, string Language)
        {
            dynamic productionList = null;

            productionList = this.KinshipDAL.CategoryM(BusinessUnitId, Language);
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
        // [Route("RetriveDataForCuttingAQLReport")]
        public IHttpActionResult Subcategory(int BusinessUnitId, int CategoryId, string Language)
        {
            dynamic productionList = null;

            productionList = this.KinshipDAL.Subcategory(BusinessUnitId, CategoryId, Language);
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
        // [Route("RetriveDataForCuttingAQLReport")]
        public IHttpActionResult SubcategoryM(int BusinessUnitId, int CategoryId, string Language)
        {
            dynamic productionList = null;

            productionList = this.KinshipDAL.SubcategoryM(BusinessUnitId, CategoryId, Language);
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
        [HttpGet]
        //[Route("login")]
        public IHttpActionResult GetUserWiseSupervisorM(string UserId)
        {
            CategoryModel data = this.KinshipDAL.GetUserWiseSupervisorM(UserId);
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
                var Location = ctx.Params["Location"];
                var IsVisible = ctx.Params["IsVisible"];


                if (HttpContext.Current.Request.Files.Count == 0)
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }

                //read the file
                HttpPostedFile postedFile = HttpContext.Current.Request.Files[0];
                string filename = Path.GetFileName(postedFile.FileName);
                string filePath = null;
                string urlPath = null;
                if (filename !="")
                {
                    //filePath = @"D:\PUBLISH PROJECTS\EnergyApi\GrivanceUpload" + filename;
                    filePath = @"D:\PUBLISH PROJECTS\EnergyApi\GrivanceUpload\" + filename;

                     urlPath = @"/GrivanceUpload/" + filename;

                    postedFile.SaveAs(filePath);
                }


                CommonModel commonModel = new CommonModel()
                {
                    UserId = UserId,
                    CategoryId = Int32.Parse(CategoryId),
                    SubCategoryId = Int32.Parse(SubCategoryId),
                    BusinessUnitId = Int32.Parse(BusinessUnitId),
                    SupervisorId = SupervisorId,
                    Issue=Issue,
                    Image = filePath,
                    ImagePath = urlPath,
                    Location = Location,
                    IsVisible = Int32.Parse(IsVisible)
                };
                var Image = "";
                var ImagePath = "";
                if (commonModel.Image !=null)
                {
                     Image = commonModel.Image;
                     ImagePath = commonModel.ImagePath;
                }
                else
                {
                     Image = "UploadlaptopScreen.JPG";
                    ImagePath = "UploadlaptopScreen.JPG";
                }
                ResultResponse list = this.KinshipDAL.DataSaveToDatabase(UserId, CategoryId, SubCategoryId, BusinessUnitId, SupervisorId, Issue, Image, ImagePath,Location,IsVisible);
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
        [HttpPost]
        public IHttpActionResult SaveGrivanceDataM()
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
                var Location = ctx.Params["Location"];
                var CreateDate = ctx.Params["CreateDate"];
                var IsVisible = ctx.Params["IsVisible"];


                if (HttpContext.Current.Request.Files.Count == 0)
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }
                //string filePath = null;
                //string urlPath = null;
                //var files = new List<string>();
                //var urlPathfiles = new List<string>();
                //if (ctx.Files.Count>0)
                //{

                //    foreach (string file in ctx.Files)
                //    {
                //        var postedFile = ctx.Files[file];
                //        string filename = Path.GetFileName(postedFile.FileName);
                //        filePath = @"D:\PUBLISH\EnergyApi\GrivanceUploadM\" + filename;
                //        urlPath = @"/GrivanceUploadM/" + filename;
                //        postedFile.SaveAs(filePath);
                //        files.Add(filePath);
                //        urlPathfiles.Add(urlPath);
                //    }
                //}
               // read the file
                 HttpPostedFile postedFile = HttpContext.Current.Request.Files[0];
                string filename = Path.GetFileName(postedFile.FileName);
                string filePath = null;
                string urlPath = null;
                if (filename != "")
                {
                    //filePath = @"D:\PUBLISH PROJECTS\EnergyApi\GrivanceUpload" + filename;
                    filePath = @"D:\PUBLISH PROJECTS\EnergyApi\GrivanceUploadM\" + filename;

                    urlPath = @"/GrivanceUploadM/" + filename;

                    postedFile.SaveAs(filePath);
                }
                CommonModel commonModel = new CommonModel()
                {
                    UserId = UserId,
                    CategoryId = Int32.Parse(CategoryId),
                    SubCategoryId = Int32.Parse(SubCategoryId),
                    BusinessUnitId = Int32.Parse(BusinessUnitId),
                    SupervisorId = SupervisorId,
                    Issue = Issue,
                    Image = filePath,
                    ImagePath = urlPath,
                    //Image = new JavaScriptSerializer().Serialize(files),
                    //ImagePath = new JavaScriptSerializer().Serialize(urlPathfiles),
                    Location = Location,
                    CreateDate = CreateDate,
                    IsVisible = Int32.Parse(IsVisible)
                };
                var Image = "";
                var ImagePath = "";
                if (commonModel.Image != null)
                {
                    Image = commonModel.Image;
                    ImagePath = commonModel.ImagePath;
                }
                else
                {
                    Image = "UploadlaptopScreen.JPG";
                    ImagePath = "UploadlaptopScreen.JPG";
                }
                ResultResponse list = this.KinshipDAL.DataSaveToDatabaseM(UserId, CategoryId, SubCategoryId, BusinessUnitId, SupervisorId, Issue, Image, ImagePath, Location, CreateDate, IsVisible);
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
        public IHttpActionResult GetReportM(string UserId)
        {
            dynamic productionList = null;

            productionList = this.KinshipDAL.RetriveDataForReportModelM(UserId);
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
        [HttpGet]
        public IHttpActionResult GetCommentM(int GRVRequesMastertId)
        {
            dynamic productionList = null;

            productionList = this.KinshipDAL.GetCommentM(GRVRequesMastertId);
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
        public IHttpActionResult GetUnreadComment(string UserId)
        {
            dynamic productionList = null;

            productionList = this.KinshipDAL.GetUnreadComment(UserId);
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
        public IHttpActionResult GetUnreadCommentM(string UserId)
        {
            dynamic productionList = null;

            productionList = this.KinshipDAL.GetUnreadCommentM(UserId);
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
        [HttpPost]
        public IHttpActionResult UpdateUnreadComment(string GRVRequesMastertId)
        {
           
            ResultResponse list = this.KinshipDAL.GetUpdateUnreadComment(GRVRequesMastertId);
            return base.Json<ResultResponse>(list);
         }
        [HttpPost]
        public IHttpActionResult UpdateUnreadCommentM(string GRVRequesMastertId)
        {

            ResultResponse list = this.KinshipDAL.GetUpdateUnreadCommentM(GRVRequesMastertId);
            return base.Json<ResultResponse>(list);
        }
        [HttpPost]
        public IHttpActionResult UpdateReply(string GRVRequesMastertId, string Reply)
        {

            ResultResponse list = this.KinshipDAL.UpdateReply(GRVRequesMastertId, Reply);
            return base.Json<ResultResponse>(list);
        }
        [HttpPost]
        public IHttpActionResult UpdateReplyM(string GRVRequesMastertId, string Reply)
        {

            ResultResponse list = this.KinshipDAL.UpdateReplyM(GRVRequesMastertId, Reply);
            return base.Json<ResultResponse>(list);
        }
        [HttpPost]
        public IHttpActionResult GRVUpdateStatusM(string GRVRequesMastertId, string CloseStatus)
        {

            ResultResponse list = this.KinshipDAL.GRVUpdateStatusM(GRVRequesMastertId, CloseStatus);
            return base.Json<ResultResponse>(list);
        }

        [HttpGet]
        public IHttpActionResult GRVGetVoiceReportAPI()
        {
            dynamic productionList = null;

            productionList = this.KinshipDAL.GRVGetVoiceReportAPI();
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
