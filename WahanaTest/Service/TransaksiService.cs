using Penjualan.Areas.Admin.Models;
using Penjualan.Helper;
using Penjualan.Service.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Penjualan.Service
{
    public class TransaksiService : ITransaksi
    {
        public List<TransaksiModel> Get()
        {
            try
            {
                DataTable dt = new DataTable();
                List<TransaksiModel> list = new List<TransaksiModel>();
                dt = SqlHelper.GetDatatableSync(SqlHelper.strConn, "usp_transaksiSelect", null, CommandType.StoredProcedure);
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new TransaksiModel
                    {
                        no_transaksi = row["no_transaksi"].AsString(),
                        tanggal = row["tanggal"].AsDateTime(),
                        total = row["total"].AsFloat(),
                        created_at = row["created_at"].AsDateTime()
                    });
                }

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> Add(TransaksiModel model)
        {
            try
            {
                List<SqlParameter> Params = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "no_transaksi", Value = model.no_transaksi.AsString() },
                    new SqlParameter() { ParameterName = "tanggal", Value = model.tanggal.AsDateTime() },
                    new SqlParameter() { ParameterName = "total", Value = model.total.AsFloat() },
                    new SqlParameter() { ParameterName = "updated_by", Value = model.updated_by.AsString() },
                    new SqlParameter() { ParameterName = "created_by", Value = model.created_by.AsString() }
                };
                return await SqlHelper.ExecuteAsync(SqlHelper.strConn, "usp_transaksiInsert", Params, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}