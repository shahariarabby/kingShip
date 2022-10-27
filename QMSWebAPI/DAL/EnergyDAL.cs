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
    public class EnergyDAL
    {
        DataAccessManager accessManager = new DataAccessManager();
        private string connStr = ConfigurationManager.ConnectionStrings["Energy"].ConnectionString;
        public SqlConnection conn = new SqlConnection(DBConnection.GetConnectionString());

        public LoginModel CheckLoging(string UserName, string Password)
        {
            LoginModel loginModel;
            LoginModel loginModels = new LoginModel();
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
                    loginModels = cnn.Query<LoginModel>("sp_Login", dynamicParameters, null, true, nullable2, new CommandType?(CommandType.StoredProcedure)).SingleOrDefault<LoginModel>();
                }
                loginModel = loginModels;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return loginModel;
        }
        public DataTable RetriveBusinessUnit()
        {
            DataTable dt = new DataTable();
            if (this.conn.State == 0)
            {
                this.conn.Open();
            }
            SqlCommand cmd = new SqlCommand("sp_GetAllBusinessUnit", this.conn)
            {
                CommandTimeout = 12000,
                CommandType = CommandType.StoredProcedure

            };
            //cmd.Parameters.Add("@StartDate", SqlDbType.NVarChar).Value = StartDate;
            //cmd.Parameters.Add("@EndDate", SqlDbType.NVarChar).Value = EndDate;
            (new SqlDataAdapter(cmd)).Fill(dt);
            return dt;
        }
        public DataTable BusinessUnitWiseLocation(int BusinessUnitId, int CategoryId )
        {
            DataTable dt = new DataTable();
            if (this.conn.State == 0)
            {
                this.conn.Open();
            }
            SqlCommand cmd = new SqlCommand("sp_SRTGetBusinessWiseLocation", this.conn)
            {
                CommandTimeout = 12000,
                CommandType = CommandType.StoredProcedure

            };
            cmd.Parameters.Add("@BusinessUnitId", SqlDbType.Int).Value = BusinessUnitId;
            cmd.Parameters.Add("@CategoryId", SqlDbType.Int).Value = CategoryId;
            (new SqlDataAdapter(cmd)).Fill(dt);
            return dt;
        }
        public DataTable LocationWiseMeter(int LocationId, int CategoryId)
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
