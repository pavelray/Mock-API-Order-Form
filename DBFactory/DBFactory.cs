using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DatabaseFactory
{
    public class DBFactory
    {
        private static DBFactory _dBFactory;

        public static DBFactory GetInstance()
        {
            if(_dBFactory == null)
            {
                _dBFactory = new DBFactory();
            }

            return _dBFactory;
        }

        public List<T> GetData<T>(IDbConnection connection, CommandDefinition command) where T : class
        {
            List<T> data = null;

            try
            {
                connection.Open();
                data = (List<T>)connection.Query<T>(command);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return data;
        }

        public int ExecuteNonQuery<T>(IDbConnection connection, CommandDefinition command, T obj) where T : class
        {
            int res = -1;

            try
            {
                connection.Open();
                res = connection.Query<int>(command).Single();
            }
            catch (Exception e)
            {
                string msg = e.Message;
            }
            finally
            {
                connection.Close();
            }

            return res;
        }

        public int ExecuteNonQuery(IDbConnection connection, string query, DynamicParameters paramsArr)
        {
            int res = -1;

            try
            {
                connection.Open();
                res = connection.Query<int>(query,
                     paramsArr,
                     commandType: CommandType.Text
                 ).Single();
            }
            catch (Exception e)
            {
                string msg = e.Message;
            }
            finally
            {
                connection.Close();
            }

            return res;
        }

        public int ExecuteNonQueryStoredProcedure(IDbConnection connection, string sp, DynamicParameters paramsArr)
        {
            int res = -1;

            try
            {
                connection.Open();
                res = connection.Query<int>(sp,
                     paramsArr,
                     commandType: CommandType.StoredProcedure
                 ).Single();
            }
            catch (Exception e)
            {
                string msg = e.Message;
            }
            finally
            {
                connection.Close();
            }

            return res;
        }

        public List<T> ExecuteStoredProcedure<T>(IDbConnection connection, string sp, params object[] paramsArr) where T : class
        {
            List<T> data = null;
            try
            {
                connection.Open();
                data = connection.Query<T>(sp,
                     paramsArr,
                     commandType: CommandType.StoredProcedure
                 ).ToList();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return data;
        }

        public IDbConnection GetConnection(DbServer server)
        {
            IDbConnection connection;

            switch (server)
            {
                case DbServer.SQLSERVER:
                    //connection = new SqlConnection(Environment.GetEnvironmentVariable("SqlServerDbConnection"));
                    connection = new SqlConnection("Data Source=.;Initial Catalog=React_Form_Data;Integrated Security=True;");
                    break;
                default:
                    connection = null;
                    break;
            }

            return connection;
        }
    }

    public enum DbServer
    {
        SQLSERVER
    }
}

