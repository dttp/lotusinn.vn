using System;
using System.Data;
using System.Data.SqlClient;
using LotusInn.Core;

namespace Lotusinn.Data.DataAdapter
{
    public class BaseAdapter
    {
        public void Call(string spName, SqlParameter[] parameters)
        {
            SqlHelper.ExecuteNonQuery(ConfigManager.ConnectionString, spName, parameters);
        }

        public T Call<T>(string spName, SqlParameter[] parameters)
        {
            return SqlHelper.ExecuteScalar<T>(ConfigManager.ConnectionString, spName, parameters);
        }

        public T Call<T>(string spName, SqlParameter[] parameters, Func<IDataReader, T> reader)
        {
            return SqlHelper.ExecuteReader(ConfigManager.ConnectionString, spName, parameters, reader);
        }
    }
}