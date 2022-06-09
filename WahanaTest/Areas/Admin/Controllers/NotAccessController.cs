using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Penjualan.Areas.Admin.Controllers
{
    public class NotAccessController : Controller
    {
        // GET: Admin/NotAccess
        public ActionResult Index()
        {
            return View();
        }
    }
}