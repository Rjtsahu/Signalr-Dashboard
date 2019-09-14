using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Sahurjt.Signalr.Dashboard.Extensions;
using Sahurjt.Signalr.Dashboard.Helpers;

namespace Sahurjt.Signalr.Dashboard.DataStore
{
    internal abstract class BaseSqlOperation : ISqlOperation
    {
        protected abstract string ProviderName { get; }

        private readonly ISqlQueryProvider _sqlQueryProvider;

        protected BaseSqlOperation(ISqlQueryProvider sqlQueryProvider)
        {
            _sqlQueryProvider = sqlQueryProvider;
        }

        public int Execute(ExecuteSqlQuery executeSql, params object[] parameters)
        {
            return ExecuteNonQuery(_sqlQueryProvider.GetSql(executeSql), parameters);
        }

        public async Task<int> ExecuteAsync(ExecuteSqlQuery executeSql, params object[] parameters)
        {
            return await Task.Run(() => (ExecuteNonQuery(_sqlQueryProvider.GetSql(executeSql), parameters)));
        }

        public int Execute(string executeRawSql, params object[] parameters)
        {
            return ExecuteNonQuery(executeRawSql, parameters);
        }

        public async Task<int> ExecuteAsync(string executeRawSql, params object[] parameters)
        {
            return await Task.Run(() => (ExecuteNonQuery(executeRawSql, parameters)));
        }

        public T Select<T>(SelectSqlQuery selectSql, params object[] parameters)
        {
            var sqlQuery = _sqlQueryProvider.GetSql(selectSql);

            return Select<T>(sqlQuery, parameters);
        }

        public T Select<T>(string selectRawSql, params object[] parameters)
        {
            using (var dbConnection = GetDbConnection())
            {
                using (var dbCmd = GetCommandParameter(selectRawSql, parameters))
                {
                    try
                    {
                        dbCmd.Connection = dbConnection;
                        dbCmd.CommandText = selectRawSql;

                        dbConnection.Open();

                        var reader = dbCmd.ExecuteReader();

                        var result = reader.ToObject<T>();

                        reader.Close();
                        return result;
                    }
                    catch (Exception e)
                    {
                        LogHelper.Log("Error ", e.Message, e.StackTrace);
                        throw new SqlOperationException(e.Message, e);
                    }
                    finally
                    {
                        dbConnection.Close();
                    }

                }
            }

        }

        public IList<T> SelectMultiple<T>(SelectSqlQuery selectSql, params object[] parameters)
        {
            var sqlQuery = _sqlQueryProvider.GetSql(selectSql);

            return SelectMultiple<T>(sqlQuery, parameters);
        }

        public IList<T> SelectMultiple<T>(string selectRawSql, params object[] parameters)
        {
            using (var dbConnection = GetDbConnection())
            {
                using (var dbCmd = GetCommandParameter(selectRawSql, parameters))
                {
                    try
                    {
                        dbCmd.Connection = dbConnection;
                        dbCmd.CommandText = selectRawSql;

                        dbConnection.Open();

                        var reader = dbCmd.ExecuteReader();

                        var result = reader.ToList<T>();

                        reader.Close();
                        return result;
                    }
                    catch (Exception e)
                    {
                        LogHelper.Log("Error ", e.Message, e.StackTrace);
                        throw new SqlOperationException(e.Message, e);
                    }
                    finally
                    {
                        dbConnection.Close();
                    }

                }
            }
        }

        private int ExecuteNonQuery(string sqlQuery, params object[] parameters)
        {
            var result = -1;

            using (var dbConnection = GetDbConnection())
            {
                using (var dbCmd = GetCommandParameter(sqlQuery, parameters))
                {
                    try
                    {
                        dbCmd.Connection = dbConnection;
                        dbCmd.CommandText = sqlQuery;

                        dbConnection.Open();

                        result = dbCmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        LogHelper.Log("Error ", e.Message, e.StackTrace);
                        throw new SqlOperationException(e.Message, e);
                    }
                    finally
                    {
                        dbConnection.Close();
                    }
                }
            }
            return result;
        }

        protected abstract DbCommand GetCommandParameter(string sql, params object[] parameters);

        protected abstract DbConnection GetDbConnection();

    }
}
