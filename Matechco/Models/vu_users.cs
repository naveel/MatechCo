using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Matechco.Models
{
    public class vu_users
    {
        public int user_sk { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        
        public DateTime? updated_on { get; set; }
        public int? updated_by { get; set; }
        public DateTime? created_on { get; set; }
        public int? created_by { get; set; }
    }
}