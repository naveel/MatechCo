using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Matechco.Models
{
    public class ApplicationSession
    {
        public static vu_users Session
        {


            get
            {
                if (HttpContext.Current.Session["App"] == null)
                {
                    return null;
                }
                else
                {
                    return HttpContext.Current.Session["App"] as vu_users;
                }
            }
            set
            {

                HttpContext.Current.Session["App"] = JsonConvert.SerializeObject(value).ToString();
            }
        }
        public string Token { get; set; }
    }
}