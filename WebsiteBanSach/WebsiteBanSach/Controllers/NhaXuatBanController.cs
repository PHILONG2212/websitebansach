using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webbansach.Model;
using WebsiteBanSach.Models;

namespace webbansach.Controllers
{
    public class NhaXuatBanController : Controller
    {
        //
        // GET: /NhaXuatBan/
       QuanLyBanSachEntities db = new QuanLyBanSachEntities();

        public PartialViewResult NhaXuatBanPartial()
        {
            return PartialView(db.NhaXuatBans.Take(10).OrderBy(n=>n.TenNXB).ToList());


         //   var NhaXuatBans = db.Saches.Take(10).OrderBy(n => n.TenNXB).ToList();
          //  return PartialView(lstSachMoi);
        }
       //Hien thi sach theo chu de nha xuat ban
        public ViewResult SachTheoNXB(int MaNXB = 0)
        {
           NhaXuatBan nxb = db.NhaXuatBans.SingleOrDefault(n => n.MaNXB == MaNXB);
            if (nxb == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //truy xuat danh sach cac quyen sach theo nha xuat ban//

            List<Sach> lstSach = db.Saches.Where(n => n.MaNXB == MaNXB).OrderBy(n => n.GiaBan).ToList();
            if (lstSach.Count == 0)
            {
                ViewBag.Sach = "Không có sách nào thuộc chủ đề này: ";

            }
            return View(lstSach);
        }
        public ViewResult DanhMucNXB()
        {
            return View(db.NhaXuatBans.ToList());

        }
    }
}
