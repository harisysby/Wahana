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
    public class SatuanController : Controller
    {
        private GeneralHelper dHelper = new GeneralHelper();
        private readonly ISatuan service;

        public SatuanController() : this(new SatuanService()) { }
        public SatuanController(ISatuan service)
        {
            this.service = service;
        }
        // GET: Admin/Satuan
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetList([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            var query = service.Get().AsQueryable();

            var totalCount = query.Count();

            #region Filtering
            // Apply filters for searching
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.nama.ToLower().ToString().Contains(value.ToString().ToLower()));
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

            query = query.OrderBy(orderByString == string.Empty ? "nama asc" : orderByString);

            #endregion Sorting

            // Paging
            query = query.Skip(requestModel.Start).Take(requestModel.Length);

            var data = query.Select(satuan => new
            {
                satuan.id,
                satuan.nama,
                satuan.created_at
            }).ToList();

            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Add(Satuan model)
        {
            bool isSave = false;
            try
            {

                model.created_by = dHelper.CurrentLoginUser();

                var data = service.GetRow(model.nama);
                if (data.id == 0 || data == null)
                {
                    await service.Add(model);

                    isSave = true;
                    return Json(new { success = isSave, message = "Simpan Data Berhasil" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = isSave, message = $"Satuan '{model.nama}' sudah terdaftar" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {

                return Json(new { success = isSave, message = "Simpan Data Gagal " + ex.Message }, JsonRequestBehavior.AllowGet);

            }

        }
        public async Task<ActionResult> Edit(Satuan model)
        {
            bool isSave = false;
            try
            {

                var data = service.GetRow(model.nama);
                if (data.id == 0 || data == null)
                {
                    await service.Edit(model);

                    isSave = true;
                    return Json(new { success = isSave, message = "Update Data Berhasil" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = isSave, message = $"Satuan '{model.nama}' sudah terdaftar" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {

                return Json(new { success = isSave, message = "Update Data Gagal " + ex.Message }, JsonRequestBehavior.AllowGet);

            }

        }
        public async Task<ActionResult> Delete(int id)
        {
            bool isSave = false;
            try
            {

                await service.Delete(id);

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