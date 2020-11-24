using Matechco.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Matechco.Controllers
{
    //[SessionAuthorize]
    public class ProductController : Controller
    {
        MatechcoEntities db = new MatechcoEntities(); 
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> GetData()
        {
            List<tbl_product> lst = new List<tbl_product>();

            // Get active cities list
            var res = db.tbl_product.ToList();
            if (res != null && res.Count > 0) {
                return new JsonResult { Data = lst, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            // Add actions

            return new JsonResult { Data = new tbl_product(), JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(tbl_product model)
        {
            db.tbl_product.Add(model);
            db.SaveChanges();
            return View();
        }
        public ActionResult Edit(int Id)
        {
            tbl_product model = db.tbl_product.Where(x => x.Id == Id).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(tbl_product model)
        {
            tbl_product dbmodel = db.tbl_product.Where(x => x.Id == model.Id).FirstOrDefault();
            dbmodel = model;
            db.SaveChanges();
            return View();
        }
    }
}