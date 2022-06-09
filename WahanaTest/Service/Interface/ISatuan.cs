using Penjualan.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Penjualan.Service.Interface
{
    public interface ISatuan
    {
        List<Satuan> Get();
        Satuan GetRow(string nama);
        Task<int> Add(Satuan model);
        Task<int> Edit(Satuan model);
        Task<int> Delete(int id);
    }
}
