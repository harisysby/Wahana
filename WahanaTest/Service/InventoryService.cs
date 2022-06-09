using Penjualan.Areas.Admin.Models;
using Penjualan.Service.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Penjualan.Helper;
using System.Data;
using System.Threading.Tasks;

namespace Penjualan.Service
{
    public class InventoryService : IInventory
    {
        public async Task<int> Add(InventoryModel model)
        {
            try
            {
                List<SqlParameter> Params = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "InvtID", Value = model.InvtID.AsString() },
                    new SqlParameter() { ParameterName = "Invt_Name", Value = model.Invt_Name.AsString() },
                    new SqlParameter() { ParameterName = "UnitID", Value = model.UnitID.AsString() },
                    new SqlParameter() { ParameterName = "Product_ClassID", Value = model.Product_ClassID.AsString() },
                    new SqlParameter() { ParameterName = "BrandID", Value = model.BrandID.AsString() },
                    new SqlParameter() { ParameterName = "Pemilik", Value = model.Pemilik.AsString() },
                    new SqlParameter() { ParameterName = "Jenis", Value = model.Jenis.AsString() },
                    new SqlParameter() { ParameterName = "Vendor", Value = model.VendorID.AsString() },
                    new SqlParameter() { ParameterName = "Produsen", Value = model.Produsen.AsString() },
                    new SqlParameter() { ParameterName = "Buyer", Value = model.Buyer.AsString() },
                    new SqlParameter() { ParameterName = "Createby", Value = model.Createby }
                };
                return await SqlHelper.ExecuteAsync(SqlHelper.strConn, "InventoryInsert", Params, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<int> Delete(string InvtId)
        {
            try
            {
                List<SqlParameter> Params = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "InvtID", Value = InvtId }
                };
                return await SqlHelper.ExecuteAsync(SqlHelper.strConn, "InventoryDelete", Params, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<int> Edit(InventoryModel model)
        {
            try
            {
                List<SqlParameter> Params = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "InvtID", Value = model.InvtID.AsString() },
                    new SqlParameter() { ParameterName = "Invt_Name", Value = model.Invt_Name.AsString() },
                    new SqlParameter() { ParameterName = "UnitID", Value = model.UnitID.AsString() },
                    new SqlParameter() { ParameterName = "Product_ClassID", Value = model.Product_ClassID.AsString() },
                    new SqlParameter() { ParameterName = "BrandID", Value = model.BrandID.AsString() },
                    new SqlParameter() { ParameterName = "Pemilik", Value = model.Pemilik.AsString() },
                    new SqlParameter() { ParameterName = "Jenis", Value = model.Jenis.AsString() },
                    new SqlParameter() { ParameterName = "Vendor", Value = model.VendorID.AsString() },
                    new SqlParameter() { ParameterName = "Produsen", Value = model.Produsen.AsString() },
                    new SqlParameter() { ParameterName = "Buyer", Value = model.Buyer.AsString() },
                    new SqlParameter() { ParameterName = "Updateby", Value = model.Updateby }
                };
                return await SqlHelper.ExecuteAsync(SqlHelper.strConn, "InventoryUpdate", Params, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

     
        public bool IsInventoryExist(string InvtId)
        {
            bool Hasil = false;
            try
            {
                List<SqlParameter> Params = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "InvtId", Value = InvtId }
                };
                DataTable dt = new DataTable();
                dt = SqlHelper.GetDatatableSync(SqlHelper.strConn,"InventorySelectById", Params,CommandType.StoredProcedure);
                if (dt.Rows.Count > 0)
                {
                    Hasil = true;
                }
                return Hasil;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private static readonly Func<IDataReader, InventoryModel> Make = reader =>
            new InventoryModel
            {
                InvtID = reader["InvtID"].AsString(),
                Invt_Name = reader["Invt_Name"].AsString(),
                UnitID = reader["UnitID"].AsString(),
                Unit = reader["Unit"].AsString(),
                Product_ClassID = reader["Product_ClassID"].AsString(),
                Tipe = reader["Tipe"].AsString(),
                BrandID = reader["BrandID"].AsString(),
                Brand = reader["Brand"].AsString(),
                Pemilik = reader["Pemilik"].AsString(),
                Jenis = reader["Jenis"].AsString(),
                VendorID = reader["Vendor"].AsString(),
                VendorName = reader["VendorName"].AsString(),
                Produsen = reader["Produsen"].AsString(),
                Buyer = reader["Buyer"].AsString()
            };
    }
}