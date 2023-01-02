using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QMSWebAPI.Models.AttendanceModels
{
    public class AttendanceInfoModel
    {

        public long ID
        {
            get;
            set;
        }

        public string EmployeeCode
        {
            get;
            set;
        }

        public string EmployeeName
        {
            get;
            set;
        }

        public string DOJ
        {
            get;
            set;
        }

        public string Designation
        {
            get;
            set;
        }

        public string LineInfo
        {
            get;
            set;
        }

        public string SectionInfo
        {
            get;
            set;
        }

        public string Unit
        {
            get;
            set;
        }

        public string WorkDate
        {
            get;
            set;
        }

        public string DayName
        {
            get;
            set;
        }

        public string DayStatus
        {
            get;
            set;
        }

        public string ShiftID
        {
            get;
            set;
        }

        public string ShiftInTime
        {
            get;
            set;
        }

        public string ShiftOutTime
        {
            get;
            set;
        }

        public string InTime
        {
            get;
            set;
        }

        public string OutTime
        {
            get;
            set;
        }

        public int RequiredHour
        {
            get;
            set;
        }

        public double PayHour
        {
            get;
            set;
        }

        public string DifferenceHour
        {
            get;
            set;
        }

        public string Remarks
        {
            get;
            set;
        }

        public DateTime UpdateDate
        {
            get;
            set;
        }

        public AttendanceInfoModel()
        {
        }
    }
}