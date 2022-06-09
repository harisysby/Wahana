using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Penjualan.Areas.Admin.Models
{
    public class TransaksiModel
    {
        public int id { get; set; }

        public string no_transaksi { get; set; }

        public DateTime tanggal { get; set; }

        public float total { get; set; }

        public DateTime? created_at { get; set; }

        public string created_by { get; set; }

        public DateTime? updated_at { get; set; }

        public string updated_by { get; set; }
    }
}