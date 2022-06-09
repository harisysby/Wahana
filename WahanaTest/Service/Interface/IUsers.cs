using Penjualan.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Penjualan.Service.Interface
{
    public interface IUsers
    {
        Task<Users> login(string username, string password);
        List<Users> Get();
        Users GetRow(string username);
        Task<int> Add(Users model);
        Task<int> Edit(Users model);
        Task<int> Delete(string username);
        List<Users> chart(int month, int year);
    }
}
