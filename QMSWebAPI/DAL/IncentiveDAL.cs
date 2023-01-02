using Dapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QMSWebAPI.Models;
using QMSWebAPI.Utilities;
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
    public class IncentiveDAL
    {
        DataAccessManager accessManager = new DataAccessManager();
        private string connStr = ConfigurationManager.ConnectionStrings["IncentiveDB"].ConnectionString;
        public SqlConnection conn = new SqlConnection(DBConnection.GetConnectionStringIncentive());

        public IncentiveModel GetEmplyoeeInformation(int BarcodeNo)
        {
            IncentiveModel loginModel;
            IncentiveModel loginModels = new IncentiveModel();
            try
            {
                using (IDbConnection cnn = new SqlConnection(this.connStr))
                {
                    if (cnn.State == 0)
                    {
                        cnn.Open();
                    }
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    DbType? nullable = null;
                    ParameterDirection? nullable1 = null;
                    int? nullable2 = null;
                    byte? nullable3 = null;
                    byte? nullable4 = nullable3;
                    nullable3 = null;
                    dynamicParameters.Add("@BarcodeNo", BarcodeNo, nullable, nullable1, nullable2, nullable4, nullable3);
                    nullable = null;
                    loginModels = cnn.Query<IncentiveModel>("sp_SRTGetBarcodeWiseEmployeee", dynamicParameters, null, true, nullable2, new CommandType?(CommandType.StoredProcedure)).SingleOrDefault<IncentiveModel>();
                }
                loginModel = loginModels;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return loginModel;
        }
        public IncentiveModel CheckLoging(string UserName, string Password)
        {
            IncentiveModel loginModel;
            IncentiveModel loginModels = new IncentiveModel();
            try
            {
                using (IDbConnection cnn = new SqlConnection(this.connStr))
                {
                    if (cnn.State == 0)
                    {
                        cnn.Open();
                    }
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    DbType? nullable = null;
                    ParameterDirection? nullable1 = null;
                    int? nullable2 = null;
                    byte? nullable3 = null;
                    byte? nullable4 = nullable3;
                    nullable3 = null;
                    dynamicParameters.Add("@UserName", UserName, nullable, nullable1, nullable2, nullable4, nullable3);
                    nullable = null;
                    nullable1 = null;
                    nullable2 = null;
                    nullable3 = null;
                    byte? nullable5 = nullable3;
                    nullable3 = null;
                    dynamicParameters.Add("@UserPassword", Password, nullable, nullable1, nullable2, nullable5, nullable3);
                    nullable2 = null;
                    loginModels = cnn.Query<IncentiveModel>("sp_Login", dynamicParameters, null, true, nullable2, new CommandType?(CommandType.StoredProcedure)).SingleOrDefault<IncentiveModel>();
                }
                loginModel = loginModels;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                this.conn.Close();
            }
            return loginModel;
        }
        public IncentiveModel GetOperationNameWiseEmployeee(int EmpBarcodeNo, int BarcodeNo, int OperationID)
        {
            IncentiveModel loginModel;
            IncentiveModel loginModels = new IncentiveModel();
            try
            {
                using (IDbConnection cnn = new SqlConnection(this.connStr))
                {
                    if (cnn.State == 0)
                    {
                        cnn.Open();
                    }
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    DbType? nullable = null;
                    ParameterDirection? nullable1 = null;
                    int? nullable2 = null;
                    byte? nullable3 = null;
                    byte? nullable4 = nullable3;
                    nullable3 = null;
                    dynamicParameters.Add("@EmpBarcodeNo", EmpBarcodeNo, nullable, nullable1, nullable2, nullable4, nullable3);
                    nullable = null;
                    nullable = null;
                    nullable1 = null;
                    nullable2 = null;
                    nullable3 = null;
                    byte? nullable5 = nullable3;
                    nullable3 = null;
                    dynamicParameters.Add("@BarcodeNo", BarcodeNo, nullable, nullable1, nullable2, nullable5, nullable3);
                    nullable = null;
                    nullable = null;
                    nullable1 = null;
                    nullable2 = null;
                    nullable3 = null;
                    byte? nullable6 = nullable3;
                    nullable3 = null;
                    dynamicParameters.Add("@OperationID", OperationID, nullable, nullable1, nullable2, nullable6, nullable3);
                    nullable2 = null;
                    loginModels = cnn.Query<IncentiveModel>("sp_SRTGetOperationNameWiseEmployeee", dynamicParameters, null, true, nullable2, new CommandType?(CommandType.StoredProcedure)).SingleOrDefault<IncentiveModel>();
                }
                loginModel = loginModels;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                this.conn.Close();
            }
            return loginModel;
        }
        public ResultResponse DataSaveToDatabase(int EmpBarcodeNo, string EmployeeCode, int OperationID, string OperationName, int BarcodeNo,int AllocatedQty,int UsedQty)
        {
            IncentiveModel incentiveModel = new IncentiveModel();
            try
            {
                using (IDbConnection cnn = (IDbConnection)new SqlConnection(this.connStr))
                {
                    if (cnn.State == ConnectionState.Closed)
                        cnn.Open();

                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@EmpBarcodeNo", EmpBarcodeNo);
                    dynamicParameters.Add("@EmployeeCode", EmployeeCode);
                    dynamicParameters.Add("@OperationID", OperationID);
                    dynamicParameters.Add("@OperationName", OperationName);
                    dynamicParameters.Add("@BarcodeNo", BarcodeNo);
                    dynamicParameters.Add("@AllocatedQty", AllocatedQty);
                    dynamicParameters.Add("@UsedQty", UsedQty);
                    incentiveModel.BarcoadCount = cnn.Query<IncentiveModel>("sp_DataSave", dynamicParameters, commandTimeout: 120000, commandType: new CommandType?(CommandType.StoredProcedure)).Select(s => s.BarcoadCount).FirstOrDefault();
                    return new ResultResponse()
                    {
                        data = incentiveModel.BarcoadCount,
                        isSuccess = true
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.conn.Close();
            }
        }
        //public OperationModel RetriveOperation(string BusinessUnit, string ProductionFloor, string LineNumber)
        //{
        //    OperationModel loginModel;
        //    OperationModel loginModels = new OperationModel();
        //   // IncentiveModel loginModels = new IncentiveModel();
        //    try
        //    {
        //        using (IDbConnection cnn = (IDbConnection)new SqlConnection(this.connStr))
        //        {
        //            if (cnn.State == ConnectionState.Closed)
        //                cnn.Open();

        //            DynamicParameters dynamicParameters = new DynamicParameters();
        //            dynamicParameters.Add("@BusinessUnit", BusinessUnit);
        //            dynamicParameters.Add("@ProductionFloor", ProductionFloor);
        //            dynamicParameters.Add("@LineNumber", LineNumber);
        //            loginModels = cnn.Query<OperationModel>("sp_SRTGetOperationName", dynamicParameters, commandType: new CommandType?(CommandType.StoredProcedure)).SingleOrDefault<OperationModel>();
        //            loginModel = loginModels;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        this.conn.Close();
        //    }
        //    return loginModel;
        //}
        public List<OperationModel> RetriveOperation(string BusinessUnit, string ProductionFloor, string LineNumber)

        {
            List<OperationModel> OperationList = new List<OperationModel>();
            try
            {
                using (IDbConnection cnn = (IDbConnection)new SqlConnection(this.connStr))
                {
                    if (cnn.State == ConnectionState.Closed)
                        cnn.Open();
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@BusinessUnit", (object)BusinessUnit);
                    dynamicParameters.Add("@LineNumber", LineNumber);
                    dynamicParameters.Add("@ProductionFloor", (object)ProductionFloor);
                    OperationList = cnn.Query<OperationModel>("sp_SRTGetOperationName", (object)dynamicParameters, commandType: new CommandType?(CommandType.StoredProcedure)).ToList<OperationModel>();
                }
                return OperationList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.conn.Close();
            }
        }
        //public DataTable RetriveOperation(string BusinessUnit, string ProductionFloor, string LineNumber)
        //{
        //    try
        //    {
        //        DataTable dt = new DataTable();
        //        if (this.conn.State == 0)
        //        {
        //            this.conn.Open();
        //        }

        //        SqlCommand cmd = new SqlCommand("sp_SRTGetOperationName", this.conn)
        //        {
        //            CommandTimeout = 12000,
        //            CommandType = CommandType.StoredProcedure

        //        };
        //        cmd.Parameters.Add("@BusinessUnit", SqlDbType.NVarChar).Value = BusinessUnit;
        //        cmd.Parameters.Add("@ProductionFloor", SqlDbType.NVarChar).Value = ProductionFloor;
        //        cmd.Parameters.Add("@LineNumber", SqlDbType.NVarChar).Value = LineNumber;
        //        (new SqlDataAdapter(cmd)).Fill(dt);
        //        return dt;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //    finally
        //    {
        //        this.conn.Close();
        //    }
        //}

        public ResultResponse GetDataSave(string EmployeeCode, int OperationID)
        {
            IncentiveModel incentiveModel = new IncentiveModel();
            var line = string.Empty;
            SqlConnection cnn = new SqlConnection(this.connStr);
            try
            {

                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@EmployeeCode", (object)EmployeeCode);
                dynamicParameters.Add("@OperationID", (object)OperationID);
                incentiveModel.BarcoadCount = cnn.Query<IncentiveModel>("sp_GetDataSave", dynamicParameters, commandTimeout: 120000, commandType: new CommandType?(CommandType.StoredProcedure)).Select(s => s.BarcoadCount).FirstOrDefault();

                return new ResultResponse()
                {
                    data = incentiveModel.BarcoadCount,
                    isSuccess = true
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
            //string dt = "";
            //if (this.conn.State == 0)
            //{
            //    this.conn.Open();
            //}
            //SqlCommand cmd = new SqlCommand("sp_GetDataSave", this.conn)
            //{
            //    CommandTimeout = 12000,
            //    CommandType = CommandType.StoredProcedure

            //};
            //cmd.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;
            //cmd.Parameters.Add("@OperationID", SqlDbType.Int).Value = OperationID;
            //(new SqlDataAdapter(cmd)).Fill(dt);
            //return dt;
        }
        public ResultResponse GetRemainingQty(int BarcodeNo, int OperationID)
        {
            IncentiveModel incentiveModel = new IncentiveModel();
            var line = string.Empty;
            SqlConnection cnn = new SqlConnection(this.connStr);
            try
            {

                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@BarcodeNo", (object)BarcodeNo);
                dynamicParameters.Add("@OperationID", (object)OperationID);
                incentiveModel.RemainingQty = cnn.Query<IncentiveModel>("sp_GetRemainingQty", dynamicParameters, commandTimeout: 120000, commandType: new CommandType?(CommandType.StoredProcedure)).Select(s => s.RemainingQty).FirstOrDefault();

                return new ResultResponse()
                {
                    data = Convert.ToString(incentiveModel.RemainingQty),
                    isSuccess = true
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
            //string dt = "";
            //if (this.conn.State == 0)
            //{
            //    this.conn.Open();
            //}
            //SqlCommand cmd = new SqlCommand("sp_GetDataSave", this.conn)
            //{
            //    CommandTimeout = 12000,
            //    CommandType = CommandType.StoredProcedure

            //};
            //cmd.Parameters.Add("@EmployeeCode", SqlDbType.Int).Value = EmployeeCode;
            //cmd.Parameters.Add("@OperationID", SqlDbType.Int).Value = OperationID;
            //(new SqlDataAdapter(cmd)).Fill(dt);
            //return dt;
        }
        public DataTable LocationWiseMeter(int LocationId, int CategoryId)
        {
            try
            {
                DataTable dt = new DataTable();
                if (this.conn.State == 0)
                {
                    this.conn.Open();
                }
                SqlCommand cmd = new SqlCommand("sp_SRTGetLocationWiseMeter", this.conn)
                {
                    CommandTimeout = 12000,
                    CommandType = CommandType.StoredProcedure

                };
                cmd.Parameters.Add("@LocationId", SqlDbType.Int).Value = LocationId;
                cmd.Parameters.Add("@CategoryId", SqlDbType.Int).Value = CategoryId;
                (new SqlDataAdapter(cmd)).Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                this.conn.Close();
            }
        }
        public ResultResponse DataSaveToDatabase(string RequestorId, string CategoryId, string BusinessUnitId, string LocationId, string MeterId, string Image, string ImagePath)
        {
            try
            {
                using (IDbConnection cnn = (IDbConnection)new SqlConnection(this.connStr))
                {
                    if (cnn.State == ConnectionState.Closed)
                        cnn.Open();
                    
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@RequestorId", RequestorId);
                    dynamicParameters.Add("@CategoryId",CategoryId);
                    dynamicParameters.Add("@BusinessUnitId", BusinessUnitId);
                    dynamicParameters.Add("@LocationId", LocationId);
                    dynamicParameters.Add("@MeterId", MeterId);
                    dynamicParameters.Add("@Image", Image);
                    dynamicParameters.Add("@ImagePath", ImagePath);
                    int num = cnn.Execute("sp_DataSave", (object)dynamicParameters, commandType: new CommandType?(CommandType.StoredProcedure));
                    return new ResultResponse()
                    {
                        pk = num,
                        isSuccess = true
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.conn.Close();
            }
            //try
            //{
            //    accessManager.SqlConnectionOpen(DataBase.Energy);
            //    ResultResponse result = new ResultResponse();
            //    List<SqlParameter> aParameters = new List<SqlParameter>();
            //    aParameters.Add(new SqlParameter("@RequestorId", RequestorId));
            //    aParameters.Add(new SqlParameter("@CategoryId", CategoryId));
            //    aParameters.Add(new SqlParameter("@BusinessUnitId", BusinessUnitId));
            //    aParameters.Add(new SqlParameter("@LocationId", LocationId));
            //    aParameters.Add(new SqlParameter("@MeterId", MeterId));
            //    aParameters.Add(new SqlParameter("@Image", Image));
            //    aParameters.Add(new SqlParameter("@ImagePath", ImagePath));
            //    result.pk = accessManager.SaveDataReturnPrimaryKey("sp_DataSave", aParameters);
            //    if (result.pk > 0)
            //    {
            //        result.msg = "Data Save Successfully";
            //        result.isSuccess = true;
            //    }
            //    else
            //    {
            //        result.msg = "Some Error Occoured";
            //    }
            //    return result;
            //}
            //catch (Exception e)
            //{
            //    throw e;
            //}
            //finally
            //{
            //    accessManager.SqlConnectionClose();
            //}
        }
        public IEnumerable<CommonModel> RetriveDataForReportModel()
        {
            IEnumerable<CommonModel> aQLReportModels;
            List<CommonModel> reportlist = new List<CommonModel>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(DBConnection.GetConnectionString()))
                {
                    if (cnn.State == 0)
                    {
                        cnn.Open();
                    }
                    SqlCommand cmd = new SqlCommand("sp_EnergyGetAllRequest", cnn)
                    {
                        CommandTimeout = 12000,
                        CommandType = CommandType.StoredProcedure
                    };
                    //cmd.Parameters.Add("@StartDate", SqlDbType.NVarChar).Value = StartDate;
                    //cmd.Parameters.Add("@EndDate", SqlDbType.NVarChar).Value = EndDate;
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        CommonModel reportModel = new CommonModel()
                        {
                            EnergyRequesMastertId = Convert.ToInt32(dr["EnergyRequesMastertId"]),
                            DateOfRequest = dr["DateOfRequest"].ToString(),
                            RequestorId = Convert.ToInt32(dr["RequestorId"]),
                            UserName = dr["UserName"].ToString(),
                            CategoryId = Convert.ToInt32(dr["CategoryId"]),
                            CategoryName = dr["CategoryName"].ToString(),
                            BusinessUnitId = Convert.ToInt32(dr["BusinessUnitId"]),
                            BusinessUnitName = dr["BusinessUnitName"].ToString(),
                            LocationId = Convert.ToInt32(dr["LocationId"]),
                            LocationName = dr["LocationName"].ToString(),
                            MeterId = Convert.ToInt32(dr["MeterId"]),
                            MeterName = dr["MeterName"].ToString(),
                            Image = dr["Image"].ToString(),
                            ImagePath = dr["ImagePath"].ToString()
                        };
                        reportlist.Add(reportModel);
                    }
                }
                aQLReportModels = reportlist;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return aQLReportModels;
        }
    }
}
