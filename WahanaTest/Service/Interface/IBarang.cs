using Penjualan.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Penjualan.Service.Interface
{
    public interface IBarang
    {
        List<Barang> Get();
        Barang GetRow(string nama);
        Task<int> Add(Barang model);
        Task<int> Edit(Barang model);
        Task<int> Delete(int id);


    }
}
