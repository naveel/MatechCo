using Matechco.App_Start;
using Matechco.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Matechco.Controllers
{
    //[SessionAuthorize]
    public class AccountController : Controller
    {
        MatechcoEntities db = new MatechcoEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Index(vu_users urs)
        {
            ApplicationSession sess = new ApplicationSession();
            EncryptDecrypt encrypt = new EncryptDecrypt();
            try
            {
                string Encrypt_password = encrypt.Encrypt(urs.password);
                string Encrypt_username = encrypt.Encrypt(urs.username);
                vu_users usr = new vu_users();
                var res = (from x in db.tbl_users where x.username == Encrypt_username && x.password == Encrypt_password  select x ).FirstOrDefault();
                if (res != null)
                {
                    sess.UserAccountObj = res;
                    TempData["UserName"] = res.username;
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    TempData["ResultLogin"] = "Username or password is incorrect";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["ResultLogin"] = "Something went wrong";
                return View("Index");
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            Session.RemoveAll();
            return RedirectToAction("Index", "Login");
        }
    }
}