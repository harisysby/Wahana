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
    public class UserService : IUsers
    {
        public async Task<int> Add(Users model)
        {
            try
            {
                List<SqlParameter> Params = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "username", Value = model.username.AsString() },
                    new SqlParameter() { ParameterName = "email", Value = model.email.AsString() },
                    new SqlParameter() { ParameterName = "phone", Value = model.phone.AsString() },
                    new SqlParameter() { ParameterName = "password", Value = model.password.AsString() },
                    new SqlParameter() { ParameterName = "created_by", Value = model.created_by.AsString() }
                };
                return await SqlHelper.ExecuteAsync(SqlHelper.strConn, "usp_usersInsert", Params, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Users> chart(int month, int year)
        {
            try
            {
                DataTable dt = new DataTable();
                List<Users> list = new List<Users>();
                List<SqlParameter> Params = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "month", Value = month.AsInt() },
                    new SqlParameter() { ParameterName = "year", Value = year.AsInt() }
                };
                dt = SqlHelper.GetDatatableSync(SqlHelper.strConn, "usp_users_total_user", Params, CommandType.StoredProcedure);
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new Users
                    {
                        id = row["id"].AsInt(),
                        username = row["username"].AsString(),
                        email = row["email"].AsString(),
                        phone = row["phone"].AsString()
                    });
                }

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> Delete(string username)
        {
            try
            {
                List<SqlParameter> Params = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "username", Value = username.AsString() }
                };
                return await SqlHelper.ExecuteAsync(SqlHelper.strConn, "usp_usersDelete", Params, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> Edit(Users model)
        {
            try
            {
                List<SqlParameter> Params = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "username", Value = model.username.AsString() },
                    new SqlParameter() { ParameterName = "email", Value = model.email.AsString() },
                    new SqlParameter() { ParameterName = "phone", Value = model.phone.AsString() },
                    new SqlParameter() { ParameterName = "updated_by", Value = model.updated_by.AsString() }
                };
                return await SqlHelper.ExecuteAsync(SqlHelper.strConn, "usp_usersUpdate", Params, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Users> Get()
        {
            try
            {
                DataTable dt = new DataTable();
                List<Users> list = new List<Users>();
                dt = SqlHelper.GetDatatableSync(SqlHelper.strConn, "usp_usersSelect", null, CommandType.StoredProcedure);
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new Users
                    {
                        username = row["username"].AsString(),
                        email = row["email"].AsString(),
                        phone = row["phone"].AsString(),
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

        public Users GetRow(string username)
        {
            try
            {
                DataTable dt = new DataTable();
                Users data = new Users();
                List<SqlParameter> Params = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "username", Value = username.AsString() }
                };
                dt = SqlHelper.GetDatatableSync(SqlHelper.strConn, "usp_usersSelectRow", Params, CommandType.StoredProcedure);
                if (dt.Rows.Count > 0)
                {
                    data.id = dt.Rows[0]["id"].AsInt();
                    data.username = dt.Rows[0]["username"].AsString();
                    data.email = dt.Rows[0]["email"].AsString();
                    data.phone = dt.Rows[0]["phone"].AsString();
                    data.created_at = dt.Rows[0]["created_at"].AsDateTime();
                }

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Users> login(string username, string password)
        {
            try
            {
                DataTable dt = new DataTable();
                Users data = new Users();
                List<SqlParameter> Params = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "username", Value = username.AsString() },
                    new SqlParameter() { ParameterName = "password", Value = password.AsString() }
                };
                dt = await SqlHelper.GetDatatableAsync(SqlHelper.strConn, "usp_user_login", Params, CommandType.StoredProcedure);
                if (dt.Rows.Count > 0)
                {
                    data.id = dt.Rows[0]["id"].AsInt();
                    data.username = dt.Rows[0]["username"].AsString();
                    data.email = dt.Rows[0]["email"].AsString();
                    data.phone = dt.Rows[0]["phone"].AsString();
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