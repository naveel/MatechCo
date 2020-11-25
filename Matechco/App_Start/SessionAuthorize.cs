using Matechco.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Matechco.App_Start
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class SessionAuthorize : AuthorizeAttribute
    {
       
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            
            return httpContext.Session["App"] != null;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {

            string logon_url = HttpContext.Current.Request.Url.Host + ":"+HttpContext.Current.Request.Url.Port+"/Account/Index";
            
            if (ApplicationSession.Session != null)// ApplicationSession.Session.UserAccountDetailObj.Count > 0)
            {
                filterContext.Result = new RedirectResult(System.Web.HttpContext.Current.Request.UrlReferrer.OriginalString.ToString());
            }
            else
            {
                //LOCAL
                filterContext.Result = new RedirectResult(logon_url);
            }
        }
        

    }
}