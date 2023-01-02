using QMSWebAPI.Models;
using QMSWebAPI.Models.StockLot;
using SQIndustryThree.DataManager;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace QMSWebAPI.DAL
{
    public class KinshipEventDAL
    {

        DataAccessManager accessManager = new DataAccessManager();

      

        public BaseResultResponse GetAllEvent()
        {
            try
            {

                List<KinshipEvent> eventList = new List<KinshipEvent>();
                accessManager.SqlConnectionOpen(DataBase.KinshipEventDB);

                List<SqlParameter> aParameters = new List<SqlParameter>();             

                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetAllEvent", aParameters);
                while (dr.Read())
                {
                    var baseSaticUrl = "http://119.148.12.173:8040/";
                    KinshipEvent kinshipEvent = new KinshipEvent();
                    kinshipEvent.Id = (int)dr["Id"];
                    kinshipEvent.Name = dr["Name"].ToString();
                    kinshipEvent.Icon = baseSaticUrl + dr["Icon"].ToString();
                    kinshipEvent.IsActive = (bool)dr["IsActive"];
                    kinshipEvent.Type = (int)dr["Type"];
                    kinshipEvent.Title = dr["Title"].ToString();                   
                    kinshipEvent.BannerUrl = baseSaticUrl + dr["BannerUrl"].ToString();
                    kinshipEvent.AltBannerUrl = baseSaticUrl + dr["AltBannerUrl"].ToString();

                    eventList.Add(kinshipEvent);
                }

                var baseResultResponse = baseResultResponseMethode(eventList);

                return baseResultResponse;
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
        public ResultResponse Save(Registration registration)
        {
            try
            {

                accessManager.SqlConnectionOpen(DataBase.KinshipEventDB);
                ResultResponse result = new ResultResponse();
                //DataValidation(registration);

                List<SqlParameter> aParameters = new List<SqlParameter>();

                aParameters.Add(new SqlParameter("@Name", registration.Name));
                aParameters.Add(new SqlParameter("@UserId", registration.UserId));
                aParameters.Add(new SqlParameter("@Location", registration.Location));
                aParameters.Add(new SqlParameter("@Designation", registration.Designation));
                aParameters.Add(new SqlParameter("@BusinessUnitId", registration.BusinessUnitId));
                aParameters.Add(new SqlParameter("@BusinessUnit", registration.BusinessUnit));
                aParameters.Add(new SqlParameter("@CreateDateTime", DateTime.Now));

                result.pk = accessManager.SaveDataReturnPrimaryKey("sp_Save", aParameters);

                if (result.pk > 0)
                {
                    result.msg = "Data Save Successfully";
                    result.isSuccess = true;
                }
                else
                {
                    result.msg = "Already Participated";
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public BaseResultResponse GetInstructions(int type)
        {
            try
            {

                List<Instructions> eventList = new List<Instructions>();
                accessManager.SqlConnectionOpen(DataBase.KinshipEventDB);

                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@type", type));

                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetInstructions", aParameters);
                while (dr.Read())
                {
                    Instructions instructions = new Instructions();
                    instructions.Id = (int)dr["Id"];
                    instructions.Name = dr["Name"].ToString();
                    instructions.Type = (int)dr["Type"];
                    
                    eventList.Add(instructions);
                }

                var baseResultResponse = baseResultResponseMethode(eventList);

                return baseResultResponse;
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


        #region private

        private BaseResultResponse baseResultResponseMethode(dynamic data)
        {
            try
            {
                BaseResultResponse baseResultResponse = new BaseResultResponse();
                if (data.Count > 0)
                {
                    baseResultResponse.data = data;
                    baseResultResponse.msg = "Successful";
                    baseResultResponse.isSuccess = true;
                    baseResultResponse.status = "200";
                }
                else
                {
                    baseResultResponse.data = data;
                    baseResultResponse.msg = "No Record Found";
                    baseResultResponse.isSuccess = false;
                    baseResultResponse.status = "204";
                }

                return baseResultResponse;
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion
    }
}