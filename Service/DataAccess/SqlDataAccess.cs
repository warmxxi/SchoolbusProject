using Common.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Service.DataAccess
{
    public class SqlDataAccess
    {
        private string Connection_String = "";
        private int Connection_Timeout = 300;

        public SqlDataAccess()
        {
            Connection_String = DataConfigHelper.Config.Server.ConnectionString;
        }

        public void ExecuteTransaction(List<SqlCommand> command)
        {
            using (SqlConnection connection = new SqlConnection(Connection_String))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        foreach (var x in command)
                        {
                            x.Connection = connection;
                            x.CommandTimeout = Connection_Timeout;
                            x.Transaction = transaction;
                            x.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                    finally
                    {
                        foreach (var x in command)
                        {
                            x.Dispose();
                        }
                    }
                }
            }
        }

        public void ExecuteNonQuery(SqlCommand command)
        {
            using (SqlConnection connection = new SqlConnection(Connection_String))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                command.Connection = connection;
                command.CommandTimeout = Connection_Timeout;
                command.ExecuteNonQuery();
            }
        }

        public T GetDataObject<T>(SqlCommand command)
        {
            using (SqlConnection connection = new SqlConnection(Connection_String))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                command.Connection = connection;
                command.CommandTimeout = Connection_Timeout;
                return DataModelHelper.CreateObject<T>(command.ExecuteReader());
            }
        }

        public List<T> GetDataList<T>(SqlCommand command)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Connection_String))
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    command.Connection = connection;
                    command.CommandTimeout = Connection_Timeout;
                    return DataModelHelper.CreateList<T>(command.ExecuteReader());
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
