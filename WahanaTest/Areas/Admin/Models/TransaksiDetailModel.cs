using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Penjualan.Areas.Admin.Models
{
    public class TransaksiDetailModel
    {
        public int id { get; set; }

        public string no_transaksi { get; set; }

        public int id_barang { get; set; }

        public int qty { get; set; }

        public float sales_price { get; set; }

        public float sub_total { get; set; }
    }
}