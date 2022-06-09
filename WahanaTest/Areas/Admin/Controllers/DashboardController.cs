using Penjualan.Helper;
using Penjualan.Service;
using Penjualan.Service.Interface;
using System;
using System.Globalization;
using System.Web.Mvc;

namespace Penjualan.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        private GeneralHelper dHelper = new GeneralHelper();
        private readonly ISetting setting;
        private readonly IUsers user;

        public DashboardController() : this(new SetttingService(), new UserService()) { }
        public DashboardController(ISetting setting, IUsers user)
        {
            this.setting = setting;
            this.user = user;
        }
        public ActionResult Index()
        {
            PopulateChart();
            GetH();
            return View();
        }
        

        #region Dashboard Chart

        private void PopulateChart()
        {
            string MonthName = "", saleData = "";
            try
            {
                for (int i = 0; i < 12; i++)
                {
                    DateTime reportDate = (DateTime.Now.AddMonths(0 - 11 + i));
                    MonthName += "\"" + ToMonthName(reportDate) + "\",";
                    
                    var Data = user.chart(reportDate.Month, reportDate.Year);
                    if (Data != null)
                    {
                        saleData += Data.Count.ToString() + ",";
                    }
                    else
                    {
                        saleData += "0,";
                    }
                }
            }
            catch { }
            ViewBag.MonthName = MonthName.Remove(MonthName.Length - 1);
            ViewBag.Data = saleData.Remove(saleData.Length - 1);


        }

        private void GetH()
        {
            //var data = await setting.Get();
            //if (data != null)
            //{
            //    ViewBag.Name = data.nama;
            //}

            var userID = dHelper.CurrentLoginUser();
            var userInfo = user.GetRow(userID.AsString());
            if (userInfo != null)
            {
                ViewBag.UserName = userInfo.username;
                //ViewBag.PhotoPath = userInfo.PhotoPath;
                ViewBag.RegisterDate = userInfo.created_at.Value.ToString("dd-MMM-yyyy");
            }
        }

        #endregion Dashboard Chart

        #region Header


        //Get Header
        public ActionResult GetHeader()
        {
            try
            {
                var userID = new Guid(dHelper.CurrentLoginUser());
                var userInfo = user.GetRow(userID.AsString());
                if (userInfo != null)
                {
                    ViewBag.UserName = userInfo.username;
                    //ViewBag.PhotoPath = userInfo.PhotoPath;
                    ViewBag.RegisterDate = userInfo.created_at.Value.ToString("dd-MMM-yyyy");
                }
            }
            catch
            { }
            return PartialView("_Header");
        }

        #endregion Header

        public string ToMonthName(DateTime dateTime)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dateTime.Month);
        }

    }
}