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
    public class BarangService : IBarang
    {

        public List<Barang> Get()
        {
            try
            {
                DataTable dt = new DataTable();
                List<Barang> list = new List<Barang>();
                dt = SqlHelper.GetDatatableSync(SqlHelper.strConn, "usp_barangSelect", null, CommandType.StoredProcedure);
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new Barang
                    {
                        id = row["id"].AsInt(),
                        nama = row["nama"].AsString(),
                        sales_price = row["sales_price"].AsString(),
                        id_satuan = row["id_satuan"].AsString(),
                        satuan = row["satuan"].AsString(),
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

        public Barang GetRow(string nama)
        {
            try
            {
                DataTable dt = new DataTable();
                Barang data = new Barang();
                List<SqlParameter> Params = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "nama", Value = nama.AsString() }
                };
                dt = SqlHelper.GetDatatableSync(SqlHelper.strConn, "usp_barangSelectRow", Params, CommandType.StoredProcedure);
                if (dt.Rows.Count > 0)
                {
                    data.id = dt.Rows[0]["id"].AsInt();
                    data.nama = dt.Rows[0]["nama"].AsString();
                    data.sales_price = dt.Rows[0]["sales_price"].AsString();
                    data.id_satuan = dt.Rows[0]["id_satuan"].AsString();
                    data.created_at = dt.Rows[0]["created_at"].AsDateTime();
                }

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> Add(Barang model)
        {
            try
            {
                List<SqlParameter> Params = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "nama", Value = model.nama.AsString() },
                    new SqlParameter() { ParameterName = "sales_price", Value = model.sales_price.AsFloat() },
                    new SqlParameter() { ParameterName = "id_satuan", Value = model.id_satuan.AsString() },
                    new SqlParameter() { ParameterName = "created_by", Value = model.created_by.AsString() }
                };
                return await SqlHelper.ExecuteAsync(SqlHelper.strConn, "usp_barangInsert", Params, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> Delete(int id)
        {
            try
            {
                List<SqlParameter> Params = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "id", Value = id.AsInt() }
                };
                return await SqlHelper.ExecuteAsync(SqlHelper.strConn, "usp_barangDelete", Params, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> Edit(Barang model)
        {
            try
            {
                List<SqlParameter> Params = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "id", Value = model.id.AsInt() },
                    new SqlParameter() { ParameterName = "nama", Value = model.nama.AsString() },
                    new SqlParameter() { ParameterName = "sales_price", Value = model.sales_price.AsFloat() },
                    new SqlParameter() { ParameterName = "id_satuan", Value = model.id_satuan.AsString() },
                    new SqlParameter() { ParameterName = "updated_by", Value = model.updated_by.AsString() }
                };
                return await SqlHelper.ExecuteAsync(SqlHelper.strConn, "usp_barangUpdate", Params, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}