using Penjualan.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Penjualan.Service.Interface
{
    public interface ITransaksi
    {
        List<TransaksiModel> Get();
        Task<int> Add(TransaksiModel model);
    }
}
