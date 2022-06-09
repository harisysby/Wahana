using Penjualan.Areas.Admin.Models;
using Penjualan.Helper;
using Penjualan.Service.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Penjualan.Service
{
    public class GlobalService : IGlobal
    {
 
        public List<ComboBoxModel> Satuan()
        {
            try
            {
                DataTable dt = new DataTable();
                List<ComboBoxModel> list = new List<ComboBoxModel>();
                dt = SqlHelper.GetDatatableSync(SqlHelper.strConn, "usp_satuanCombobox", null, CommandType.StoredProcedure);
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new ComboBoxModel
                    {
                        Value = row["value"].AsString(),
                        Text = row["text"].AsString()
                    });
                }

                return list;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}