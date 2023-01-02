using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QMSWebAPI.Models;
using SQIndustryThree.DataManager;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QMSWebAPI.DAL
{
    public class FootballPredictionDAL
    {
        DataAccessManager accessManager = new DataAccessManager();

        public List<Fixtures> GetTodayMatch()
        {
            try
            {
                List<Fixtures> reportlist = new List<Fixtures>();
                accessManager.SqlConnectionOpen(DataBase.FootballPrediction); //work incentive         
                
                
                List<SqlParameter> aParameters = new List<SqlParameter>();
              //  aParameters.Add(new SqlParameter("@MatchDate", MatchDate));

                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetTodayMatch", aParameters);
                while (dr.Read())
                {
                    Fixtures fixtureseModel = new Fixtures();
                    fixtureseModel.Id = (int)dr["Id"];
                    fixtureseModel.Home = dr["Home"].ToString();
                    fixtureseModel.Away = dr["Away"].ToString();
                    fixtureseModel.Matchs = dr["Matchs"].ToString();
                    fixtureseModel.MatchDate = dr["MatchDate"].ToString();
                    fixtureseModel.MatchTime = dr["MatchTime"].ToString();
                    fixtureseModel.Venue = dr["Venue"].ToString();
                    fixtureseModel.TeamGroup = dr["TeamGroup"].ToString();
                    fixtureseModel.HomeShort = dr["HomeShort"].ToString();
                    fixtureseModel.AwayShort = dr["AwayShort"].ToString();
                    fixtureseModel.HomeImageUrl = dr["HomeImageUrl"].ToString();
                    fixtureseModel.AwayImageUrl = dr["AwayImageUrl"].ToString();                    
                    fixtureseModel.MatchFullDate = Convert.ToDateTime( dr["MatchFullDate"].ToString());
                    fixtureseModel.Status = false;

                    reportlist.Add(fixtureseModel);
                }
                return reportlist;
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


        public FixturesObj GetTodayMatchX()
        {
            try
            {

                FixturesObj reportObj = new FixturesObj();


                List<Fixtures> reportlist = new List<Fixtures>();
                accessManager.SqlConnectionOpen(DataBase.FootballPrediction); //work incentive         


                List<SqlParameter> aParameters = new List<SqlParameter>();
                //  aParameters.Add(new SqlParameter("@MatchDate", MatchDate));

                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetTodayMatch", aParameters);
                while (dr.Read())
                {
                    Fixtures fixtureseModel = new Fixtures();
                    fixtureseModel.Id = (int)dr["Id"];
                    fixtureseModel.Home = dr["Home"].ToString();
                    fixtureseModel.Away = dr["Away"].ToString();
                    fixtureseModel.Matchs = dr["Matchs"].ToString();
                    fixtureseModel.MatchDate = dr["MatchDate"].ToString();
                    fixtureseModel.MatchTime = dr["MatchTime"].ToString();
                    fixtureseModel.Venue = dr["Venue"].ToString();
                    fixtureseModel.TeamGroup = dr["TeamGroup"].ToString();
                    fixtureseModel.HomeShort = dr["HomeShort"].ToString();
                    fixtureseModel.AwayShort = dr["AwayShort"].ToString();
                    fixtureseModel.HomeImageUrl = dr["HomeImageUrl"].ToString();
                    fixtureseModel.AwayImageUrl = dr["AwayImageUrl"].ToString();
                    fixtureseModel.MatchFullDate = Convert.ToDateTime(dr["MatchFullDate"].ToString());
                    fixtureseModel.Status = false;


                    reportlist.Add(fixtureseModel);
                    reportObj.FixturesList = reportlist;

                }


                return reportObj;
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

        public List<Quiz> GetAllQuiz(string MatchDate)
        {
            try
            {
                List<Quiz> quizlist = new List<Quiz>();
                accessManager.SqlConnectionOpen(DataBase.FootballPrediction); //work incentive



                List<SqlParameter> aParameters = new List<SqlParameter>();
                  aParameters.Add(new SqlParameter("@MatchDate", MatchDate));

                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetAllQuiz", aParameters);
                while (dr.Read())
                {
                    Quiz fixtureseModel = new Quiz();
                    fixtureseModel.Id = (int)dr["Id"];
                   // fixtureseModel.FixtureId = (int)dr["FixtureId"];
                    fixtureseModel.SLNo = (int)dr["SLNo"];
                    fixtureseModel.QuizName = dr["QuizName"].ToString();
                    fixtureseModel.Score = (int)dr["Score"];                   
                    fixtureseModel.AvailableTime = Convert.ToDateTime(dr["AvailableTime"].ToString());
                    
                    
                    quizlist.Add(fixtureseModel);
                }
                return quizlist;
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



        public List<Quiz> GetAllQuizOneTime()
        {
            try
            {
                List<Quiz> quizlist = new List<Quiz>();
                //List<QuizAsnList> QuizAsnlist = new List<QuizAsnList>();
                accessManager.SqlConnectionOpen(DataBase.FootballPrediction); //work incentive



                List<SqlParameter> aParameters = new List<SqlParameter>();
                //aParameters.Add(new SqlParameter("@MatchDate", MatchDate));

                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetAllQuizOneTine", aParameters);
               // SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetAllQuizOptionList", aParameters);
                while (dr.Read())
                {
                    Quiz fixtureseModel = new Quiz();
                    //QuizAsnList QuizAsnobj = new QuizAsnList();
                    fixtureseModel.Id = (int)dr["Id"];
                    //fixtureseModel.FixtureId = (int)dr["FixtureId"];
                    fixtureseModel.SLNo = (int)dr["SLNo"];
                    fixtureseModel.QuizName = dr["QuizName"].ToString();
                    fixtureseModel.Score = (int)dr["Score"];
                    fixtureseModel.AvailableTime = Convert.ToDateTime(dr["AvailableTime"].ToString());


          

                    quizlist.Add(fixtureseModel);

                    //var QuizAsnList = GetAllQuizAsnList(fixtureseModel.Id);

                    //foreach (QuizAsnList item in QuizAsnList)
                    //{
                    //    fixtureseModel.FixturesList.Add(new QuizAsnList() { Id = item.Id, SLNo = item.SLNo, QuizId = item.QuizId, PlayerCountry = item.PlayerCountry, PlayerName = item.PlayerName });
                    //}

                    


                }






                return quizlist;
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

        public QuizList GetAllQuizOneTimeOption()
        {
            try
            {
                List<Quiz> quizlist = new List<Quiz>();
                QuizList fixtureseModel = new QuizList();
                //List<QuizAsnList> QuizAsnlist = new List<QuizAsnList>();
                accessManager.SqlConnectionOpen(DataBase.FootballPrediction); //work incentive



                List<SqlParameter> aParameters = new List<SqlParameter>();
                //aParameters.Add(new SqlParameter("@MatchDate", MatchDate));

                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetAllQuizOneTine", aParameters);
                // SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetAllQuizOptionList", aParameters);
            
                    

                    var chimpionList = GetAllQuizAsnList(6);

                    foreach (QuizAsnList item in chimpionList)
                    {
                        fixtureseModel.ChappionList.Add(new QuizAsnList() { Id = item.Id, SLNo = item.SLNo, QuizId = item.QuizId, PlayerCountry = item.PlayerCountry, PlayerName = item.PlayerName });
                    }


                var GoldenBallList = GetAllQuizAsnList(4);

                foreach (QuizAsnList item in GoldenBallList)
                {
                    fixtureseModel.GoldenBollList.Add(new QuizAsnList() { Id = item.Id, SLNo = item.SLNo, QuizId = item.QuizId, PlayerCountry = item.PlayerCountry, PlayerName = item.PlayerName });
                }


                var GoldenBootList = GetAllQuizAsnList(3);

                foreach (QuizAsnList item in GoldenBootList)
                {
                    fixtureseModel.GoldenBootlList.Add(new QuizAsnList() { Id = item.Id, SLNo = item.SLNo, QuizId = item.QuizId, PlayerCountry = item.PlayerCountry, PlayerName = item.PlayerName });
                }

                var GoldenGlovesList = GetAllQuizAsnList(5);

                foreach (QuizAsnList item in GoldenGlovesList)
                {
                    fixtureseModel.GoldenGloveslList.Add(new QuizAsnList() { Id = item.Id, SLNo = item.SLNo, QuizId = item.QuizId, PlayerCountry = item.PlayerCountry, PlayerName = item.PlayerName });
                }




                return fixtureseModel;
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
        public List<QuizAsnList> GetAllQuizAsnList(int QuizId)
        {
            try
            {
                //List<Quiz> quizlist = new List<Quiz>();
                List<QuizAsnList> QuizAsnlist = new List<QuizAsnList>();
                accessManager.SqlConnectionOpen(DataBase.FootballPrediction); //work incentive



                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@MatchDate", QuizId));

                //SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetAllQuizOneTine", aParameters);
                 SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetAllQuizOptionList", aParameters);
                while (dr.Read())
                {
                    QuizAsnList fixtureseModel = new QuizAsnList();
                    //QuizAsnList QuizAsnobj = new QuizAsnList();
                    fixtureseModel.Id = (int)dr["Id"];
                    fixtureseModel.QuizId = (int)dr["QuizId"];
                    fixtureseModel.SLNo = (int)dr["SLNo"];
                    fixtureseModel.PlayerName = dr["PlayerName"].ToString();
                    fixtureseModel.PlayerCountry = dr["PlayerCountry"].ToString();
                    //fixtureseModel.AvailableTime = Convert.ToDateTime(dr["AvailableTime"].ToString());

                    // QuizAsnobj.PlayerName = dr["PlayerName"].ToString();
                    //fixtureseModel.FixturesList.Add(dr["PlayerName"].ToString());



                    QuizAsnlist.Add(fixtureseModel);
                    // QuizAsnlist.Add(QuizAsnobj);

                    //  var subList = quizlist.Where(p => p.Id == fixtureseModel.Id);

                    ////foreach (List<fixtureseModel.FixturesList> newList in subList)
                    ////{
                    //    foreach (var item in subList)
                    //    {
                    //    List<QuizAsnList> bookList = new List<QuizAsnList>();

                    //    //  fixtureseModel.FixturesList.Add(item.QuizName);

                    //    // List<Book> bookList = new List<Book>()
                    //    bookList.Add(item.QuizName.ToList());



                    //}


                    //List<string> names = new List<string>();
                    //foreach (var group in subList)
                    //{
                    //    names.Add(group.QuizName);
                    //}


                    // }

                    // This will filter ints that are not > 7 out of the list; Where returns an
                    // IEnumerable<T>, so call ToList to convert back to a List<T>.
                    // List<int> filteredList = myList.Where(x => x > 7).ToList();


                }




                //List<List<string>> myList = new List<List<string>>();
                //myList.Add(new List<string> { "a", "b" });
                //myList.Add(new List<string> { "c", "d", "e" });
                //myList.Add(new List<string> { "qwerty", "asdf", "zxcv" });
                //myList.Add(new List<string> { "a", "b" });

                //// To iterate over it.
                //foreach (List<string> subList in quizlist)
                //{
                //    foreach (string item in subList)
                //    {
                //        Console.WriteLine(item);
                //    }
                //}



                return QuizAsnlist;
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

        public QuizAsnList GetAllQuizAsnObj(int QuizId)
        {
            try
            {
                //List<Quiz> quizlist = new List<Quiz>();
               QuizAsnList QuizAsnlist = new QuizAsnList();
                accessManager.SqlConnectionOpen(DataBase.FootballPrediction); //work incentive



                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@MatchDate", QuizId));

                //SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetAllQuizOneTine", aParameters);
                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetAllQuizOptionList", aParameters);
                while (dr.Read())
                {
                    QuizAsnList fixtureseModel = new QuizAsnList();
                    //QuizAsnList QuizAsnobj = new QuizAsnList();
                    fixtureseModel.Id = (int)dr["Id"];
                    fixtureseModel.QuizId = (int)dr["QuizId"];
                    fixtureseModel.SLNo = (int)dr["SLNo"];
                    fixtureseModel.PlayerName = dr["PlayerName"].ToString();
                    fixtureseModel.PlayerCountry = dr["PlayerCountry"].ToString();
                    //fixtureseModel.AvailableTime = Convert.ToDateTime(dr["AvailableTime"].ToString());

                    // QuizAsnobj.PlayerName = dr["PlayerName"].ToString();
                    //fixtureseModel.FixturesList.Add(dr["PlayerName"].ToString());



                    //QuizAsnlist.Add(fixtureseModel);
                    // QuizAsnlist.Add(QuizAsnobj);

                    //  var subList = quizlist.Where(p => p.Id == fixtureseModel.Id);

                    ////foreach (List<fixtureseModel.FixturesList> newList in subList)
                    ////{
                    //    foreach (var item in subList)
                    //    {
                    //    List<QuizAsnList> bookList = new List<QuizAsnList>();

                    //    //  fixtureseModel.FixturesList.Add(item.QuizName);

                    //    // List<Book> bookList = new List<Book>()
                    //    bookList.Add(item.QuizName.ToList());



                    //}


                    //List<string> names = new List<string>();
                    //foreach (var group in subList)
                    //{
                    //    names.Add(group.QuizName);
                    //}


                    // }

                    // This will filter ints that are not > 7 out of the list; Where returns an
                    // IEnumerable<T>, so call ToList to convert back to a List<T>.
                    // List<int> filteredList = myList.Where(x => x > 7).ToList();


                }




                //List<List<string>> myList = new List<List<string>>();
                //myList.Add(new List<string> { "a", "b" });
                //myList.Add(new List<string> { "c", "d", "e" });
                //myList.Add(new List<string> { "qwerty", "asdf", "zxcv" });
                //myList.Add(new List<string> { "a", "b" });

                //// To iterate over it.
                //foreach (List<string> subList in quizlist)
                //{
                //    foreach (string item in subList)
                //    {
                //        Console.WriteLine(item);
                //    }
                //}



                return QuizAsnlist;
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


        public ResultResponse SaveToQuizParticipantMaster(string UserId, string MatchDate, int FixtureId, int QuizIdWin,string QuizAnswearWin,int QuizIdScore,int Home, int Away)
        {
            try
            {

               //string matchTime = GetFixtureIdTime(12);

               // DateTime wewe = Convert.ToDateTime(matchTime);

               // string datetimeNow = DateTime.Now.ToString();

               // if (matchTime == "1:00 AM")
               // {

               //     var datetime = DateTime.Now;
               //     int CurrenYear = datetime.Year;
               //     int CurrenMonth = datetime.Month;
               //     int CurrenDay = datetime.Day;
               //     int CurrenHour = datetime.Hour;
               //     int CurrenMinute = datetime.Minute;
               //     //int CurrenDay = datetime.Day;

               //     int FixtureYear = datetime.Year;
               //     int FixtureMonth = datetime.Month;
               //     int FixtureDay = datetime.Day +1;
               //     int FixtureHour = datetime.Hour;
               //     int FixtureMinute = datetime.Minute;

               // }
               // else
               // {
               //     var datetime = DateTime.Now;
               //     int CurrenYear = datetime.Year;
               //     int CurrenMonth = datetime.Month;
               //     int CurrenDay = datetime.Day;
               //     int CurrenHour = datetime.Hour;
               //     int CurrenMinute = datetime.Minute;
               //     //int CurrenDay = datetime.Day;

               //     int FixtureYear = datetime.Year;
               //     int FixtureMonth = datetime.Month;
               //     int FixtureDay = datetime.Day;
               //     int FixtureHour = datetime.Hour;
               //     int FixtureMinute = datetime.Minute;

               // }

               // //var datetime = DateTime.Now;
               // //int CurrenYear = datetime.Year;
               // //int CurrenMonth = datetime.Month;
               // //int CurrenDay = datetime.Day;
               // //int CurrenHour = datetime.Hour;
               // //int CurrenMinute = datetime.Minute;
               // ////int CurrenDay = datetime.Day;

               // //int FixtureYear = datetime.Year;
               // //int FixtureMonth = datetime.Month;
               // //int FixtureDay = datetime.Day;
               // //int FixtureHour = datetime.Hour;
               // //int FixtureMinute = datetime.Minute;


               // //DateTime Currendate1 = new DateTime(CurrenYear, CurrenMonth, CurrenDay, CurrenHour, CurrenMinute, 0);
               // //DateTime Fixturedate2 = new DateTime(FixtureYear, FixtureMonth, FixtureDay, FixtureHour, FixtureMinute, 0);
               // //int result = DateTime.Compare(Currendate1, Fixturedate2);

               // DateTime date1 = new DateTime(2009, 8, 2, 5, 45, 0);
               // DateTime date2 = new DateTime(2009, 8, 2, 7, 0, 0);

               // if (date1 > date2)
               // {

               // }
               // else
               // {

               // }


               // int resultTime = DateTime.Compare(date1, date2);
               // string relationship;

               // if (resultTime < 0)
               //     relationship = "is earlier than";
               // else if (resultTime == 0)
               //     relationship = "is the same time as";
               // else
               //     relationship = "is later than";


                accessManager.SqlConnectionOpen(DataBase.FootballPrediction);
                ResultResponse result = new ResultResponse();
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@UserId", UserId));
                aParameters.Add(new SqlParameter("@MatchDate", MatchDate));
                aParameters.Add(new SqlParameter("@FixtureId", FixtureId));
                aParameters.Add(new SqlParameter("@QuizIdWin", QuizIdWin));
                aParameters.Add(new SqlParameter("@QuizAnswearWin", QuizAnswearWin));
                aParameters.Add(new SqlParameter("@QuizIdScore", QuizIdScore));
                aParameters.Add(new SqlParameter("@Home", Home));
                aParameters.Add(new SqlParameter("@Away", Away));
              
                result.pk = accessManager.SaveDataReturnPrimaryKey("sp_SaveQuizParticipantMaster", aParameters);
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
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }


        public ResultResponse SaveToQuizParticipantOneTimeMaster(string UserId, int QuizNameChampionId, string QuizAnswearChampion, int QuizNameBallId, string QuizAnswearBall, int QuizNameBootId, string QuizAnswearBoot, int QuizNameGlovesId, string QuizAnswearGloves)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.FootballPrediction);
                ResultResponse result = new ResultResponse();
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@UserId", UserId));
                aParameters.Add(new SqlParameter("@QuizNameChampionId", QuizNameChampionId));
                aParameters.Add(new SqlParameter("@QuizAnswearChampion", QuizAnswearChampion));
                aParameters.Add(new SqlParameter("@QuizNameBallId", QuizNameBallId));
                aParameters.Add(new SqlParameter("@QuizAnswearBall", QuizAnswearBall));
                aParameters.Add(new SqlParameter("@QuizNameBootId", QuizNameBootId));
                aParameters.Add(new SqlParameter("@QuizAnswearBoot", QuizAnswearBoot));
                aParameters.Add(new SqlParameter("@QuizNameGlovesId", QuizNameGlovesId));
                aParameters.Add(new SqlParameter("@QuizAnswearGloves", QuizAnswearGloves));
           

                result.pk = accessManager.SaveDataReturnPrimaryKey("sp_SaveQuizParticipantOneTimeMaster", aParameters);
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
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }
        }

        public ResultResponse SaveToChangePassword(string UserId, string PassWord, string NewPassWord)
        {
            try
            {
                accessManager.SqlConnectionOpen(DataBase.FootballPrediction);
                ResultResponse result = new ResultResponse();
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@UserId", UserId));
                aParameters.Add(new SqlParameter("@PassWord", PassWord));
                aParameters.Add(new SqlParameter("@NewPassWord", NewPassWord));
          


                result.pk = accessManager.SaveDataReturnPrimaryKey("sp_SaveChangePassword", aParameters);
                if (result.pk > 0)
                {
                    result.msg = "Data Save Successfully";
                    result.isSuccess = true;
                }
                else
                {
                    result.msg = "Password successful changed ";
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
                accessManager.SqlConnectionOpen(DataBase.FootballPrediction);
                QuizParticipant qMSMaster = JsonConvert.DeserializeObject<QuizParticipant>(jObject.ToString());
                ResultResponse result = new ResultResponse();
                result.msg = "Data Save Successful";
                result.isSuccess = true;
                foreach (var item in qMSMaster.QmsDetailsInformation)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@UserId", qMSMaster.UserId));
                    aParameters.Add(new SqlParameter("@MatchDate", qMSMaster.FixtureId));
                    aParameters.Add(new SqlParameter("@FixtureId", qMSMaster.FixtureId));
                    //aParameters.Add(new SqlParameter("@questionId", item.QuizIdWin));
                    //aParameters.Add(new SqlParameter("@questionId", item.QuizIdScore));
                    //aParameters.Add(new SqlParameter("@questionId", item.QuizAnswearWin));
                   // aParameters.Add(new SqlParameter("@answer", item.QuizAnswear));
                    aParameters.Add(new SqlParameter("@QuizIdWin", qMSMaster.QuizIdWin));
                    aParameters.Add(new SqlParameter("@QuizAnswearWin", qMSMaster.QuizAnswearWin));
                    aParameters.Add(new SqlParameter("@QuizIdScore", qMSMaster.QuizIdScore));
                    aParameters.Add(new SqlParameter("@Home", qMSMaster.QuizScoreHome));
                    aParameters.Add(new SqlParameter("@Away", qMSMaster.QuizScoreAway));
                
                    SqlDataReader sqlData = accessManager.GetSqlDataReader("sp_SaveQuizParticipantMaster", aParameters);
                    while (sqlData.Read())
                    {
                        result.pk += (int)sqlData["ROWADDED"];
                    }
                    sqlData.Close();
                }
                if (result.pk <= 0) { result.msg = "Data Save Successful"; }
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



        public List<ScoringPolicyModel> GetAllScoringPolicy()
        {
            try
            {
                List<ScoringPolicyModel> quizlist = new List<ScoringPolicyModel>();
                //List<QuizAsnList> QuizAsnlist = new List<QuizAsnList>();
                accessManager.SqlConnectionOpen(DataBase.FootballPrediction); //work incentive



                List<SqlParameter> aParameters = new List<SqlParameter>();
                //aParameters.Add(new SqlParameter("@MatchDate", MatchDate));

                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetAllScoringPolicy", aParameters);
                // SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetAllQuizOptionList", aParameters);
                while (dr.Read())
                {
                    ScoringPolicyModel fixtureseModel = new ScoringPolicyModel();                  
                    fixtureseModel.Id = (int)dr["Id"];                 
                    fixtureseModel.PolicyName = dr["PolicyName"].ToString();
                    fixtureseModel.Point = (int)dr["Point"];
                

                    quizlist.Add(fixtureseModel);

                    //var QuizAsnList = GetAllQuizAsnList(fixtureseModel.Id);

                    //foreach (QuizAsnList item in QuizAsnList)
                    //{
                    //    fixtureseModel.FixturesList.Add(new QuizAsnList() { Id = item.Id, SLNo = item.SLNo, QuizId = item.QuizId, PlayerCountry = item.PlayerCountry, PlayerName = item.PlayerName });
                    //}




                }



                return quizlist;
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

        public List<PriceTable> GetAllGiftPolicy()
        {
            try
            {
                List<PriceTable> quizlist = new List<PriceTable>();
                //List<QuizAsnList> QuizAsnlist = new List<QuizAsnList>();
                accessManager.SqlConnectionOpen(DataBase.FootballPrediction); //work incentive



                List<SqlParameter> aParameters = new List<SqlParameter>();
                //aParameters.Add(new SqlParameter("@MatchDate", MatchDate));

                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetAllPriceTable", aParameters);
                // SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetAllQuizOptionList", aParameters);
                while (dr.Read())
                {
                    PriceTable fixtureseModel = new PriceTable();
                    fixtureseModel.Id = (int)dr["Id"];
                    fixtureseModel.PlaceName = dr["PlaceName"].ToString();
                    fixtureseModel.PrizeName = dr["PrizeName"].ToString();


                    quizlist.Add(fixtureseModel);

                    

                }



                return quizlist;
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



        public List<LeaderDashBoardModel> GetAllLeaderDashBoardData()
        {
            try
            {
                List<LeaderDashBoardModel> quizlist = new List<LeaderDashBoardModel>();
                //List<QuizAsnList> QuizAsnlist = new List<QuizAsnList>();
                accessManager.SqlConnectionOpen(DataBase.FootballPrediction); //work incentive



                List<SqlParameter> aParameters = new List<SqlParameter>();
                //aParameters.Add(new SqlParameter("@MatchDate", MatchDate));

                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetLeaderBoard", aParameters);
                // SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetAllQuizOptionList", aParameters);
                while (dr.Read())
                {
                    LeaderDashBoardModel fixtureseModel = new LeaderDashBoardModel();
                    //fixtureseModel.Id = (int)dr["Id"];
                    fixtureseModel.Name = dr["Name"].ToString();
                    fixtureseModel.Department = dr["Department"].ToString();
                    fixtureseModel.Designation = dr["Designation"].ToString();
                    fixtureseModel.Cluster = dr["Cluster"].ToString();
                    fixtureseModel.Score = dr["Score"].ToString();


                    quizlist.Add(fixtureseModel);



                }



                return quizlist;
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


        public ScoreByUserId GetAllScoreByUserId(string UserId)
        {
            try
            {
                ScoreByUserId fixtureseModel = new ScoreByUserId();
                List<ScoreByUserId> quizlist = new List<ScoreByUserId>();
                //List<QuizAsnList> QuizAsnlist = new List<QuizAsnList>();
                accessManager.SqlConnectionOpen(DataBase.FootballPrediction); //work incentive



                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@UserId", UserId));

                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetTotalScore", aParameters);
                // SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetAllQuizOptionList", aParameters);
                while (dr.Read())
                {
                 
                    fixtureseModel.TotalScore = (int)dr["TotalScore"];
                   

                    //quizlist.Add(fixtureseModel);



                }



                return fixtureseModel;
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



        public string GetFixtureIdTime(int FixtureId)
        {
            try
            {

                string time = "";
                FixtureTime fixtureseModel = new FixtureTime();
                List<FixtureTime> quizlist = new List<FixtureTime>();
                //List<QuizAsnList> QuizAsnlist = new List<QuizAsnList>();
                accessManager.SqlConnectionOpen(DataBase.FootballPrediction); //work incentive



                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@FixtureId", FixtureId));

                SqlDataReader dr = accessManager.GetSqlDataReader("sp_GetFixturesTime", aParameters);

                while (dr.Read())
                {

                    time = dr["MatchTime"].ToString();

                }



                return time;
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

    }
}