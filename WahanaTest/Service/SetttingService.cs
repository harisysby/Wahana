using Penjualan.Areas.Admin.Models;
using Penjualan.Helper;
using Penjualan.Service.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Penjualan.Service
{
    public class SetttingService : ISetting
    {
        public async Task<Setting> Get()
        {
            try
            {
                DataTable dt = new DataTable();
                Setting data = new Setting();
                dt = await SqlHelper.GetDatatableAsync(SqlHelper.strConn, "usp_perusahaanSelect", null, CommandType.StoredProcedure);
                if (dt.Rows.Count >0)
                {
                    data.nama = dt.Rows[0]["nama"].AsString();
                    data.alamat = dt.Rows[0]["alamat"].AsString();
                    data.email = dt.Rows[0]["email"].AsString();
                    data.phone = dt.Rows[0]["phone"].AsString();
                    data.fax = dt.Rows[0]["fax"].AsString();
                }
                
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<int> Update(Setting model)
        {
            throw new NotImplementedException();
        }
    }
}