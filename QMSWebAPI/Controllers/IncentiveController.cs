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
    public class IncentiveController : ApiController
    {
        IncentiveDAL IncentiveDAL = new IncentiveDAL();

        [HttpGet]
        //[Route("login")]
        public IHttpActionResult GetEmplyoeeInformation(int BarcodeNo)
        {
            IncentiveModel data = this.IncentiveDAL.GetEmplyoeeInformation(BarcodeNo);
            bool isSuccess = false;
            string msg = string.Empty;
           
            List<IncentiveModel> loginModels = new List<IncentiveModel>();
            IncentiveModel incentiveModel = null;

            if (data !=null)
            {
                incentiveModel = new IncentiveModel()
                {
                    EmployeeCode=data.EmployeeCode,
                    EmployeeName = data.EmployeeName,
                    Designation = data.Designation,
                    Department = data.Department,
                    SectionInfo = data.SectionInfo,
                    SubSection = data.SubSection,
                    LineInfo=data.LineInfo,
                    Floor=data.Floor
                };
                loginModels.Add(incentiveModel);
            }
       
          if (data == null)
            {
                isSuccess = false;
                msg = "Invalid Barcode";
               
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
        public IHttpActionResult GetLogin(string UserName, string UserPassword)
        {
            IncentiveModel data = this.IncentiveDAL.CheckLoging(UserName, UserPassword);
            bool isSuccess = false;
            string msg = string.Empty;

            List<IncentiveModel> loginModels = new List<IncentiveModel>();
            IncentiveModel incentiveModel = null;

            if (data != null)
            {
                incentiveModel = new IncentiveModel()
                {
                    EmployeeCode = data.EmployeeCode,
                    EmployeeName = data.EmployeeName,
                    Designation = data.Designation,
                    Department = data.Department,
                    SectionInfo = data.SectionInfo,
                    SubSection = data.SubSection,
                    LineInfo = data.LineInfo,
                    Floor = data.Floor
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
        public IHttpActionResult GetOperationNameWiseEmployeee(int EmpBarcodeNo,int BarcodeNo,int OperationID)
        {
            IncentiveModel data = this.IncentiveDAL.GetOperationNameWiseEmployeee(EmpBarcodeNo,BarcodeNo, OperationID);
            bool isSuccess = false;
            string msg = string.Empty;

            List<IncentiveModel> loginModels = new List<IncentiveModel>();
            IncentiveModel incentiveModel = null;

            if (data != null)
            {
                incentiveModel = new IncentiveModel()
                {
                    EmpBarcodeNo=data.EmpBarcodeNo,
                    EmployeeCode=data.EmployeeCode,
                    EmployeeName = data.EmployeeName,
                    Designation = data.Designation,
                    Department = data.Department,
                    SectionInfo = data.SectionInfo,
                    SubSection = data.SubSection,
                    LineInfo = data.LineInfo,
                    Floor = data.Floor,
                    OperationID=data.OperationID,
                    OperationName=data.OperationName,
                    BarcodeNo=data.BarcodeNo
                };
                loginModels.Add(incentiveModel);
            }

            if (data == null)
            {
                isSuccess = true;
                msg = "Sucess";
             
            }
            else if (true)
            {
                 isSuccess = false;
                msg = "Already Scanned";
            }
            else
            {
                isSuccess = false;
                msg = "Invalid Barcode";
            }
           
            return base.Json(new { isSuccess = isSuccess, msg = msg, userdata = incentiveModel });
        }
        [HttpPost]
        public IHttpActionResult SaveIncentiveData(int EmpBarcodeNo, string EmployeeCode, int OperationID, string OperationName,int BarcodeNo)
        {
            return Json(IncentiveDAL.DataSaveToDatabase(EmpBarcodeNo, EmployeeCode, OperationID, OperationName, BarcodeNo));
        }
        [HttpGet]
         public IHttpActionResult RetriveOperation(string BusinessUnit, string ProductionFloor, string LineNumber)
        {
            return base.Json<IEnumerable<OperationModel>>(this.IncentiveDAL.RetriveOperation(BusinessUnit, ProductionFloor, LineNumber));
           // return base.Json<DataTable>(this.IncentiveDAL.RetriveOperation(BusinessUnit, ProductionFloor, LineNumber));
        }
        [HttpGet]
        // [Route("RetriveDataForCuttingAQLReport")]
        public IHttpActionResult GetDataSave(string EmployeeCode, int OperationID)
        {
            return base.Json(this.IncentiveDAL.GetDataSave(EmployeeCode, OperationID));
        }
        [HttpGet]
        // [Route("RetriveDataForCuttingAQLReport")]
        public IHttpActionResult LocationWiseMeter(int LocationId, int CategoryId)
        {
            return base.Json<DataTable>(this.IncentiveDAL.LocationWiseMeter(LocationId, CategoryId));
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
                ResultResponse list = this.IncentiveDAL.DataSaveToDatabase(RequestorId, CategoryId, BusinessUnitId, LocationId, MeterId, Image, ImagePath);
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
           return base.Json<IEnumerable<CommonModel>>(this.IncentiveDAL.RetriveDataForReportModel());
        }
    }
}
