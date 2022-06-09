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
    public class TransaksiController : Controller
    {
        private GeneralHelper dHelper = new GeneralHelper();
        private readonly ITransaksi transaksi;

        public TransaksiController() : this(new TransaksiService()) { }
        public TransaksiController(ITransaksi transaksi)
        {
            this.transaksi = transaksi;
        }
        // GET: Admin/Transaksi
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetList([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            var query = transaksi.Get().AsQueryable();

            var totalCount = query.Count();

            #region Filtering
            // Apply filters for searching
            if (requestModel.Search.Value != string.Empty)
            {
                var value = requestModel.Search.Value.Trim();
                query = query.Where(p => p.no_transaksi.ToLower().ToString().Contains(value.ToString().ToLower()) ||
                                         p.tanggal.ToString().Contains(value.ToString().ToLower()) ||
                                         p.total.ToString().Contains(value.ToString().ToLower())
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

            query = query.OrderBy(orderByString == string.Empty ? "no_transaksi asc" : orderByString);

            #endregion Sorting

            // Paging
            query = query.Skip(requestModel.Start).Take(requestModel.Length);

            var data = query.Select(trans => new
            {
                trans.no_transaksi,
                trans.tanggal,
                trans.total,
                trans.created_at
            }).ToList();

            return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Add(TransaksiModel model)
        {
            bool isSave = false;
            try
            {

                model.created_by = dHelper.CurrentLoginUser();
                model.updated_by = dHelper.CurrentLoginUser();

                await transaksi.Add(model);

                isSave = true;
                return Json(new { success = isSave, message = "Simpan Data Berhasil" }, JsonRequestBehavior.AllowGet);
                
            }
            catch (Exception ex)
            {

                return Json(new { success = isSave, message = "Simpan Data Gagal " + ex.Message }, JsonRequestBehavior.AllowGet);

            }

        }
    }
}