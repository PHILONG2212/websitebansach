using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webbansach.Model;
using WebsiteBanSach.Models;

namespace webbansach.Controllers
{
    public class ChuDeController : Controller
    {
        //
        // GET: /ChuDe/
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        public ActionResult ChuDePartial()
        {
            return PartialView(db.ChuDes.Take(5).ToList());
        }
        //sach theo chu de//
        public ViewResult SachTheoChuDe ( int MaChuDe = 0)
        {
            //kiem tra chu de ton tai hay khong//
            ChuDe cd = db.ChuDes.SingleOrDefault(n => n.MaChuDe == MaChuDe);
            if(cd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //truy xuat danh sach cac quyen sach theo chu de//

            List<Sach> lstSach = db.Saches.Where(n=>n.MaChuDe==MaChuDe).OrderBy(n=>n.GiaBan).ToList(); 
            if(lstSach.Count==0)
            {
                ViewBag.Sach="Không có sách nào thuộc chủ đề này: ";

            }
            return View(lstSach);
        }
      
    }
}
