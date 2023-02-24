using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webbansach.Model;
using WebsiteBanSach.Models;
using PagedList;
using PagedList.Mvc;

namespace webbansach.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
      public ActionResult Index(int? page)
        {
         //tạo biến số sản phẩm trên trang
            int pageSize = 9;
          //tạo biến số trang
            int pageNumber = (page ?? 1);
           return View(db.Saches.Where(n=>n.Moi==1).OrderBy(n=>n.GiaBan).ToPagedList(pageNumber,pageSize));
       }
      
    }
}
