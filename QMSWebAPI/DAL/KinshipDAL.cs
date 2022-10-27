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
    public class KinshipDAL
    {
        DataAccessManager accessManager = new DataAccessManager();
        private string connStr = ConfigurationManager.ConnectionStrings["Kinship"].ConnectionString;
        public SqlConnection conn = new SqlConnection(DBConnection.GetConnectionStringKinship());

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
        
        public List<CategoryModel> Category(int BusinessUnitId)
        {
            try
            {
                this.accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                List<CategoryModel> commonModelList = new List<CategoryModel>();
                SqlDataReader sqlDataReader = this.accessManager.GetSqlDataReader("sp_SRTGetBusinessUnitWiseCategory", new List<SqlParameter>()
        {
                    new SqlParameter("@BusinessUnitId", (object) BusinessUnitId)
                    
        });
                while (sqlDataReader.Read())
                    commonModelList.Add(new CategoryModel()
                    {
                        BusinessUnitId = (int)sqlDataReader["BusinessUnitId"],
                        Id = (int)sqlDataReader["Id"],
                        Name = sqlDataReader["Name"].ToString()
                    });
                return commonModelList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.accessManager.SqlConnectionClose();
            }
        }

        public List<CategoryModel> Subcategory(int BusinessUnitId, int CategoryId)
        {
            try
            {
                this.accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                List<CategoryModel> commonModelList = new List<CategoryModel>();
                SqlDataReader sqlDataReader = this.accessManager.GetSqlDataReader("sp_SRTGetCategoryWiseSubCategory", new List<SqlParameter>()
        {
                    new SqlParameter("@BusinessUnitId", (object) BusinessUnitId),
                    new SqlParameter("@CategoryId", (object) CategoryId)
        });
                while (sqlDataReader.Read())
                    commonModelList.Add(new CategoryModel()
                    {
                        BusinessUnitId = (int)sqlDataReader["BusinessUnitId"],
                        Id = (int)sqlDataReader["Id"],
                        Name = sqlDataReader["Name"].ToString()
                    });
                return commonModelList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.accessManager.SqlConnectionClose();
            }
        }
        public CategoryModel GetUserWiseSupervisor(string UserId)
        {
            CategoryModel loginModel;
            CategoryModel loginModels = new CategoryModel();
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
                    dynamicParameters.Add("@UserId", UserId, nullable, nullable1, nullable2, nullable4, nullable3);
                    nullable2 = null;
                    loginModels = cnn.Query<CategoryModel>("sp_SRTGetUserWiseSupervisor", dynamicParameters, null, true, nullable2, new CommandType?(CommandType.StoredProcedure)).SingleOrDefault<CategoryModel>();
                }
                loginModel = loginModels;
            }
            catch (Exception exception)
            {
                throw exception;
            }
           
            return loginModel;
        }
        public ResultResponse DataSaveToDatabase(string UserId, string CategoryId,string SubCategoryId, string BusinessUnitId, string SupervisorId, string Issue, string Image, string ImagePath)
        {
            
            try
            {
                accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                ResultResponse result = new ResultResponse();
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@UserId", UserId));
                aParameters.Add(new SqlParameter("@SupervisorId", SupervisorId));
                aParameters.Add(new SqlParameter("@CategoryId", CategoryId));
                aParameters.Add(new SqlParameter("@SubCategoryId", SubCategoryId));
                aParameters.Add(new SqlParameter("@Issue", Issue));
                aParameters.Add(new SqlParameter("@Image", Image));
                aParameters.Add(new SqlParameter("@ImagePath", ImagePath));
                aParameters.Add(new SqlParameter("@BusinessUnitId", BusinessUnitId));
                result.pk = accessManager.SaveDataReturnPrimaryKey("SP_GRVDataInsert", aParameters);
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
        public List<KinshipModel> RetriveDataForReportModel(string UserId)
        {
            try
            {
                this.accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                List<KinshipModel> commonModelList = new List<KinshipModel>();
                SqlDataReader sqlDataReader = this.accessManager.GetSqlDataReader("sp_GRVGetAllRequesttest", new List<SqlParameter>()
        {
          new SqlParameter("@UserId", (object) UserId)
        });
                while (sqlDataReader.Read())
                    commonModelList.Add(new KinshipModel()
                    {
                        GRVRequesMastertId = (int)sqlDataReader["GRVRequesMastertId"],
                        DateOfRequest = sqlDataReader["DateOfRequest"].ToString(),
                        UserId = sqlDataReader["UserId"].ToString(),
                        UserName = sqlDataReader["UserName"].ToString(),
                        CategoryId = (int)sqlDataReader["CategoryId"],
                        CategoryName = sqlDataReader["CategoryName"].ToString(),
                        SubCategoryId = (int)sqlDataReader["SubCategoryId"],
                        SubCategoryName = sqlDataReader["SubCategoryName"].ToString(),
                        SupervisorId = sqlDataReader["SupervisorId"].ToString(),
                        SupervisoName = sqlDataReader["SupervisoName"].ToString(),
                        IsApproved = (int)sqlDataReader["IsApproved"],
                        Status = sqlDataReader["Status"].ToString(),
                        Image = sqlDataReader["Image"].ToString(),
                        ImagePath = sqlDataReader["ImagePath"].ToString(),
                        BusinessUnitId = (int)sqlDataReader["BusinessUnitId"],
                        ApproverName = sqlDataReader["ApproverName"].ToString(),
                        Issue = sqlDataReader["Issue"].ToString()
                    });
                return commonModelList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.accessManager.SqlConnectionClose();
            }
        }
        //public IEnumerable<KinshipModel> RetriveDataForReportModel(string UserId)
        //{
        //    IEnumerable<KinshipModel> aQLReportModels;
        //    List<KinshipModel> reportlist = new List<KinshipModel>();
        //    try
        //    {
        //        using (SqlConnection cnn = new SqlConnection(DBConnection.GetConnectionString()))
        //        {
        //            if (cnn.State == 0)
        //            {
        //                cnn.Open();
        //            }
        //            else
        //            {
        //                cnn.Close();
        //            }
        //            SqlCommand cmd = new SqlCommand("sp_GRVGetAllRequesttest", cnn)
        //            {
        //                CommandTimeout = 12000,
        //                CommandType = CommandType.StoredProcedure
        //            };
        //           cmd.Parameters.Add("@UserId", SqlDbType.NVarChar).Value = UserId;
        //            //cmd.Parameters.Add("@EndDate", SqlDbType.NVarChar).Value = EndDate;
        //            SqlDataReader dr = cmd.ExecuteReader();
        //            while (dr.Read())
        //            {
        //                KinshipModel reportModel = new KinshipModel()
        //                {
        //                    GRVRequesMastertId = Convert.ToInt32(dr["GRVRequesMastertId"]),
        //                    DateOfRequest = dr["DateOfRequest"].ToString(),
        //                    UserId = dr["UserId"].ToString(),
        //                    UserName = dr["UserName"].ToString(),
        //                    CategoryId = Convert.ToInt32(dr["CategoryId"]),
        //                    CategoryName = dr["CategoryName"].ToString(),
        //                    SubCategoryId = Convert.ToInt32(dr["SubCategoryId"]),
        //                    SubCategoryName = dr["SubCategoryName"].ToString(),
        //                    SupervisorId = dr["SupervisorId"].ToString(),
        //                    SupervisoName = dr["SupervisoName"].ToString(),
        //                    IsApproved = Convert.ToInt32(dr["IsApproved"]),
        //                    Status = dr["Status"].ToString(),
        //                    Image = dr["Image"].ToString(),
        //                    ImagePath = dr["ImagePath"].ToString(),
        //                    BusinessUnitId = Convert.ToInt32(dr["BusinessUnitId"]),
        //                    ApproverName = dr["ApproverName"].ToString(),
        //                    Issue = dr["Issue"].ToString(),
        //                };
        //                reportlist.Add(reportModel);
        //            }
        //        }
        //        aQLReportModels = reportlist;
        //    }
        //    catch (Exception exception)
        //    {
        //        throw exception;
        //    }
        //    return aQLReportModels;
        //}
        public List<KinshipModel> GetComment(int GRVRequesMastertId)
        {
            try
            {
                this.accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                List<KinshipModel> commonModelList = new List<KinshipModel>();
                SqlDataReader sqlDataReader = this.accessManager.GetSqlDataReader("sp_GRVGetComment", new List<SqlParameter>()
        {
          new SqlParameter("@GRVRequesMastertId", (object) GRVRequesMastertId)
        });
                while (sqlDataReader.Read())
                    commonModelList.Add(new KinshipModel()
                    {
                        GRVRequesMastertId = (int)sqlDataReader["GRVRequesMastertId"],
                        ApproverName = sqlDataReader["ApproverName"].ToString(),
                        ReviewComment = sqlDataReader["ReviewComment"].ToString(),
                        UpdateDate = sqlDataReader["UpdateDate"].ToString()
                    });
                return commonModelList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.accessManager.SqlConnectionClose();
            }
        }
        //public IEnumerable<KinshipModel> GetComment(int GRVRequesMastertId)
        //{
        //    IEnumerable<KinshipModel> aQLReportModels;
        //    List<KinshipModel> reportlist = new List<KinshipModel>();
        //    try
        //    {
        //        using (SqlConnection cnn = new SqlConnection(DBConnection.GetConnectionString()))
        //        {
        //            if (cnn.State == 0)
        //            {
        //                cnn.Open();
        //            }
        //            else
        //            {
        //                cnn.Close();
        //            }
        //            SqlCommand cmd = new SqlCommand("sp_GRVGetComment", cnn)
        //            {
        //                CommandTimeout = 12000,
        //                CommandType = CommandType.StoredProcedure
        //            };
        //            cmd.Parameters.Add("@GRVRequesMastertId", SqlDbType.NVarChar).Value = GRVRequesMastertId;
        //            //cmd.Parameters.Add("@EndDate", SqlDbType.NVarChar).Value = EndDate;
        //            SqlDataReader dr = cmd.ExecuteReader();
        //            while (dr.Read())
        //            {
        //                KinshipModel reportModel = new KinshipModel()
        //                {
        //                    GRVRequesMastertId = Convert.ToInt32(dr["GRVRequesMastertId"]),
        //                    ApproverName = dr["ApproverName"].ToString(),
        //                    ReviewComment = dr["ReviewComment"].ToString(),
        //                };
        //                reportlist.Add(reportModel);
        //            }
        //        }
        //        aQLReportModels = reportlist;
        //    }
        //    catch (Exception exception)
        //    {
        //        throw exception;
        //    }
        //    return aQLReportModels;
        //}
    }

}
