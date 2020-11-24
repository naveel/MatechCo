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
                    return JsonConvert.DeserializeObject<vu_users>(HttpContext.Current.Session["App"].ToString());
                }
            }
            set
            {

                HttpContext.Current.Session["App"] = JsonConvert.SerializeObject(value).ToString();
            }
        }
        public object UserAccountObj { get; set; }
    }
}