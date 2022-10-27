using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QMSWebAPI.Models
{
    public class CategoryModel
    {
    
        public int BusinessUnitId { get; set; }
        public int CategoryId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public string SupervisorId { get; set; }
    }
}