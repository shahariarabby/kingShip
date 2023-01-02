using Dapper;
using ExcelDataReader;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QMSWebAPI.Models;
using QMSWebAPI.Models.SustainabilityModels;
using QMSWebAPI.Models.SustainabilityModels.SustainabilityViewModels;
using QMSWebAPI.Utilities;
using SQIndustryThree.DataManager;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace QMSWebAPI.DAL
{
    public class SustainabilityDAL
    {
        private DataAccessManager accessManager = new DataAccessManager();

        #region Sustainability Survey
        public ResultResponse SaveSustainabilitySurvey(List<SustainabilitySurvey> sustainabilitySurveys)
        {
            try
            {

                accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                ResultResponse result = new ResultResponse();

                foreach (var sustainabilitySurvey in sustainabilitySurveys)
                {
                    DataValidation(sustainabilitySurvey);

                    bool status = true;

                    //var code = date.ToString("MM-dd-yyyy");
                    List<SqlParameter> aParameters = new List<SqlParameter>();

                    aParameters.Add(new SqlParameter("@CreateDateTime", DateTime.Now));
                    aParameters.Add(new SqlParameter("@Question", sustainabilitySurvey.Question));
                    aParameters.Add(new SqlParameter("@Answer", sustainabilitySurvey.Answer));
                    aParameters.Add(new SqlParameter("@Score", sustainabilitySurvey.Score));
                    aParameters.Add(new SqlParameter("@UserId", sustainabilitySurvey.UserId));
                    aParameters.Add(new SqlParameter("@BusinessUnitId", sustainabilitySurvey.BusinessUnitId));
                    aParameters.Add(new SqlParameter("@Status", status));

                    result.isSuccess = accessManager.SaveData("sp_SaveSustainabilitySurvey", aParameters);
                }

                result.msg = "Sustainability Survey Added Successfully";
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
        public SustainabilitySurvey GetSustainabilitySurveyByUser(string UserId)
        {
            try
            {

                accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                ResultResponse result = new ResultResponse();


                SustainabilitySurvey sustainabilitySurvey = new SustainabilitySurvey();
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@UserId", UserId));

                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetSustainabilitySurveyByUserId", aParameters);

                while (dr.Read())
                {

                    sustainabilitySurvey.Question = dr["Question"].ToString();
                    sustainabilitySurvey.Answer = dr["Answer"].ToString();
                    sustainabilitySurvey.Score = (long)dr["Score"];
                    sustainabilitySurvey.BusinessUnitId = (long)dr["BusinessUnitId"];
                    sustainabilitySurvey.UserId = dr["UserId"].ToString();
                    sustainabilitySurvey.Status = (bool)dr["Status"];

                }


                return sustainabilitySurvey;

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

        #endregion

        #region Daily Task
        public List<DailyTaskList> GetDailyTaskList()
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.KinshipDB);

                List<DailyTaskList> dailyTaskList = new List<DailyTaskList>();

                List<SqlParameter> aParameters = new List<SqlParameter>();

                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetDailyTaskList", aParameters);

                while (dr.Read())
                {

                    DailyTaskList dailyTaskListVM = new DailyTaskList();
                    //var dateTime = Convert.ToDateTime(dr["CreateDateTime"]).ToString("dd-MMM-yy hh:mm tt");

                    dailyTaskListVM.Id = (long)dr["Id"];
                    dailyTaskListVM.CreateDateTime = (DateTime?)dr["CreateDateTime"];
                    dailyTaskListVM.BanglaName = dr["BanglaName"].ToString();
                    dailyTaskListVM.EnglishName = dr["EnglishName"].ToString();
                    dailyTaskListVM.Type = dr["Type"].ToString();
                    dailyTaskListVM.ImageName = dr["ImageName"].ToString();
                    dailyTaskListVM.ImageUrl = dr["ImageUrl"].ToString();
                    dailyTaskListVM.ImagePath = dr["ImagePath"].ToString();
                    dailyTaskListVM.InfoLink = dr["InfoLink"].ToString();

                    //dailyTaskListVM.Action = (long)dr["Action"];
                    dailyTaskListVM.Co2 = dr["Co2"].ToString();
                    dailyTaskListVM.Water = dr["Water"].ToString();
                    dailyTaskListVM.Energy = dr["Energy"].ToString();
                    dailyTaskListVM.Kindness = dr["Kindness"].ToString();

                    dailyTaskListVM.SustainabilityCategoryId = (long)dr["SustainabilityCategoryId"];


                    dailyTaskList.Add(dailyTaskListVM);
                }
                return dailyTaskList;
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
        public CopyDailyTaskList GetCopyDailyTaskList(string type)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.KinshipDB);

                List<CopyDailyTaskList> dailyTaskList = new List<CopyDailyTaskList>();

                List<SqlParameter> aParameters = new List<SqlParameter>();
                //aParameters.Add(new SqlParameter("@dailyTaskId", id));
                aParameters.Add(new SqlParameter("@type", type));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetCopyDailyTaskList", aParameters);

                CopyDailyTaskList dailyTaskListVM = new CopyDailyTaskList();
                while (dr.Read())
                {


                    //var dateTime = Convert.ToDateTime(dr["CreateDateTime"]).ToString("dd-MMM-yy hh:mm tt");

                    dailyTaskListVM.Id = (long)dr["Id"];
                    dailyTaskListVM.DailyTaskListId = (long)dr["DailyTaskListId"];
                    dailyTaskListVM.CreateDateTime = (DateTime?)dr["CreateDateTime"];
                    dailyTaskListVM.BanglaName = dr["BanglaName"].ToString();
                    dailyTaskListVM.EnglishName = dr["EnglishName"].ToString();
                    dailyTaskListVM.Type = dr["Type"].ToString();
                    dailyTaskListVM.ImageName = dr["ImageName"].ToString();
                    dailyTaskListVM.ImageUrl = dr["ImageUrl"].ToString();
                    dailyTaskListVM.ImagePath = dr["ImagePath"].ToString();
                    dailyTaskListVM.InfoLink = dr["InfoLink"].ToString();

                    //dailyTaskListVM.Action = (long)dr["Action"];
                    dailyTaskListVM.Co2 = dr["Co2"].ToString();
                    dailyTaskListVM.Water = dr["Water"].ToString();
                    dailyTaskListVM.Energy = dr["Energy"].ToString();
                    dailyTaskListVM.Kindness = dr["Kindness"].ToString();

                    dailyTaskListVM.SustainabilityCategoryId = (long)dr["SustainabilityCategoryId"];


                    //dailyTaskList.Add(dailyTaskListVM);
                }
                return dailyTaskListVM;
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
        public DailyTaskList GetDailyTaskListById(long taskId)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.KinshipDB);

                List<CopyDailyTaskList> dailyTaskList = new List<CopyDailyTaskList>();

                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@TaskId", taskId));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetDailyTaskListById", aParameters);

                DailyTaskList dailyTaskListVM = new DailyTaskList();
                while (dr.Read())
                {
                    dailyTaskListVM.Id = (long)dr["Id"];
                    dailyTaskListVM.CreateDateTime = (DateTime?)dr["CreateDateTime"];
                    dailyTaskListVM.BanglaName = dr["BanglaName"].ToString();
                    dailyTaskListVM.EnglishName = dr["EnglishName"].ToString();
                    dailyTaskListVM.Type = dr["Type"].ToString();
                    dailyTaskListVM.ImageName = dr["ImageName"].ToString();
                    dailyTaskListVM.ImageUrl = dr["ImageUrl"].ToString();
                    dailyTaskListVM.ImagePath = dr["ImagePath"].ToString();
                    dailyTaskListVM.InfoLink = dr["InfoLink"].ToString();
                    dailyTaskListVM.Co2 = dr["Co2"].ToString();
                    dailyTaskListVM.Water = dr["Water"].ToString();
                    dailyTaskListVM.Energy = dr["Energy"].ToString();
                    dailyTaskListVM.Kindness = dr["Kindness"].ToString();

                    dailyTaskListVM.SustainabilityCategoryId = (long)dr["SustainabilityCategoryId"];
                }
                return dailyTaskListVM;
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
        public List<DailyTaskList> GetDailyTaskListSimple()
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.KinshipDB);

                List<DailyTaskList> dailyTaskList = new List<DailyTaskList>();

                List<SqlParameter> aParameters = new List<SqlParameter>();

                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetDailyTaskListSimple", aParameters);

                while (dr.Read())
                {

                    DailyTaskList dailyTaskListVM = new DailyTaskList();
                    //var dateTime = Convert.ToDateTime(dr["CreateDateTime"]).ToString("dd-MMM-yy hh:mm tt");

                    dailyTaskListVM.Id = (long)dr["Id"];
                    dailyTaskListVM.CreateDateTime = (DateTime?)dr["CreateDateTime"];
                    dailyTaskListVM.BanglaName = dr["BanglaName"].ToString();
                    dailyTaskListVM.EnglishName = dr["EnglishName"].ToString();
                    dailyTaskListVM.Type = dr["Type"].ToString();
                    dailyTaskListVM.ImageName = dr["ImageName"].ToString();
                    dailyTaskListVM.ImageUrl = dr["ImageUrl"].ToString();
                    dailyTaskListVM.ImagePath = dr["ImagePath"].ToString();
                    dailyTaskListVM.InfoLink = dr["InfoLink"].ToString();

                    //dailyTaskListVM.Action = (long)dr["Action"];
                    dailyTaskListVM.Co2 = dr["Co2"].ToString();
                    dailyTaskListVM.Water = dr["Water"].ToString();
                    dailyTaskListVM.Energy = dr["Energy"].ToString();
                    dailyTaskListVM.Kindness = dr["Kindness"].ToString();

                    dailyTaskListVM.SustainabilityCategoryId = (long)dr["SustainabilityCategoryId"];


                    dailyTaskList.Add(dailyTaskListVM);
                }
                return dailyTaskList;
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

        public List<DailyTaskList> GetDailyTaskListMedium()
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.KinshipDB);

                List<DailyTaskList> dailyTaskList = new List<DailyTaskList>();

                List<SqlParameter> aParameters = new List<SqlParameter>();

                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetDailyTaskListMedium", aParameters);

                while (dr.Read())
                {

                    DailyTaskList dailyTaskListVM = new DailyTaskList();
                    //var dateTime = Convert.ToDateTime(dr["CreateDateTime"]).ToString("dd-MMM-yy hh:mm tt");

                    dailyTaskListVM.Id = (long)dr["Id"];
                    dailyTaskListVM.CreateDateTime = (DateTime?)dr["CreateDateTime"];
                    dailyTaskListVM.BanglaName = dr["BanglaName"].ToString();
                    dailyTaskListVM.EnglishName = dr["EnglishName"].ToString();
                    dailyTaskListVM.Type = dr["Type"].ToString();
                    dailyTaskListVM.ImageName = dr["ImageName"].ToString();
                    dailyTaskListVM.ImageUrl = dr["ImageUrl"].ToString();
                    dailyTaskListVM.ImagePath = dr["ImagePath"].ToString();
                    dailyTaskListVM.InfoLink = dr["InfoLink"].ToString();

                    //dailyTaskListVM.Action = (long)dr["Action"];
                    dailyTaskListVM.Co2 = dr["Co2"].ToString();
                    dailyTaskListVM.Water = dr["Water"].ToString();
                    dailyTaskListVM.Energy = dr["Energy"].ToString();
                    dailyTaskListVM.Kindness = dr["Kindness"].ToString();

                    dailyTaskListVM.SustainabilityCategoryId = (long)dr["SustainabilityCategoryId"];


                    dailyTaskList.Add(dailyTaskListVM);
                }
                return dailyTaskList;
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

        public List<DailyTaskList> GetDailyTaskListHard()
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.KinshipDB);

                List<DailyTaskList> dailyTaskList = new List<DailyTaskList>();

                List<SqlParameter> aParameters = new List<SqlParameter>();

                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetDailyTaskListHard", aParameters);

                while (dr.Read())
                {

                    DailyTaskList dailyTaskListVM = new DailyTaskList();
                    //var dateTime = Convert.ToDateTime(dr["CreateDateTime"]).ToString("dd-MMM-yy hh:mm tt");

                    dailyTaskListVM.Id = (long)dr["Id"];
                    dailyTaskListVM.CreateDateTime = (DateTime?)dr["CreateDateTime"];
                    dailyTaskListVM.BanglaName = dr["BanglaName"].ToString();
                    dailyTaskListVM.EnglishName = dr["EnglishName"].ToString();
                    dailyTaskListVM.Type = dr["Type"].ToString();
                    dailyTaskListVM.ImageName = dr["ImageName"].ToString();
                    dailyTaskListVM.ImageUrl = dr["ImageUrl"].ToString();
                    dailyTaskListVM.ImagePath = dr["ImagePath"].ToString();
                    dailyTaskListVM.InfoLink = dr["InfoLink"].ToString();

                    //dailyTaskListVM.Action = (long)dr["Action"];
                    dailyTaskListVM.Co2 = dr["Co2"].ToString();
                    dailyTaskListVM.Water = dr["Water"].ToString();
                    dailyTaskListVM.Energy = dr["Energy"].ToString();
                    dailyTaskListVM.Kindness = dr["Kindness"].ToString();

                    dailyTaskListVM.SustainabilityCategoryId = (long)dr["SustainabilityCategoryId"];


                    dailyTaskList.Add(dailyTaskListVM);
                }
                return dailyTaskList;
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

        public List<DailyTaskListViewModel> GetDailyTaskListAndDetails()
        {
            try
            {
                var list = GetDailyTaskList();

                accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                List<DailyTaskListViewModel> dailyTaskList = new List<DailyTaskListViewModel>();

                foreach (var dailyTask in list)
                {
                    DailyTaskListViewModel dailyTaskListVM = new DailyTaskListViewModel();

                    var dateTime = dailyTask.CreateDateTime?.ToString("dd-MMM-yy hh:mm tt");

                    dailyTaskListVM.Id = dailyTask.Id;
                    dailyTaskListVM.CreateDate = dateTime.Split(' ')[0];
                    dailyTaskListVM.CreateTime = dateTime.Split(' ')[1] + dateTime.Split(' ')[2];
                    dailyTaskListVM.BanglaName = dailyTask.BanglaName.ToString();
                    dailyTaskListVM.EnglishName = dailyTask.EnglishName.ToString();
                    dailyTaskListVM.Type = dailyTask.Type.ToString();
                    dailyTaskListVM.ImageName = dailyTask.ImageName.ToString();
                    dailyTaskListVM.ImageUrl = dailyTask.ImageUrl.ToString();
                    dailyTaskListVM.ImagePath = dailyTask.ImagePath.ToString();
                    dailyTaskListVM.InfoLink = dailyTask.InfoLink.ToString();
                    dailyTaskListVM.SustainabilityCategoryId = dailyTask.SustainabilityCategoryId;
                    //dailyTaskListVM.Action = dailyTask.Action;
                    dailyTaskListVM.Co2 = dailyTask.Co2;
                    dailyTaskListVM.Water = dailyTask.Water;
                    dailyTaskListVM.Energy = dailyTask.Energy;
                    dailyTaskListVM.Kindness = dailyTask.Kindness;

                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@dailyTaskListId", dailyTaskListVM.Id));
                    SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetDailyTaskListDetails", aParameters);

                    List<DailyTaskListDetailsViewModel> detailList = new List<DailyTaskListDetailsViewModel>();

                    while (dr.Read())
                    {
                        DailyTaskListDetailsViewModel dailyTaskListDetails = new DailyTaskListDetailsViewModel();


                        dailyTaskListDetails.Id = (long)dr["Id"];
                        dailyTaskListDetails.DailyTaskListId = (long)dr["DailyTaskListId"];
                        dailyTaskListDetails.Title = dr["Title"].ToString();
                        dailyTaskListDetails.Note = dr["Note"].ToString();
                        dailyTaskListDetails.TitleBangla = dr["TitleBangla"].ToString();
                        dailyTaskListDetails.NoteBangla = dr["NoteBangla"].ToString();
                        dailyTaskListDetails.Islink = (bool)dr["Islink"];

                        detailList.Add(dailyTaskListDetails);
                    }
                    //dr.NextResult();
                    dr.Close();

                    dailyTaskListVM.DailyTaskListDetailsViewModels = detailList;

                    dailyTaskList.Add(dailyTaskListVM);
                }
                return dailyTaskList;
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

        public DailyTaskListViewModel GetDailyTaskListAndDetailsSimple()
        {
            try
            {
                var list = GetDailyTaskListSimple();

                var type = "S";
                var copyList = GetCopyDailyTaskList(type);

                accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                //List<DailyTaskListViewModel> dailyTaskList = new List<DailyTaskListViewModel>();
                DailyTaskListViewModel dailyTaskListVM = new DailyTaskListViewModel();
                foreach (var dailyTask in list)
                {
                    // save 
                    //var copyList = GetCopyDailyTaskList(dailyTask.Id);


                    if (copyList.Id > 0)
                    {
                        var dateTime = copyList.CreateDateTime?.ToString("dd-MMM-yy hh:mm tt");

                        dailyTaskListVM.Id = copyList.DailyTaskListId;
                        dailyTaskListVM.CreateDate = dateTime.Split(' ')[0];
                        dailyTaskListVM.CreateTime = dateTime.Split(' ')[1] + dateTime.Split(' ')[2];
                        dailyTaskListVM.BanglaName = copyList.BanglaName.ToString();
                        dailyTaskListVM.EnglishName = copyList.EnglishName.ToString();
                        dailyTaskListVM.Type = copyList.Type.ToString();
                        dailyTaskListVM.ImageName = copyList.ImageName.ToString();
                        dailyTaskListVM.ImageUrl = copyList.ImageUrl.ToString();
                        dailyTaskListVM.ImagePath = copyList.ImagePath.ToString();
                        dailyTaskListVM.InfoLink = copyList.InfoLink.ToString();
                        dailyTaskListVM.SustainabilityCategoryId = copyList.SustainabilityCategoryId;
                        //dailyTaskListVM.Action = dailyTask.Action;
                        dailyTaskListVM.Co2 = copyList.Co2;
                        dailyTaskListVM.Water = copyList.Water;
                        dailyTaskListVM.Energy = copyList.Energy;
                        dailyTaskListVM.Kindness = copyList.Kindness;

                        List<SqlParameter> aParameters = new List<SqlParameter>();
                        aParameters.Add(new SqlParameter("@dailyTaskListId", copyList.DailyTaskListId));
                        SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetDailyTaskListDetails", aParameters);

                        List<DailyTaskListDetailsViewModel> detailList = new List<DailyTaskListDetailsViewModel>();

                        while (dr.Read())
                        {
                            DailyTaskListDetailsViewModel dailyTaskListDetails = new DailyTaskListDetailsViewModel();


                            dailyTaskListDetails.Id = (long)dr["Id"];
                            dailyTaskListDetails.DailyTaskListId = (long)dr["DailyTaskListId"];
                            dailyTaskListDetails.Title = dr["Title"].ToString();
                            dailyTaskListDetails.Note = dr["Note"].ToString();
                            dailyTaskListDetails.TitleBangla = dr["TitleBangla"].ToString();
                            dailyTaskListDetails.NoteBangla = dr["NoteBangla"].ToString();
                            dailyTaskListDetails.Islink = (bool)dr["Islink"];

                            detailList.Add(dailyTaskListDetails);
                        }
                        //dr.NextResult();
                        dr.Close();

                        dailyTaskListVM.DailyTaskListDetailsViewModels = detailList;
                    }
                    else
                    {
                        //accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                        List<SqlParameter> aParameters2 = new List<SqlParameter>();

                        var date = DateTime.Now.ToString("dd-MMM-yy hh:mm tt");

                        aParameters2.Add(new SqlParameter("@DailyTaskListId", dailyTask.Id));
                        aParameters2.Add(new SqlParameter("@CreateDateTime", date.Split(' ')[0]));
                        aParameters2.Add(new SqlParameter("@BanglaName", dailyTask.BanglaName));
                        aParameters2.Add(new SqlParameter("@EnglishName", dailyTask.EnglishName));
                        aParameters2.Add(new SqlParameter("@Type", dailyTask.Type));
                        aParameters2.Add(new SqlParameter("@ImageName", dailyTask.ImageName));
                        aParameters2.Add(new SqlParameter("@ImagePath", dailyTask.ImagePath));
                        aParameters2.Add(new SqlParameter("@ImageUrl", dailyTask.ImageUrl));
                        aParameters2.Add(new SqlParameter("@InfoLink", dailyTask.InfoLink));
                        aParameters2.Add(new SqlParameter("@SustainabilityCategoryId", dailyTask.SustainabilityCategoryId));

                        aParameters2.Add(new SqlParameter("@Co2", dailyTask.Co2));
                        aParameters2.Add(new SqlParameter("@Water", dailyTask.Water));
                        aParameters2.Add(new SqlParameter("@Energy", dailyTask.Energy));
                        aParameters2.Add(new SqlParameter("@Kindness", dailyTask.Kindness));

                        accessManager.SaveData("sp_SaveCopyDailyTaskList", aParameters2);

                        accessManager.SqlConnectionClose();




                        var copyListNew = GetCopyDailyTaskList(type);

                        accessManager.SqlConnectionOpen(DataBase.KinshipDB);

                        var dateTime = copyListNew.CreateDateTime?.ToString("dd-MMM-yy hh:mm tt");

                        dailyTaskListVM.Id = copyListNew.DailyTaskListId;
                        dailyTaskListVM.CreateDate = dateTime.Split(' ')[0];
                        dailyTaskListVM.CreateTime = dateTime.Split(' ')[1] + dateTime.Split(' ')[2];
                        dailyTaskListVM.BanglaName = copyListNew.BanglaName.ToString();
                        dailyTaskListVM.EnglishName = copyListNew.EnglishName.ToString();
                        dailyTaskListVM.Type = copyListNew.Type.ToString();
                        dailyTaskListVM.ImageName = copyListNew.ImageName.ToString();
                        dailyTaskListVM.ImageUrl = copyListNew.ImageUrl.ToString();
                        dailyTaskListVM.ImagePath = copyListNew.ImagePath.ToString();
                        dailyTaskListVM.InfoLink = copyListNew.InfoLink.ToString();
                        dailyTaskListVM.SustainabilityCategoryId = copyListNew.SustainabilityCategoryId;
                        //dailyTaskListVM.Action = dailyTask.Action;
                        dailyTaskListVM.Co2 = copyListNew.Co2;
                        dailyTaskListVM.Water = copyListNew.Water;
                        dailyTaskListVM.Energy = copyListNew.Energy;
                        dailyTaskListVM.Kindness = copyListNew.Kindness;

                        List<SqlParameter> aParameters = new List<SqlParameter>();
                        aParameters.Add(new SqlParameter("@dailyTaskListId", copyListNew.DailyTaskListId));
                        SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetDailyTaskListDetails", aParameters);

                        List<DailyTaskListDetailsViewModel> detailList = new List<DailyTaskListDetailsViewModel>();

                        while (dr.Read())
                        {
                            DailyTaskListDetailsViewModel dailyTaskListDetails = new DailyTaskListDetailsViewModel();


                            dailyTaskListDetails.Id = (long)dr["Id"];
                            dailyTaskListDetails.DailyTaskListId = (long)dr["DailyTaskListId"];
                            dailyTaskListDetails.Title = dr["Title"].ToString();
                            dailyTaskListDetails.Note = dr["Note"].ToString();
                            dailyTaskListDetails.TitleBangla = dr["TitleBangla"].ToString();
                            dailyTaskListDetails.NoteBangla = dr["NoteBangla"].ToString();
                            dailyTaskListDetails.Islink = (bool)dr["Islink"];

                            detailList.Add(dailyTaskListDetails);
                        }
                        //dr.NextResult();
                        dr.Close();

                        dailyTaskListVM.DailyTaskListDetailsViewModels = detailList;
                    }




                    #region old

                    //var dateTime = dailyTask.CreateDateTime?.ToString("dd-MMM-yy hh:mm tt");

                    //dailyTaskListVM.Id = dailyTask.Id;
                    //dailyTaskListVM.CreateDate = dateTime.Split(' ')[0];
                    //dailyTaskListVM.CreateTime = dateTime.Split(' ')[1] + dateTime.Split(' ')[2];
                    //dailyTaskListVM.BanglaName = dailyTask.BanglaName.ToString();
                    //dailyTaskListVM.EnglishName = dailyTask.EnglishName.ToString();
                    //dailyTaskListVM.Type = dailyTask.Type.ToString();
                    //dailyTaskListVM.ImageName = dailyTask.ImageName.ToString();
                    //dailyTaskListVM.ImageUrl = dailyTask.ImageUrl.ToString();
                    //dailyTaskListVM.ImagePath = dailyTask.ImagePath.ToString();
                    //dailyTaskListVM.InfoLink = dailyTask.InfoLink.ToString();
                    //dailyTaskListVM.SustainabilityCategoryId = dailyTask.SustainabilityCategoryId;
                    ////dailyTaskListVM.Action = dailyTask.Action;
                    //dailyTaskListVM.Co2 = dailyTask.Co2;
                    //dailyTaskListVM.Water = dailyTask.Water;
                    //dailyTaskListVM.Energy = dailyTask.Energy;
                    //dailyTaskListVM.Kindness = dailyTask.Kindness;

                    //List<SqlParameter> aParameters = new List<SqlParameter>();
                    //aParameters.Add(new SqlParameter("@dailyTaskListId", dailyTaskListVM.Id));
                    //SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetDailyTaskListDetails", aParameters);

                    //List<DailyTaskListDetailsViewModel> detailList = new List<DailyTaskListDetailsViewModel>();

                    //while (dr.Read())
                    //{
                    //    DailyTaskListDetailsViewModel dailyTaskListDetails = new DailyTaskListDetailsViewModel();


                    //    dailyTaskListDetails.Id = (long)dr["Id"];
                    //    dailyTaskListDetails.DailyTaskListId = (long)dr["DailyTaskListId"];
                    //    dailyTaskListDetails.Title = dr["Title"].ToString();
                    //    dailyTaskListDetails.Note = dr["Note"].ToString();
                    //    dailyTaskListDetails.TitleBangla = dr["TitleBangla"].ToString();
                    //    dailyTaskListDetails.NoteBangla = dr["NoteBangla"].ToString();
                    //    dailyTaskListDetails.Islink = (bool)dr["Islink"];

                    //    detailList.Add(dailyTaskListDetails);
                    //}
                    ////dr.NextResult();
                    //dr.Close();

                    //dailyTaskListVM.DailyTaskListDetailsViewModels = detailList;

                    #endregion


                    //dailyTaskList.Add(dailyTaskListVM);
                }
                return dailyTaskListVM;
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

        public DailyTaskListViewModel GetDailyTaskListAndDetailsMedium()
        {
            try
            {
                var list = GetDailyTaskListMedium();
                var type = "M";
                var copyList = GetCopyDailyTaskList(type);

                accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                //List<DailyTaskListViewModel> dailyTaskList = new List<DailyTaskListViewModel>();
                DailyTaskListViewModel dailyTaskListVM = new DailyTaskListViewModel();
                foreach (var dailyTask in list)
                {

                    if (copyList.Id > 0)
                    {
                        var dateTime = copyList.CreateDateTime?.ToString("dd-MMM-yy hh:mm tt");

                        dailyTaskListVM.Id = copyList.DailyTaskListId;
                        dailyTaskListVM.CreateDate = dateTime.Split(' ')[0];
                        dailyTaskListVM.CreateTime = dateTime.Split(' ')[1] + dateTime.Split(' ')[2];
                        dailyTaskListVM.BanglaName = copyList.BanglaName.ToString();
                        dailyTaskListVM.EnglishName = copyList.EnglishName.ToString();
                        dailyTaskListVM.Type = copyList.Type.ToString();
                        dailyTaskListVM.ImageName = copyList.ImageName.ToString();
                        dailyTaskListVM.ImageUrl = copyList.ImageUrl.ToString();
                        dailyTaskListVM.ImagePath = copyList.ImagePath.ToString();
                        dailyTaskListVM.InfoLink = copyList.InfoLink.ToString();
                        dailyTaskListVM.SustainabilityCategoryId = copyList.SustainabilityCategoryId;
                        //dailyTaskListVM.Action = dailyTask.Action;
                        dailyTaskListVM.Co2 = copyList.Co2;
                        dailyTaskListVM.Water = copyList.Water;
                        dailyTaskListVM.Energy = copyList.Energy;
                        dailyTaskListVM.Kindness = copyList.Kindness;

                        List<SqlParameter> aParameters = new List<SqlParameter>();
                        aParameters.Add(new SqlParameter("@dailyTaskListId", copyList.DailyTaskListId));
                        SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetDailyTaskListDetails", aParameters);

                        List<DailyTaskListDetailsViewModel> detailList = new List<DailyTaskListDetailsViewModel>();

                        while (dr.Read())
                        {
                            DailyTaskListDetailsViewModel dailyTaskListDetails = new DailyTaskListDetailsViewModel();


                            dailyTaskListDetails.Id = (long)dr["Id"];
                            dailyTaskListDetails.DailyTaskListId = (long)dr["DailyTaskListId"];
                            dailyTaskListDetails.Title = dr["Title"].ToString();
                            dailyTaskListDetails.Note = dr["Note"].ToString();
                            dailyTaskListDetails.TitleBangla = dr["TitleBangla"].ToString();
                            dailyTaskListDetails.NoteBangla = dr["NoteBangla"].ToString();
                            dailyTaskListDetails.Islink = (bool)dr["Islink"];

                            detailList.Add(dailyTaskListDetails);
                        }
                        //dr.NextResult();
                        dr.Close();

                        dailyTaskListVM.DailyTaskListDetailsViewModels = detailList;
                    }
                    else
                    {
                        //accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                        List<SqlParameter> aParameters2 = new List<SqlParameter>();

                        var date = DateTime.Now.ToString("dd-MMM-yy hh:mm tt");

                        aParameters2.Add(new SqlParameter("@DailyTaskListId", dailyTask.Id));
                        aParameters2.Add(new SqlParameter("@CreateDateTime", date.Split(' ')[0]));
                        aParameters2.Add(new SqlParameter("@BanglaName", dailyTask.BanglaName));
                        aParameters2.Add(new SqlParameter("@EnglishName", dailyTask.EnglishName));
                        aParameters2.Add(new SqlParameter("@Type", dailyTask.Type));
                        aParameters2.Add(new SqlParameter("@ImageName", dailyTask.ImageName));
                        aParameters2.Add(new SqlParameter("@ImagePath", dailyTask.ImagePath));
                        aParameters2.Add(new SqlParameter("@ImageUrl", dailyTask.ImageUrl));
                        aParameters2.Add(new SqlParameter("@InfoLink", dailyTask.InfoLink));
                        aParameters2.Add(new SqlParameter("@SustainabilityCategoryId", dailyTask.SustainabilityCategoryId));

                        aParameters2.Add(new SqlParameter("@Co2", dailyTask.Co2));
                        aParameters2.Add(new SqlParameter("@Water", dailyTask.Water));
                        aParameters2.Add(new SqlParameter("@Energy", dailyTask.Energy));
                        aParameters2.Add(new SqlParameter("@Kindness", dailyTask.Kindness));

                        accessManager.SaveData("sp_SaveCopyDailyTaskList", aParameters2);

                        accessManager.SqlConnectionClose();




                        var copyListNew = GetCopyDailyTaskList(type);

                        accessManager.SqlConnectionOpen(DataBase.KinshipDB);

                        var dateTime = copyListNew.CreateDateTime?.ToString("dd-MMM-yy hh:mm tt");

                        dailyTaskListVM.Id = copyListNew.DailyTaskListId;
                        dailyTaskListVM.CreateDate = dateTime.Split(' ')[0];
                        dailyTaskListVM.CreateTime = dateTime.Split(' ')[1] + dateTime.Split(' ')[2];
                        dailyTaskListVM.BanglaName = copyListNew.BanglaName.ToString();
                        dailyTaskListVM.EnglishName = copyListNew.EnglishName.ToString();
                        dailyTaskListVM.Type = copyListNew.Type.ToString();
                        dailyTaskListVM.ImageName = copyListNew.ImageName.ToString();
                        dailyTaskListVM.ImageUrl = copyListNew.ImageUrl.ToString();
                        dailyTaskListVM.ImagePath = copyListNew.ImagePath.ToString();
                        dailyTaskListVM.InfoLink = copyListNew.InfoLink.ToString();
                        dailyTaskListVM.SustainabilityCategoryId = copyListNew.SustainabilityCategoryId;
                        //dailyTaskListVM.Action = dailyTask.Action;
                        dailyTaskListVM.Co2 = copyListNew.Co2;
                        dailyTaskListVM.Water = copyListNew.Water;
                        dailyTaskListVM.Energy = copyListNew.Energy;
                        dailyTaskListVM.Kindness = copyListNew.Kindness;

                        List<SqlParameter> aParameters = new List<SqlParameter>();
                        aParameters.Add(new SqlParameter("@dailyTaskListId", copyListNew.DailyTaskListId));
                        SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetDailyTaskListDetails", aParameters);

                        List<DailyTaskListDetailsViewModel> detailList = new List<DailyTaskListDetailsViewModel>();

                        while (dr.Read())
                        {
                            DailyTaskListDetailsViewModel dailyTaskListDetails = new DailyTaskListDetailsViewModel();


                            dailyTaskListDetails.Id = (long)dr["Id"];
                            dailyTaskListDetails.DailyTaskListId = (long)dr["DailyTaskListId"];
                            dailyTaskListDetails.Title = dr["Title"].ToString();
                            dailyTaskListDetails.Note = dr["Note"].ToString();
                            dailyTaskListDetails.TitleBangla = dr["TitleBangla"].ToString();
                            dailyTaskListDetails.NoteBangla = dr["NoteBangla"].ToString();
                            dailyTaskListDetails.Islink = (bool)dr["Islink"];

                            detailList.Add(dailyTaskListDetails);
                        }
                        //dr.NextResult();
                        dr.Close();

                        dailyTaskListVM.DailyTaskListDetailsViewModels = detailList;
                    }

                    #region old

                    //var dateTime = dailyTask.CreateDateTime?.ToString("dd-MMM-yy hh:mm tt");

                    //dailyTaskListVM.Id = dailyTask.Id;
                    //dailyTaskListVM.CreateDate = dateTime.Split(' ')[0];
                    //dailyTaskListVM.CreateTime = dateTime.Split(' ')[1] + dateTime.Split(' ')[2];
                    //dailyTaskListVM.BanglaName = dailyTask.BanglaName.ToString();
                    //dailyTaskListVM.EnglishName = dailyTask.EnglishName.ToString();
                    //dailyTaskListVM.Type = dailyTask.Type.ToString();
                    //dailyTaskListVM.ImageName = dailyTask.ImageName.ToString();
                    //dailyTaskListVM.ImageUrl = dailyTask.ImageUrl.ToString();
                    //dailyTaskListVM.ImagePath = dailyTask.ImagePath.ToString();
                    //dailyTaskListVM.InfoLink = dailyTask.InfoLink.ToString();
                    //dailyTaskListVM.SustainabilityCategoryId = dailyTask.SustainabilityCategoryId;
                    ////dailyTaskListVM.Action = dailyTask.Action;
                    //dailyTaskListVM.Co2 = dailyTask.Co2;
                    //dailyTaskListVM.Water = dailyTask.Water;
                    //dailyTaskListVM.Energy = dailyTask.Energy;
                    //dailyTaskListVM.Kindness = dailyTask.Kindness;

                    //List<SqlParameter> aParameters = new List<SqlParameter>();
                    //aParameters.Add(new SqlParameter("@dailyTaskListId", dailyTaskListVM.Id));
                    //SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetDailyTaskListDetails", aParameters);

                    //List<DailyTaskListDetailsViewModel> detailList = new List<DailyTaskListDetailsViewModel>();

                    //while (dr.Read())
                    //{
                    //    DailyTaskListDetailsViewModel dailyTaskListDetails = new DailyTaskListDetailsViewModel();


                    //    dailyTaskListDetails.Id = (long)dr["Id"];
                    //    dailyTaskListDetails.DailyTaskListId = (long)dr["DailyTaskListId"];
                    //    dailyTaskListDetails.Title = dr["Title"].ToString();
                    //    dailyTaskListDetails.Note = dr["Note"].ToString();
                    //    dailyTaskListDetails.TitleBangla = dr["TitleBangla"].ToString();
                    //    dailyTaskListDetails.NoteBangla = dr["NoteBangla"].ToString();
                    //    dailyTaskListDetails.Islink = (bool)dr["Islink"];

                    //    detailList.Add(dailyTaskListDetails);
                    //}
                    ////dr.NextResult();
                    //dr.Close();

                    //dailyTaskListVM.DailyTaskListDetailsViewModels = detailList;

                    #endregion

                    //dailyTaskList.Add(dailyTaskListVM);
                }
                return dailyTaskListVM;
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

        public DailyTaskListViewModel GetDailyTaskListAndDetailsHard()
        {
            try
            {
                var list = GetDailyTaskListHard();

                var type = "H";
                var copyList = GetCopyDailyTaskList(type);

                accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                //List<DailyTaskListViewModel> dailyTaskList = new List<DailyTaskListViewModel>();
                DailyTaskListViewModel dailyTaskListVM = new DailyTaskListViewModel();

                foreach (var dailyTask in list)
                {
                    if (copyList.Id > 0)
                    {
                        var dateTime = copyList.CreateDateTime?.ToString("dd-MMM-yy hh:mm tt");

                        dailyTaskListVM.Id = copyList.DailyTaskListId;
                        dailyTaskListVM.CreateDate = dateTime.Split(' ')[0];
                        dailyTaskListVM.CreateTime = dateTime.Split(' ')[1] + dateTime.Split(' ')[2];
                        dailyTaskListVM.BanglaName = copyList.BanglaName.ToString();
                        dailyTaskListVM.EnglishName = copyList.EnglishName.ToString();
                        dailyTaskListVM.Type = copyList.Type.ToString();
                        dailyTaskListVM.ImageName = copyList.ImageName.ToString();
                        dailyTaskListVM.ImageUrl = copyList.ImageUrl.ToString();
                        dailyTaskListVM.ImagePath = copyList.ImagePath.ToString();
                        dailyTaskListVM.InfoLink = copyList.InfoLink.ToString();
                        dailyTaskListVM.SustainabilityCategoryId = copyList.SustainabilityCategoryId;
                        //dailyTaskListVM.Action = dailyTask.Action;
                        dailyTaskListVM.Co2 = copyList.Co2;
                        dailyTaskListVM.Water = copyList.Water;
                        dailyTaskListVM.Energy = copyList.Energy;
                        dailyTaskListVM.Kindness = copyList.Kindness;

                        List<SqlParameter> aParameters = new List<SqlParameter>();
                        aParameters.Add(new SqlParameter("@dailyTaskListId", copyList.DailyTaskListId));
                        SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetDailyTaskListDetails", aParameters);

                        List<DailyTaskListDetailsViewModel> detailList = new List<DailyTaskListDetailsViewModel>();

                        while (dr.Read())
                        {
                            DailyTaskListDetailsViewModel dailyTaskListDetails = new DailyTaskListDetailsViewModel();


                            dailyTaskListDetails.Id = (long)dr["Id"];
                            dailyTaskListDetails.DailyTaskListId = (long)dr["DailyTaskListId"];
                            dailyTaskListDetails.Title = dr["Title"].ToString();
                            dailyTaskListDetails.Note = dr["Note"].ToString();
                            dailyTaskListDetails.TitleBangla = dr["TitleBangla"].ToString();
                            dailyTaskListDetails.NoteBangla = dr["NoteBangla"].ToString();
                            dailyTaskListDetails.Islink = (bool)dr["Islink"];

                            detailList.Add(dailyTaskListDetails);
                        }
                        //dr.NextResult();
                        dr.Close();

                        dailyTaskListVM.DailyTaskListDetailsViewModels = detailList;
                    }
                    else
                    {
                        ResultResponse result = new ResultResponse();
                        //accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                        List<SqlParameter> aParameters2 = new List<SqlParameter>();

                        var date = DateTime.Now.ToString("dd-MMM-yy hh:mm tt");

                        aParameters2.Add(new SqlParameter("@DailyTaskListId", dailyTask.Id));
                        aParameters2.Add(new SqlParameter("@CreateDateTime", date.Split(' ')[0]));
                        aParameters2.Add(new SqlParameter("@BanglaName", dailyTask.BanglaName));
                        aParameters2.Add(new SqlParameter("@EnglishName", dailyTask.EnglishName));
                        aParameters2.Add(new SqlParameter("@Type", dailyTask.Type));
                        aParameters2.Add(new SqlParameter("@ImageName", dailyTask.ImageName));
                        aParameters2.Add(new SqlParameter("@ImagePath", dailyTask.ImagePath));
                        aParameters2.Add(new SqlParameter("@ImageUrl", dailyTask.ImageUrl));
                        aParameters2.Add(new SqlParameter("@InfoLink", dailyTask.InfoLink));
                        aParameters2.Add(new SqlParameter("@SustainabilityCategoryId", dailyTask.SustainabilityCategoryId));

                        aParameters2.Add(new SqlParameter("@Co2", dailyTask.Co2));
                        aParameters2.Add(new SqlParameter("@Water", dailyTask.Water));
                        aParameters2.Add(new SqlParameter("@Energy", dailyTask.Energy));
                        aParameters2.Add(new SqlParameter("@Kindness", dailyTask.Kindness));

                        accessManager.SaveData("sp_SaveCopyDailyTaskList", aParameters2);

                        accessManager.SqlConnectionClose();



                        var copyListNew = GetCopyDailyTaskList(type);

                        accessManager.SqlConnectionOpen(DataBase.KinshipDB);

                        var dateTime = copyListNew.CreateDateTime?.ToString("dd-MMM-yy hh:mm tt");

                        dailyTaskListVM.Id = copyListNew.DailyTaskListId;
                        dailyTaskListVM.CreateDate = dateTime.Split(' ')[0];
                        dailyTaskListVM.CreateTime = dateTime.Split(' ')[1] + dateTime.Split(' ')[2];
                        dailyTaskListVM.BanglaName = copyListNew.BanglaName.ToString();
                        dailyTaskListVM.EnglishName = copyListNew.EnglishName.ToString();
                        dailyTaskListVM.Type = copyListNew.Type.ToString();
                        dailyTaskListVM.ImageName = copyListNew.ImageName.ToString();
                        dailyTaskListVM.ImageUrl = copyListNew.ImageUrl.ToString();
                        dailyTaskListVM.ImagePath = copyListNew.ImagePath.ToString();
                        dailyTaskListVM.InfoLink = copyListNew.InfoLink.ToString();
                        dailyTaskListVM.SustainabilityCategoryId = copyListNew.SustainabilityCategoryId;
                        //dailyTaskListVM.Action = dailyTask.Action;
                        dailyTaskListVM.Co2 = copyListNew.Co2;
                        dailyTaskListVM.Water = copyListNew.Water;
                        dailyTaskListVM.Energy = copyListNew.Energy;
                        dailyTaskListVM.Kindness = copyListNew.Kindness;

                        List<SqlParameter> aParameters = new List<SqlParameter>();
                        aParameters.Add(new SqlParameter("@dailyTaskListId", copyListNew.DailyTaskListId));
                        SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetDailyTaskListDetails", aParameters);

                        List<DailyTaskListDetailsViewModel> detailList = new List<DailyTaskListDetailsViewModel>();

                        while (dr.Read())
                        {
                            DailyTaskListDetailsViewModel dailyTaskListDetails = new DailyTaskListDetailsViewModel();


                            dailyTaskListDetails.Id = (long)dr["Id"];
                            dailyTaskListDetails.DailyTaskListId = (long)dr["DailyTaskListId"];
                            dailyTaskListDetails.Title = dr["Title"].ToString();
                            dailyTaskListDetails.Note = dr["Note"].ToString();
                            dailyTaskListDetails.TitleBangla = dr["TitleBangla"].ToString();
                            dailyTaskListDetails.NoteBangla = dr["NoteBangla"].ToString();
                            dailyTaskListDetails.Islink = (bool)dr["Islink"];

                            detailList.Add(dailyTaskListDetails);
                        }
                        //dr.NextResult();
                        dr.Close();

                        dailyTaskListVM.DailyTaskListDetailsViewModels = detailList;


                    }



                    #region old

                    //var dateTime = dailyTask.CreateDateTime?.ToString("dd-MMM-yy hh:mm tt");

                    //dailyTaskListVM.Id = dailyTask.Id;
                    //dailyTaskListVM.CreateDate = dateTime.Split(' ')[0];
                    //dailyTaskListVM.CreateTime = dateTime.Split(' ')[1] + dateTime.Split(' ')[2];
                    //dailyTaskListVM.BanglaName = dailyTask.BanglaName.ToString();
                    //dailyTaskListVM.EnglishName = dailyTask.EnglishName.ToString();
                    //dailyTaskListVM.Type = dailyTask.Type.ToString();
                    //dailyTaskListVM.ImageName = dailyTask.ImageName.ToString();
                    //dailyTaskListVM.ImageUrl = dailyTask.ImageUrl.ToString();
                    //dailyTaskListVM.ImagePath = dailyTask.ImagePath.ToString();
                    //dailyTaskListVM.InfoLink = dailyTask.InfoLink.ToString();
                    //dailyTaskListVM.SustainabilityCategoryId = dailyTask.SustainabilityCategoryId;
                    ////dailyTaskListVM.Action = dailyTask.Action;
                    //dailyTaskListVM.Co2 = dailyTask.Co2;
                    //dailyTaskListVM.Water = dailyTask.Water;
                    //dailyTaskListVM.Energy = dailyTask.Energy;
                    //dailyTaskListVM.Kindness = dailyTask.Kindness;

                    //List<SqlParameter> aParameters = new List<SqlParameter>();
                    //aParameters.Add(new SqlParameter("@dailyTaskListId", dailyTaskListVM.Id));
                    //SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetDailyTaskListDetails", aParameters);

                    //List<DailyTaskListDetailsViewModel> detailList = new List<DailyTaskListDetailsViewModel>();

                    //while (dr.Read())
                    //{
                    //    DailyTaskListDetailsViewModel dailyTaskListDetails = new DailyTaskListDetailsViewModel();


                    //    dailyTaskListDetails.Id = (long)dr["Id"];
                    //    dailyTaskListDetails.DailyTaskListId = (long)dr["DailyTaskListId"];
                    //    dailyTaskListDetails.Title = dr["Title"].ToString();
                    //    dailyTaskListDetails.Note = dr["Note"].ToString();
                    //    dailyTaskListDetails.TitleBangla = dr["TitleBangla"].ToString();
                    //    dailyTaskListDetails.NoteBangla = dr["NoteBangla"].ToString();
                    //    dailyTaskListDetails.Islink = (bool)dr["Islink"];

                    //    detailList.Add(dailyTaskListDetails);
                    //}
                    ////dr.NextResult();
                    //dr.Close();

                    //dailyTaskListVM.DailyTaskListDetailsViewModels = detailList;

                    #endregion

                    //dailyTaskList.Add(dailyTaskListVM);
                }
                return dailyTaskListVM;
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

        public DailyTask GetDailyTaskByDate(string userId)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@UserId", userId));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetDailyTaskByDate", aParameters);

                DailyTask dailyTask = new DailyTask();
                while (dr.Read())
                {

                    dailyTask.Id = (long)dr["Id"];
                    dailyTask.CreateDateTime = (DateTime?)dr["CreateDateTime"];
                    dailyTask.Score = (long)dr["Score"];
                    dailyTask.Action = (long)dr["Action"];
                    dailyTask.Type = dr["Type"].ToString();
                    //dailyTask.LevelUp = (int)dr["LevelUp"];
                    dailyTask.Status = (bool?)dr["Status"];
                    dailyTask.BusinessUnitId = (long)dr["BusinessUnitId"];
                }
                return dailyTask;
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

        public ResultResponse SaveDailyTask(DailyTask dailyTask)
        {
            try
            {

                accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                ResultResponse result = new ResultResponse();

                DataValidationDailyTask(dailyTask);
                var task = GetDailyTaskByDate(dailyTask.UserId);

                //if (task != null)
                if (task.Id > 0)
                {


                    result.msg = "You Have Completed Your Daily Task.";
                    return result;
                }
                else
                {
                    var dailyTaskId = GetDailyTaskListById(dailyTask.DailyTaskListId);

                    if (dailyTaskId.Water == "")
                    {
                        dailyTask.Water = 0;
                    }
                    else
                    {

                        dailyTask.Water = decimal.Parse(dailyTaskId.Water);
                    }
                    if (dailyTaskId.Energy == "")
                    {
                        dailyTask.Energy = 0;
                    }
                    else
                    {

                        dailyTask.Energy = decimal.Parse(dailyTaskId.Energy);
                    }
                    if (dailyTaskId.Co2 == "")
                    {
                        dailyTask.Co2 = 0;
                    }
                    else
                    {

                        dailyTask.Co2 = decimal.Parse(dailyTaskId.Co2);
                    }
                    if (dailyTaskId.Kindness == "")
                    {
                        dailyTask.Kindness = 0;
                    }
                    else
                    {

                        dailyTask.Kindness = decimal.Parse(dailyTaskId.Kindness);
                    }

                    accessManager.SqlConnectionOpen(DataBase.KinshipDB);

                    bool status = true;
                    List<SqlParameter> aParameters = new List<SqlParameter>();

                    aParameters.Add(new SqlParameter("@CreateDateTime", DateTime.Now));
                    aParameters.Add(new SqlParameter("@DailyTaskListId", dailyTask.DailyTaskListId));
                    aParameters.Add(new SqlParameter("@Score", dailyTask.Score));
                    aParameters.Add(new SqlParameter("@UserId", dailyTask.UserId));
                    aParameters.Add(new SqlParameter("@BusinessUnitId", dailyTask.BusinessUnitId));
                    aParameters.Add(new SqlParameter("@Status", status));
                    aParameters.Add(new SqlParameter("@Type", dailyTask.Type));
                    aParameters.Add(new SqlParameter("@Action", dailyTask.Action));
                    //aParameters.Add(new SqlParameter("@LevelUp", dailyTask.LevelUp));
                    aParameters.Add(new SqlParameter("@Co2", dailyTask.Co2));
                    aParameters.Add(new SqlParameter("@Water", dailyTask.Water));
                    aParameters.Add(new SqlParameter("@Energy", dailyTask.Energy));
                    aParameters.Add(new SqlParameter("@Kindness", dailyTask.Kindness));

                    result.isSuccess = accessManager.SaveData("sp_SaveDailyTask", aParameters);



                    result.msg = "Daily Task Added Successfully";
                    return result;
                }




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

        public ResultResponse SaveDailyTaskList(DailyTaskList dailyTaskList)
        {
            try
            {

                accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                ResultResponse result = new ResultResponse();

                //DataValidation(sustainabilitySurvey);

                bool status = true;
                List<SqlParameter> aParameters = new List<SqlParameter>();

                aParameters.Add(new SqlParameter("@CreateDateTime", DateTime.Now));
                aParameters.Add(new SqlParameter("@BanglaName", dailyTaskList.BanglaName));
                aParameters.Add(new SqlParameter("@EnglishName", dailyTaskList.EnglishName));
                aParameters.Add(new SqlParameter("@Type", dailyTaskList.Type));
                aParameters.Add(new SqlParameter("@ImageName", dailyTaskList.ImageName));
                aParameters.Add(new SqlParameter("@ImagePath", dailyTaskList.ImagePath));
                aParameters.Add(new SqlParameter("@ImageUrl", dailyTaskList.ImageUrl));
                aParameters.Add(new SqlParameter("@InfoLink", dailyTaskList.InfoLink));
                aParameters.Add(new SqlParameter("@SustainabilityCategoryId", dailyTaskList.SustainabilityCategoryId));

                aParameters.Add(new SqlParameter("@Co2", dailyTaskList.Co2));
                aParameters.Add(new SqlParameter("@Water", dailyTaskList.Water));
                aParameters.Add(new SqlParameter("@Energy", dailyTaskList.Energy));
                aParameters.Add(new SqlParameter("@Kindness", dailyTaskList.Kindness));

                result.isSuccess = accessManager.SaveData("sp_UploadDailyTaskList", aParameters);

                result.msg = "Daily Task Added Successfully";
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

        public ResultResponse SaveDailyTaskListDetails(DailyTaskListDetails dailyTaskListDetails)
        {
            try
            {

                accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                ResultResponse result = new ResultResponse();

                //DataValidation(sustainabilitySurvey);

                bool status = true;
                List<SqlParameter> aParameters = new List<SqlParameter>();

                aParameters.Add(new SqlParameter("@CreateDateTime", DateTime.Now));
                aParameters.Add(new SqlParameter("@DailyTaskListId", dailyTaskListDetails.DailyTaskListId));
                aParameters.Add(new SqlParameter("@Title", dailyTaskListDetails.Title));
                aParameters.Add(new SqlParameter("@Note", dailyTaskListDetails.Note));
                aParameters.Add(new SqlParameter("@TitleBangla", dailyTaskListDetails.TitleBangla));
                aParameters.Add(new SqlParameter("@NoteBangla", dailyTaskListDetails.NoteBangla));

                bool isLink = true;

                aParameters.Add(new SqlParameter("@Islink", isLink));

                result.isSuccess = accessManager.SaveData("sp_UploadDailyTaskListDetails", aParameters);

                result.msg = "Daily Task Added Successfully";
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


        // new method 

        public DailyTaskListViewModel GetDailyTaskListAndDetailsAllType(string type)
        {
            try
            {
                var list = GetDailyTaskListByType(type);
                var copyList = GetCopyDailyTaskList(type);
                accessManager.SqlConnectionOpen(DataBase.KinshipDB);

                DailyTaskListViewModel dailyTaskListVM = new DailyTaskListViewModel();
                foreach (var dailyTask in list)
                {
                    if (copyList.Id > 0)
                    {


                        var dateTime = copyList.CreateDateTime?.ToString("dd-MMM-yy hh:mm tt");
                        dailyTaskListVM.Id = copyList.DailyTaskListId;
                        dailyTaskListVM.CreateDate = dateTime.Split(' ')[0];
                        dailyTaskListVM.CreateTime = dateTime.Split(' ')[1] + dateTime.Split(' ')[2];
                        dailyTaskListVM.BanglaName = copyList.BanglaName.ToString();
                        dailyTaskListVM.EnglishName = copyList.EnglishName.ToString();
                        dailyTaskListVM.Type = copyList.Type.ToString();
                        dailyTaskListVM.ImageName = copyList.ImageName.ToString();
                        dailyTaskListVM.ImageUrl = copyList.ImageUrl.ToString();
                        dailyTaskListVM.ImagePath = copyList.ImagePath.ToString();
                        dailyTaskListVM.InfoLink = copyList.InfoLink.ToString();
                        dailyTaskListVM.SustainabilityCategoryId = copyList.SustainabilityCategoryId;
                        //dailyTaskListVM.Action = dailyTask.Action;
                        dailyTaskListVM.Co2 = copyList.Co2;
                        dailyTaskListVM.Water = copyList.Water;
                        dailyTaskListVM.Energy = copyList.Energy;
                        dailyTaskListVM.Kindness = copyList.Kindness;

                        List<SqlParameter> aParameters = new List<SqlParameter>();
                        aParameters.Add(new SqlParameter("@dailyTaskListId", copyList.DailyTaskListId));
                        SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetDailyTaskListDetails", aParameters);

                        List<DailyTaskListDetailsViewModel> detailList = new List<DailyTaskListDetailsViewModel>();

                        while (dr.Read())
                        {
                            DailyTaskListDetailsViewModel dailyTaskListDetails = new DailyTaskListDetailsViewModel();

                            dailyTaskListDetails.Id = (long)dr["Id"];
                            dailyTaskListDetails.DailyTaskListId = (long)dr["DailyTaskListId"];
                            dailyTaskListDetails.Title = dr["Title"].ToString();
                            dailyTaskListDetails.Note = dr["Note"].ToString();
                            dailyTaskListDetails.TitleBangla = dr["TitleBangla"].ToString();
                            dailyTaskListDetails.NoteBangla = dr["NoteBangla"].ToString();
                            dailyTaskListDetails.Islink = (bool)dr["Islink"];

                            detailList.Add(dailyTaskListDetails);
                        }
                        //dr.NextResult();
                        //dr.Close();

                        dailyTaskListVM.DailyTaskListDetailsViewModels = detailList;

                    }
                    else
                    {
                        // Daily New task ramdomly Save
                        SaveDailyTaskRamdomly(dailyTask);

                        var copyListNew = GetCopyDailyTaskList(type);

                        accessManager.SqlConnectionOpen(DataBase.KinshipDB);


                        var dateTime = copyListNew.CreateDateTime?.ToString("dd-MMM-yy hh:mm tt");
                        dailyTaskListVM.Id = copyListNew.DailyTaskListId;
                        dailyTaskListVM.CreateDate = dateTime.Split(' ')[0];
                        dailyTaskListVM.CreateTime = dateTime.Split(' ')[1] + dateTime.Split(' ')[2];
                        dailyTaskListVM.BanglaName = copyListNew.BanglaName.ToString();
                        dailyTaskListVM.EnglishName = copyListNew.EnglishName.ToString();
                        dailyTaskListVM.Type = copyListNew.Type.ToString();
                        dailyTaskListVM.ImageName = copyListNew.ImageName.ToString();
                        dailyTaskListVM.ImageUrl = copyListNew.ImageUrl.ToString();
                        dailyTaskListVM.ImagePath = copyListNew.ImagePath.ToString();
                        dailyTaskListVM.InfoLink = copyListNew.InfoLink.ToString();
                        dailyTaskListVM.SustainabilityCategoryId = copyListNew.SustainabilityCategoryId;
                        //dailyTaskListVM.Action = dailyTask.Action;
                        dailyTaskListVM.Co2 = copyListNew.Co2;
                        dailyTaskListVM.Water = copyListNew.Water;
                        dailyTaskListVM.Energy = copyListNew.Energy;
                        dailyTaskListVM.Kindness = copyListNew.Kindness;

                        List<SqlParameter> aParameters = new List<SqlParameter>();
                        aParameters.Add(new SqlParameter("@dailyTaskListId", copyListNew.DailyTaskListId));
                        SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetDailyTaskListDetails", aParameters);

                        List<DailyTaskListDetailsViewModel> detailList = new List<DailyTaskListDetailsViewModel>();
                        while (dr.Read())
                        {
                            DailyTaskListDetailsViewModel dailyTaskListDetails = new DailyTaskListDetailsViewModel();

                            dailyTaskListDetails.Id = (long)dr["Id"];
                            dailyTaskListDetails.DailyTaskListId = (long)dr["DailyTaskListId"];
                            dailyTaskListDetails.Title = dr["Title"].ToString();
                            dailyTaskListDetails.Note = dr["Note"].ToString();
                            dailyTaskListDetails.TitleBangla = dr["TitleBangla"].ToString();
                            dailyTaskListDetails.NoteBangla = dr["NoteBangla"].ToString();
                            dailyTaskListDetails.Islink = (bool)dr["Islink"];

                            detailList.Add(dailyTaskListDetails);
                        }
                        //dr.NextResult();
                        // dr.Close();

                        dailyTaskListVM.DailyTaskListDetailsViewModels = detailList;


                    }

                }
                return dailyTaskListVM;

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

        private void SaveDailyTaskRamdomly(DailyTaskList dailyTask)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                ResultResponse result = new ResultResponse();

                List<SqlParameter> aParameters = new List<SqlParameter>();

                var date = DateTime.Now.ToString("dd-MMM-yy hh:mm tt");

                aParameters.Add(new SqlParameter("@DailyTaskListId", dailyTask.Id));
                aParameters.Add(new SqlParameter("@CreateDateTime", date.Split(' ')[0]));
                aParameters.Add(new SqlParameter("@BanglaName", dailyTask.BanglaName));
                aParameters.Add(new SqlParameter("@EnglishName", dailyTask.EnglishName));
                aParameters.Add(new SqlParameter("@Type", dailyTask.Type));
                aParameters.Add(new SqlParameter("@ImageName", dailyTask.ImageName));
                aParameters.Add(new SqlParameter("@ImagePath", dailyTask.ImagePath));
                aParameters.Add(new SqlParameter("@ImageUrl", dailyTask.ImageUrl));
                aParameters.Add(new SqlParameter("@InfoLink", dailyTask.InfoLink));
                aParameters.Add(new SqlParameter("@SustainabilityCategoryId", dailyTask.SustainabilityCategoryId));
                aParameters.Add(new SqlParameter("@Co2", dailyTask.Co2));
                aParameters.Add(new SqlParameter("@Water", dailyTask.Water));
                aParameters.Add(new SqlParameter("@Energy", dailyTask.Energy));
                aParameters.Add(new SqlParameter("@Kindness", dailyTask.Kindness));

                accessManager.SaveData("sp_SaveCopyDailyTaskList", aParameters);


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

        public List<DailyTaskList> GetDailyTaskListByType(string type)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.KinshipDB);

                List<DailyTaskList> dailyTaskList = new List<DailyTaskList>();

                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@Type", type));

                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetDailyTaskListByType", aParameters);

                while (dr.Read())
                {

                    DailyTaskList dailyTaskListVM = new DailyTaskList();

                    dailyTaskListVM.Id = (long)dr["Id"];
                    dailyTaskListVM.CreateDateTime = (DateTime?)dr["CreateDateTime"];
                    dailyTaskListVM.BanglaName = dr["BanglaName"].ToString();
                    dailyTaskListVM.EnglishName = dr["EnglishName"].ToString();
                    dailyTaskListVM.Type = dr["Type"].ToString();
                    dailyTaskListVM.ImageName = dr["ImageName"].ToString();
                    dailyTaskListVM.ImageUrl = dr["ImageUrl"].ToString();
                    dailyTaskListVM.ImagePath = dr["ImagePath"].ToString();
                    dailyTaskListVM.InfoLink = dr["InfoLink"].ToString();

                    //dailyTaskListVM.Action = (long)dr["Action"];
                    dailyTaskListVM.Co2 = dr["Co2"].ToString();
                    dailyTaskListVM.Water = dr["Water"].ToString();
                    dailyTaskListVM.Energy = dr["Energy"].ToString();
                    dailyTaskListVM.Kindness = dr["Kindness"].ToString();

                    dailyTaskListVM.SustainabilityCategoryId = (long)dr["SustainabilityCategoryId"];


                    dailyTaskList.Add(dailyTaskListVM);
                }
                return dailyTaskList;
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

        #endregion

        #region Monthly Task

        // randomly 10 lis
        public List<MonthlyTaskList> GetMonthlyTaskListRandomly()
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.KinshipDB);

                List<MonthlyTaskList> dailyTaskList = new List<MonthlyTaskList>();

                List<SqlParameter> aParameters = new List<SqlParameter>();

                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetMonthlyTaskListRandomly", aParameters);

                while (dr.Read())
                {

                    MonthlyTaskList dailyTaskListVM = new MonthlyTaskList();
                    //var dateTime = Convert.ToDateTime(dr["CreateDateTime"]).ToString("dd-MMM-yy hh:mm tt");

                    dailyTaskListVM.Id = (long)dr["Id"];
                    dailyTaskListVM.CreateDateTime = (DateTime?)dr["CreateDateTime"];
                    dailyTaskListVM.BanglaName = dr["BanglaName"].ToString();
                    dailyTaskListVM.EnglishName = dr["EnglishName"].ToString();
                    dailyTaskListVM.Type = dr["Type"].ToString();
                    dailyTaskListVM.ImageName = dr["ImageName"].ToString();
                    dailyTaskListVM.ImageUrl = dr["ImageUrl"].ToString();
                    dailyTaskListVM.ImagePath = dr["ImagePath"].ToString();
                    dailyTaskListVM.InfoLink = dr["InfoLink"].ToString();

                    //dailyTaskListVM.Action = (long)dr["Action"];
                    dailyTaskListVM.Co2 = dr["Co2"].ToString();
                    dailyTaskListVM.Water = dr["Water"].ToString();
                    dailyTaskListVM.Energy = dr["Energy"].ToString();
                    dailyTaskListVM.Kindness = dr["Kindness"].ToString();

                    dailyTaskListVM.SustainabilityCategoryId = (long)dr["SustainabilityCategoryId"];


                    dailyTaskList.Add(dailyTaskListVM);
                }
                return dailyTaskList;
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

        public List<CopyMonthlyTaskList> GetCopyMonthlyTaskListRandomly()
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.KinshipDB);

                List<CopyMonthlyTaskList> copyMonthlyTaskLists = new List<CopyMonthlyTaskList>();

                List<SqlParameter> aParameters = new List<SqlParameter>();
                //aParameters.Add(new SqlParameter("@dailyTaskId", id));

                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetCopyMonthlyTaskList", aParameters);


                while (dr.Read())
                {
                    CopyMonthlyTaskList copyMonthlyTaskList = new CopyMonthlyTaskList();

                    //var dateTime = Convert.ToDateTime(dr["CreateDateTime"]).ToString("dd-MMM-yy hh:mm tt");


                    //var dateTime = Convert.ToDateTime(dr["CreateDateTime"]).ToString("dd-MMM-yy hh:mm tt");

                    copyMonthlyTaskList.Id = (long)dr["Id"];
                    copyMonthlyTaskList.MonthlyTaskListId = (long)dr["MonthlyTaskListId"];
                    copyMonthlyTaskList.CreateDateTime = (DateTime?)dr["CreateDateTime"];
                    copyMonthlyTaskList.BanglaName = dr["BanglaName"].ToString();
                    copyMonthlyTaskList.EnglishName = dr["EnglishName"].ToString();
                    copyMonthlyTaskList.Type = dr["Type"].ToString();
                    copyMonthlyTaskList.ImageName = dr["ImageName"].ToString();
                    copyMonthlyTaskList.ImageUrl = dr["ImageUrl"].ToString();
                    copyMonthlyTaskList.ImagePath = dr["ImagePath"].ToString();
                    copyMonthlyTaskList.InfoLink = dr["InfoLink"].ToString();

                    //dailyTaskListVM.Action = (long)dr["Action"];
                    copyMonthlyTaskList.Co2 = dr["Co2"].ToString();
                    copyMonthlyTaskList.Water = dr["Water"].ToString();
                    copyMonthlyTaskList.Energy = dr["Energy"].ToString();
                    copyMonthlyTaskList.Kindness = dr["Kindness"].ToString();

                    copyMonthlyTaskList.SustainabilityCategoryId = (long)dr["SustainabilityCategoryId"];


                    copyMonthlyTaskLists.Add(copyMonthlyTaskList);
                }
                return copyMonthlyTaskLists;
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

        public List<MonthlyTaskListViewModel> GetMonthlyTaskListAndDetailsRandomly(string userId)
        {
            try
            {
                var list = GetMonthlyTaskListRandomly();

                var copyList = GetCopyMonthlyTaskListRandomly();

                accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                List<MonthlyTaskListViewModel> monthlyTaskList = new List<MonthlyTaskListViewModel>();
                List<MonthlyTaskListViewModel> monthlyTaskListVMs = new List<MonthlyTaskListViewModel>();



                //if (copyList.Id > 0)
                if (copyList.Count > 0)
                {
                    foreach (var item in copyList)
                    {
                        var DisableStatus = GetTaskdisableorNot(item.MonthlyTaskListId, userId);

                        accessManager.SqlConnectionOpen(DataBase.KinshipDB);

                        MonthlyTaskListViewModel monthlyTaskListVM = new MonthlyTaskListViewModel();

                        var dateTime = item.CreateDateTime?.ToString("dd-MMM-yy hh:mm tt");

                        monthlyTaskListVM.Id = item.MonthlyTaskListId;
                        monthlyTaskListVM.CreateDate = dateTime.Split(' ')[0];
                        monthlyTaskListVM.CreateTime = dateTime.Split(' ')[1] + dateTime.Split(' ')[2];
                        monthlyTaskListVM.BanglaName = item.BanglaName.ToString();
                        monthlyTaskListVM.EnglishName = item.EnglishName.ToString();
                        monthlyTaskListVM.Type = item.Type.ToString();
                        monthlyTaskListVM.ImageName = item.ImageName.ToString();
                        monthlyTaskListVM.ImageUrl = item.ImageUrl.ToString();
                        monthlyTaskListVM.ImagePath = item.ImagePath.ToString();
                        monthlyTaskListVM.InfoLink = item.InfoLink.ToString();
                        monthlyTaskListVM.SustainabilityCategoryId = item.SustainabilityCategoryId;
                        //monthlyTaskListVM.Action = monthlyTask.Action;
                        monthlyTaskListVM.Co2 = item.Co2;
                        monthlyTaskListVM.Water = item.Water;
                        monthlyTaskListVM.Energy = item.Energy;
                        monthlyTaskListVM.Kindness = item.Kindness;

                        monthlyTaskListVM.DisableStatus = DisableStatus;

                        List<SqlParameter> aParameters = new List<SqlParameter>();
                        aParameters.Add(new SqlParameter("@monthlyTaskListId", item.MonthlyTaskListId));
                        SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetMonthlyTaskListDetails", aParameters);

                        List<MonthlyTaskListDetailsViewModel> detailList = new List<MonthlyTaskListDetailsViewModel>();

                        while (dr.Read())
                        {
                            MonthlyTaskListDetailsViewModel monthlyTaskListDetails = new MonthlyTaskListDetailsViewModel();


                            monthlyTaskListDetails.Id = (long)dr["Id"];
                            monthlyTaskListDetails.MonthlyTaskListId = (long)dr["MonthlyTaskListId"];
                            monthlyTaskListDetails.Title = dr["Title"].ToString();
                            monthlyTaskListDetails.Note = dr["Note"].ToString();
                            monthlyTaskListDetails.TitleBangla = dr["TitleBangla"].ToString();
                            monthlyTaskListDetails.NoteBangla = dr["NoteBangla"].ToString();
                            monthlyTaskListDetails.Islink = (bool)dr["Islink"];

                            detailList.Add(monthlyTaskListDetails);
                        }
                        //dr.NextResult();
                        dr.Close();

                        monthlyTaskListVM.MonthlyTaskListDetailsViewModels = detailList;

                        monthlyTaskListVMs.Add(monthlyTaskListVM);
                    }

                }
                else
                {
                    foreach (var monthlyTask in list)
                    {
                        ResultResponse result = new ResultResponse();
                        accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                        List<SqlParameter> aParameters2 = new List<SqlParameter>();

                        var date = DateTime.Now.ToString("dd-MMM-yy hh:mm tt");

                        aParameters2.Add(new SqlParameter("@MonthlyTaskListId", monthlyTask.Id));
                        aParameters2.Add(new SqlParameter("@CreateDateTime", date.Split(' ')[0]));
                        aParameters2.Add(new SqlParameter("@BanglaName", monthlyTask.BanglaName));
                        aParameters2.Add(new SqlParameter("@EnglishName", monthlyTask.EnglishName));
                        aParameters2.Add(new SqlParameter("@Type", monthlyTask.Type));
                        aParameters2.Add(new SqlParameter("@ImageName", monthlyTask.ImageName));
                        aParameters2.Add(new SqlParameter("@ImagePath", monthlyTask.ImagePath));
                        aParameters2.Add(new SqlParameter("@ImageUrl", monthlyTask.ImageUrl));
                        aParameters2.Add(new SqlParameter("@InfoLink", monthlyTask.InfoLink));
                        aParameters2.Add(new SqlParameter("@SustainabilityCategoryId", monthlyTask.SustainabilityCategoryId));

                        aParameters2.Add(new SqlParameter("@Co2", monthlyTask.Co2));
                        aParameters2.Add(new SqlParameter("@Water", monthlyTask.Water));
                        aParameters2.Add(new SqlParameter("@Energy", monthlyTask.Energy));
                        aParameters2.Add(new SqlParameter("@Kindness", monthlyTask.Kindness));

                        accessManager.SaveData("sp_SaveCopyMonthlyTaskList", aParameters2);

                        accessManager.SqlConnectionClose();
                    }

                    var copyListNew = GetCopyMonthlyTaskListRandomly();
                    accessManager.SqlConnectionOpen(DataBase.KinshipDB);

                    foreach (var item in copyListNew)
                    {
                        var DisableStatus = GetTaskdisableorNot(item.MonthlyTaskListId, userId);

                        accessManager.SqlConnectionOpen(DataBase.KinshipDB);

                        MonthlyTaskListViewModel monthlyTaskListVM = new MonthlyTaskListViewModel();

                        var dateTime = item.CreateDateTime?.ToString("dd-MMM-yy hh:mm tt");

                        monthlyTaskListVM.Id = item.MonthlyTaskListId;
                        monthlyTaskListVM.CreateDate = dateTime.Split(' ')[0];
                        monthlyTaskListVM.CreateTime = dateTime.Split(' ')[1] + dateTime.Split(' ')[2];
                        monthlyTaskListVM.BanglaName = item.BanglaName.ToString();
                        monthlyTaskListVM.EnglishName = item.EnglishName.ToString();
                        monthlyTaskListVM.Type = item.Type.ToString();
                        monthlyTaskListVM.ImageName = item.ImageName.ToString();
                        monthlyTaskListVM.ImageUrl = item.ImageUrl.ToString();
                        monthlyTaskListVM.ImagePath = item.ImagePath.ToString();
                        monthlyTaskListVM.InfoLink = item.InfoLink.ToString();
                        monthlyTaskListVM.SustainabilityCategoryId = item.SustainabilityCategoryId;
                        //dailyTaskListVM.Action = dailyTask.Action;
                        monthlyTaskListVM.Co2 = item.Co2;
                        monthlyTaskListVM.Water = item.Water;
                        monthlyTaskListVM.Energy = item.Energy;
                        monthlyTaskListVM.Kindness = item.Kindness;

                        monthlyTaskListVM.DisableStatus = DisableStatus;

                        List<SqlParameter> aParameters = new List<SqlParameter>();
                        aParameters.Add(new SqlParameter("@monthlyTaskListId", item.MonthlyTaskListId));
                        SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetMonthlyTaskListDetails", aParameters);

                        List<MonthlyTaskListDetailsViewModel> detailList = new List<MonthlyTaskListDetailsViewModel>();

                        while (dr.Read())
                        {
                            MonthlyTaskListDetailsViewModel dailyTaskListDetails = new MonthlyTaskListDetailsViewModel();


                            dailyTaskListDetails.Id = (long)dr["Id"];
                            dailyTaskListDetails.MonthlyTaskListId = (long)dr["MonthlyTaskListId"];
                            dailyTaskListDetails.Title = dr["Title"].ToString();
                            dailyTaskListDetails.Note = dr["Note"].ToString();
                            dailyTaskListDetails.TitleBangla = dr["TitleBangla"].ToString();
                            dailyTaskListDetails.NoteBangla = dr["NoteBangla"].ToString();
                            dailyTaskListDetails.Islink = (bool)dr["Islink"];

                            detailList.Add(dailyTaskListDetails);
                        }
                        //dr.NextResult();
                        dr.Close();

                        monthlyTaskListVM.MonthlyTaskListDetailsViewModels = detailList;

                        monthlyTaskListVMs.Add(monthlyTaskListVM);
                    }


                }

                return monthlyTaskListVMs;
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


        //new solve methode

        public List<MonthlyTaskListViewModel> GetMonthlyTaskListAndDetailsRandomlyNew(string userId)
        {
            try
            {
                var list = GetMonthlyTaskListRandomly();
                var copyList = GetCopyMonthlyTaskListRandomly();

                List<MonthlyTaskListViewModel> monthlyTaskList = new List<MonthlyTaskListViewModel>();
                List<MonthlyTaskListViewModel> monthlyTaskListVMs = new List<MonthlyTaskListViewModel>();

                if (copyList.Count > 0)
                {
                    foreach (var item in copyList)
                    {
                        var DisableStatus = GetTaskdisableorNot(item.MonthlyTaskListId, userId);

                        accessManager.SqlConnectionOpen(DataBase.KinshipDB);

                        MonthlyTaskListViewModel monthlyTaskListVM = new MonthlyTaskListViewModel();

                        var dateTime = item.CreateDateTime?.ToString("dd-MMM-yy hh:mm tt");

                        monthlyTaskListVM.Id = item.MonthlyTaskListId;
                        monthlyTaskListVM.CreateDate = dateTime.Split(' ')[0];
                        monthlyTaskListVM.CreateTime = dateTime.Split(' ')[1] + dateTime.Split(' ')[2];
                        monthlyTaskListVM.BanglaName = item.BanglaName.ToString();
                        monthlyTaskListVM.EnglishName = item.EnglishName.ToString();
                        monthlyTaskListVM.Type = item.Type.ToString();
                        monthlyTaskListVM.ImageName = item.ImageName.ToString();
                        monthlyTaskListVM.ImageUrl = item.ImageUrl.ToString();
                        monthlyTaskListVM.ImagePath = item.ImagePath.ToString();
                        monthlyTaskListVM.InfoLink = item.InfoLink.ToString();
                        monthlyTaskListVM.SustainabilityCategoryId = item.SustainabilityCategoryId;
                        //monthlyTaskListVM.Action = monthlyTask.Action;
                        monthlyTaskListVM.Co2 = item.Co2;
                        monthlyTaskListVM.Water = item.Water;
                        monthlyTaskListVM.Energy = item.Energy;
                        monthlyTaskListVM.Kindness = item.Kindness;

                        monthlyTaskListVM.DisableStatus = DisableStatus;

                        List<SqlParameter> aParameters = new List<SqlParameter>();
                        aParameters.Add(new SqlParameter("@monthlyTaskListId", item.MonthlyTaskListId));
                        SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetMonthlyTaskListDetails", aParameters);

                        List<MonthlyTaskListDetailsViewModel> detailList = new List<MonthlyTaskListDetailsViewModel>();

                        while (dr.Read())
                        {
                            MonthlyTaskListDetailsViewModel monthlyTaskListDetails = new MonthlyTaskListDetailsViewModel();


                            monthlyTaskListDetails.Id = (long)dr["Id"];
                            monthlyTaskListDetails.MonthlyTaskListId = (long)dr["MonthlyTaskListId"];
                            monthlyTaskListDetails.Title = dr["Title"].ToString();
                            monthlyTaskListDetails.Note = dr["Note"].ToString();
                            monthlyTaskListDetails.TitleBangla = dr["TitleBangla"].ToString();
                            monthlyTaskListDetails.NoteBangla = dr["NoteBangla"].ToString();
                            monthlyTaskListDetails.Islink = (bool)dr["Islink"];

                            detailList.Add(monthlyTaskListDetails);
                        }
                        //dr.NextResult();
                        //dr.Close();

                        monthlyTaskListVM.MonthlyTaskListDetailsViewModels = detailList;

                        monthlyTaskListVMs.Add(monthlyTaskListVM);

                        // 
                        accessManager.SqlConnectionClose();
                    }

                }
                else
                {

                    //save method for monthly 10 data save
                    foreach (var monthlyTask in list)
                    {
                        SaveMonthlyTaskListNew(monthlyTask);
                    }


                    var copyListNew = GetCopyMonthlyTaskListRandomly();
                    //accessManager.SqlConnectionOpen(DataBase.KinshipDB);

                    foreach (var item in copyListNew)
                    {
                        var DisableStatus = GetTaskdisableorNot(item.MonthlyTaskListId, userId);

                        accessManager.SqlConnectionOpen(DataBase.KinshipDB);

                        MonthlyTaskListViewModel monthlyTaskListVM = new MonthlyTaskListViewModel();

                        var dateTime = item.CreateDateTime?.ToString("dd-MMM-yy hh:mm tt");

                        monthlyTaskListVM.Id = item.MonthlyTaskListId;
                        monthlyTaskListVM.CreateDate = dateTime.Split(' ')[0];
                        monthlyTaskListVM.CreateTime = dateTime.Split(' ')[1] + dateTime.Split(' ')[2];
                        monthlyTaskListVM.BanglaName = item.BanglaName.ToString();
                        monthlyTaskListVM.EnglishName = item.EnglishName.ToString();
                        monthlyTaskListVM.Type = item.Type.ToString();
                        monthlyTaskListVM.ImageName = item.ImageName.ToString();
                        monthlyTaskListVM.ImageUrl = item.ImageUrl.ToString();
                        monthlyTaskListVM.ImagePath = item.ImagePath.ToString();
                        monthlyTaskListVM.InfoLink = item.InfoLink.ToString();
                        monthlyTaskListVM.SustainabilityCategoryId = item.SustainabilityCategoryId;
                        //dailyTaskListVM.Action = dailyTask.Action;
                        monthlyTaskListVM.Co2 = item.Co2;
                        monthlyTaskListVM.Water = item.Water;
                        monthlyTaskListVM.Energy = item.Energy;
                        monthlyTaskListVM.Kindness = item.Kindness;

                        monthlyTaskListVM.DisableStatus = DisableStatus;

                        List<SqlParameter> aParameters = new List<SqlParameter>();
                        aParameters.Add(new SqlParameter("@monthlyTaskListId", item.MonthlyTaskListId));
                        SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetMonthlyTaskListDetails", aParameters);

                        List<MonthlyTaskListDetailsViewModel> detailList = new List<MonthlyTaskListDetailsViewModel>();

                        while (dr.Read())
                        {
                            MonthlyTaskListDetailsViewModel dailyTaskListDetails = new MonthlyTaskListDetailsViewModel();


                            dailyTaskListDetails.Id = (long)dr["Id"];
                            dailyTaskListDetails.MonthlyTaskListId = (long)dr["MonthlyTaskListId"];
                            dailyTaskListDetails.Title = dr["Title"].ToString();
                            dailyTaskListDetails.Note = dr["Note"].ToString();
                            dailyTaskListDetails.TitleBangla = dr["TitleBangla"].ToString();
                            dailyTaskListDetails.NoteBangla = dr["NoteBangla"].ToString();
                            dailyTaskListDetails.Islink = (bool)dr["Islink"];

                            detailList.Add(dailyTaskListDetails);
                        }
                        //dr.NextResult();
                        //dr.Close();

                        monthlyTaskListVM.MonthlyTaskListDetailsViewModels = detailList;

                        monthlyTaskListVMs.Add(monthlyTaskListVM);

                        accessManager.SqlConnectionClose();
                    }


                }

                return monthlyTaskListVMs;

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

        private void SaveMonthlyTaskListNew(MonthlyTaskList monthlyTask)
        {
            try
            {
                ResultResponse result = new ResultResponse();
                accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                List<SqlParameter> aParameters = new List<SqlParameter>();

                var date = DateTime.Now.ToString("dd-MMM-yy hh:mm tt");

                aParameters.Add(new SqlParameter("@MonthlyTaskListId", monthlyTask.Id));
                aParameters.Add(new SqlParameter("@CreateDateTime", date.Split(' ')[0]));
                aParameters.Add(new SqlParameter("@BanglaName", monthlyTask.BanglaName));
                aParameters.Add(new SqlParameter("@EnglishName", monthlyTask.EnglishName));
                aParameters.Add(new SqlParameter("@Type", monthlyTask.Type));
                aParameters.Add(new SqlParameter("@ImageName", monthlyTask.ImageName));
                aParameters.Add(new SqlParameter("@ImagePath", monthlyTask.ImagePath));
                aParameters.Add(new SqlParameter("@ImageUrl", monthlyTask.ImageUrl));
                aParameters.Add(new SqlParameter("@InfoLink", monthlyTask.InfoLink));
                aParameters.Add(new SqlParameter("@SustainabilityCategoryId", monthlyTask.SustainabilityCategoryId));

                aParameters.Add(new SqlParameter("@Co2", monthlyTask.Co2));
                aParameters.Add(new SqlParameter("@Water", monthlyTask.Water));
                aParameters.Add(new SqlParameter("@Energy", monthlyTask.Energy));
                aParameters.Add(new SqlParameter("@Kindness", monthlyTask.Kindness));

                accessManager.SaveData("sp_SaveCopyMonthlyTaskList", aParameters);
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

       

        // end

        public MonthlyTaskList GetMonthlyTaskListById(long taskId)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.KinshipDB);

                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@TaskId", taskId));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetMonthlyTaskListById", aParameters);

                MonthlyTaskList dailyTaskListVM = new MonthlyTaskList();
                while (dr.Read())
                {
                    dailyTaskListVM.Id = (long)dr["Id"];
                    dailyTaskListVM.CreateDateTime = (DateTime?)dr["CreateDateTime"];
                    dailyTaskListVM.BanglaName = dr["BanglaName"].ToString();
                    dailyTaskListVM.EnglishName = dr["EnglishName"].ToString();
                    dailyTaskListVM.Type = dr["Type"].ToString();
                    dailyTaskListVM.ImageName = dr["ImageName"].ToString();
                    dailyTaskListVM.ImageUrl = dr["ImageUrl"].ToString();
                    dailyTaskListVM.ImagePath = dr["ImagePath"].ToString();
                    dailyTaskListVM.InfoLink = dr["InfoLink"].ToString();
                    dailyTaskListVM.Co2 = dr["Co2"].ToString();
                    dailyTaskListVM.Water = dr["Water"].ToString();
                    dailyTaskListVM.Energy = dr["Energy"].ToString();
                    dailyTaskListVM.Kindness = dr["Kindness"].ToString();

                    dailyTaskListVM.SustainabilityCategoryId = (long)dr["SustainabilityCategoryId"];
                }
                return dailyTaskListVM;
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
        public CopyMonthlyTaskList GetCopyMonthlyTaskList()
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.KinshipDB);

                List<CopyMonthlyTaskList> dailyTaskList = new List<CopyMonthlyTaskList>();

                List<SqlParameter> aParameters = new List<SqlParameter>();
                //aParameters.Add(new SqlParameter("@dailyTaskId", id));

                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetCopyMonthlyTaskList", aParameters);

                CopyMonthlyTaskList copyMonthlyTaskList = new CopyMonthlyTaskList();
                while (dr.Read())
                {


                    //var dateTime = Convert.ToDateTime(dr["CreateDateTime"]).ToString("dd-MMM-yy hh:mm tt");


                    //var dateTime = Convert.ToDateTime(dr["CreateDateTime"]).ToString("dd-MMM-yy hh:mm tt");

                    copyMonthlyTaskList.Id = (long)dr["Id"];
                    copyMonthlyTaskList.MonthlyTaskListId = (long)dr["MonthlyTaskListId"];
                    copyMonthlyTaskList.CreateDateTime = (DateTime?)dr["CreateDateTime"];
                    copyMonthlyTaskList.BanglaName = dr["BanglaName"].ToString();
                    copyMonthlyTaskList.EnglishName = dr["EnglishName"].ToString();
                    copyMonthlyTaskList.Type = dr["Type"].ToString();
                    copyMonthlyTaskList.ImageName = dr["ImageName"].ToString();
                    copyMonthlyTaskList.ImageUrl = dr["ImageUrl"].ToString();
                    copyMonthlyTaskList.ImagePath = dr["ImagePath"].ToString();
                    copyMonthlyTaskList.InfoLink = dr["InfoLink"].ToString();

                    //dailyTaskListVM.Action = (long)dr["Action"];
                    copyMonthlyTaskList.Co2 = dr["Co2"].ToString();
                    copyMonthlyTaskList.Water = dr["Water"].ToString();
                    copyMonthlyTaskList.Energy = dr["Energy"].ToString();
                    copyMonthlyTaskList.Kindness = dr["Kindness"].ToString();

                    copyMonthlyTaskList.SustainabilityCategoryId = (long)dr["SustainabilityCategoryId"];


                    //dailyTaskList.Add(dailyTaskListVM);
                }
                return copyMonthlyTaskList;
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
        public List<MonthlyTaskList> GetMonthlyTaskList()
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.KinshipDB);

                List<MonthlyTaskList> dailyTaskList = new List<MonthlyTaskList>();

                List<SqlParameter> aParameters = new List<SqlParameter>();

                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetMonthlyTaskList", aParameters);

                while (dr.Read())
                {

                    MonthlyTaskList dailyTaskListVM = new MonthlyTaskList();
                    //var dateTime = Convert.ToDateTime(dr["CreateDateTime"]).ToString("dd-MMM-yy hh:mm tt");

                    dailyTaskListVM.Id = (long)dr["Id"];
                    dailyTaskListVM.CreateDateTime = (DateTime?)dr["CreateDateTime"];
                    dailyTaskListVM.BanglaName = dr["BanglaName"].ToString();
                    dailyTaskListVM.EnglishName = dr["EnglishName"].ToString();
                    dailyTaskListVM.Type = dr["Type"].ToString();
                    dailyTaskListVM.ImageName = dr["ImageName"].ToString();
                    dailyTaskListVM.ImageUrl = dr["ImageUrl"].ToString();
                    dailyTaskListVM.ImagePath = dr["ImagePath"].ToString();
                    dailyTaskListVM.InfoLink = dr["InfoLink"].ToString();

                    //dailyTaskListVM.Action = (long)dr["Action"];
                    dailyTaskListVM.Co2 = dr["Co2"].ToString();
                    dailyTaskListVM.Water = dr["Water"].ToString();
                    dailyTaskListVM.Energy = dr["Energy"].ToString();
                    dailyTaskListVM.Kindness = dr["Kindness"].ToString();

                    dailyTaskListVM.SustainabilityCategoryId = (long)dr["SustainabilityCategoryId"];


                    dailyTaskList.Add(dailyTaskListVM);
                }
                return dailyTaskList;
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

        public MonthlyTaskListViewModel GetMonthlyTaskListAndDetails()
        {
            try
            {
                var list = GetMonthlyTaskList();

                var copyList = GetCopyMonthlyTaskList();

                accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                List<MonthlyTaskListViewModel> dailyTaskList = new List<MonthlyTaskListViewModel>();
                MonthlyTaskListViewModel monthlyTaskListVM = new MonthlyTaskListViewModel();

                foreach (var monthlyTask in list)
                {



                    if (copyList.Id > 0)
                    {
                        var dateTime = monthlyTask.CreateDateTime?.ToString("dd-MMM-yy hh:mm tt");

                        monthlyTaskListVM.Id = copyList.MonthlyTaskListId;
                        monthlyTaskListVM.CreateDate = dateTime.Split(' ')[0];
                        monthlyTaskListVM.CreateTime = dateTime.Split(' ')[1] + dateTime.Split(' ')[2];
                        monthlyTaskListVM.BanglaName = copyList.BanglaName.ToString();
                        monthlyTaskListVM.EnglishName = copyList.EnglishName.ToString();
                        monthlyTaskListVM.Type = copyList.Type.ToString();
                        monthlyTaskListVM.ImageName = copyList.ImageName.ToString();
                        monthlyTaskListVM.ImageUrl = copyList.ImageUrl.ToString();
                        monthlyTaskListVM.ImagePath = copyList.ImagePath.ToString();
                        monthlyTaskListVM.InfoLink = copyList.InfoLink.ToString();
                        monthlyTaskListVM.SustainabilityCategoryId = copyList.SustainabilityCategoryId;
                        //monthlyTaskListVM.Action = monthlyTask.Action;
                        monthlyTaskListVM.Co2 = copyList.Co2;
                        monthlyTaskListVM.Water = copyList.Water;
                        monthlyTaskListVM.Energy = copyList.Energy;
                        monthlyTaskListVM.Kindness = copyList.Kindness;

                        List<SqlParameter> aParameters = new List<SqlParameter>();
                        aParameters.Add(new SqlParameter("@monthlyTaskListId", copyList.MonthlyTaskListId));
                        SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetMonthlyTaskListDetails", aParameters);

                        List<MonthlyTaskListDetailsViewModel> detailList = new List<MonthlyTaskListDetailsViewModel>();

                        while (dr.Read())
                        {
                            MonthlyTaskListDetailsViewModel monthlyTaskListDetails = new MonthlyTaskListDetailsViewModel();


                            monthlyTaskListDetails.Id = (long)dr["Id"];
                            monthlyTaskListDetails.MonthlyTaskListId = (long)dr["MonthlyTaskListId"];
                            monthlyTaskListDetails.Title = dr["Title"].ToString();
                            monthlyTaskListDetails.Note = dr["Note"].ToString();
                            monthlyTaskListDetails.TitleBangla = dr["TitleBangla"].ToString();
                            monthlyTaskListDetails.NoteBangla = dr["NoteBangla"].ToString();
                            monthlyTaskListDetails.Islink = (bool)dr["Islink"];

                            detailList.Add(monthlyTaskListDetails);
                        }
                        //dr.NextResult();
                        dr.Close();

                        monthlyTaskListVM.MonthlyTaskListDetailsViewModels = detailList;
                    }
                    else
                    {
                        ResultResponse result = new ResultResponse();
                        //accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                        List<SqlParameter> aParameters2 = new List<SqlParameter>();

                        var date = DateTime.Now.ToString("dd-MMM-yy hh:mm tt");

                        aParameters2.Add(new SqlParameter("@MonthlyTaskListId", monthlyTask.Id));
                        aParameters2.Add(new SqlParameter("@CreateDateTime", date.Split(' ')[0]));
                        aParameters2.Add(new SqlParameter("@BanglaName", monthlyTask.BanglaName));
                        aParameters2.Add(new SqlParameter("@EnglishName", monthlyTask.EnglishName));
                        aParameters2.Add(new SqlParameter("@Type", monthlyTask.Type));
                        aParameters2.Add(new SqlParameter("@ImageName", monthlyTask.ImageName));
                        aParameters2.Add(new SqlParameter("@ImagePath", monthlyTask.ImagePath));
                        aParameters2.Add(new SqlParameter("@ImageUrl", monthlyTask.ImageUrl));
                        aParameters2.Add(new SqlParameter("@InfoLink", monthlyTask.InfoLink));
                        aParameters2.Add(new SqlParameter("@SustainabilityCategoryId", monthlyTask.SustainabilityCategoryId));

                        aParameters2.Add(new SqlParameter("@Co2", monthlyTask.Co2));
                        aParameters2.Add(new SqlParameter("@Water", monthlyTask.Water));
                        aParameters2.Add(new SqlParameter("@Energy", monthlyTask.Energy));
                        aParameters2.Add(new SqlParameter("@Kindness", monthlyTask.Kindness));

                        accessManager.SaveData("sp_SaveCopyMonthlyTaskList", aParameters2);

                        accessManager.SqlConnectionClose();



                        var copyListNew = GetCopyMonthlyTaskList();

                        accessManager.SqlConnectionOpen(DataBase.KinshipDB);

                        var dateTime = copyListNew.CreateDateTime?.ToString("dd-MMM-yy hh:mm tt");

                        monthlyTaskListVM.Id = copyListNew.MonthlyTaskListId;
                        monthlyTaskListVM.CreateDate = dateTime.Split(' ')[0];
                        monthlyTaskListVM.CreateTime = dateTime.Split(' ')[1] + dateTime.Split(' ')[2];
                        monthlyTaskListVM.BanglaName = copyListNew.BanglaName.ToString();
                        monthlyTaskListVM.EnglishName = copyListNew.EnglishName.ToString();
                        monthlyTaskListVM.Type = copyListNew.Type.ToString();
                        monthlyTaskListVM.ImageName = copyListNew.ImageName.ToString();
                        monthlyTaskListVM.ImageUrl = copyListNew.ImageUrl.ToString();
                        monthlyTaskListVM.ImagePath = copyListNew.ImagePath.ToString();
                        monthlyTaskListVM.InfoLink = copyListNew.InfoLink.ToString();
                        monthlyTaskListVM.SustainabilityCategoryId = copyListNew.SustainabilityCategoryId;
                        //dailyTaskListVM.Action = dailyTask.Action;
                        monthlyTaskListVM.Co2 = copyListNew.Co2;
                        monthlyTaskListVM.Water = copyListNew.Water;
                        monthlyTaskListVM.Energy = copyListNew.Energy;
                        monthlyTaskListVM.Kindness = copyListNew.Kindness;

                        List<SqlParameter> aParameters = new List<SqlParameter>();
                        aParameters.Add(new SqlParameter("@monthlyTaskListId", copyListNew.MonthlyTaskListId));
                        SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetMonthlyTaskListDetails", aParameters);

                        List<MonthlyTaskListDetailsViewModel> detailList = new List<MonthlyTaskListDetailsViewModel>();

                        while (dr.Read())
                        {
                            MonthlyTaskListDetailsViewModel dailyTaskListDetails = new MonthlyTaskListDetailsViewModel();


                            dailyTaskListDetails.Id = (long)dr["Id"];
                            dailyTaskListDetails.MonthlyTaskListId = (long)dr["MonthlyTaskListId"];
                            dailyTaskListDetails.Title = dr["Title"].ToString();
                            dailyTaskListDetails.Note = dr["Note"].ToString();
                            dailyTaskListDetails.TitleBangla = dr["TitleBangla"].ToString();
                            dailyTaskListDetails.NoteBangla = dr["NoteBangla"].ToString();
                            dailyTaskListDetails.Islink = (bool)dr["Islink"];

                            detailList.Add(dailyTaskListDetails);
                        }
                        //dr.NextResult();
                        dr.Close();

                        monthlyTaskListVM.MonthlyTaskListDetailsViewModels = detailList;


                    }



                    #region old

                    //var dateTime = dailyTask.CreateDateTime?.ToString("dd-MMM-yy hh:mm tt");

                    //monthlyTaskListVM.Id = dailyTask.Id;
                    //monthlyTaskListVM.CreateDate = dateTime.Split(' ')[0];
                    //monthlyTaskListVM.CreateTime = dateTime.Split(' ')[1] + dateTime.Split(' ')[2];
                    //monthlyTaskListVM.BanglaName = dailyTask.BanglaName.ToString();
                    //monthlyTaskListVM.EnglishName = dailyTask.EnglishName.ToString();
                    //monthlyTaskListVM.Type = dailyTask.Type.ToString();
                    //monthlyTaskListVM.ImageName = dailyTask.ImageName.ToString();
                    //monthlyTaskListVM.ImageUrl = dailyTask.ImageUrl.ToString();
                    //monthlyTaskListVM.ImagePath = dailyTask.ImagePath.ToString();
                    //monthlyTaskListVM.InfoLink = dailyTask.InfoLink.ToString();
                    //monthlyTaskListVM.SustainabilityCategoryId = dailyTask.SustainabilityCategoryId;
                    ////dailyTaskListVM.Action = dailyTask.Action;
                    //monthlyTaskListVM.Co2 = dailyTask.Co2;
                    //monthlyTaskListVM.Water = dailyTask.Water;
                    //monthlyTaskListVM.Energy = dailyTask.Energy;
                    //monthlyTaskListVM.Kindness = dailyTask.Kindness;

                    //List<SqlParameter> aParameters = new List<SqlParameter>();
                    //aParameters.Add(new SqlParameter("@monthlyTaskListId", monthlyTaskListVM.Id));
                    //SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetMonthlyTaskListDetails", aParameters);

                    //List<MonthlyTaskListDetailsViewModel> detailList = new List<MonthlyTaskListDetailsViewModel>();

                    //while (dr.Read())
                    //{
                    //    MonthlyTaskListDetailsViewModel monthlyTaskListDetails = new MonthlyTaskListDetailsViewModel();


                    //    monthlyTaskListDetails.Id = (long)dr["Id"];
                    //    monthlyTaskListDetails.MonthlyTaskListId = (long)dr["MonthlyTaskListId"];
                    //    monthlyTaskListDetails.Title = dr["Title"].ToString();
                    //    monthlyTaskListDetails.Note = dr["Note"].ToString();
                    //    monthlyTaskListDetails.TitleBangla = dr["TitleBangla"].ToString();
                    //    monthlyTaskListDetails.NoteBangla = dr["NoteBangla"].ToString();
                    //    monthlyTaskListDetails.Islink = (bool)dr["Islink"];

                    //    detailList.Add(monthlyTaskListDetails);
                    //}
                    ////dr.NextResult();
                    //dr.Close();

                    //monthlyTaskListVM.MonthlyTaskListDetailsViewModels = detailList;

                    #endregion

                    //dailyTaskList.Add(monthlyTaskListVM);
                }
                return monthlyTaskListVM;
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

        public MonthlyTask GetMonthlyTaskByDate(string userId)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@UserId", userId));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetMonthlyTaskByMonth", aParameters);

                MonthlyTask task = new MonthlyTask();
                while (dr.Read())
                {

                    task.Id = (long)dr["Id"];
                    task.CreateDateTime = (DateTime?)dr["CreateDateTime"];
                    task.Score = (long)dr["Score"];
                    task.Action = (long)dr["Action"];
                    task.Type = dr["Type"].ToString();
                    //task.LevelUp = (int)dr["LevelUp"];
                    task.Status = (bool?)dr["Status"];
                    task.BusinessUnitId = (long)dr["BusinessUnitId"];
                }
                return task;
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

        public List<MonthlyTask> GetMonthlyTaskByDateCount(string userId)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@UserId", userId));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetMonthlyTaskByMonth", aParameters);

                List<MonthlyTask> tasks = new List<MonthlyTask>();
                //MonthlyTask task = new MonthlyTask();
                while (dr.Read())
                {
                    MonthlyTask task = new MonthlyTask();

                    task.Id = (long)dr["Id"];
                    task.CreateDateTime = (DateTime?)dr["CreateDateTime"];
                    task.Score = (long)dr["Score"];
                    task.Action = (long)dr["Action"];
                    task.Type = dr["Type"].ToString();
                    //task.LevelUp = (int)dr["LevelUp"];
                    task.Status = (bool?)dr["Status"];
                    task.BusinessUnitId = (long)dr["BusinessUnitId"];

                    tasks.Add(task);
                }
                return tasks;
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

        public MonthlyTask GetMonthlyTaskByByIdandUserDate(long monthlyTaskId, string userId)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@MonthlyTaskId", monthlyTaskId));
                aParameters.Add(new SqlParameter("@UserId", userId));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetMonthlyTaskByIdandUser", aParameters);

                MonthlyTask task = new MonthlyTask();
                while (dr.Read())
                {

                    task.Id = (long)dr["Id"];
                    task.CreateDateTime = (DateTime?)dr["CreateDateTime"];
                    task.Score = (long)dr["Score"];
                    task.Action = (long)dr["Action"];
                    task.Type = dr["Type"].ToString();
                    //task.LevelUp = (int)dr["LevelUp"];
                    task.Status = (bool?)dr["Status"];
                    task.BusinessUnitId = (long)dr["BusinessUnitId"];
                }
                return task;
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

        public bool GetTaskdisableorNot(long MonthlyTaskId, string userId)
        {
            try
            {
                var taskId = GetMonthlyTaskByByIdandUserDate(MonthlyTaskId, userId);
                if (taskId.Id > 0)
                {
                    return true;
                }

                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ResultResponse SaveMonthlyTaskNew(MonthlyTask monthlyTask)
        {
            try
            {
                //var task = GetMonthlyTaskByDate(monthlyTask.UserId);
                var list = GetMonthlyTaskByDateCount(monthlyTask.UserId);
                var taskId = GetMonthlyTaskByByIdandUserDate(monthlyTask.MonthlyTaskListId, monthlyTask.UserId);

                accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                ResultResponse result = new ResultResponse();

                //DataValidation(sustainabilitySurvey);


                //if (task.Id > 0)
                if (list.Count <= 9)
                {
                    if (taskId.Id > 0)
                    {
                        result.msg = "You Have Already Completed This Task";
                        return result;
                    }

                    monthlyTask.Score = 0;
                    if (list.Count() == 6)
                    {
                        monthlyTask.Score = 100;
                    }
                    //else if (list.Count() == 7)
                    //{
                    //    monthlyTask.Score = 105;
                    //}
                    //else if (list.Count() == 8)
                    //{
                    //    monthlyTask.Score = 110;
                    //}
                    else if (list.Count() == 9)
                    {
                        monthlyTask.Score = 150;
                    }

                    var monthklyTaskId = GetMonthlyTaskListById(monthlyTask.MonthlyTaskListId);

                    if (monthklyTaskId.Water == "")
                    {
                        monthlyTask.Water = 0;
                    }
                    else
                    {

                        monthlyTask.Water = decimal.Parse(monthklyTaskId.Water);
                    }
                    if (monthklyTaskId.Energy == "")
                    {
                        monthlyTask.Energy = 0;
                    }
                    else
                    {

                        monthlyTask.Energy = decimal.Parse(monthklyTaskId.Energy);
                    }
                    if (monthklyTaskId.Co2 == "")
                    {
                        monthlyTask.Co2 = 0;
                    }
                    else
                    {

                        monthlyTask.Co2 = decimal.Parse(monthklyTaskId.Co2);
                    }
                    if (monthklyTaskId.Kindness == "")
                    {
                        monthlyTask.Kindness = 0;
                    }
                    else
                    {

                        monthlyTask.Kindness = decimal.Parse(monthklyTaskId.Kindness);
                    }

                    accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                    bool status = true;
                    List<SqlParameter> aParameters = new List<SqlParameter>();

                    aParameters.Add(new SqlParameter("@CreateDateTime", DateTime.Now));
                    aParameters.Add(new SqlParameter("@MonthlyTaskListId", monthlyTask.MonthlyTaskListId));
                    aParameters.Add(new SqlParameter("@Score", monthlyTask.Score));
                    aParameters.Add(new SqlParameter("@UserId", monthlyTask.UserId));
                    aParameters.Add(new SqlParameter("@BusinessUnitId", monthlyTask.BusinessUnitId));
                    aParameters.Add(new SqlParameter("@Status", status));
                    aParameters.Add(new SqlParameter("@Type", monthlyTask.Type));
                    aParameters.Add(new SqlParameter("@Action", monthlyTask.Action));
                    //aParameters.Add(new SqlParameter("@LevelUp", monthlyTask.LevelUp));
                    aParameters.Add(new SqlParameter("@Co2", monthlyTask.Co2));
                    aParameters.Add(new SqlParameter("@Water", monthlyTask.Water));
                    aParameters.Add(new SqlParameter("@Energy", monthlyTask.Energy));
                    aParameters.Add(new SqlParameter("@Kindness", monthlyTask.Kindness));

                    result.isSuccess = accessManager.SaveData("sp_SaveMonthlyTask", aParameters);

                    result.msg = "Monthly Task Added Successfully";
                    return result;


                }
                else
                {
                    result.msg = "You Have Completed Your Monthly Task.";
                    return result;
                }


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
        public ResultResponse SaveMonthlyTask(MonthlyTask monthlyTask)
        {
            try
            {

                accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                ResultResponse result = new ResultResponse();

                //DataValidation(sustainabilitySurvey);
                var task = GetMonthlyTaskByDate(monthlyTask.UserId);

                if (task.Id > 0)
                {


                    result.msg = "You Have Completed Your Monthly Task.";
                    return result;
                }
                else
                {
                    var monthklyTaskId = GetMonthlyTaskListById(monthlyTask.MonthlyTaskListId);

                    if (monthklyTaskId.Water == "")
                    {
                        monthlyTask.Water = 0;
                    }
                    else
                    {

                        monthlyTask.Water = decimal.Parse(monthklyTaskId.Water);
                    }
                    if (monthklyTaskId.Energy == "")
                    {
                        monthlyTask.Energy = 0;
                    }
                    else
                    {

                        monthlyTask.Energy = decimal.Parse(monthklyTaskId.Energy);
                    }
                    if (monthklyTaskId.Co2 == "")
                    {
                        monthlyTask.Co2 = 0;
                    }
                    else
                    {

                        monthlyTask.Co2 = decimal.Parse(monthklyTaskId.Co2);
                    }
                    if (monthklyTaskId.Kindness == "")
                    {
                        monthlyTask.Kindness = 0;
                    }
                    else
                    {

                        monthlyTask.Kindness = decimal.Parse(monthklyTaskId.Kindness);
                    }

                    accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                    bool status = true;
                    List<SqlParameter> aParameters = new List<SqlParameter>();

                    aParameters.Add(new SqlParameter("@CreateDateTime", DateTime.Now));
                    aParameters.Add(new SqlParameter("@MonthlyTaskListId", monthlyTask.MonthlyTaskListId));
                    aParameters.Add(new SqlParameter("@Score", monthlyTask.Score));
                    aParameters.Add(new SqlParameter("@UserId", monthlyTask.UserId));
                    aParameters.Add(new SqlParameter("@BusinessUnitId", monthlyTask.BusinessUnitId));
                    aParameters.Add(new SqlParameter("@Status", status));
                    aParameters.Add(new SqlParameter("@Type", monthlyTask.Type));
                    aParameters.Add(new SqlParameter("@Action", monthlyTask.Action));
                    //aParameters.Add(new SqlParameter("@LevelUp", monthlyTask.LevelUp));
                    aParameters.Add(new SqlParameter("@Co2", monthlyTask.Co2));
                    aParameters.Add(new SqlParameter("@Water", monthlyTask.Water));
                    aParameters.Add(new SqlParameter("@Energy", monthlyTask.Energy));
                    aParameters.Add(new SqlParameter("@Kindness", monthlyTask.Kindness));

                    result.isSuccess = accessManager.SaveData("sp_SaveMonthlyTask", aParameters);

                    result.msg = "Daily Task Added Successfully";
                    return result;
                }


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

        public ResultResponse SaveMonthlyTaskList(MonthlyTaskList monthlyTaskList)
        {
            try
            {

                accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                ResultResponse result = new ResultResponse();

                //DataValidation(sustainabilitySurvey);

                bool status = true;
                List<SqlParameter> aParameters = new List<SqlParameter>();

                aParameters.Add(new SqlParameter("@CreateDateTime", DateTime.Now));
                aParameters.Add(new SqlParameter("@BanglaName", monthlyTaskList.BanglaName));
                aParameters.Add(new SqlParameter("@EnglishName", monthlyTaskList.EnglishName));
                aParameters.Add(new SqlParameter("@Type", monthlyTaskList.Type));
                aParameters.Add(new SqlParameter("@ImageName", monthlyTaskList.ImageName));
                aParameters.Add(new SqlParameter("@ImagePath", monthlyTaskList.ImagePath));
                aParameters.Add(new SqlParameter("@ImageUrl", monthlyTaskList.ImageUrl));
                aParameters.Add(new SqlParameter("@InfoLink", monthlyTaskList.InfoLink));
                aParameters.Add(new SqlParameter("@SustainabilityCategoryId", monthlyTaskList.SustainabilityCategoryId));

                aParameters.Add(new SqlParameter("@Co2", monthlyTaskList.Co2));
                aParameters.Add(new SqlParameter("@Water", monthlyTaskList.Water));
                aParameters.Add(new SqlParameter("@Energy", monthlyTaskList.Energy));
                aParameters.Add(new SqlParameter("@Kindness", monthlyTaskList.Kindness));

                result.isSuccess = accessManager.SaveData("sp_UploadMonthlyTaskList", aParameters);

                result.msg = "Daily Task Added Successfully";
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

        public ResultResponse SaveMonthlyTaskListDetails(MonthlyTaskListDetails monthlyTaskListDetails)
        {
            try
            {

                accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                ResultResponse result = new ResultResponse();

                //DataValidation(sustainabilitySurvey);


                List<SqlParameter> aParameters = new List<SqlParameter>();

                aParameters.Add(new SqlParameter("@CreateDateTime", DateTime.Now));
                aParameters.Add(new SqlParameter("@MonthlyTaskListId", monthlyTaskListDetails.MonthlyTaskListId));
                aParameters.Add(new SqlParameter("@Title", monthlyTaskListDetails.Title));
                aParameters.Add(new SqlParameter("@Note", monthlyTaskListDetails.Note));
                aParameters.Add(new SqlParameter("@TitleBangla", monthlyTaskListDetails.TitleBangla));
                aParameters.Add(new SqlParameter("@NoteBangla", monthlyTaskListDetails.NoteBangla));

                bool isLink = true;

                aParameters.Add(new SqlParameter("@Islink", isLink));

                result.isSuccess = accessManager.SaveData("sp_UploadMonthlyTaskListDetails", aParameters);

                result.msg = "Daily Task Added Successfully";
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

        #endregion

        #region Sustainability ContactUs

        public List<SustainabilityContactUs> GetSustainabilityContactUsByUserAndDate(string userId)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.KinshipDB);

                List<SustainabilityContactUs> monthlyTasks = new List<SustainabilityContactUs>();

                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@UserId", userId));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetSustainabilityContactUsByUserAndDate", aParameters);

                while (dr.Read())
                {

                    SustainabilityContactUs sc = new SustainabilityContactUs();
                    //var dateTime = Convert.ToDateTime(dr["CreateDateTime"]).ToString("dd-MMM-yy hh:mm tt");

                    sc.Id = (long)dr["Id"];
                    sc.CreateDateTime = (DateTime?)dr["CreateDateTime"];
                    sc.Type = dr["Type"].ToString();
                    sc.ImageName = dr["ImageName"].ToString();
                    sc.ImageUrl = dr["ImageUrl"].ToString();
                    sc.ImagePath = dr["ImagePath"].ToString();
                    sc.Action = (long)dr["Action"];
                    sc.Discription = dr["Discription"].ToString();
                    sc.Score = (long)dr["Score"];
                    sc.ImageName = dr["ImageName"].ToString();
                    sc.ImagePath = dr["ImagePath"].ToString();
                    sc.ImageUrl = dr["ImageUrl"].ToString();


                    monthlyTasks.Add(sc);
                }
                return monthlyTasks;
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

        public ResultResponse SaveSustainabilityContactUs()
        {
            try
            {

                accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                ResultResponse result = new ResultResponse();

                var ctx = HttpContext.Current.Request;

                // HTTP Respons
                //HttpResponseMessage responseMessage = Request.CreateResponse(HttpStatusCode.OK);

                var Score = ctx.Params["Score"];
                var UserId = ctx.Params["UserId"];
                var BusinessUnitId = ctx.Params["BusinessUnitId"];
                var Action = ctx.Params["Action"];
                var Discription = ctx.Params["Discription"];
                var Type = ctx.Params["Type"];
                //var LevelUp = ctx.Params["LevelUp"];          


                var list = GetSustainabilityContactUsByUserAndDate(UserId);

                if (list.Count <= 2)
                {
                    accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                    if (HttpContext.Current.Request.Files.Count == 0)
                    {
                        throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                    }

                    //read the file
                    HttpPostedFile postedFile = HttpContext.Current.Request.Files[0];
                    string filename = Path.GetFileName(postedFile.FileName);

                    string filePath = "";
                    string urlPath = "";
                    if (filename != "")
                    {
                        //filePath = @"D:\Rabby\Kingship\SustainabilityImages\" + filename; //local
                        //string filePath = @"D:\PUBLISH PROJECTS\EnergyApi\SustainabilityImages\" + filename;
                        filePath = @"D:\PUBLISH PROJECTS\EnergyApi\SustainabilityImages\" + filename;

                        urlPath = @"/SustainabilityImages/" + filename;
                        postedFile.SaveAs(filePath);
                    }

                    var count = list.Count();
                    if (list.Count() == 1)
                    {
                        Score = "40";
                    }
                    else if (list.Count() == 2)
                    {
                        Score = "20";
                    }


                    //DataValidation(sustainabilitySurvey);

                    bool status = true;
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@CreateDateTime", DateTime.Now));
                    aParameters.Add(new SqlParameter("@Score", Score));
                    //aParameters.Add(new SqlParameter("@UserId", long.Parse(UserId)));
                    aParameters.Add(new SqlParameter("@UserId", UserId));
                    aParameters.Add(new SqlParameter("@BusinessUnitId", long.Parse(BusinessUnitId)));
                    aParameters.Add(new SqlParameter("@Status", status));
                    aParameters.Add(new SqlParameter("@Type", Type));
                    aParameters.Add(new SqlParameter("@Action", Action));

                    aParameters.Add(new SqlParameter("@Discription", Discription));
                    aParameters.Add(new SqlParameter("@ImageName", filename));
                    aParameters.Add(new SqlParameter("@ImagePath", filePath));
                    aParameters.Add(new SqlParameter("@ImageUrl", urlPath));
                    //aParameters.Add(new SqlParameter("@LevelUp", LevelUp));

                    //aParameters.Add(new SqlParameter("@CreateDateTime", DateTime.Now));
                    //aParameters.Add(new SqlParameter("@Score", sustainabilityContactUs.Score));
                    //aParameters.Add(new SqlParameter("@UserId", sustainabilityContactUs.UserId));
                    //aParameters.Add(new SqlParameter("@BusinessUnitId", sustainabilityContactUs.BusinessUnitId));
                    //aParameters.Add(new SqlParameter("@Status", status));
                    //aParameters.Add(new SqlParameter("@Type", sustainabilityContactUs.Type));
                    //aParameters.Add(new SqlParameter("@Action", sustainabilityContactUs.Action));

                    //aParameters.Add(new SqlParameter("@Discription", sustainabilityContactUs.Discription));
                    //aParameters.Add(new SqlParameter("@ImageName", sustainabilityContactUs.ImageName));
                    //aParameters.Add(new SqlParameter("@ImagePath", sustainabilityContactUs.ImagePath));
                    //aParameters.Add(new SqlParameter("@ImageUrl", sustainabilityContactUs.ImageUrl));

                    result.isSuccess = accessManager.SaveData("sp_SaveSustainabilityContactUs", aParameters);

                    result.msg = "Sustainability ContactUs Added Successfully";
                    return result;
                }
                else
                {
                    result.msg = "You Have Completed Your Activity For This Month";
                    return result;
                }




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

        #endregion

        #region Daily Tips

        public List<DailyTipsTask> GetDailyTipsTaskList()
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.KinshipDB);

                List<DailyTipsTask> dailyTipsTaskList = new List<DailyTipsTask>();

                List<SqlParameter> aParameters = new List<SqlParameter>();

                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetDailyTipsTask", aParameters);

                while (dr.Read())
                {

                    DailyTipsTask dailyTipsTask = new DailyTipsTask();
                    //var dateTime = Convert.ToDateTime(dr["CreateDateTime"]).ToString("dd-MMM-yy hh:mm tt");

                    dailyTipsTask.Id = (long)dr["Id"];
                    dailyTipsTask.CreateDateTime = (DateTime?)dr["CreateDateTime"];
                    dailyTipsTask.OriginalLink = dr["OriginalLink"].ToString();
                    dailyTipsTask.LinkCode = dr["LinkCode"].ToString();
                    dailyTipsTask.Type = dr["Type"].ToString();

                    dailyTipsTaskList.Add(dailyTipsTask);
                }
                return dailyTipsTaskList;
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

        public CopyDailyTipsTask GetCopyDailyTipsTaskList()
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.KinshipDB);

                List<CopyDailyTipsTask> copyDailyTipsTask = new List<CopyDailyTipsTask>();
                List<SqlParameter> aParameters = new List<SqlParameter>();
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetCopyDailyTipsTask", aParameters);

                CopyDailyTipsTask dailyTipsTask = new CopyDailyTipsTask();
                while (dr.Read())
                {


                    //var dateTime = Convert.ToDateTime(dr["CreateDateTime"]).ToString("dd-MMM-yy hh:mm tt");

                    dailyTipsTask.Id = (long)dr["Id"];
                    dailyTipsTask.DailyTipsTaskId = (long)dr["DailyTipsTaskId"];
                    dailyTipsTask.CreateDateTime = (DateTime?)dr["CreateDateTime"];
                    dailyTipsTask.OriginalLink = dr["OriginalLink"].ToString();
                    dailyTipsTask.LinkCode = dr["LinkCode"].ToString();
                    dailyTipsTask.Type = dr["Type"].ToString();


                }
                return dailyTipsTask;
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

        public DailyTipsTaskViewModel GetDailyTipsTask()
        {
            try
            {
                var list = GetDailyTipsTaskList();


                var copyList = GetCopyDailyTipsTaskList();

                accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                //List<DailyTaskListViewModel> dailyTaskList = new List<DailyTaskListViewModel>();
                DailyTipsTaskViewModel dailyTipsTaskVM = new DailyTipsTaskViewModel();
                foreach (var dailyTask in list)
                {
                    // save 
                    //var copyList = GetCopyDailyTaskList(dailyTask.Id);


                    if (copyList.Id > 0)
                    {
                        var dateTime = copyList.CreateDateTime?.ToString("dd-MMM-yy hh:mm tt");

                        dailyTipsTaskVM.Id = copyList.DailyTipsTaskId;
                        dailyTipsTaskVM.CreateDate = dateTime.Split(' ')[0];
                        dailyTipsTaskVM.CreateTime = dateTime.Split(' ')[1] + dateTime.Split(' ')[2];
                        dailyTipsTaskVM.OriginalLink = copyList.OriginalLink.ToString();
                        dailyTipsTaskVM.LinkCode = copyList.LinkCode.ToString();
                        dailyTipsTaskVM.Type = copyList.Type.ToString();

                    }
                    else
                    {
                        //accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                        List<SqlParameter> aParameters2 = new List<SqlParameter>();

                        var date = DateTime.Now.ToString("dd-MMM-yy hh:mm tt");

                        aParameters2.Add(new SqlParameter("@DailyTaskListId", dailyTask.Id));
                        aParameters2.Add(new SqlParameter("@CreateDateTime", date.Split(' ')[0]));
                        aParameters2.Add(new SqlParameter("@OriginalLink", dailyTask.OriginalLink));
                        aParameters2.Add(new SqlParameter("@LinkCode", dailyTask.LinkCode));
                        aParameters2.Add(new SqlParameter("@Type", dailyTask.Type));

                        accessManager.SaveData("sp_SaveCopyDailyTipsTask", aParameters2);

                        accessManager.SqlConnectionClose();

                        var copyListNew = GetCopyDailyTipsTaskList();
                        accessManager.SqlConnectionOpen(DataBase.KinshipDB);

                        var dateTime = copyListNew.CreateDateTime?.ToString("dd-MMM-yy hh:mm tt");

                        dailyTipsTaskVM.Id = copyListNew.DailyTipsTaskId;
                        dailyTipsTaskVM.CreateDate = dateTime.Split(' ')[0];
                        dailyTipsTaskVM.CreateTime = dateTime.Split(' ')[1] + dateTime.Split(' ')[2];
                        dailyTipsTaskVM.OriginalLink = copyListNew.OriginalLink.ToString();
                        dailyTipsTaskVM.LinkCode = copyListNew.LinkCode.ToString();
                        dailyTipsTaskVM.Type = copyListNew.Type.ToString();

                    }

                }
                return dailyTipsTaskVM;
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

        public DailyTips GetDailyTipsByDate(string userId)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@UserId", userId));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetDailyTipsByDate", aParameters);

                DailyTips task = new DailyTips();
                while (dr.Read())
                {

                    task.Id = (long)dr["Id"];
                    task.CreateDateTime = (DateTime?)dr["CreateDateTime"];
                    task.Score = (long)dr["Score"];
                    task.Action = (long)dr["Action"];
                    task.Type = dr["Type"].ToString();
                    //task.LevelUp = (int)dr["LevelUp"];
                    task.Status = (bool?)dr["Status"];
                    task.BusinessUnitId = (long)dr["BusinessUnitId"];
                }
                return task;
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

        public List<DailyTips> GetDailyTipsCount(string userId)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@UserId", userId));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetDailyTipsByDate", aParameters);

                List<DailyTips> taskList = new List<DailyTips>();
                while (dr.Read())
                {
                    DailyTips task = new DailyTips();
                    task.Id = (long)dr["Id"];
                    task.CreateDateTime = (DateTime?)dr["CreateDateTime"];
                    task.Score = (long)dr["Score"];
                    task.Action = (long)dr["Action"];
                    task.Type = dr["Type"].ToString();
                    //task.LevelUp = (int)dr["LevelUp"];
                    task.Status = (bool?)dr["Status"];
                    task.BusinessUnitId = (long)dr["BusinessUnitId"];

                    taskList.Add(task);
                }
                return taskList;
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

        public ResultResponse SaveDailyTips(DailyTips dailyTips)
        {
            try
            {
                var list = GetDailyTipsCount(dailyTips.UserId);

                accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                ResultResponse result = new ResultResponse();

                //DataValidation(sustainabilitySurvey);
                //var task = GetDailyTipsByDate(dailyTips.UserId);

                //if (task.Id > 0)
                //{


                //    result.msg = "You Have Completed Your Daily Tips.";
                //    return result;
                //}
                if (list.Count <= 2)
                {
                    bool status = true;
                    List<SqlParameter> aParameters = new List<SqlParameter>();

                    var count = list.Count();
                    if (list.Count() == 1)
                    {
                        dailyTips.Score = 50;
                    }
                    else if (list.Count() == 2)
                    {
                        dailyTips.Score = 20;
                    }


                    aParameters.Add(new SqlParameter("@CreateDateTime", DateTime.Now));
                    aParameters.Add(new SqlParameter("@DailyTipsTaskId", dailyTips.DailyTipsTaskId));
                    aParameters.Add(new SqlParameter("@Score", dailyTips.Score));
                    aParameters.Add(new SqlParameter("@UserId", dailyTips.UserId));
                    aParameters.Add(new SqlParameter("@BusinessUnitId", dailyTips.BusinessUnitId));
                    aParameters.Add(new SqlParameter("@Status", status));
                    aParameters.Add(new SqlParameter("@Type", dailyTips.Type));
                    aParameters.Add(new SqlParameter("@Action", dailyTips.Action));
                    //aParameters.Add(new SqlParameter("@LevelUp", dailyTips.LevelUp));

                    result.isSuccess = accessManager.SaveData("sp_SaveDailyTips", aParameters);

                    result.msg = "Daily Tips Added Successfully";
                    return result;
                }
                else
                {
                    result.msg = "You Have Completed Your Daily Tips.";
                    return result;
                }
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

        public ResultResponse SaveDailyTipsTask(DailyTipsTask dailyTipsTask)
        {
            try
            {

                accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                ResultResponse result = new ResultResponse();

                //DataValidation(sustainabilitySurvey);

                string[] s2 = dailyTipsTask.OriginalLink.Split('=');
                dailyTipsTask.LinkCode = s2[1];

                List<SqlParameter> aParameters = new List<SqlParameter>();

                aParameters.Add(new SqlParameter("@CreateDateTime", DateTime.Now));
                aParameters.Add(new SqlParameter("@OriginalLink", dailyTipsTask.OriginalLink));
                aParameters.Add(new SqlParameter("@LinkCode", dailyTipsTask.LinkCode));
                aParameters.Add(new SqlParameter("@Type", dailyTipsTask.Type));

                result.isSuccess = accessManager.SaveData("sp_UploadDailyTipsTask", aParameters);

                result.msg = "Daily Tips Added Successfully";
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

        #endregion

        #region Monthly Tips

        public bool GetTipsTaskdisableorNot(long monthlyTipsId, string userId)
        {
            try
            {
                var taskId = GetMonthlyTipsByIdandUser(monthlyTipsId, userId);
                if (taskId.Id > 0)
                {
                    return true;
                }

                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public MonthlyTips GetMonthlyTipsByIdandUser(long monthlyTipsId, string userId)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@MonthlyTipsTaskId", monthlyTipsId));
                aParameters.Add(new SqlParameter("@UserId", userId));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetMonthlyTipsByIdandUser", aParameters);

                MonthlyTips task = new MonthlyTips();
                while (dr.Read())
                {

                    task.Id = (long)dr["Id"];
                    task.CreateDateTime = (DateTime?)dr["CreateDateTime"];
                    task.Score = (long)dr["Score"];
                    task.Action = (long)dr["Action"];
                    task.Type = dr["Type"].ToString();
                    //task.LevelUp = (int)dr["LevelUp"];
                    task.Status = (bool?)dr["Status"];
                    task.BusinessUnitId = (long)dr["BusinessUnitId"];
                }
                return task;
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

        public List<MonthlyTipsTask> GetMonthlyTipsTaskList()
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.KinshipDB);

                List<MonthlyTipsTask> monthlyTipsTaskList = new List<MonthlyTipsTask>();

                List<SqlParameter> aParameters = new List<SqlParameter>();

                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetMonthlyTipsTask", aParameters);

                while (dr.Read())
                {

                    MonthlyTipsTask monthlyTipsTask = new MonthlyTipsTask();
                    //var dateTime = Convert.ToDateTime(dr["CreateDateTime"]).ToString("dd-MMM-yy hh:mm tt");

                    monthlyTipsTask.Id = (long)dr["Id"];
                    monthlyTipsTask.CreateDateTime = (DateTime?)dr["CreateDateTime"];
                    monthlyTipsTask.OriginalLink = dr["OriginalLink"].ToString();
                    monthlyTipsTask.LinkCode = dr["LinkCode"].ToString();
                    monthlyTipsTask.Type = dr["Type"].ToString();

                    monthlyTipsTaskList.Add(monthlyTipsTask);
                }
                return monthlyTipsTaskList;
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

        public List<MonthlyTipsTaskViewModel> GetMonthlyTipsTaskListByType(string type, string userId)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.KinshipDB);

                List<MonthlyTipsTaskViewModel> monthlyTipsTaskList = new List<MonthlyTipsTaskViewModel>();

                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@Type", type));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetMonthlyTipsTaskByType", aParameters);

                while (dr.Read())
                {
                    var DisableStatus = false;
                    var taskId = GetMonthlyTipsByIdandUser((long)dr["Id"], userId);
                    if (taskId.Score == 20)
                    {
                        DisableStatus = true; //GetTipsTaskdisableorNot((long)dr["Id"], userId);
                    }
                    //var DisableStatus = GetTipsTaskdisableorNot((long)dr["Id"], userId);

                    MonthlyTipsTaskViewModel monthlyTipsTask = new MonthlyTipsTaskViewModel();
                    var dateTime = Convert.ToDateTime(dr["CreateDateTime"]).ToString("dd-MMM-yy hh:mm tt");

                    monthlyTipsTask.Id = (long)dr["Id"];
                    //monthlyTipsTask.CreateDateTime = (DateTime?)dr["CreateDateTime"];
                    monthlyTipsTask.CreateDate = dateTime.Split(' ')[0];
                    monthlyTipsTask.CreateTime = dateTime.Split(' ')[1] + dateTime.Split(' ')[2];
                    monthlyTipsTask.OriginalLink = dr["OriginalLink"].ToString();
                    monthlyTipsTask.LinkCode = dr["LinkCode"].ToString();
                    monthlyTipsTask.Type = dr["Type"].ToString();

                    monthlyTipsTask.DisableStatus = DisableStatus;

                    monthlyTipsTaskList.Add(monthlyTipsTask);
                }
                return monthlyTipsTaskList;
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

        public CopyMonthlyTipsTask GetCopyMonthlyTipsTaskList()
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.KinshipDB);

                List<CopyMonthlyTipsTask> copyMonthlyTipsTask = new List<CopyMonthlyTipsTask>();
                List<SqlParameter> aParameters = new List<SqlParameter>();
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetCopyMonthlyTipsTask", aParameters);

                CopyMonthlyTipsTask monthlyTipsTask = new CopyMonthlyTipsTask();
                while (dr.Read())
                {


                    //var dateTime = Convert.ToDateTime(dr["CreateDateTime"]).ToString("dd-MMM-yy hh:mm tt");

                    monthlyTipsTask.Id = (long)dr["Id"];
                    monthlyTipsTask.MonthlyTipsTaskId = (long)dr["MonthlyTipsTaskId"];
                    monthlyTipsTask.CreateDateTime = (DateTime?)dr["CreateDateTime"];
                    monthlyTipsTask.OriginalLink = dr["OriginalLink"].ToString();
                    monthlyTipsTask.LinkCode = dr["LinkCode"].ToString();
                    monthlyTipsTask.Type = dr["Type"].ToString();


                }
                return monthlyTipsTask;
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

        public MonthlyTipsTaskViewModel GetMonthlyTipsTask()
        {
            try
            {
                var list = GetMonthlyTipsTaskList();


                var copyList = GetCopyMonthlyTipsTaskList();

                accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                //List<MonthlyTaskListViewModel> MonthlyTaskList = new List<MonthlyTaskListViewModel>();
                MonthlyTipsTaskViewModel monthlyTipsTaskVM = new MonthlyTipsTaskViewModel();
                foreach (var monthlyTask in list)
                {
                    // save 
                    //var copyList = GetCopyMonthlyTaskList(MonthlyTask.Id);


                    if (copyList.Id > 0)
                    {
                        var dateTime = copyList.CreateDateTime?.ToString("dd-MMM-yy hh:mm tt");

                        monthlyTipsTaskVM.Id = copyList.MonthlyTipsTaskId;
                        monthlyTipsTaskVM.CreateDate = dateTime.Split(' ')[0];
                        monthlyTipsTaskVM.CreateTime = dateTime.Split(' ')[1] + dateTime.Split(' ')[2];
                        monthlyTipsTaskVM.OriginalLink = copyList.OriginalLink.ToString();
                        monthlyTipsTaskVM.LinkCode = copyList.LinkCode.ToString();
                        monthlyTipsTaskVM.Type = copyList.Type.ToString();

                    }
                    else
                    {
                        //accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                        List<SqlParameter> aParameters2 = new List<SqlParameter>();

                        var date = DateTime.Now.ToString("dd-MMM-yy hh:mm tt");

                        aParameters2.Add(new SqlParameter("@MonthlyTaskListId", monthlyTask.Id));
                        aParameters2.Add(new SqlParameter("@CreateDateTime", date.Split(' ')[0]));
                        aParameters2.Add(new SqlParameter("@OriginalLink", monthlyTask.OriginalLink));
                        aParameters2.Add(new SqlParameter("@LinkCode", monthlyTask.LinkCode));
                        aParameters2.Add(new SqlParameter("@Type", monthlyTask.Type));

                        accessManager.SaveData("sp_SaveCopyMonthlyTipsTask", aParameters2);

                        accessManager.SqlConnectionClose();

                        var copyListNew = GetCopyMonthlyTipsTaskList();
                        accessManager.SqlConnectionOpen(DataBase.KinshipDB);

                        var dateTime = copyListNew.CreateDateTime?.ToString("dd-MMM-yy hh:mm tt");

                        monthlyTipsTaskVM.Id = copyListNew.MonthlyTipsTaskId;
                        monthlyTipsTaskVM.CreateDate = dateTime.Split(' ')[0];
                        monthlyTipsTaskVM.CreateTime = dateTime.Split(' ')[1] + dateTime.Split(' ')[2];
                        monthlyTipsTaskVM.OriginalLink = copyListNew.OriginalLink.ToString();
                        monthlyTipsTaskVM.LinkCode = copyListNew.LinkCode.ToString();
                        monthlyTipsTaskVM.Type = copyListNew.Type.ToString();

                    }

                }
                return monthlyTipsTaskVM;
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

        public List<MonthlyTipsTaskViewModel> GetMonthlyTipsTasks(string type)
        {
            try
            {
                var list = GetMonthlyTipsTaskList();

                var copyList = GetCopyMonthlyTipsTaskList();

                accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                List<MonthlyTipsTaskViewModel> MonthlyTaskList = new List<MonthlyTipsTaskViewModel>();

                // save 
                //var copyList = GetCopyMonthlyTaskList(MonthlyTask.Id);
                MonthlyTipsTaskViewModel monthlyTipsTaskVM = new MonthlyTipsTaskViewModel();

                if (copyList.Id > 0)
                {
                    var dateTime = copyList.CreateDateTime?.ToString("dd-MMM-yy hh:mm tt");

                    monthlyTipsTaskVM.Id = copyList.MonthlyTipsTaskId;
                    monthlyTipsTaskVM.CreateDate = dateTime.Split(' ')[0];
                    monthlyTipsTaskVM.CreateTime = dateTime.Split(' ')[1] + dateTime.Split(' ')[2];
                    monthlyTipsTaskVM.OriginalLink = copyList.OriginalLink.ToString();
                    monthlyTipsTaskVM.LinkCode = copyList.LinkCode.ToString();
                    monthlyTipsTaskVM.Type = copyList.Type.ToString();

                    MonthlyTaskList.Add(monthlyTipsTaskVM);
                }
                else
                {
                    foreach (var monthlyTask in list)
                    {
                        //accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                        List<SqlParameter> aParameters2 = new List<SqlParameter>();

                        var date = DateTime.Now.ToString("dd-MMM-yy hh:mm tt");

                        aParameters2.Add(new SqlParameter("@MonthlyTaskListId", monthlyTask.Id));
                        aParameters2.Add(new SqlParameter("@CreateDateTime", date.Split(' ')[0]));
                        aParameters2.Add(new SqlParameter("@OriginalLink", monthlyTask.OriginalLink));
                        aParameters2.Add(new SqlParameter("@LinkCode", monthlyTask.LinkCode));
                        aParameters2.Add(new SqlParameter("@Type", monthlyTask.Type));

                        accessManager.SaveData("sp_SaveCopyMonthlyTipsTask", aParameters2);

                        accessManager.SqlConnectionClose();

                        var copyListNew = GetCopyMonthlyTipsTaskList();
                        accessManager.SqlConnectionOpen(DataBase.KinshipDB);

                        var dateTime = copyListNew.CreateDateTime?.ToString("dd-MMM-yy hh:mm tt");

                        monthlyTipsTaskVM.Id = copyListNew.MonthlyTipsTaskId;
                        monthlyTipsTaskVM.CreateDate = dateTime.Split(' ')[0];
                        monthlyTipsTaskVM.CreateTime = dateTime.Split(' ')[1] + dateTime.Split(' ')[2];
                        monthlyTipsTaskVM.OriginalLink = copyListNew.OriginalLink.ToString();
                        monthlyTipsTaskVM.LinkCode = copyListNew.LinkCode.ToString();
                        monthlyTipsTaskVM.Type = copyListNew.Type.ToString();

                        MonthlyTaskList.Add(monthlyTipsTaskVM);
                    }

                }

                return MonthlyTaskList;
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

        public MonthlyTips GetMonthlyTipsByDate(string userId)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@UserId", userId));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetMonthlyTipsByMonth", aParameters);

                MonthlyTips task = new MonthlyTips();
                while (dr.Read())
                {

                    task.Id = (long)dr["Id"];
                    task.CreateDateTime = (DateTime?)dr["CreateDateTime"];
                    task.Score = (long)dr["Score"];
                    task.Action = (long)dr["Action"];
                    task.Type = dr["Type"].ToString();
                    //task.LevelUp = (int)dr["LevelUp"];
                    task.Status = (bool?)dr["Status"];
                    task.BusinessUnitId = (long)dr["BusinessUnitId"];
                }
                return task;
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

        public List<MonthlyTips> GetMonthlyTipsCount(long monthlyTipsId, string userId)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@MonthlyTipsTaskId", monthlyTipsId));
                aParameters.Add(new SqlParameter("@UserId", userId));
                //SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetMonthlyTipsByMonth", aParameters);
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetMonthlyTipsByIdandUser", aParameters);

                List<MonthlyTips> taskList = new List<MonthlyTips>();


                while (dr.Read())
                {
                    MonthlyTips task = new MonthlyTips();
                    task.Id = (long)dr["Id"];
                    task.CreateDateTime = (DateTime?)dr["CreateDateTime"];
                    task.Score = (long)dr["Score"];
                    task.Action = (long)dr["Action"];
                    task.Type = dr["Type"].ToString();
                    //task.LevelUp = (int)dr["LevelUp"];
                    task.Status = (bool?)dr["Status"];
                    task.BusinessUnitId = (long)dr["BusinessUnitId"];

                    taskList.Add(task);
                }
                return taskList;
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

        public ResultResponse SaveMonthlyTips(MonthlyTips monthlyTips)
        {
            try
            {
                var list = GetMonthlyTipsCount(monthlyTips.MonthlyTipsTaskId, monthlyTips.UserId);

                accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                ResultResponse result = new ResultResponse();

                //DataValidation(sustainabilitySurvey);

                //var task = GetMonthlyTipsByDate(monthlyTips.UserId);

                //if (task.Id > 0)
                //{


                //    result.msg = "You Have Completed Your Monthly Tips.";
                //    return result;
                //}
                if (list.Count <= 2)
                {

                    accessManager.SqlConnectionOpen(DataBase.KinshipDB);

                    bool status = true;
                    List<SqlParameter> aParameters = new List<SqlParameter>();

                    var count = list.Count();
                    if (list.Count() == 1)
                    {
                        monthlyTips.Score = 50;
                    }
                    else if (list.Count() == 2)
                    {
                        monthlyTips.Score = 20;
                    }

                    aParameters.Add(new SqlParameter("@CreateDateTime", DateTime.Now));
                    aParameters.Add(new SqlParameter("@MonthlyTipsTaskId", monthlyTips.MonthlyTipsTaskId));
                    aParameters.Add(new SqlParameter("@Score", monthlyTips.Score));
                    aParameters.Add(new SqlParameter("@UserId", monthlyTips.UserId));
                    aParameters.Add(new SqlParameter("@BusinessUnitId", monthlyTips.BusinessUnitId));
                    aParameters.Add(new SqlParameter("@Status", status));
                    aParameters.Add(new SqlParameter("@Type", monthlyTips.Type));
                    aParameters.Add(new SqlParameter("@Action", monthlyTips.Action));
                    //aParameters.Add(new SqlParameter("@LevelUp", monthlyTips.LevelUp));

                    result.isSuccess = accessManager.SaveData("sp_SaveMonthlyTips", aParameters);

                    result.msg = "Monthly Tips Added Successfully";
                    return result;
                }
                else
                {
                    result.msg = "You Have Completed Your Monthly Tips.";
                    return result;
                }
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

        public ResultResponse SaveMonthlyTipsTask(MonthlyTipsTask monthlyTipsTask)
        {
            try
            {

                accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                ResultResponse result = new ResultResponse();

                //DataValidation(sustainabilitySurvey);

                string[] s2 = monthlyTipsTask.OriginalLink.Split('=');
                monthlyTipsTask.LinkCode = s2[1];

                List<SqlParameter> aParameters = new List<SqlParameter>();

                aParameters.Add(new SqlParameter("@CreateDateTime", DateTime.Now));
                aParameters.Add(new SqlParameter("@OriginalLink", monthlyTipsTask.OriginalLink));
                aParameters.Add(new SqlParameter("@LinkCode", monthlyTipsTask.LinkCode));
                aParameters.Add(new SqlParameter("@Type", monthlyTipsTask.Type));

                result.isSuccess = accessManager.SaveData("sp_UploadMonthlyTipsTask", aParameters);

                result.msg = "Monthly Tips Added Successfully";
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

        #endregion

        #region Result of task

        public List<ResultOfTaskViewModel> GetAllTaskResult(string userId)
        {
            accessManager.SqlConnectionOpen(DataBase.KinshipDB);

            List<SqlParameter> aParameters = new List<SqlParameter>();
            aParameters.Add(new SqlParameter("@UserId", userId));
            //aParameters.Add(new SqlParameter("@LevelUp", levelUp));
            SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetAllTaskByUser", aParameters);

            List<ResultOfTaskViewModel> resultOfTaskVMs = new List<ResultOfTaskViewModel>();
            while (dr.Read())
            {
                ResultOfTaskViewModel resultOfTaskVM = new ResultOfTaskViewModel();
                //resultOfTaskVM.UserId = dr["UserId"].ToString();

                resultOfTaskVM.Action = (long)dr["Action"];
                resultOfTaskVM.Score = (long)dr["Score"];

                resultOfTaskVMs.Add(resultOfTaskVM);
            }

            return resultOfTaskVMs;
        }

        public ResultOfTaskViewModel GetDailyAndMonthlyTaskSumByUser(string userId)
        {
            accessManager.SqlConnectionOpen(DataBase.KinshipDB);

            List<SqlParameter> aParameters = new List<SqlParameter>();
            aParameters.Add(new SqlParameter("@UserId", userId));
            SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetDailyAndMonthlyTaskSumByUser", aParameters);

            List<ResultOfTaskViewModel> resultOfTaskVMs = new List<ResultOfTaskViewModel>();
            ResultOfTaskViewModel resultOfTaskVM = new ResultOfTaskViewModel();
            while (dr.Read())
            {

                //resultOfTaskVM.UserId = dr["UserId"].ToString();

                resultOfTaskVM.Co2 = (decimal)dr["Co2"];
                resultOfTaskVM.Water = (decimal)dr["Water"];
                resultOfTaskVM.Energy = (decimal)dr["Energy"];
                resultOfTaskVM.Kindness = (decimal)dr["Kindness"];

                //resultOfTaskVMs.Add(resultOfTaskVM);
            }

            return resultOfTaskVM;
        }

        public ResultOfTaskViewModel GetResultOfTaskByUser(string userId)
        {
            try
            {

                ResultOfTaskViewModel resultOfTaskVM = new ResultOfTaskViewModel();

                var resultOfTaskVMs = GetAllTaskResult(userId).FirstOrDefault();
                var vm = GetDailyAndMonthlyTaskSumByUser(userId);

                resultOfTaskVM.levlUpStatus = false;

                if (resultOfTaskVMs == null)
                {
                    resultOfTaskVM.Score = 0;
                    resultOfTaskVM.levelUp = 1;

                    return resultOfTaskVM;

                }

                accessManager.SqlConnectionOpen(DataBase.KinshipDB);

                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@UserId", userId));
                //aParameters.Add(new SqlParameter("@LevelUp", levelUp));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetTaskResultByUser", aParameters);


                while (dr.Read())
                {
                    //resultOfTaskVM.UserId = dr["UserId"].ToString();

                    resultOfTaskVM.Action = (long)dr["Action"];
                    resultOfTaskVM.Score = (long)dr["Score"];

                }

                // if task result is null defaul set levelup 1

                var taskResult = GetResultTaskByUserId(userId).LastOrDefault();
                var allNewResults = GetResultTaskByUserId(userId);


                long newAllResultScore = 0;
                foreach (var item in allNewResults)
                {
                    newAllResultScore += item.Score;
                }

                long point = resultOfTaskVM.Score - newAllResultScore;

                if (taskResult == null)
                {
                    TaskResult tr = new TaskResult();

                    tr.Action = resultOfTaskVM.Action;
                    tr.UserId = userId;
                    //tr.BusinessUnitId = dailyTask.BusinessUnitId;
                    tr.Score = resultOfTaskVM.Score;
                    if (tr.Score >= 5)
                    {
                        tr.LevelUp = 2;
                    }

                    resultOfTaskVM.levlUpStatus = true;
                    SaveTaskResult(tr);
                }
                else
                {
                    int preLevel = 0;
                    if (taskResult.LevelUp == 1)
                    {
                        preLevel = 1;
                    }
                    else
                    {
                        preLevel = taskResult.LevelUp - 1;
                    }

                    //var preData = GetResultTaskByUserIdAndLevelUp(userId, preLevel).LastOrDefault();

                    //long newAllResultScore = 0;
                    //foreach (var item in allNewResults)
                    //{
                    //    newAllResultScore += item.Score;
                    //}

                    int level = taskResult.LevelUp;
                    //long point = resultOfTaskVM.Score - newAllResultScore;


                    int pointLevel = LevelCondition(level);

                    if (taskResult.LevelUp == level && point >= pointLevel)
                    {
                        TaskResult tr = new TaskResult();

                        tr.Action = taskResult.Action;
                        tr.UserId = taskResult.UserId;
                        tr.BusinessUnitId = taskResult.BusinessUnitId;
                        tr.Score = point;
                        tr.LevelUp = taskResult.LevelUp + 1;

                        resultOfTaskVM.levlUpStatus = true;

                        SaveTaskResult(tr);

                    }


                }

                var taskResultLevlUp = GetResultTaskByUserId(userId).LastOrDefault();

                //resultOfTaskVM.Score = 0;
                resultOfTaskVM.Score = point;
                resultOfTaskVM.levelUp = taskResultLevlUp.LevelUp;

                resultOfTaskVM.Co2 = vm.Co2;
                resultOfTaskVM.Water = vm.Water;
                resultOfTaskVM.Energy = vm.Energy;
                resultOfTaskVM.Kindness = vm.Kindness;

                return resultOfTaskVM;
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


        public List<TaskResult> GetResultTaskByUserId(string userId)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.KinshipDB);

                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@UserId", userId));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetTaskResultByUserId", aParameters);

                List<TaskResult> taskResults = new List<TaskResult>();


                while (dr.Read())
                {
                    TaskResult taskResult = new TaskResult();
                    taskResult.Id = (long)dr["Id"];
                    taskResult.Score = (long)dr["Score"];
                    taskResult.UserId = dr["UserId"].ToString();
                    taskResult.BusinessUnitId = (long)dr["BusinessUnitId"];
                    taskResult.Action = (long)dr["Action"];
                    taskResult.LevelUp = (int)dr["LevelUp"];

                    taskResults.Add(taskResult);
                }
                return taskResults;
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

        public List<TaskResult> GetResultTaskByUserIdAndLevelUp(string userId, int level)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.KinshipDB);

                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@UserId", userId));
                aParameters.Add(new SqlParameter("@LevelUp", level));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetTaskResultByUserIdAndLevel", aParameters);

                List<TaskResult> taskResults = new List<TaskResult>();


                while (dr.Read())
                {
                    TaskResult taskResult = new TaskResult();
                    taskResult.Id = (long)dr["Id"];
                    taskResult.Score = (long)dr["Score"];
                    taskResult.UserId = dr["UserId"].ToString();
                    taskResult.BusinessUnitId = (long)dr["BusinessUnitId"];
                    taskResult.Action = (long)dr["Action"];
                    taskResult.LevelUp = (int)dr["LevelUp"];

                    taskResults.Add(taskResult);
                }
                return taskResults;
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

        public TaskResult GetResultTaskByUserIdAndId(string userId, long id)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.KinshipDB);

                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@UserId", userId));
                aParameters.Add(new SqlParameter("@Id", id));
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetTaskResultByUserIdAndId", aParameters);

                //List<TaskResult> taskResults = new List<TaskResult>();
                TaskResult taskResult = new TaskResult();

                while (dr.Read())
                {

                    taskResult.Id = (long)dr["Id"];
                    taskResult.Score = (long)dr["Score"];
                    taskResult.UserId = dr["UserId"].ToString();
                    taskResult.BusinessUnitId = (long)dr["BusinessUnitId"];
                    taskResult.Action = (long)dr["Action"];
                    taskResult.LevelUp = (int)dr["LevelUp"];

                    //taskResults.Add(taskResult);
                }
                return taskResult;
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

        public ResultResponse SaveTaskResult(TaskResult taskResult)
        {
            try
            {

                accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                ResultResponse result = new ResultResponse();

                List<SqlParameter> aParameters = new List<SqlParameter>();

                aParameters.Add(new SqlParameter("@CreateDateTime", DateTime.Now));
                aParameters.Add(new SqlParameter("@Score", taskResult.Score));
                aParameters.Add(new SqlParameter("@UserId", taskResult.UserId));
                aParameters.Add(new SqlParameter("@BusinessUnitId", taskResult.BusinessUnitId));
                aParameters.Add(new SqlParameter("@Action", taskResult.Action));
                aParameters.Add(new SqlParameter("@LevelUp", taskResult.LevelUp));

                result.isSuccess = accessManager.SaveData("sp_SaveTaskResult", aParameters);

                result.msg = "Save Task Result Added Successfully";
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


        public ResultResponse UpdateTaskResult(TaskResult taskResult)
        {
            try
            {

                accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                ResultResponse result = new ResultResponse();

                List<SqlParameter> aParameters = new List<SqlParameter>();

                aParameters.Add(new SqlParameter("@Id", taskResult.Id));
                aParameters.Add(new SqlParameter("@CreateDateTime", DateTime.Now));
                aParameters.Add(new SqlParameter("@Score", taskResult.Score));
                aParameters.Add(new SqlParameter("@UserId", taskResult.UserId));
                aParameters.Add(new SqlParameter("@BusinessUnitId", taskResult.BusinessUnitId));
                aParameters.Add(new SqlParameter("@Action", taskResult.Action));
                aParameters.Add(new SqlParameter("@LevelUp", taskResult.LevelUp));

                result.isSuccess = accessManager.SaveData("sp_UpdateTaskResult", aParameters);

                result.msg = "Update Task Result Added Successfully";
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


        #endregion



        #region Excel File Upload

        public ResultResponse ExcelUploadForDailyTaskList()
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                ResultResponse result = new ResultResponse();

                #region Variable Declaration  
                string message = "";
                //HttpResponseMessage ResponseMessage = null;
                var httpRequest = HttpContext.Current.Request;
                DataSet dsexcelRecords = new DataSet();
                IExcelDataReader reader = null;
                HttpPostedFile Inputfile = null;
                Stream FileStream = null;
                #endregion

                #region Save Excel  

                if (httpRequest.Files.Count > 0)
                {
                    Inputfile = httpRequest.Files[0];
                    FileStream = Inputfile.InputStream;

                    if (Inputfile != null && FileStream != null)
                    {
                        if (Inputfile.FileName.EndsWith(".xls"))
                            reader = ExcelReaderFactory.CreateBinaryReader(FileStream);
                        else if (Inputfile.FileName.EndsWith(".xlsx"))
                            reader = ExcelReaderFactory.CreateOpenXmlReader(FileStream);
                        else
                            //message = "The file format is not supported.";
                            result.msg = "The file format is not supported.";

                        dsexcelRecords = reader.AsDataSet();
                        reader.Close();

                        if (dsexcelRecords != null && dsexcelRecords.Tables.Count > 0)
                        {
                            DataTable dtWFX = dsexcelRecords.Tables[0];
                            for (int i = 1; i < dtWFX.Rows.Count; i++)
                            {

                                var fileName = dtWFX.Rows[i][3].ToString();
                                string imagePath = @"D:\PUBLISH PROJECTS\EnergyApi\SustainabilityImages";
                                string imageUrl = @"/SustainabilityImages/" + fileName;


                                DailyTaskList obj = new DailyTaskList();
                                obj.BanglaName = dtWFX.Rows[i][0].ToString();
                                obj.EnglishName = dtWFX.Rows[i][1].ToString();
                                obj.Type = dtWFX.Rows[i][2].ToString();

                                obj.ImageName = dtWFX.Rows[i][3].ToString();
                                //obj.ImagePath = dtWFX.Rows[i][4].ToString();
                                //obj.ImageUrl = dtWFX.Rows[i][5].ToString();
                                obj.ImagePath = imagePath;
                                obj.ImageUrl = imageUrl;

                                obj.InfoLink = dtWFX.Rows[i][4].ToString();
                                var sc = dtWFX.Rows[i][5].ToString();
                                obj.SustainabilityCategoryId = long.Parse(sc);

                                obj.Co2 = dtWFX.Rows[i][6].ToString();
                                obj.Water = dtWFX.Rows[i][7].ToString();
                                obj.Energy = dtWFX.Rows[i][8].ToString();
                                obj.Kindness = dtWFX.Rows[i][9].ToString();

                                //var Co2 = dtWFX.Rows[i][6].ToString();
                                //obj.Co2 = decimal.Parse(Co2);
                                //var Water = dtWFX.Rows[i][7].ToString();
                                //obj.Water = decimal.Parse(Water);
                                //var Energy = dtWFX.Rows[i][8].ToString();
                                //obj.Energy = decimal.Parse(Energy);
                                //var Kindness = dtWFX.Rows[i][9].ToString();
                                //obj.Kindness = decimal.Parse(Kindness);

                                List<SqlParameter> aParameters = new List<SqlParameter>();

                                aParameters.Add(new SqlParameter("@CreateDateTime", DateTime.Now));
                                aParameters.Add(new SqlParameter("@BanglaName", obj.BanglaName));
                                aParameters.Add(new SqlParameter("@EnglishName", obj.EnglishName));
                                aParameters.Add(new SqlParameter("@Type", obj.Type));
                                aParameters.Add(new SqlParameter("@ImageName", obj.ImageName));
                                aParameters.Add(new SqlParameter("@ImagePath", obj.ImagePath));
                                aParameters.Add(new SqlParameter("@ImageUrl", obj.ImageUrl));
                                aParameters.Add(new SqlParameter("@InfoLink", obj.InfoLink));
                                aParameters.Add(new SqlParameter("@SustainabilityCategoryId", obj.SustainabilityCategoryId));

                                aParameters.Add(new SqlParameter("@Co2", obj.Co2));
                                aParameters.Add(new SqlParameter("@Water", obj.Water));
                                aParameters.Add(new SqlParameter("@Energy", obj.Energy));
                                aParameters.Add(new SqlParameter("@Kindness", obj.Kindness));
                                //aParameters.Add(new SqlParameter("@BusinessUnitId", sustainabilitySurvey.BusinessUnitId));


                                result.isSuccess = accessManager.SaveData("sp_UploadDailyTaskList", aParameters);
                            }


                            if (result.isSuccess == true)
                                //message = "The Excel file has been successfully uploaded.";
                                result.msg = "The Excel file has been successfully uploaded.";
                            else
                                //message = "Something Went Wrong!, The Excel file uploaded has fiald.";
                                result.msg = "Something Went Wrong!, The Excel file uploaded has fiald.";
                        }
                        else
                            //message = "Selected file is empty.";
                            result.msg = "Selected file is empty.";
                    }
                    else
                        //message = "Invalid File.";
                        result.msg = "Invalid File.";
                }
                //else
                //    ResponseMessage = Request.CreateResponse(HttpStatusCode.BadRequest);
                //    ResponseMessage = Request.CreateResponse(HttpStatusCode.BadRequest);
                //using (YarnDbContext objEntity = new YarnDbContext())
                //{

                //}
                return result;
                #endregion
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
        public ResultResponse ExcelUploadForDailyTaskListDetails()
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                ResultResponse result = new ResultResponse();

                #region Variable Declaration  
                string message = "";
                var httpRequest = HttpContext.Current.Request;
                DataSet dsexcelRecords = new DataSet();
                IExcelDataReader reader = null;
                HttpPostedFile Inputfile = null;
                Stream FileStream = null;
                #endregion

                #region Save Excel  

                if (httpRequest.Files.Count > 0)
                {
                    Inputfile = httpRequest.Files[0];
                    FileStream = Inputfile.InputStream;

                    if (Inputfile != null && FileStream != null)
                    {
                        if (Inputfile.FileName.EndsWith(".xls"))
                            reader = ExcelReaderFactory.CreateBinaryReader(FileStream);
                        else if (Inputfile.FileName.EndsWith(".xlsx"))
                            reader = ExcelReaderFactory.CreateOpenXmlReader(FileStream);
                        else
                            //message = "The file format is not supported.";
                            result.msg = "The file format is not supported.";

                        dsexcelRecords = reader.AsDataSet();
                        reader.Close();

                        if (dsexcelRecords != null && dsexcelRecords.Tables.Count > 0)
                        {
                            DataTable dtWFX = dsexcelRecords.Tables[0];
                            for (int i = 1; i < dtWFX.Rows.Count; i++)
                            {


                                DailyTaskListDetails obj = new DailyTaskListDetails();
                                var dailyTaskListId = dtWFX.Rows[i][0].ToString();
                                obj.DailyTaskListId = long.Parse(dailyTaskListId);
                                obj.Title = dtWFX.Rows[i][1].ToString();
                                obj.Note = dtWFX.Rows[i][2].ToString();
                                obj.TitleBangla = dtWFX.Rows[i][3].ToString();
                                obj.NoteBangla = dtWFX.Rows[i][4].ToString();

                                List<SqlParameter> aParameters = new List<SqlParameter>();

                                aParameters.Add(new SqlParameter("@CreateDateTime", DateTime.Now));
                                aParameters.Add(new SqlParameter("@DailyTaskListId", obj.DailyTaskListId));
                                aParameters.Add(new SqlParameter("@Title", obj.Title));
                                aParameters.Add(new SqlParameter("@Note", obj.Note));
                                aParameters.Add(new SqlParameter("@TitleBangla", obj.TitleBangla));
                                aParameters.Add(new SqlParameter("@NoteBangla", obj.NoteBangla));

                                bool isLink = true;

                                aParameters.Add(new SqlParameter("@Islink", isLink));

                                //aParameters.Add(new SqlParameter("@BusinessUnitId", sustainabilitySurvey.BusinessUnitId));

                                result.isSuccess = accessManager.SaveData("sp_UploadDailyTaskListDetails", aParameters);
                            }


                            if (result.isSuccess == true)
                                //message = "The Excel file has been successfully uploaded.";
                                result.msg = "The Excel file has been successfully uploaded.";
                            else
                                //message = "Something Went Wrong!, The Excel file uploaded has fiald.";
                                result.msg = "Something Went Wrong!, The Excel file uploaded has fiald.";
                        }
                        else
                            //message = "Selected file is empty.";
                            result.msg = "Selected file is empty.";
                    }
                    else
                        //message = "Invalid File.";
                        result.msg = "Invalid File.";
                }

                return result;
                #endregion
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

        public ResultResponse ExcelUploadForMonthlyTaskList()
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                ResultResponse result = new ResultResponse();

                #region Variable Declaration  
                string message = "";
                //HttpResponseMessage ResponseMessage = null;
                var httpRequest = HttpContext.Current.Request;
                DataSet dsexcelRecords = new DataSet();
                IExcelDataReader reader = null;
                HttpPostedFile Inputfile = null;
                Stream FileStream = null;
                #endregion

                #region Save Excel  

                if (httpRequest.Files.Count > 0)
                {
                    Inputfile = httpRequest.Files[0];
                    FileStream = Inputfile.InputStream;

                    if (Inputfile != null && FileStream != null)
                    {
                        if (Inputfile.FileName.EndsWith(".xls"))
                            reader = ExcelReaderFactory.CreateBinaryReader(FileStream);
                        else if (Inputfile.FileName.EndsWith(".xlsx"))
                            reader = ExcelReaderFactory.CreateOpenXmlReader(FileStream);
                        else
                            //message = "The file format is not supported.";
                            result.msg = "The file format is not supported.";

                        dsexcelRecords = reader.AsDataSet();
                        reader.Close();

                        if (dsexcelRecords != null && dsexcelRecords.Tables.Count > 0)
                        {
                            DataTable dtWFX = dsexcelRecords.Tables[0];
                            for (int i = 1; i < dtWFX.Rows.Count; i++)
                            {
                                var fileName = dtWFX.Rows[i][3].ToString();
                                string imagePath = @"D:\PUBLISH PROJECTS\EnergyApi\SustainabilityImages";
                                string imageUrl = @"/SustainabilityImages/" + fileName;

                                MonthlyTaskList obj = new MonthlyTaskList();
                                obj.BanglaName = dtWFX.Rows[i][0].ToString();
                                obj.EnglishName = dtWFX.Rows[i][1].ToString();
                                obj.Type = dtWFX.Rows[i][2].ToString();

                                obj.ImageName = dtWFX.Rows[i][3].ToString();

                                //obj.ImagePath = dtWFX.Rows[i][4].ToString();
                                //obj.ImageUrl = dtWFX.Rows[i][5].ToString();
                                obj.ImagePath = imagePath;
                                obj.ImageUrl = imageUrl;

                                obj.InfoLink = dtWFX.Rows[i][4].ToString();
                                var sc = dtWFX.Rows[i][5].ToString();
                                obj.SustainabilityCategoryId = long.Parse(sc);

                                obj.Co2 = dtWFX.Rows[i][6].ToString();
                                obj.Water = dtWFX.Rows[i][7].ToString();
                                obj.Energy = dtWFX.Rows[i][8].ToString();
                                obj.Kindness = dtWFX.Rows[i][9].ToString();

                                List<SqlParameter> aParameters = new List<SqlParameter>();

                                aParameters.Add(new SqlParameter("@CreateDateTime", DateTime.Now));
                                aParameters.Add(new SqlParameter("@BanglaName", obj.BanglaName));
                                aParameters.Add(new SqlParameter("@EnglishName", obj.EnglishName));
                                aParameters.Add(new SqlParameter("@Type", obj.Type));
                                aParameters.Add(new SqlParameter("@ImageName", obj.ImageName));
                                aParameters.Add(new SqlParameter("@ImagePath", obj.ImagePath));
                                aParameters.Add(new SqlParameter("@ImageUrl", obj.ImageUrl));
                                aParameters.Add(new SqlParameter("@InfoLink", obj.InfoLink));
                                aParameters.Add(new SqlParameter("@SustainabilityCategoryId", obj.SustainabilityCategoryId));

                                aParameters.Add(new SqlParameter("@Co2", obj.Co2));
                                aParameters.Add(new SqlParameter("@Water", obj.Water));
                                aParameters.Add(new SqlParameter("@Energy", obj.Energy));
                                aParameters.Add(new SqlParameter("@Kindness", obj.Kindness));
                                //aParameters.Add(new SqlParameter("@BusinessUnitId", sustainabilitySurvey.BusinessUnitId));


                                result.isSuccess = accessManager.SaveData("sp_UploadMonthlyTaskList", aParameters);
                            }


                            if (result.isSuccess == true)
                                //message = "The Excel file has been successfully uploaded.";
                                result.msg = "The Excel file has been successfully uploaded.";
                            else
                                //message = "Something Went Wrong!, The Excel file uploaded has fiald.";
                                result.msg = "Something Went Wrong!, The Excel file uploaded has fiald.";
                        }
                        else
                            //message = "Selected file is empty.";
                            result.msg = "Selected file is empty.";
                    }
                    else
                        //message = "Invalid File.";
                        result.msg = "Invalid File.";
                }
                //else
                //    ResponseMessage = Request.CreateResponse(HttpStatusCode.BadRequest);
                //    ResponseMessage = Request.CreateResponse(HttpStatusCode.BadRequest);
                //using (YarnDbContext objEntity = new YarnDbContext())
                //{

                //}
                return result;
                #endregion
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

        public ResultResponse ExcelUploadForMonthlyTaskListDetails()
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                ResultResponse result = new ResultResponse();

                #region Variable Declaration  
                string message = "";
                var httpRequest = HttpContext.Current.Request;
                DataSet dsexcelRecords = new DataSet();
                IExcelDataReader reader = null;
                HttpPostedFile Inputfile = null;
                Stream FileStream = null;
                #endregion

                #region Save Excel  

                if (httpRequest.Files.Count > 0)
                {
                    Inputfile = httpRequest.Files[0];
                    FileStream = Inputfile.InputStream;

                    if (Inputfile != null && FileStream != null)
                    {
                        if (Inputfile.FileName.EndsWith(".xls"))
                            reader = ExcelReaderFactory.CreateBinaryReader(FileStream);
                        else if (Inputfile.FileName.EndsWith(".xlsx"))
                            reader = ExcelReaderFactory.CreateOpenXmlReader(FileStream);
                        else
                            //message = "The file format is not supported.";
                            result.msg = "The file format is not supported.";

                        dsexcelRecords = reader.AsDataSet();
                        reader.Close();

                        if (dsexcelRecords != null && dsexcelRecords.Tables.Count > 0)
                        {
                            DataTable dtWFX = dsexcelRecords.Tables[0];
                            for (int i = 1; i < dtWFX.Rows.Count; i++)
                            {


                                MonthlyTaskListDetails obj = new MonthlyTaskListDetails();
                                var monthlyTaskListId = dtWFX.Rows[i][0].ToString();
                                obj.MonthlyTaskListId = long.Parse(monthlyTaskListId);
                                obj.Title = dtWFX.Rows[i][1].ToString();
                                obj.Note = dtWFX.Rows[i][2].ToString();
                                obj.TitleBangla = dtWFX.Rows[i][3].ToString();
                                obj.NoteBangla = dtWFX.Rows[i][4].ToString();

                                List<SqlParameter> aParameters = new List<SqlParameter>();

                                aParameters.Add(new SqlParameter("@CreateDateTime", DateTime.Now));
                                aParameters.Add(new SqlParameter("@MonthlyTaskListId", obj.MonthlyTaskListId));
                                aParameters.Add(new SqlParameter("@Title", obj.Title));
                                aParameters.Add(new SqlParameter("@Note", obj.Note));
                                aParameters.Add(new SqlParameter("@TitleBangla", obj.TitleBangla));
                                aParameters.Add(new SqlParameter("@NoteBangla", obj.NoteBangla));

                                bool isLink = true;

                                aParameters.Add(new SqlParameter("@Islink", isLink));

                                //aParameters.Add(new SqlParameter("@BusinessUnitId", sustainabilitySurvey.BusinessUnitId));

                                result.isSuccess = accessManager.SaveData("sp_UploadMonthlyTaskListDetails", aParameters);
                            }


                            if (result.isSuccess == true)
                                //message = "The Excel file has been successfully uploaded.";
                                result.msg = "The Excel file has been successfully uploaded.";
                            else
                                //message = "Something Went Wrong!, The Excel file uploaded has fiald.";
                                result.msg = "Something Went Wrong!, The Excel file uploaded has fiald.";
                        }
                        else
                            //message = "Selected file is empty.";
                            result.msg = "Selected file is empty.";
                    }
                    else
                        //message = "Invalid File.";
                        result.msg = "Invalid File.";
                }

                return result;
                #endregion
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

        public ResultResponse ExcelUploadForDailyTips()
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                ResultResponse result = new ResultResponse();

                #region Variable Declaration  
                string message = "";
                //HttpResponseMessage ResponseMessage = null;
                var httpRequest = HttpContext.Current.Request;
                DataSet dsexcelRecords = new DataSet();
                IExcelDataReader reader = null;
                HttpPostedFile Inputfile = null;
                Stream FileStream = null;
                #endregion

                #region Save Excel  

                if (httpRequest.Files.Count > 0)
                {
                    Inputfile = httpRequest.Files[0];
                    FileStream = Inputfile.InputStream;

                    if (Inputfile != null && FileStream != null)
                    {
                        if (Inputfile.FileName.EndsWith(".xls"))
                            reader = ExcelReaderFactory.CreateBinaryReader(FileStream);
                        else if (Inputfile.FileName.EndsWith(".xlsx"))
                            reader = ExcelReaderFactory.CreateOpenXmlReader(FileStream);
                        else
                            //message = "The file format is not supported.";
                            result.msg = "The file format is not supported.";

                        dsexcelRecords = reader.AsDataSet();
                        reader.Close();

                        if (dsexcelRecords != null && dsexcelRecords.Tables.Count > 0)
                        {
                            DataTable dtWFX = dsexcelRecords.Tables[0];
                            for (int i = 1; i < dtWFX.Rows.Count; i++)
                            {


                                DailyTipsTask obj = new DailyTipsTask();
                                obj.OriginalLink = dtWFX.Rows[i][0].ToString();
                                obj.Type = dtWFX.Rows[i][1].ToString();

                                //var sc = dtWFX.Rows[i][7].ToString();
                                //obj.SustainabilityCategoryId = long.Parse(sc);

                                //var tt = "https://www.youtube.com/watch?v=eq_LSOz5IhM";

                                string[] s2 = obj.OriginalLink.Split('=');
                                obj.LinkCode = s2[1];

                                List<SqlParameter> aParameters = new List<SqlParameter>();

                                aParameters.Add(new SqlParameter("@CreateDateTime", DateTime.Now));
                                aParameters.Add(new SqlParameter("@OriginalLink", obj.OriginalLink));
                                aParameters.Add(new SqlParameter("@LinkCode", obj.LinkCode));
                                aParameters.Add(new SqlParameter("@Type", obj.Type));

                                //aParameters.Add(new SqlParameter("@SustainabilityCategoryId", obj.SustainabilityCategoryId));


                                //aParameters.Add(new SqlParameter("@BusinessUnitId", sustainabilitySurvey.BusinessUnitId));


                                result.isSuccess = accessManager.SaveData("sp_UploadDailyTipsTask", aParameters);
                            }


                            if (result.isSuccess == true)
                                //message = "The Excel file has been successfully uploaded.";
                                result.msg = "The Excel file has been successfully uploaded.";
                            else
                                //message = "Something Went Wrong!, The Excel file uploaded has fiald.";
                                result.msg = "Something Went Wrong!, The Excel file uploaded has fiald.";
                        }
                        else
                            //message = "Selected file is empty.";
                            result.msg = "Selected file is empty.";
                    }
                    else
                        //message = "Invalid File.";
                        result.msg = "Invalid File.";
                }
                //else
                //    ResponseMessage = Request.CreateResponse(HttpStatusCode.BadRequest);
                //    ResponseMessage = Request.CreateResponse(HttpStatusCode.BadRequest);
                //using (YarnDbContext objEntity = new YarnDbContext())
                //{

                //}
                return result;
                #endregion
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

        public ResultResponse ExcelUploadForMonthlyTips()
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.KinshipDB);
                ResultResponse result = new ResultResponse();

                #region Variable Declaration  
                string message = "";
                //HttpResponseMessage ResponseMessage = null;
                var httpRequest = HttpContext.Current.Request;
                DataSet dsexcelRecords = new DataSet();
                IExcelDataReader reader = null;
                HttpPostedFile Inputfile = null;
                Stream FileStream = null;
                #endregion

                #region Save Excel  

                if (httpRequest.Files.Count > 0)
                {
                    Inputfile = httpRequest.Files[0];
                    FileStream = Inputfile.InputStream;

                    if (Inputfile != null && FileStream != null)
                    {
                        if (Inputfile.FileName.EndsWith(".xls"))
                            reader = ExcelReaderFactory.CreateBinaryReader(FileStream);
                        else if (Inputfile.FileName.EndsWith(".xlsx"))
                            reader = ExcelReaderFactory.CreateOpenXmlReader(FileStream);
                        else
                            //message = "The file format is not supported.";
                            result.msg = "The file format is not supported.";

                        dsexcelRecords = reader.AsDataSet();
                        reader.Close();

                        if (dsexcelRecords != null && dsexcelRecords.Tables.Count > 0)
                        {
                            DataTable dtWFX = dsexcelRecords.Tables[0];
                            for (int i = 1; i < dtWFX.Rows.Count; i++)
                            {


                                MonthlyTipsTask obj = new MonthlyTipsTask();
                                obj.OriginalLink = dtWFX.Rows[i][0].ToString();
                                obj.Type = dtWFX.Rows[i][1].ToString();

                                //var sc = dtWFX.Rows[i][7].ToString();
                                //obj.SustainabilityCategoryId = long.Parse(sc);

                                //var tt = "https://www.youtube.com/watch?v=eq_LSOz5IhM";

                                string[] s2 = obj.OriginalLink.Split('=');
                                obj.LinkCode = s2[1];

                                List<SqlParameter> aParameters = new List<SqlParameter>();

                                aParameters.Add(new SqlParameter("@CreateDateTime", DateTime.Now));
                                aParameters.Add(new SqlParameter("@OriginalLink", obj.OriginalLink));
                                aParameters.Add(new SqlParameter("@LinkCode", obj.LinkCode));
                                aParameters.Add(new SqlParameter("@Type", obj.Type));

                                //aParameters.Add(new SqlParameter("@SustainabilityCategoryId", obj.SustainabilityCategoryId));


                                //aParameters.Add(new SqlParameter("@BusinessUnitId", sustainabilitySurvey.BusinessUnitId));


                                result.isSuccess = accessManager.SaveData("sp_UploadMonthlyTipsTask", aParameters);
                            }


                            if (result.isSuccess == true)
                                //message = "The Excel file has been successfully uploaded.";
                                result.msg = "The Excel file has been successfully uploaded.";
                            else
                                //message = "Something Went Wrong!, The Excel file uploaded has fiald.";
                                result.msg = "Something Went Wrong!, The Excel file uploaded has fiald.";
                        }
                        else
                            //message = "Selected file is empty.";
                            result.msg = "Selected file is empty.";
                    }
                    else
                        //message = "Invalid File.";
                        result.msg = "Invalid File.";
                }
                //else
                //    ResponseMessage = Request.CreateResponse(HttpStatusCode.BadRequest);
                //    ResponseMessage = Request.CreateResponse(HttpStatusCode.BadRequest);
                //using (YarnDbContext objEntity = new YarnDbContext())
                //{

                //}
                return result;
                #endregion
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

        #endregion


        #region Private
        private void DataValidation(SustainabilitySurvey sustainability)
        {
            sustainability.UserId = string.IsNullOrWhiteSpace(sustainability.UserId) ? throw new ArgumentException("User Id Is Messing Please Provide the Information") : sustainability.UserId;


            //if (sustainability.UserId <= 0) throw new ArgumentException("User Id Is Messing Please Provide the Information");
            if (sustainability.BusinessUnitId <= 0) throw new ArgumentException("BusinessUnit Id Is Messing Please Provide the Information");

        }


        private void DataValidationDailyTask(DailyTask dailyTask)
        {
            ResultResponse result = new ResultResponse();

            //dailyTask.UserId = string.IsNullOrWhiteSpace(dailyTask.UserId) ? throw new ArgumentException("User Id Is Messing Please Provide the Information") : dailyTask.UserId;
            //if (dailyTask.BusinessUnitId <= 0) throw new ArgumentException("BusinessUnit Id Is Messing Please Provide the Information");



            dailyTask.UserId = string.IsNullOrWhiteSpace(dailyTask.UserId) ? throw new ArgumentException("User Id Is Messing Please Provide the Information") : dailyTask.UserId;
            if (dailyTask.BusinessUnitId <= 0) throw new ArgumentException("BusinessUnit Id Is Messing Please Provide the Information");





        }

        private int LevelCondition(int level)
        {
            int point = 0;

            if (level == 2)
            {
                point = 50;
            }
            if (level == 3)
            {
                point = 300;
            }
            if (level == 4)
            {
                point = 1500;
            }
            if (level == 5)
            {
                point = 2500;
            }
            if (level == 6)
            {
                point = 3000;
            }
            if (level == 7)
            {
                point = 3500;
            }
            if (level == 8)
            {
                point = 3600;
            }
            if (level == 9)
            {
                point = 3700;
            }
            if (level == 10)
            {
                point = 3800;
            }
            if (level == 11)
            {
                point = 3900;
            }
            if (level == 12)
            {
                point = 4000;
            }
            if (level == 13)
            {
                point = 4050;
            }
            if (level == 14)
            {
                point = 4100;
            }
            if (level == 15)
            {
                point = 4150;
            }

            return point;
        }
        #endregion

        #region 
        #endregion



    }
}
