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
    public class BarangController : Controller
    {
        private GeneralHelper dHelper = new GeneralHelper();
        private readonly IBarang barang;
        private readonly IGlobal common;

        public BarangController() : this(new BarangService(), new GlobalService()) { }
        public BarangController(IBarang barang, IGlobal common)
        {
            this.barang = barang;
            this.common = common;
        }

        public ActionResult Index()
        {
            ViewData["satuan"] = common.Satuan();
            return View();
        }

        [HttpPost]
        public ActionResult GetList([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            var query = barang.Get().AsQueryable();

            var totalCount = query.Count();

            #region Filtering
            // Apply filters for searching
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.nama.ToLower().ToString().Contains(value.ToString().ToLower()) ||
                                         p.sales_price.ToLower().ToString().Contains(value.ToString().ToLower()) ||
                                         p.id_satuan.ToLower().ToString().Contains(value.ToString().ToLower())
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

            var data = query.Select(barang => new
            {
                barang.id,
                barang.nama,
                barang.sales_price,
                barang.id_satuan,
                barang.satuan,
                barang.created_at
            }).ToList();

            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Add(Barang model)
        {
            bool isSave = false;
            try
            {

                model.created_by = dHelper.CurrentLoginUser();
             
                var data = barang.GetRow(model.nama);
                if (data == null || data.id == 0)
                {
                    await barang.Add(model);

                    isSave = true;
                    return Json(new { success = isSave, message = "Simpan Data Berhasil" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = isSave, message = $"Barang '{model.nama}' sudah terdaftar" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {

                return Json(new { success = isSave, message = "Simpan Data Gagal " + ex.Message }, JsonRequestBehavior.AllowGet);

            }

        }

        public async Task<ActionResult> Edit(Barang model)
        {
            bool isSave = false;
            try
            {

                model.updated_by = dHelper.CurrentLoginUser();

                var data = barang.GetRow(model.nama);
                if (data == null || data.id == 0)
                {
                    await barang.Edit(model);

                    isSave = true;
                    return Json(new { success = isSave, message = "Update Data Berhasil" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = isSave, message = $"Barang '{model.nama}' sudah terdaftar" }, JsonRequestBehavior.AllowGet);
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

                await barang.Delete(id);

                isSave = true;
                return Json(new { success = isSave, message = "Update Data Berhasil" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                return Json(new { success = isSave, message = "Update Data Gagal " + ex.Message }, JsonRequestBehavior.AllowGet);

            }

        }

    }
}