using Penjualan.Areas.Admin.Models;
using Penjualan.Helper;
using Penjualan.Service.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Penjualan.Service
{
    public class SatuanService : ISatuan
    {
        public async Task<int> Add(Satuan model)
        {
            try
            {
                List<SqlParameter> Params = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "nama", Value = model.nama.AsString() },
                    new SqlParameter() { ParameterName = "created_by", Value = model.created_by.AsString() }
                };
                return await SqlHelper.ExecuteAsync(SqlHelper.strConn, "usp_satuanInsert", Params, CommandType.StoredProcedure);
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
                return await SqlHelper.ExecuteAsync(SqlHelper.strConn, "usp_satuanDelete", Params, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> Edit(Satuan model)
        {
            try
            {
                List<SqlParameter> Params = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "id", Value = model.id.AsInt() },
                    new SqlParameter() { ParameterName = "nama", Value = model.nama.AsString() },
                    new SqlParameter() { ParameterName = "updated_by", Value = model.updated_by.AsString() }
                };
                return await SqlHelper.ExecuteAsync(SqlHelper.strConn, "usp_satuanUpdate", Params, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Satuan> Get()
        {
            try
            {
                DataTable dt = new DataTable();
                List<Satuan> list = new List<Satuan>();
                dt = SqlHelper.GetDatatableSync(SqlHelper.strConn, "usp_satuanSelect", null, CommandType.StoredProcedure);
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new Satuan
                    {
                        id = row["id"].AsInt(),
                        nama = row["nama"].AsString(),
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

        public Satuan GetRow(string nama)
        {
            try
            {
                DataTable dt = new DataTable();
                Satuan data = new Satuan();
                List<SqlParameter> Params = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "nama", Value = nama.AsString() }
                };
                dt = SqlHelper.GetDatatableSync(SqlHelper.strConn, "usp_satuanSelectRow", Params, CommandType.StoredProcedure);
                if (dt.Rows.Count > 0)
                {
                    data.id = dt.Rows[0]["id"].AsInt();
                    data.nama = dt.Rows[0]["nama"].AsString();
                    data.created_at = dt.Rows[0]["created_at"].AsDateTime();
                }

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}