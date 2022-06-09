using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Penjualan.Areas.Admin.Models
{
    public class InventoryModel
    {
        public string InvtID { get; set; }

        public string Invt_Name { get; set; }

        public string UnitID { get; set; }
        public string Unit { get; set; }

        public string Product_ClassID { get; set; }
        public string Tipe { get; set; }

        public string BrandID { get; set; }
        public string Brand { get; set; }

        public string Pemilik { get; set; }

        public string Jenis { get; set; }

        public string VendorID { get; set; }
        public string VendorName { get; set; }

        public string Produsen { get; set; }

        public string Buyer { get; set; }

        public bool? IsNew { get; set; }

        public DateTime? CreateDate { get; set; }

        public string Createby { get; set; }

        public DateTime? UpdateDate { get; set; }

        public string Updateby { get; set; }
    }
}