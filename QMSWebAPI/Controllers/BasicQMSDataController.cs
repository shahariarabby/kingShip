using Newtonsoft.Json.Linq;
using QMSWebAPI.DAL;
using QMSWebAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace QMSWebAPI.Controllers
{
    [RoutePrefix("api/{Controller}")]
    public class BasicQMSDataController : ApiController
    {
        BasicQMSDAL basicDal = new BasicQMSDAL();

        [HttpGet]
        public IHttpActionResult GetBarcodeInformation(int BusinessUnit,string BarcodeNumber)
        {
            return Json(basicDal.GetBarcodeInformation(BusinessUnit,BarcodeNumber));
        }

        [HttpPost]
        public IHttpActionResult SaveDataToQmsTable(JArray qMSDataModels)
        {
            return Json(basicDal.QmsDataSaveToDatabase(qMSDataModels.ToString()));
        }

        [HttpPost]
        public IHttpActionResult SaveToQmsMasterTable(int UserId, int MasterGenarationId, string TabId,float OperationSMV)
        {
            return Json(basicDal.SaveToQmsMasterTable(UserId,MasterGenarationId,TabId,OperationSMV));
        }

        [HttpPost]
        public IHttpActionResult QmsDefectEntryDetails(JObject qmsDetailsData)
        {
            return Json(basicDal.SaveDetailsToQms(qmsDetailsData));
        }

        [HttpPost]
        public IHttpActionResult DeleteQmsDataDetais(JObject qmsDetailsData)
        {
            return Json(basicDal.DeleteQmsDetailsData(qmsDetailsData));
        }

        [HttpPost]
        public IHttpActionResult DeleteDataQMS(JArray dataList)
        {
            return Json(basicDal.DeleteDataFromQms(dataList.ToString()));
        }

        [HttpGet]
        public IHttpActionResult RetriveDataForReport(string StartDate, string EndDate)
        {
            return Json(basicDal.RetriveDataForReport(StartDate, EndDate));
        }

        [HttpGet]
        public IHttpActionResult CelsiusDataForReport(string StartDate, string EndDate)
        {
            return Json(basicDal.CelsiusDataForReport(StartDate, EndDate));
        }

        [HttpGet]
        public IHttpActionResult BirichinaDataForReport(string StartDate, string EndDate)
        {
            return Json(basicDal.BirichinaDataForReport(StartDate, EndDate));
        }

        [HttpGet]
        public IHttpActionResult CheckoginQms(string UserName, string UserPassword)
        {
            return Json(basicDal.CheckLogingQms(UserName, UserPassword));
        }
        [HttpGet]
        public IHttpActionResult PositionGridListAPI(int StyleId,int Status)
        {
            SilhouteeModel silhouteeModel = basicDal.APIGetGridList(StyleId, Status);
            silhouteeModel.BaseServerPath= System.Web.Hosting.HostingEnvironment.MapPath("~/StyleUpload");
            return Json(silhouteeModel);
        }

        [HttpGet]
        public IHttpActionResult DefectListByPositionAPI(int StyleId, int Status)
        {
            List<DefectPositionModel> defectPositionModels = basicDal.PositionBasedDefectList(StyleId, Status);
            PositionListForAPI position = new PositionListForAPI();
            if (defectPositionModels.Count < 1)
            {
                position.IsSuccess = false;
                position.Messgae = "No Defect Position Found";
            }
            else
            {
                position.IsSuccess = true;
                position.Messgae = defectPositionModels.Count+ "Defect Position Found";
                position.DefectPositions = defectPositionModels;
            }
            return Json(position);
        }
        [HttpGet]
        public IHttpActionResult GetAllCommonData(int status)
        {
            return Json(basicDal.AllCommonModel(status));
        }

        //[HttpGet]
        //public FileCo GetDownloadFile(string DownloadableFIle)
        //{
        //    Byte[] b;
        //    b = System.IO.File.ReadAllBytes(DownloadableFIle);
        //    return File(b, "image/jpeg"); 
        //}

        [HttpGet]
        public IHttpActionResult GetDashboardDataForLinkIn()
        {
            var IP = basicDal.GetIp();
            var modelList = new List<LineTvModel>();

            var linelist = basicDal.LineTvList(IP);
            var dataList = new List<dynamic>();

            if (linelist != null)
            {
                foreach (var item in linelist)
                {
                    //var totalProduction = basicDal.TotalProductionForCelsius(item.PunitName, item.Line);
                    //var getSMV = basicDal.SMVValueForCelsius(item.PunitName, item.Line);
                    //var getSAH = Convert.ToDouble(totalProduction) * getSMV;
                    var totalDefect = basicDal.TotalDefectForCelsius(item.PunitName, item.Line);
                    var defectivePieces = basicDal.DefectivePiecesForCelsius(item.PunitName, item.Line);
                    //double percentage = (Convert.ToDouble(defectivePieces) / Convert.ToDouble(totalProduction)) * 100;
                    //percentage = percentage.ToString() == "NaN" ? 0 : percentage;
                    //double dhu = (Convert.ToDouble(totalDefect) / Convert.ToDouble(totalProduction)) * 100;
                    //dhu = dhu.ToString() == "NaN" ? 0 : dhu;

                    DateTime start = DateTime.Parse("8:00");
                    DateTime end = DateTime.Parse("19:00");


                    int hourCount = 0;

                    //if (DateTime.Now.DayOfWeek != DayOfWeek.Friday)
                    //{
                    //    if (DateTime.Now.TimeOfDay.Hours >= 8 && DateTime.Now.TimeOfDay.Hours < 19)
                    //    {
                    //        if (DateTime.Now.TimeOfDay.Hours < 13 || DateTime.Now.TimeOfDay.Hours >= 14)
                    //        {
                    //            if (DateTime.Now.TimeOfDay.Hours >= 14)
                    //            {
                    //                count = DateTime.Now.Hour - start.Hour - 1;
                    //            }
                    //            else
                    //            {
                    //                count = DateTime.Now.Hour - start.Hour;
                    //            }

                    //        }

                    //        else
                    //        {
                    //            count = DateTime.Now.Hour - start.Hour;
                    //        }

                    //    }
                    //}

                    //DateTime.Now.TimeOfDay.Hours

                    if(DateTime.Now.TimeOfDay.Hours < 13)
                    {
                        hourCount = DateTime.Now.Hour - start.Hour + 1;
                    }
                    else if (DateTime.Now.TimeOfDay.Hours < end.Hour)
                    {
                        hourCount = DateTime.Now.Hour - start.Hour;
                    }
                    else
                    {
                        hourCount = end.Hour - start.Hour - 1;
                    }

                    //var hourCount = 1;

                    //if (count == 0)
                    //{
                    //    hourCount = 1;
                    //}
                    //else
                    //{
                    //    hourCount = count;
                    //}

                    double clockMinute = item.MachineNumber * hourCount * 60;
                    //var efficiency = (getSAH / clockMinute) * 100;

                    var subData = basicDal.GetDashboardDataForCelsius(item.BusinessUnitId, item.PunitName, item.Line);
                    var hourlyActualPcsList = basicDal.GetHourlyActualPcsList(item.BusinessUnitId, item.PunitName, item.Line);
                    var topDefectsList = basicDal.GetTopDefectsForCelsius(item.PunitName, item.Line);
                    var cumData = basicDal.GetCumulativeDashboardData(item.BusinessUnitId, item.PunitName, item.Line);
                    dynamic data = new { };

                    if (subData != null)
                    {
                        var tempData = new
                        {
                            actualPcs = subData.actualPcs,
                            buyerName = subData.buyerName,
                            cumActualPcs = cumData.CumActualPcs,
                            cumPlannedPcs = cumData.CumPlannedPCs,
                            cumVariance = cumData.CumVariance,
                            hourlyTarget = subData.hourlyTarget,
                            variance = subData.variance,
                            lineName = subData.lineName,
                            plannedPcs = subData.plannedPcs,
                            hourlyActualPcsList = hourlyActualPcsList,
                            topDefects = topDefectsList.topDefects,
                            totalDefect = topDefectsList.totalDefects,
                            defectPercentage = ((Convert.ToDouble(defectivePieces) / Convert.ToDouble(cumData.CumActualPcs)) * 100).ToString() == "NaN" ? 0 : (Convert.ToDouble(defectivePieces) / Convert.ToDouble(cumData.CumActualPcs)) * 100,
                            defectivePcs = defectivePieces,
                            dhu = ((Convert.ToDouble(totalDefect) / Convert.ToDouble(cumData.CumActualPcs)) * 100).ToString() == "NaN" ? 0 : (Convert.ToDouble(totalDefect) / Convert.ToDouble(cumData.CumActualPcs)) * 100,
                            efficiency = (Convert.ToDouble(cumData.CumActualPcs) * subData.totalSMV / clockMinute)*100,
                            balanceToProduce = cumData.CumActualPcs - subData.plannedPcs
                        };
                        data = tempData;
                    }

                    dataList.Add(data);
                }
            }

            return Json(dataList);
        }
        
        [HttpGet]
        public IHttpActionResult GetDashboardDataForSewing()
        {
            var IP = basicDal.GetIp();
            var modelList = new List<LineTvModel>();

            var linelist = basicDal.LineTvList(IP);
            var dataList = new List<dynamic>();

            if (linelist != null)
            {
                foreach (var item in linelist)
                {
                    //var totalProduction = basicDal.TotalProduction(item.PunitName, item.Line);
                    //var getSMV = basicDal.SMVValue(item.PunitName, item.Line);
                    //var getSAH = Convert.ToDouble(totalProduction) * getSMV / 60;
                    var totalDefect = basicDal.TotalDefect(item.PunitName, item.Line);
                    var defectivePieces = basicDal.DefectivePieces(item.PunitName, item.Line);
                    //double percentage = (Convert.ToDouble(defectivePieces) / Convert.ToDouble(totalProduction)) * 100;
                    //percentage = percentage.ToString() == "NaN" ? 0 : percentage;
                    //double dhu = (Convert.ToDouble(totalDefect) / Convert.ToDouble(totalProduction)) * 100;
                    //dhu = dhu.ToString() == "NaN" ? 0 : dhu;

                    DateTime start = DateTime.Parse("8:00");
                    DateTime end = DateTime.Parse("19:00");

                    //Lunch Hour
                    //DateTime lunchStart = DateTime.Parse("13:00");
                    //DateTime lunchEnd = DateTime.Parse("14:00");
                    //TimeSpan diff = end - start;
                    //long count = long.Parse(diff.ToString());
                    int hourCount = 0;

                    //if (DateTime.Now.DayOfWeek != DayOfWeek.Friday)
                    //{
                    //    if (DateTime.Now.TimeOfDay.Hours >= 8 && DateTime.Now.TimeOfDay.Hours < 19)
                    //    {
                    //        if (DateTime.Now.TimeOfDay.Hours < 13 || DateTime.Now.TimeOfDay.Hours >= 14)
                    //        {
                    //            if (DateTime.Now.TimeOfDay.Hours >= 14)
                    //            {
                    //                count = DateTime.Now.Hour - start.Hour - 1;
                    //            }
                    //            else
                    //            {
                    //                count = DateTime.Now.Hour - start.Hour;
                    //            }

                    //        }

                    //        else
                    //        {
                    //            count = DateTime.Now.Hour - start.Hour;
                    //        }
                    //    }
                    //}
                    //var hourCount = 1;

                    //if (count == 0)
                    //{
                    //    hourCount = 1;
                    //}
                    //else
                    //{
                    //    hourCount = count;
                    //}

                    if (DateTime.Now.TimeOfDay.Hours < 13)
                    {
                        hourCount = DateTime.Now.Hour - start.Hour + 1;
                    }
                    else if (DateTime.Now.TimeOfDay.Hours < end.Hour)
                    {
                        hourCount = DateTime.Now.Hour - start.Hour;
                    }
                    else
                    {
                        hourCount = end.Hour - start.Hour - 1;
                    }

                    double clockHour = item.MachineNumber * hourCount;
                    //var efficiency = (getSAH / clockHour) * 100;

                    var subData = basicDal.GetDashboardDataForBirichina(item.BusinessUnitId, item.PunitName, item.Line);
                    var hourlyActualPcsList = basicDal.GetHourlyActualPcsList(item.BusinessUnitId, item.PunitName, item.Line);
                    var topDefectsList = basicDal.GetTopDefectsForBirichina(item.PunitName, item.Line);
                    var cumData = basicDal.GetCumulativeDashboardData(item.BusinessUnitId, item.PunitName, item.Line);
                    dynamic data = new { };

                    if(subData != null)
                    {
                        var tempData = new
                        {
                            actualPcs = subData.actualPcs,
                            buyerName = subData.buyerName,
                            cumActualPcs = cumData.CumActualPcs,
                            cumPlannedPcs = cumData.CumPlannedPCs,
                            cumVariance = cumData.CumVariance,
                            hourlyTarget = subData.hourlyTarget,
                            variance = subData.variance,
                            lineName = subData.lineName,
                            plannedPcs = subData.plannedPcs,
                            hourlyActualPcsList = hourlyActualPcsList,
                            topDefects = topDefectsList.topDefects,
                            totalDefect = topDefectsList.totalDefects,
                            defectPercentage = ((Convert.ToDouble(defectivePieces) / Convert.ToDouble(cumData.CumActualPcs)) * 100).ToString() == "NaN" ? 0 : (Convert.ToDouble(defectivePieces) / Convert.ToDouble(cumData.CumActualPcs)) * 100,
                            defectivePcs = defectivePieces,
                            dhu = ((Convert.ToDouble(totalDefect) / Convert.ToDouble(cumData.CumActualPcs)) * 100).ToString() == "NaN" ? 0 : (Convert.ToDouble(totalDefect) / Convert.ToDouble(cumData.CumActualPcs)) * 100,
                            efficiency = ((Convert.ToDouble(cumData.CumActualPcs) * subData.totalSMV / 60) / clockHour) * 100,
                            balanceToProduce = cumData.CumActualPcs - subData.plannedPcs
                        };
                        data = tempData;
                    }
                    
                    dataList.Add(data);
                }
            }

            return Json(dataList);
        }
    }
}
