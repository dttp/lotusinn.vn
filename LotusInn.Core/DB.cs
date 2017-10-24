using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace LotusInn.Core
{
    public class DB
    {
        public string ConnectionString { get; set; }        

        public DB(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public TResult Get<TResult>(string spName, Func<SqlDataReader, TResult> processor, params KeyValuePair<string, object>[] paramters )
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var cmd = new SqlCommand(spName, connection)
                {                    
                    CommandType = CommandType.StoredProcedure                    
                };
                foreach (var param in paramters)
                {                    
                    cmd.Parameters.AddWithValue(param.Key, param.Value);
                }
                return processor(cmd.ExecuteReader());
            }
        }
    }
}
