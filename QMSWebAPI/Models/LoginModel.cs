using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QMSWebAPI.Models
{
    public class LoginModel
    {
        public int UserId
        {
            get;
            set;
        }
        public string SQId
        {
            get;
            set;
        }

        public int BusinessUnitId
        {
            get;
            set;
        }

        public DateTime CreateDate
        {
            get;
            set;
        }

        public string DisplayName
        {
            get;
            set;
        }

      
        public int IsActive
        {
            get;
            set;
        }

       

        public string Password
        {
            get;
            set;
        }

        public int ProductionUnitId
        {
            get;
            set;
        }

        public DateTime UpdateDate
        {
            get;
            set;
        }

        public string UserName
        {
            get;
            set;
        }
        public bool IsSuccess
        {
            get;
            set;
        }
        public string Email
        {
            get;
            set;
        }
        public LoginModel()
        {
        }
    }
}