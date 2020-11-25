using Matechco.App_Start;
using Matechco.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Matechco.Controllers
{
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
           
            EncryptDecrypt encrypt = new EncryptDecrypt();
            try
            {
                //ApplicationSession s = new ApplicationSession();
                string Encrypt_password = encrypt.Encrypt(urs.password);
                string Encrypt_username = encrypt.Encrypt(urs.username);
                vu_users usr = new vu_users();
                var res = (from x in db.tbl_users where x.username == Encrypt_username && x.password == Encrypt_password  select x ).FirstOrDefault();
                if (res != null)
                {

                    usr.user_sk = res.user_sk;
                    usr.username = res.username;
                    usr.password = res.password;
                    usr.UserFullName = res.UserFullName;
                    usr.Token = GetToken(encrypt.Encrypt(res.user_sk.ToString()));
                    HttpContext.Session["App"] = usr;
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

        public string GetToken(string userid)
        {
            string key = "MAKV2SPBNI99212JWTKEY_KU1909_76656";
            var issuer = HttpContext.Request.Url.AbsoluteUri;

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var permclaims = new List<Claim>();
            permclaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            permclaims.Add(new Claim("valid", "1"));
            permclaims.Add(new Claim("user", userid.ToString()));
            //permclaims.Add(new Claim("", "Ahmed"));

            var token = new JwtSecurityToken(issuer,
                issuer,
                permclaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;

        }
    }
}