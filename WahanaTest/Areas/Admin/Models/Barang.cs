using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Penjualan.Areas.Admin.Models
{
    public class Barang
    {

        public int id { get; set; }

        public string nama { get; set; }

        public string sales_price { get; set; }

        public string id_satuan { get; set; }
        public string satuan { get; set; }


        public DateTime? created_at { get; set; }

        public string created_by { get; set; }

        public DateTime? updated_at { get; set; }

        public string updated_by { get; set; }

    }
}