using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Penjualan.Helper
{
    public static class SqlHelper
    {
        public static string strConn = ConfigurationManager.ConnectionStrings["WahanaConnection"].ConnectionString;

        // EXEC ASYNC
        public static Task<int> ExecuteAsync(string sConnectionString, string sSQL, List<SqlParameter> parameters, CommandType commandType)
        {
            return Task.Run(() =>
            {
                using (var newConnection = new SqlConnection(sConnectionString))
                using (var newCommand = new SqlCommand(sSQL, newConnection))
                {
                    newCommand.CommandType = commandType;
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            newCommand.Parameters.AddWithValue("@" + param.ParameterName, param.Value);
                        }
                    }
                    newConnection.Open();
                    return newCommand.ExecuteNonQuery();
                }
            });
        }
        // EXEC SYNC
        public static int ExecuteSync(string sConnectionString, string sSQL, List<SqlParameter> parameters, CommandType commandType)
        {
            using (var newConnection = new SqlConnection(sConnectionString))
            using (var newCommand = new SqlCommand(sSQL, newConnection))
            {
                newCommand.CommandType = commandType;
                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        newCommand.Parameters.AddWithValue("@" + param.ParameterName, param.Value);
                    }
                }
                newConnection.Open();
                return newCommand.ExecuteNonQuery();
            }

        }
        // RETURN DATASET ASYNC
        public static Task<DataSet> GetDataSetAsync(string sConnectionString, string sSQL, List<SqlParameter> parameters, CommandType commandType)
        {
            return Task.Run(() =>
            {
                using (SqlConnection newConnection = new SqlConnection(sConnectionString))
                using (SqlDataAdapter mySQLAdapter = new SqlDataAdapter(sSQL, newConnection))
                {
                    mySQLAdapter.SelectCommand.CommandType = commandType;
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            mySQLAdapter.SelectCommand.Parameters.AddWithValue("@" + param.ParameterName, param.Value);
                        }
                    }
                    DataSet myDataSet = new DataSet();
                    mySQLAdapter.Fill(myDataSet);
                    return myDataSet;
                }
            });
        }
        // RETURN DATASET SYNC
        public static DataSet GetDataSetSync(string sConnectionString, string sSQL, List<SqlParameter> parameters, CommandType commandType)
        {
            using (SqlConnection newConnection = new SqlConnection(sConnectionString))
            using (SqlDataAdapter mySQLAdapter = new SqlDataAdapter(sSQL, newConnection))
            {
                mySQLAdapter.SelectCommand.CommandType = commandType;
                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        mySQLAdapter.SelectCommand.Parameters.AddWithValue("@" + param.ParameterName, param.Value);
                    }
                }
                DataSet myDataSet = new DataSet();
                mySQLAdapter.Fill(myDataSet);
                return myDataSet;
            }

        }
        // RETURN DATATABLE ASYCN
        public static Task<DataTable> GetDatatableAsync(string sConnectionString, string sSQL, List<SqlParameter> parameters, CommandType commandType)
        {
            return Task.Run(() =>
            {
                using (SqlConnection newConnection = new SqlConnection(sConnectionString))
                using (SqlDataAdapter mySQLAdapter = new SqlDataAdapter(sSQL, newConnection))
                {
                    mySQLAdapter.SelectCommand.CommandType = commandType;
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            mySQLAdapter.SelectCommand.Parameters.AddWithValue("@" + param.ParameterName, param.Value);
                        }
                    }
                    DataTable myDataSet = new DataTable();
                    mySQLAdapter.Fill(myDataSet);
                    return myDataSet;
                }
            });
        }
        // RETURN DATATABLE SYCN
        public static DataTable GetDatatableSync(string sConnectionString, string sSQL, List<SqlParameter> parameters, CommandType commandType)
        {
            using (SqlConnection newConnection = new SqlConnection(sConnectionString))
            using (SqlDataAdapter mySQLAdapter = new SqlDataAdapter(sSQL, newConnection))
            {
                mySQLAdapter.SelectCommand.CommandType = commandType;
                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        mySQLAdapter.SelectCommand.Parameters.AddWithValue("@" + param.ParameterName, param.Value);
                    }
                }
                DataTable myDataSet = new DataTable();
                mySQLAdapter.Fill(myDataSet);
                return myDataSet;
            }
        }
    }
}