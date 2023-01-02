using QMSWebAPI.Models.AttendanceModels;
using QMSWebAPI.Utilities;
using SQIndustryThree.DataManager;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace QMSWebAPI.DAL
{
    public class AttendanceDAL
    {

        public IEnumerable<AttendanceInfoModel> RetriveDataForReportModel(string EmpoyeeCode, string Date)
        {
            IEnumerable<AttendanceInfoModel> attendModel;
            List<AttendanceInfoModel> attendlist = new List<AttendanceInfoModel>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(DBConnection.GetConnectionStringKinship()))
                {
                    if (cnn.State == 0)
                    {
                        cnn.Open();
                    }
                    SqlCommand cmd = new SqlCommand("sp_GetAttendanceInfoFromKormee", cnn)
                    {
                        CommandTimeout = 12000,
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add("@EmpoyeeCode", SqlDbType.NVarChar).Value = EmpoyeeCode;
                    cmd.Parameters.Add("@Date", SqlDbType.NVarChar).Value = Date;
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        AttendanceInfoModel reportModel = new AttendanceInfoModel()
                        {
                            EmployeeCode = dr["EmployeeCode"].ToString(),
                            EmployeeName = dr["EmployeeName"].ToString(),
                            DOJ = dr["DOJ"].ToString(),
                            Designation = dr["Designation"].ToString(),
                            LineInfo = dr["LineInfo"].ToString(),
                            SectionInfo = dr["SectionInfo"].ToString(),
                            Unit = dr["Unit"].ToString(),
                            WorkDate = dr["WorkDate"].ToString(),
                            DayName = dr["DayName"].ToString(),
                            DayStatus = dr["DayStatus"].ToString(),
                            ShiftID = dr["ShiftID"].ToString(),
                            ShiftInTime = dr["ShiftInTime"].ToString(),
                            ShiftOutTime = dr["ShiftOutTime"].ToString(),
                            InTime = dr["InTime"].ToString(),
                            OutTime = dr["OutTime"].ToString(),
                            RequiredHour = Convert.ToInt32(dr["RequiredHour"]),
                            PayHour = Convert.ToDouble(dr["PayHour"]),
                            DifferenceHour = dr["DifferenceHour"].ToString(),
                            Remarks = dr["Remarks"].ToString(),

                        };
                        attendlist.Add(reportModel);
                    }
                    cnn.Close();
                }
                attendModel = attendlist;

                return attendModel;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {

            }

        }


    }
}
