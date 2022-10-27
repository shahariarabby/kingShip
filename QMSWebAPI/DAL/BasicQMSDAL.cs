using Dapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QMSWebAPI.Models;
using SQIndustryThree.DataManager;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace QMSWebAPI.DAL
{
    public class BasicQMSDAL
    {
        DataAccessManager accessManager = new DataAccessManager();
        private string connStrLineTv = ConfigurationManager.ConnectionStrings["LineTVDB"].ConnectionString;
        public BarcodeGenarateModal GetBarcodeInformation(int BusinessUnitId, string BarcodeNumber)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.QmsDatabase); //work incentive
                BarcodeGenarateModal barcodeGenarateModel = new BarcodeGenarateModal();
                barcodeGenarateModel.Status = false;
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@BusinessID", BusinessUnitId));
                aParameters.Add(new SqlParameter("@BarcodeNumber", BarcodeNumber));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_BarcodeScanInformationAPI", aParameters);
                while (dr.Read())
                {
                        barcodeGenarateModel.Status = true;
                        barcodeGenarateModel.BusinessUnitName = dr["BusinessUnitName"].ToString();
                        barcodeGenarateModel.BuyerName = dr["BuyerName"].ToString();
                        barcodeGenarateModel.StyleName = dr["StyleName"].ToString();
                        barcodeGenarateModel.PONumber = dr["PurchaseOrderNumber"].ToString();
                        barcodeGenarateModel.BundleSize = dr["Size"].ToString();
                        barcodeGenarateModel.BatchQuantity = (int)dr["BundleQuantity"];
                        barcodeGenarateModel.BuyerID = (int)dr["BuyerId"];
                        barcodeGenarateModel.StyleID = (int)dr["StyleId"];
                        barcodeGenarateModel.Color = dr["Color"].ToString();
                        barcodeGenarateModel.ShadeNO = (int)dr["ShadeNO"];
                        barcodeGenarateModel.CutNo = (int)dr["CutNo"];                    
                        barcodeGenarateModel.MasterBarcodeId = (int)dr["MasterGenarationId"];
                        barcodeGenarateModel.MachineId = dr["MachineId"].ToString();
                        barcodeGenarateModel.LotNo = dr["LotNo"].ToString();
                        //barcodeGenarateModel.BundleNo = (int)dr["BundleNO"];                    
                        barcodeGenarateModel.OperationSMV = dr["OperationSMV"].ToString();                    
                }
                return barcodeGenarateModel;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }
        public ResultResponse QmsDataSaveToDatabase(string qmsdataList)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.WorkerIncentive);//
                ResultResponse result = new ResultResponse();
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@QmsDataList", qmsdataList));
                result.isSuccess = accessManager.SaveData("sp_QmsDataSave", aParameters);
                result.msg = "Data Save Successfully";
                return result;
            }
            catch (Exception e)
            {
                ResultResponse result = new ResultResponse();
                result.msg = e.Message;
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }
        public List<ReportModelQms> RetriveDataForReport(string StartDate,string EndDate)
        {
            try
            {
                List<ReportModelQms> reportlist = new List<ReportModelQms>();
                accessManager.SqlConnectionOpen(DataBase.WorkerIncentive);//
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@StartDate", StartDate));
                aParameters.Add(new SqlParameter("@EndDate", EndDate));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_QMSDataRetriveForReporting", aParameters);
                while (dr.Read())
                {
                    ReportModelQms reportModel = new ReportModelQms();
                    reportModel.BusinessUnit = dr["BusinessUnit"].ToString();
                    reportModel.LineNumber= dr["LineNumber"].ToString();
                    reportModel.Time = dr["EntryTime"].ToString();
                    reportModel.Date = dr["EntryDate"].ToString();
                    reportModel.BatchQty = dr["BatchQty"].ToString();
                    reportModel.BuyerName = dr["BuyerName"].ToString();
                    reportModel.ProductType = dr["ProductType"].ToString();
                    reportModel.StyleSubCat = dr["StyleSubCat"].ToString();
                    reportModel.PoNumber = dr["PoNumber"].ToString();
                    reportModel.GarmentsNumber = dr["GarmentsNumber"].ToString();
                    reportModel.DefectName = dr["DefectName"].ToString();
                    reportModel.DefectCount = dr["DefectCount"].ToString();
                    reportModel.Color = dr["Color"].ToString();
                    reportModel.DefectPos = dr["DefectPos"].ToString();
                    reportModel.SMV = dr["SMV"].ToString();
                    reportModel.Size = dr["Size"].ToString();
                    reportModel.TabId = dr["TabId"].ToString();
                    reportModel.StyleCat = dr["StyleCat"].ToString();
                    reportModel.DefectID = dr["DefectID"].ToString();
                    reportModel.OperatorId = dr["OperatorId"].ToString();
                    reportModel.MachineId = dr["MachineId"].ToString();
                    reportModel.UserID = dr["UserID"].ToString();
                    reportModel.ModuleName = dr["ModuleName"].ToString();
                    reportlist.Add(reportModel);
                }
                return reportlist;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }
        public List<ReportModelQms> CelsiusDataForReport(string StartDate, string EndDate)
        {
            try
            {
                List<ReportModelQms> reportlist = new List<ReportModelQms>();
                accessManager.SqlConnectionOpen(DataBase.QMSCelsiusDB);//
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@StartDate", StartDate));
                aParameters.Add(new SqlParameter("@EndDate", EndDate));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_QMSDataRetriveForReporting", aParameters);
                while (dr.Read())
                {
                    ReportModelQms reportModel = new ReportModelQms();
                    reportModel.BusinessUnit = dr["BusinessUnit"].ToString();
                    reportModel.LineNumber = dr["LineNumber"].ToString();
                    reportModel.Time = dr["EntryTime"].ToString();
                    reportModel.Date = dr["EntryDate"].ToString();
                    reportModel.BatchQty = dr["BatchQty"].ToString();
                    reportModel.BuyerName = dr["BuyerName"].ToString();
                    reportModel.ProductType = dr["ProductType"].ToString();
                    reportModel.StyleSubCat = dr["StyleSubCat"].ToString();
                    reportModel.PoNumber = dr["PoNumber"].ToString();
                    reportModel.GarmentsNumber = dr["GarmentsNumber"].ToString();
                    reportModel.DefectName = dr["DefectName"].ToString();
                    reportModel.DefectCount = dr["DefectCount"].ToString();
                    reportModel.Color = dr["Color"].ToString();
                    reportModel.DefectPos = dr["DefectPos"].ToString();
                    reportModel.SMV = dr["SMV"].ToString();
                    reportModel.Size = dr["Size"].ToString();
                    reportModel.TabId = dr["TabId"].ToString();
                    reportModel.StyleCat = dr["StyleCat"].ToString();
                    reportModel.DefectID = dr["DefectID"].ToString();
                    reportModel.OperatorId = dr["OperatorId"].ToString();
                    reportModel.MachineId = dr["MachineId"].ToString();
                    reportModel.UserID = dr["UserID"].ToString();
                    reportModel.ModuleName = dr["ModuleName"].ToString();
                    reportModel.Shift = dr["Shift"].ToString();
                    reportlist.Add(reportModel);
                }
                return reportlist;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }
        public List<ReportModelQms> BirichinaDataForReport(string StartDate, string EndDate)
        {
            try
            {
                List<ReportModelQms> reportlist = new List<ReportModelQms>();
                accessManager.SqlConnectionOpen(DataBase.QMSBirichinaDB);//
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@StartDate", StartDate));
                aParameters.Add(new SqlParameter("@EndDate", EndDate));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_QMSDataRetriveForReporting", aParameters);
                while (dr.Read())
                {
                    ReportModelQms reportModel = new ReportModelQms();
                    reportModel.BusinessUnit = dr["BusinessUnit"].ToString();
                    reportModel.LineNumber = dr["LineNumber"].ToString();
                    reportModel.Time = dr["EntryTime"].ToString();
                    reportModel.Date = dr["EntryDate"].ToString();
                    reportModel.BatchQty = dr["BatchQty"].ToString();
                    reportModel.BuyerName = dr["BuyerName"].ToString();
                    reportModel.ProductType = dr["ProductType"].ToString();
                    reportModel.StyleSubCat = dr["StyleSubCat"].ToString();
                    reportModel.PoNumber = dr["PoNumber"].ToString();
                    reportModel.GarmentsNumber = dr["GarmentsNumber"].ToString();
                    reportModel.DefectName = dr["DefectName"].ToString();
                    reportModel.DefectCount = dr["DefectCount"].ToString();
                    reportModel.Color = dr["Color"].ToString();
                    reportModel.DefectPos = dr["DefectPos"].ToString();
                    reportModel.SMV = dr["SMV"].ToString();
                    reportModel.Size = dr["Size"].ToString();
                    reportModel.TabId = dr["TabId"].ToString();
                    reportModel.StyleCat = dr["StyleCat"].ToString();
                    reportModel.DefectID = dr["DefectID"].ToString();
                    reportModel.OperatorId = dr["OperatorId"].ToString();
                    reportModel.MachineId = dr["MachineId"].ToString();
                    reportModel.UserID = dr["UserID"].ToString();
                    reportModel.ModuleName = dr["ModuleName"].ToString();
                    reportModel.Shift = dr["Shift"].ToString();
                    reportlist.Add(reportModel);
                }
                return reportlist;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }
        public ResultResponse DeleteDataFromQms(string qmsdataList)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.WorkerIncentive);//
                ResultResponse result = new ResultResponse();
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@QmsDataList", qmsdataList));
                result.isSuccess = accessManager.DeleteData("sp_DeleteQmsData", aParameters);
                result.msg = "Data Deleted Successfully";
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }
        public UserInformation CheckLogingQms(string userName,string userPassword)
        {
            UserInformation users = new UserInformation();
            users.IsSuccessful = false;
            users.Message = "Wrong Username Or Password";
            try
            {
                accessManager.SqlConnectionOpen(DataBase.QmsDatabase);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@UserName", userName));
                aParameters.Add(new SqlParameter("@UserPassword", userPassword));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_QmsApiLogin", aParameters);
                while (dr.Read())
                {
                    users.IsSuccessful = true;
                    users.Message = "Login Successful";
                    users.QmsUserId = (int)dr["QmsUserId"];
                    users.BusinessUnitId = (int)dr["BusinessUnitId"];
                    users.ProductionUnitId = (int)dr["ProductionUnitId"];
                    users.BusinessUnit = dr["BusinessUnitName"].ToString();
                    users.ProductionUnit = dr["PunitName"].ToString();
                    users.ModuleName = dr["FloorModuleName"].ToString();
                    users.LineNumber = dr["LineName"].ToString();
                    users.UserName = dr["UserName"].ToString();
                }
                return users;
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }
        public SilhouteeModel APIGetGridList(int StyleId,int Status)
        {
            try
            {
                SilhouteeModel silhouteeModels = new SilhouteeModel();
                accessManager.SqlConnectionOpen(DataBase.QmsDatabase);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@StyleID", StyleId));
                aParameters.Add(new SqlParameter("@Status", Status));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_DefectPositionAPI", aParameters);
                while (dr.Read())
                {
                    
                    silhouteeModels.SilhouetteId =(int)dr["SilhouetteId"];
                    silhouteeModels.StyleId =(int)dr["StyleId"];
                    silhouteeModels.StyleName = dr["StyleName"].ToString();
                    silhouteeModels.SilhouetteName= dr["SilhouetteName"].ToString();
                    silhouteeModels.CreateDate =(DateTime) dr["CreateDate"];
                    silhouteeModels.ImageList = JsonConvert.DeserializeObject<List<SilhouteeImageModel>>(dr["ImageList"].ToString());
                    silhouteeModels.SilhouetteGridList = JsonConvert.DeserializeObject<List<DefectPositionModel>>(dr["ShilhoutteeGridList"].ToString());
                }
                return silhouteeModels;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }
        public List<DefectPositionModel> PositionBasedDefectList(int StyleId, int Status)
        {
            try
            {
                List<DefectPositionModel> defectPositionList = new List<DefectPositionModel>();
                accessManager.SqlConnectionOpen(DataBase.QmsDatabase);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@StyleID", StyleId));
                aParameters.Add(new SqlParameter("@Status", Status));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_PositionBasedDefectAPI", aParameters);
                while (dr.Read())
                {
                    DefectPositionModel defectPosition = new DefectPositionModel();
                    defectPosition.DefectPositionId= (int)dr["DefectPositionId"];
                    defectPosition.DefectPositionName= dr["DefectPositionName"].ToString();
                    defectPosition.DefectList = JsonConvert.DeserializeObject<List<DefectModel>>(dr["DefectList"].ToString());
                    defectPositionList.Add(defectPosition);
                   
                }
                return defectPositionList;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }
        public ResultResponse SaveToQmsMasterTable(int UserId,int MasterGenarationId,string TabId,float OperationSmv)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.QmsDatabase);
                ResultResponse result = new ResultResponse();
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@UserId", UserId));
                aParameters.Add(new SqlParameter("@MasterGenarationId", MasterGenarationId));
                aParameters.Add(new SqlParameter("@TabId", TabId));
                aParameters.Add(new SqlParameter("@OperationSMV", OperationSmv));
                result.pk = accessManager.SaveDataReturnPrimaryKey("sp_SaveQmsMasterTable", aParameters);
                if (result.pk > 0)
                {
                    result.msg = "Data Save Successfully";
                    result.isSuccess = true;
                }
                else
                {
                    result.msg = "Some Error Occoured";
                }
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }
        public ResultResponse SaveDetailsToQms(JObject jObject)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.QmsDatabase);
                QMSMasterModel qMSMaster= JsonConvert.DeserializeObject<QMSMasterModel>(jObject.ToString());
                ResultResponse result = new ResultResponse();
                result.msg = "Data Save Successful";
                result.isSuccess = true;
                foreach (var item in qMSMaster.QmsDetailsInformation)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@ProductionUnitId", qMSMaster.ProductionUnitId));
                    aParameters.Add(new SqlParameter("@QmsMasterKey", qMSMaster.QmsMasterKey));
                    aParameters.Add(new SqlParameter("@DefectId", item.DefectId));
                    aParameters.Add(new SqlParameter("@DefectPositionId", item.DefectPositionId));
                    aParameters.Add(new SqlParameter("@DefectQuantity", item.DefectQuantity));
                    aParameters.Add(new SqlParameter("@GarmentsNo", item.GarmentsNo));
                    aParameters.Add(new SqlParameter("@EntryDate", item.EntryDate));
                    aParameters.Add(new SqlParameter("@EntryTime", item.EntryTime));
                    SqlDataReader sqlData = accessManager.GetSqlDataReader("sp_SaveQmsDetailsData", aParameters);
                    while (sqlData.Read())
                    {
                        result.pk +=(int) sqlData["ROWADDED"];
                    }
                    sqlData.Close();
                }
                if (result.pk<=0) {  result.msg = "Data Save Successful"; }
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }
        public ResultResponse DeleteQmsDetailsData(JObject jObject)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.QmsDatabase);
                QMSMasterModel qMSMaster = JsonConvert.DeserializeObject<QMSMasterModel>(jObject.ToString());
                ResultResponse result = new ResultResponse();
                result.msg = "No Data Found";
                result.isSuccess = true;
                foreach (var item in qMSMaster.QmsDetailsInformation)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@ProductionUnitId", qMSMaster.ProductionUnitId));
                    aParameters.Add(new SqlParameter("@QmsMasterKey", qMSMaster.QmsMasterKey));
                    aParameters.Add(new SqlParameter("@DefectId", item.DefectId));
                    aParameters.Add(new SqlParameter("@DefectPositionId", item.DefectPositionId));
                    aParameters.Add(new SqlParameter("@DefectQuantity", item.DefectQuantity));
                    aParameters.Add(new SqlParameter("@GarmentsNo", item.GarmentsNo));
                    aParameters.Add(new SqlParameter("@EntryDate", item.EntryDate));
                    aParameters.Add(new SqlParameter("@EntryTime", item.EntryTime));
                    SqlDataReader sqlData= accessManager.GetSqlDataReader("sp_DeleteQmsDetailsData", aParameters);
                    while (sqlData.Read())
                    {
                        result.pk += (int)sqlData["ROWDeleted"];
                    }
                    sqlData.Close();
                }

                if (result.pk > 0) {  result.msg = "Data Deleted Successfully"; }
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }
        public List<CommonModel> AllCommonModel(int Status)
        {
            List<CommonModel> allcommonUnit = new List<CommonModel>();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.QmsDatabase);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@Status", Status));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetALLProductionUnit", aParameters);
                while (dr.Read())
                {
                    CommonModel commonModel = new CommonModel();
                    commonModel.CommonId = (int)dr["CommonId"];
                    commonModel.CommonName=dr["CommonName"].ToString();
                    allcommonUnit.Add(commonModel);

                }
                return allcommonUnit;
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }
        
        public dynamic GetDashboardDataForCelsius(int businessUnitId, string productionUnit, string lineName)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.HPMDB);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@LineName", lineName));
                aParameters.Add(new SqlParameter("@BusinessUnitId", businessUnitId));
                aParameters.Add(new SqlParameter("@ProductionUnitName", productionUnit));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_HPMDataForDashboard", aParameters);
                dynamic data = null;
                if(dr.Read())
                {
                    data = new
                    {
                        actualPcs = (int)dr["HourlyActualPcs"],
                        buyerName = dr["BuyerName"].ToString(),
                        hourlyTarget = (int)dr["HourlyTargetPcs"],
                        variance = (int)dr["HourlyActualPcs"] - (int)dr["HourlyTargetPcs"],
                        lineName = dr["LineName"].ToString(),
                        plannedPcs = (int)dr["PlanPcs"],
                        totalSMV = (double)dr["TotalSMV"]
                    };
                }
                return data;
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public dynamic GetHourlyActualPcsList(int busninessUnitId, string productionUnitName, string lineName)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.HPMDB);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@LineName", lineName));
                aParameters.Add(new SqlParameter("@BusinessUnitId", busninessUnitId));
                aParameters.Add(new SqlParameter("@ProductionUnitName", productionUnitName));
                SqlDataReader subDr = accessManager.GetSqlDataReader("sp_HourlyActualPcsList", aParameters);
                var hourlyActualPcsList = new List<dynamic>();
                while (subDr.Read())
                {
                    hourlyActualPcsList.Add(new
                    {
                        hourName = subDr["HourName"].ToString().Length > 4 ? subDr["HourName"].ToString().Substring(0, 2)
                        : subDr["HourName"].ToString()[0].ToString(),
                        hourlyActualPcs = (int)subDr["HourlyActualPcs"],
                        hourlyTargetPcs = (int)subDr["HourlyTargetPcs"]
                    });
                }
                return hourlyActualPcsList;
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public dynamic GetTopDefectsForCelsius(string productionUnit, string lineName)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.QMSCelsiusDB);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@LineName", lineName));
                aParameters.Add(new SqlParameter("@ProductionUnit", productionUnit));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetTopDefects", aParameters);
                decimal totalDefectsc = 0;
                var topDefects = new List<dynamic>();
                
                int sl = 0;
                while(dr.Read())
                {
                    topDefects.Add(new
                    {
                        defectName = dr["DefectName"].ToString(),
                        defectPcs = (int)dr["DefectPcs"],
                        defectPercentage = (decimal)dr["Percentage"],
                        slNo = ++sl
                    });
                    totalDefectsc = (decimal)dr["TotalDefect"];
                }
                while(topDefects.Count < 5)
                {
                    topDefects.Add(new {
                        defectName = "N/A",
                        defectPcs = 0,
                        defectPercentage = 0,
                        slNo = ++sl
                    });
                }
                var dataModel = new { 
                    totalDefects = Decimal.ToInt32(totalDefectsc),
                    topDefects = topDefects
                };
                return dataModel;
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public dynamic GetCumulativeDashboardData(int busninessUnitId, string productionUnitName, string lineName)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.HPMDB);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@LineName", lineName));
                aParameters.Add(new SqlParameter("@BusinessUnitId", busninessUnitId));
                aParameters.Add(new SqlParameter("@ProductionUnitName", productionUnitName));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetCumData", aParameters);
                dynamic data = new { };
                if (dr.Read())
                {
                    data = new
                    {
                        CumPlannedPCs = (int)dr["CumPlannedPCs"],
                        CumActualPcs = (int)dr["CumActualPcs"],
                        CumVariance = (int)dr["CumVariance"]
                    };
                }
                return data;
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        /////////////////////////////////// From Ashiq vai (Line TV project) ////////////////////////////////////
        /////////////////////////////////// QMS Cesisus /////////////////////////////////////
        public string TotalProductionForCelsius(string productionName, string lineNumber)
        {
            LineTvModel Lines = new LineTvModel();
            var line = string.Empty;
            SqlConnection cnn = new SqlConnection(this.connStrLineTv);
            try
            {

                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@productionName", (object)productionName);
                dynamicParameters.Add("@lineNumber", (object)lineNumber);
                Lines.TotalProduction = cnn.Query<LineTvModel>("sp_TotalProductionForCelsius", dynamicParameters, commandTimeout: 120000, commandType: new CommandType?(CommandType.StoredProcedure)).Select(s => s.TotalProduction).FirstOrDefault();

                return Lines.TotalProduction;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }

        public double SMVValueForCelsius(string productionName, string lineNumber)
        {
            LineTvModel Lines = new LineTvModel();
            var line = string.Empty;
            SqlConnection cnn = new SqlConnection(this.connStrLineTv);
            try
            {

                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();

                //   string query = @"SELECT qms.SMV SMV FROM QMSBirichinaDB..QMSDataTable qms 	
                //WHERE qms.BusinessUnit = @productionName and qms.LineNumber = @lineNumber
                //AND qms.GarmentsNumber IS NOT NULL AND qms.Date = '2021-06-10' AND UserID NOT IN ('user','user2','training')";

                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@productionName", (object)productionName);
                dynamicParameters.Add("@lineNumber", (object)lineNumber);
                Lines.SMV = cnn.Query<LineTvModel>("sp_getSMVForCelsius", dynamicParameters, commandTimeout: 120000, commandType: new CommandType?(CommandType.StoredProcedure)).Select(s => s.SMV).FirstOrDefault();

                return Lines.SMV;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }

        public string TotalDefectForCelsius(string productionName, string lineNumber)
        {
            LineTvModel Lines = new LineTvModel();
            var line = string.Empty;
            SqlConnection cnn = new SqlConnection(this.connStrLineTv);
            try
            {

                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@productionName", (object)productionName);
                dynamicParameters.Add("@lineNumber", (object)lineNumber);
                Lines.TotalDefect = cnn.Query<LineTvModel>("sp_TotalDefectForCelsius", dynamicParameters, commandTimeout: 120000, commandType: new CommandType?(CommandType.StoredProcedure)).Select(s => s.TotalDefect).FirstOrDefault();

                return Lines.TotalDefect;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }

        public string DefectivePiecesForCelsius(string productionName, string lineNumber)
        {
            LineTvModel Lines = new LineTvModel();
            var line = string.Empty;
            SqlConnection cnn = new SqlConnection(this.connStrLineTv);
            try
            {

                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@productionName", (object)productionName);
                dynamicParameters.Add("@lineNumber", (object)lineNumber);
                Lines.DefectivePieces = cnn.Query<LineTvModel>("sp_DefectivePiecesForCelsius", dynamicParameters, commandTimeout: 120000, commandType: new CommandType?(CommandType.StoredProcedure)).Select(s => s.DefectivePieces).FirstOrDefault();

                return Lines.DefectivePieces;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }

        public List<LineTvModel> LineTvList(string IP)
        {
            List<LineTvModel> Lines = new List<LineTvModel>();
            SqlConnection cnn = new SqlConnection(this.connStrLineTv);

            try
            {

                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@IP", (object)IP);
                Lines = cnn.Query<LineTvModel>("sp_LineTVInfo", dynamicParameters, commandTimeout: 120000, commandType: new CommandType?(CommandType.StoredProcedure)).ToList();

                return Lines;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }

        public string GetIp()
        {
            string ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ip))
            {
                ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            return ip;
        }

        /////////////////////////////////// QMS Birichina From Ashiq Vai /////////////////////////////////////

        public string TotalProduction(string productionName, string lineNumber)
        {
            LineTvModel Lines = new LineTvModel();
            var line = string.Empty;

            SqlConnection cnn = new SqlConnection(this.connStrLineTv);

            try
            {

                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@productionName", (object)productionName);
                dynamicParameters.Add("@lineNumber", (object)lineNumber);
                Lines.TotalProduction = cnn.Query<LineTvModel>("sp_TotalProduction", dynamicParameters, commandTimeout: 120000, commandType: new CommandType?(CommandType.StoredProcedure)).Select(s => s.TotalProduction).FirstOrDefault();

                return Lines.TotalProduction;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

        }

        public double SMVValue(string productionName, string lineNumber)
        {
            LineTvModel Lines = new LineTvModel();
            var line = string.Empty;
            SqlConnection cnn = new SqlConnection(this.connStrLineTv);
            try
            {

                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();

                //   string query = @"SELECT qms.SMV SMV FROM QMSBirichinaDB..QMSDataTable qms 	
                //WHERE qms.BusinessUnit = @productionName and qms.LineNumber = @lineNumber
                //AND qms.GarmentsNumber IS NOT NULL AND qms.Date = '2021-06-10' AND UserID NOT IN ('user','user2','training')";

                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@productionName", (object)productionName);
                dynamicParameters.Add("@lineNumber", (object)lineNumber);
                Lines.SMV = cnn.Query<LineTvModel>("sp_getSMV", dynamicParameters, commandTimeout: 120000, commandType: new CommandType?(CommandType.StoredProcedure)).Select(s => s.SMV).FirstOrDefault();

                return Lines.SMV;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }

        public string TotalDefect(string productionName, string lineNumber)
        {
            LineTvModel Lines = new LineTvModel();
            var line = string.Empty;
            SqlConnection cnn = new SqlConnection(this.connStrLineTv);
            try
            {

                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@productionName", (object)productionName);
                dynamicParameters.Add("@lineNumber", (object)lineNumber);
                Lines.TotalDefect = cnn.Query<LineTvModel>("sp_TotalDefect", dynamicParameters, commandTimeout: 120000, commandType: new CommandType?(CommandType.StoredProcedure)).Select(s => s.TotalDefect).FirstOrDefault();

                return Lines.TotalDefect;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }

        public string DefectivePieces(string productionName, string lineNumber)
        {
            LineTvModel Lines = new LineTvModel();
            var line = string.Empty;
            SqlConnection cnn = new SqlConnection(this.connStrLineTv);
            try
            {

                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@productionName", (object)productionName);
                dynamicParameters.Add("@lineNumber", (object)lineNumber);
                Lines.DefectivePieces = cnn.Query<LineTvModel>("sp_DefectivePieces", dynamicParameters, commandTimeout: 120000, commandType: new CommandType?(CommandType.StoredProcedure)).Select(s => s.DefectivePieces).FirstOrDefault();

                return Lines.DefectivePieces;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }

        ////////////////////////////////// Dashboard Data For Birichina ///////////////////////////

        public dynamic GetDashboardDataForBirichina(int businessUnitId, string productionUnit, string lineName)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.HPMDB);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@LineName", lineName));
                aParameters.Add(new SqlParameter("@BusinessUnitId", businessUnitId));
                aParameters.Add(new SqlParameter("@ProductionUnitName", productionUnit));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_HPMDataForDashboard", aParameters);
                dynamic data = null;
                if (dr.Read())
                {
                    data = new
                    {
                        actualPcs = (int)dr["HourlyActualPcs"],
                        buyerName = dr["BuyerName"].ToString(),
                        hourlyTarget = (int)dr["HourlyTargetPcs"],
                        variance = (int)dr["HourlyActualPcs"] - (int)dr["HourlyTargetPcs"],
                        lineName = dr["LineName"].ToString(),
                        plannedPcs = (int)dr["PlanPcs"],
                        totalSMV = (double)dr["TotalSMV"]
                    };
                }
                return data;
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public dynamic GetTopDefectsForBirichina(string productionUnit, string lineName)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.QMSBirichinaDB);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@LineName", lineName));
                aParameters.Add(new SqlParameter("@ProductionUnit", productionUnit));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetTopDefectsForBirichina", aParameters);
                decimal totalDefectsc = 0;
                var topDefects = new List<dynamic>();

                int sl = 0;
                while (dr.Read())
                {
                    topDefects.Add(new
                    {
                        defectName = dr["DefectName"].ToString(),
                        defectPcs = (int)dr["DefectPcs"],
                        defectPercentage = (decimal)dr["Percentage"],
                        slNo = ++sl
                    });
                    totalDefectsc = (decimal)dr["TotalDefect"];
                }
                while (topDefects.Count < 5)
                {
                    topDefects.Add(new
                    {
                        defectName = "N/A",
                        defectPcs = 0,
                        defectPercentage = 0,
                        slNo = ++sl
                    });
                }
                var dataModel = new
                {
                    totalDefects = Decimal.ToInt32(totalDefectsc),
                    topDefects = topDefects
                };
                return dataModel;
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }
    }
}
