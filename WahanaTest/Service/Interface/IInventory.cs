using Penjualan.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Penjualan.Service.Interface
{
    public interface IInventory
    {
        //List<InventoryModel> GetInventory();
        //List<InventoryModel> GetInventoryById(string InvtId);
        Task<int> Add(InventoryModel model);
        Task<int> Edit(InventoryModel model);
        Task<int> Delete(string InvtId);

        bool IsInventoryExist(string InvtId);
    }
}
