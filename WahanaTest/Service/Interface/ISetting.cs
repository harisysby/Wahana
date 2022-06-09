using Penjualan.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Penjualan.Service.Interface
{
    public interface ISetting
    {
        Task<Setting> Get();
        Task<int> Update(Setting model);
    }
}
