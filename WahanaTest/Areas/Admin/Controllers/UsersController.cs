using Penjualan.Helper;
using Penjualan.Service;
using Penjualan.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using DataTables.Mvc;
using System.Threading.Tasks;
using Penjualan.Areas.Admin.Models;

namespace Penjualan.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        private GeneralHelper dHelper = new GeneralHelper();
        private readonly IUsers user;

        public UsersController() : this(new UserService()) { }
        public UsersController(IUsers user)
        {
            this.user = user;
        }
        // GET: Admin/Users
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetList([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            var query = user.Get().AsQueryable();

            var totalCount = query.Count();

            #region Filtering
            // Apply filters for searching
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.username.ToLower().ToString().Contains(value.ToString().ToLower()) ||
                                         p.email.ToLower().ToString().Contains(value.ToString().ToLower()) ||
                                         p.phone.ToLower().ToString().Contains(value.ToString().ToLower())
                                    );
            }

            var filteredCount = query.Count();

            #endregion Filtering

            #region Sorting
            // Sorting
            var sortedColumns = requestModel.Columns.GetSortedColumns();
            var orderByString = String.Empty;

            foreach (var column in sortedColumns)
            {
                orderByString += orderByString != String.Empty ? "," : "";
                orderByString += (column.Data) + (column.SortDirection == Column.OrderDirection.Ascendant ? " asc" : " desc");
            }

            query = query.OrderBy(orderByString == string.Empty ? "username asc" : orderByString);

            #endregion Sorting

            // Paging
            query = query.Skip(requestModel.Start).Take(requestModel.Length);

            var data = query.Select(user => new
            {   
                user.username,
                user.email,
                user.phone,
                user.created_at
            }).ToList();

            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> Add(Users model)
        {
            bool isSave = false;
            try
            {

                model.created_by = dHelper.CurrentLoginUser();

                var data = user.GetRow(model.created_by);
                if (data == null)
                {
                    await user.Add(model);
               
                    isSave = true;
                    return Json(new { success = isSave, message = "Simpan Data Berhasil" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = isSave, message = "Username sudah terdaftar" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {

                return Json(new { success = isSave, message = "Simpan Data Gagal " + ex.Message }, JsonRequestBehavior.AllowGet);

            }

        }
        public async Task<ActionResult> Edit(Users model)
        {
            bool isSave = false;
            try
            {

                model.updated_by = dHelper.CurrentLoginUser();
                await user.Edit(model);

                isSave = true;
                return Json(new { success = isSave, message = "Update Data Berhasil" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                return Json(new { success = isSave, message = "Update Data Gagal " + ex.Message }, JsonRequestBehavior.AllowGet);

            }

        }

        public async Task<ActionResult> Delete(string username)
        {
            bool isSave = false;
            try
            {
                if (username.ToLower() == "admin")
                {
                    return Json(new { success = isSave, message = "Admin tidak boleh di hapus" }, JsonRequestBehavior.AllowGet);
                }
                await user.Delete(username);

                isSave = true;
                return Json(new { success = isSave, message = "Hapus Data Berhasil" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                return Json(new { success = isSave, message = "Hapus Data Gagal " + ex.Message }, JsonRequestBehavior.AllowGet);

            }

        }
    }
}