using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QMSWebAPI.Models
{
    public class BaseResultResponse
    {
        public bool isSuccess { get; set; }
        public string msg { get; set; }
        public string status { get; set; }
        public dynamic data { get; set; }
        //public List<T> datas { get; set; }
        
    }
}