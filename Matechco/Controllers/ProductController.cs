using Matechco.App_Start;
using Matechco.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Matechco.Controllers
{
    [SessionAuthorize]
    public class ProductController : Controller
    {
        ApplicationSession sess = new ApplicationSession();
        MatechcoEntities db = new MatechcoEntities(); 
        // GET: Product
        public ActionResult Index()
        {
            if (!GetTokenValidation(ApplicationSession.Session.Token)) { return RedirectToAction("Index", "Login"); }
            return View();
        }
        public async Task<JsonResult> GetData()
        {
            List<tbl_product> lst = new List<tbl_product>();

            // Get active cities list
            var res = db.tbl_product.ToList();
            if (res != null && res.Count > 0) {
                lst = res;
                return new JsonResult { Data = lst, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            // Add actions

            return new JsonResult { Data = new tbl_product(), JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        public async Task<ActionResult> Create()
        {
            if (!GetTokenValidation(ApplicationSession.Session.Token)) { return RedirectToAction("Index", "Login"); }
            ViewBag.ProductType = await ProductTyp();
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(tbl_product model)
        {
            if (!GetTokenValidation(ApplicationSession.Session.Token)) { return RedirectToAction("Index", "Login"); }
            try
            {
                ViewBag.ProductType = await ProductTyp();
                db.tbl_product.Add(model);
                db.SaveChanges();
                SetNotification("info", "Record save successfully");
                return RedirectToAction("Index");
            }
            catch {
                SetNotification("error", "Error Record not save");
                return View();
            }
        }
        public async Task<ActionResult> Edit(int Id)
        {
            if (!GetTokenValidation(ApplicationSession.Session.Token)) { return RedirectToAction("Index", "Login"); }
            Product model = new Product();
            ViewBag.ProductType = await ProductTyp();
            var res = db.tbl_product.Where(x => x.Id == Id).FirstOrDefault();
            model.Id = res.Id;
            model.Name = res.Name;
            model.ProductTypeId  = res.ProductTypeId;
            model.Code = res.Code;
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(Product model)
        {
            if (!GetTokenValidation(ApplicationSession.Session.Token)) { return RedirectToAction("Index", "Login"); }
            ViewBag.ProductType = await ProductTyp();
            
            try
            {
                tbl_product dbmodel = db.tbl_product.Where(x => x.Id == model.Id).FirstOrDefault();
                dbmodel.Id = model.Id;
                dbmodel.Name = model.Name;
                dbmodel.ProductTypeId = model.ProductTypeId;
                dbmodel.Code = model.Code;
                db.SaveChanges();
                SetNotification("info", "Record save update");
                return RedirectToAction("Index");
            }
            catch
            {
                SetNotification("error", "Error Record not update");
                return View();
            }
        }
        private async Task<List<tbl_productType>> ProductTyp()
        {

            var res = db.tbl_productType.ToList();
            if (res != null && res.Count > 0)
            {
                return res;
            }
            return null;
        }
        public async Task<ActionResult> ProductType(int Id)
        {
            var res = db.SP_GenerateProductCode(Id).FirstOrDefault();
            string code = res.code + "-" + res.Id;
            return new JsonResult { Data = code, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        private void SetNotification(string type, string message)
        {
            TempData["Notify"] = message;
            TempData["NotifyType"] = type;
        }

        private bool GetTokenValidation(string encUserId)
        {
            var handler = new JwtSecurityTokenHandler();
            //var jwt = handler.ReadJwtToken(encUserId);
            var tokenS = handler.ReadJwtToken(encUserId) as JwtSecurityToken;
            var u = tokenS.Claims.Where(y => y.Type == "user").FirstOrDefault().Value;



            return true;

        }
    }
}