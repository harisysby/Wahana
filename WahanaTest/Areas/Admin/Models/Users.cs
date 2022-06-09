using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Penjualan.Areas.Admin.Models
{
    public class Users
    {
        public int id { get; set; }

        public string username { get; set; }

        public string email { get; set; }

        public string phone { get; set; }

        public string password { get; set; }

        public DateTime? created_at { get; set; }

        public string created_by { get; set; }

        public DateTime? updated_at { get; set; }

        public string updated_by { get; set; }
    }
}