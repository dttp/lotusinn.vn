using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace LotusInn.Core
{
    public class SqlHelper
    {
        public static DataSet GetDataSet(string connectionString, CommandType commandType, string commandText, SqlParameter[] parameters, int timeoutSeconds = 60)
        {
            DataSet ds = null;
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (var comm = CreateCommand(conn, commandType, commandText, parameters, timeoutSeconds))
                {
                    using (DbDataAdapter adapter = new SqlDataAdapter(comm))
                    {
                        ds = new DataSet();
                        ds.EnforceConstraints = false;
                        adapter.Fill(ds);
                    }
                }
            }
            return ds;
        }

        public static TResult ExecuteReader<TResult>(string connectionString, string spName, SqlParameter[] parameters, Func<SqlDataReader, TResult> func)
        {
            return ExecuteReader(connectionString, CommandType.StoredProcedure, spName, parameters, func);
        }
        public static TResult ExecuteReader<TResult>(string connectionString, CommandType commandType, string commandText, SqlParameter[] parameters, Func<SqlDataReader, TResult> func)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (var comm = CreateCommand(conn, commandType, commandText, parameters))
                {
                    var dr = comm.ExecuteReader();
                    var result = func(dr);

                    DisposeCommand(comm);

                    return result;
                }
            }
        }

        public static TResult ExecuteScalar<TResult>(string connectionString, string spName, SqlParameter[] parameters)
        {
            return ExecuteScalar<TResult>(connectionString, CommandType.StoredProcedure, spName, parameters);
        }

        public static TResult ExecuteScalar<TResult>(string connectionString, CommandType commandType, string commandText, SqlParameter[] parameters)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (var comm = CreateCommand(conn, commandType, commandText, parameters))
                {
                    var result = comm.ExecuteScalar();

                    DisposeCommand(comm);

                    return (TResult)result;
                }
            }
        }

        public static void ExecuteNonQuery(string connectionString, string spName, SqlParameter[] parameters)
        {
            ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName, parameters);
        }
        public static void ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, SqlParameter[] parameters)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (var comm = CreateCommand(conn, commandType, commandText, parameters))
                {
                    comm.ExecuteNonQuery();

                    DisposeCommand(comm);
                }
            }
        }

        public static void ExecuteCommand(SqlConnection connection, SqlTransaction transaction, CommandType commandType,
            string commandText, SqlParameter[] parameters)
        {
            using (var command = CreateCommand(connection, commandType, commandText, parameters))
            {
                command.Transaction = transaction;
                command.ExecuteNonQuery();
            }
        }

        public static void StartTransaction(string connectionString, Action<SqlConnection, SqlTransaction> action)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var transaction = conn.BeginTransaction(IdHelper.Generate(32));
                try
                {
                    action.Invoke(conn, transaction);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }                                               
            }
        }

        #region private methods


        private static SqlCommand CreateCommand(SqlConnection conn, CommandType commandType, string commandText, SqlParameter[] parameters, int timeoutSeconds = 60)
        {
            var comm = new SqlCommand(commandText);
            comm.Parameters.Clear();
            if (parameters != null && parameters.Any())
            {
                foreach (var param in parameters)
                {
                    comm.Parameters.Add(param);
                }
            }
            comm.CommandType = commandType;
            comm.CommandTimeout = timeoutSeconds;
            comm.Connection = conn;
            comm.CommandText = commandText;
            return comm;
        }

        private static void DisposeCommand(SqlCommand comm)
        {
            comm.Parameters.Clear();
            comm.Connection.Close();
            comm.Dispose();
        }
        #endregion private methods

    }
}
