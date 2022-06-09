using Penjualan.Areas.Admin.Models;
using Penjualan.Helper;
using Penjualan.Service;
using Penjualan.Service.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Penjualan.Areas.Admin.Controllers
{
    public class InventoryController : Controller
    {
        private readonly IInventory inventoryService;
        private readonly IGlobal globalService;
        private GeneralHelper dHelper = new GeneralHelper();
        private readonly Dictionary<string, object> res = new Dictionary<string, object>();

        public InventoryController() : this(new GlobalService(), new InventoryService())
        {
        }

        public InventoryController(IGlobal globalService, IInventory inventoryService)
        {
            this.globalService = globalService;
            this.inventoryService = inventoryService;
        }

        // GET: Admin/Inventory
        public ActionResult Index()
        {
            //string Url = Request.RawUrl.ToString();
            //var control = dHelper.GetUserPermission(Url);
            //if (control.IsLogin == false)
            //{
            //    return Redirect("/account/login");
            //}

            //if (control.IsView == false)
            //{
            //    return Redirect("/admin/notaccess");
            //}

            //ViewBag.IsEdit = control.IsEdit;
            //ViewBag.IsDelete = control.IsDelete;
            //ViewBag.IsAdd = control.IsAdd;
            //ViewData["Unit"] = globalService.GetUnit();
            //ViewData["Brand"] = globalService.GetBrand();
            //ViewData["ProductClass"] = globalService.GetProductClass();
            //ViewData["Vendor"] = globalService.GetVendor();
            //ViewData["Buyer"] = globalService.GetCustomer();
            return View();
        }

        //[HttpPost]
        //public ActionResult GetList([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        //{
        //    var query = inventoryService.GetInventory().AsQueryable();

        //    var totalCount = query.Count();

        //    #region Filtering
        //    // Apply filters for searching
        //    if (requestModel.Search.Value != string.Empty)
        //    {
        //        var value = requestModel.Search.Value.Trim();
        //        query = query.Where(p => p.InvtID.ToLower().ToString().Contains(value.ToString().ToLower()) ||
        //                                 p.Invt_Name.ToLower().ToString().Contains(value.ToString().ToLower()) ||
        //                                 p.Tipe.ToLower().ToString().Contains(value.ToString().ToLower()) ||
        //                                 p.Pemilik.ToLower().ToString().Contains(value.ToString().ToLower()) ||
        //                                 p.VendorName.ToLower().ToString().Contains(value.ToString().ToLower()) ||
        //                                 p.Brand.ToLower().ToString().Contains(value.ToString().ToLower()) ||
        //                                 p.Unit.ToLower().ToString().Contains(value.ToString().ToLower())
        //                            );
        //    }

        //    var filteredCount = query.Count();

        //    #endregion Filtering

        //    #region Sorting
        //    // Sorting
        //    var sortedColumns = requestModel.Columns.GetSortedColumns();
        //    var orderByString = String.Empty;

        //    foreach (var column in sortedColumns)
        //    {
        //        orderByString += orderByString != String.Empty ? "," : "";
        //        orderByString += (column.Data) + (column.SortDirection == Column.OrderDirection.Ascendant ? " asc" : " desc");
        //    }

        //    query = query.OrderBy(orderByString == string.Empty ? "Invt_Name asc" : orderByString);

        //    #endregion Sorting

        //    // Paging
        //    query = query.Skip(requestModel.Start).Take(requestModel.Length);

        //    var data = query.Select(Inventory => new
        //    {
        //        Inventory.InvtID,
        //        Inventory.Invt_Name,
        //        Inventory.UnitID,
        //        Inventory.Unit,
        //        Inventory.Product_ClassID,
        //        Inventory.Tipe,
        //        Inventory.BrandID,
        //        Inventory.Brand,
        //        Inventory.Pemilik,
        //        Inventory.Jenis,
        //        Inventory.VendorID,
        //        Inventory.VendorName,
        //        Inventory.Produsen,
        //        Inventory.Buyer,
        //    }).ToList();

        //    return Json(new DataTablesResponse(requestModel.Draw, data, filteredCount, totalCount), JsonRequestBehavior.AllowGet);
        //}

        //[HttpGet]
        //public ActionResult GetInventoryById(string InvtID)
        //{
        //    var result = inventoryService.GetInventoryById(InvtID);
        //    if (result != null)
        //    {
        //        return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //    }
        //    return null;
        //}
        [HttpPost]
        public async Task<ActionResult> Create(InventoryModel model)
        {
            try
            {
                model.Createby = User.Identity.Name;
                bool Data = inventoryService.IsInventoryExist(model.InvtID);
                if (!Data)
                {
                    int hasil = await inventoryService.Add(model);
                    if (hasil != -1)
                    {
                        return Json(new { success = true, message = "Simpan Data Berhasil" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, message = "Simpan Data Gagal" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Data sudah ada !" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Simpan Data Gagal : " + ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(InventoryModel model)
        {
            try
            {
                model.Updateby = User.Identity.Name;

                int hasil = await inventoryService.Edit(model);
                if (hasil != -1)
                {
                    return Json(new { success = true, message = "Edit Data Berhasil" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "Edit Data Gagal" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Edit Data Gagal : " + ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string InvtId)
        {
            try
            {
                int hasil = await inventoryService.Delete(InvtId);
                if (hasil != -1)
                {
                    return Json(new { success = true, message = "Delete Data Berhasil" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "Delete Data Gagal" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Delete Data Gagal : " + ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}