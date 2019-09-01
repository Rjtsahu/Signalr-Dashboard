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

        private readonly DbConnection _dbConnection;
        private DbCommand _dbCommand;
        private readonly ISqlQueryProvider _sqlQueryProvider;

        public BaseSqlOperation(DbConnection dbConnection, DbCommand dbCommand, ISqlQueryProvider sqlQueryProvider)
        {
            _dbConnection = dbConnection;
            _dbCommand = dbCommand;
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


        public T Select<T>(SelectSqlQuery selectSql, params object[] parameters)
        {
            var sqlQuery = _sqlQueryProvider.GetSql(selectSql);

            return Select<T>(sqlQuery, parameters);
        }

        public T Select<T>(string selectRawSql, params object[] parameters)
        {
            _dbCommand = AddSqlParameters(_dbCommand, selectRawSql, parameters);

            try
            {
                _dbCommand.Connection = _dbConnection;
                _dbConnection.Open();
                _dbCommand.CommandText = selectRawSql;

                var reader = _dbCommand.ExecuteReader();

                return reader.ToObject<T>();
            }
            catch (Exception e)
            {
                LogHelper.Log("Error ", e.Message, e.StackTrace);
                throw (SqlOperationException)e;
            }
            finally
            {
                _dbConnection.Close();
            }
        }

        public IList<T> SelectMultiple<T>(SelectSqlQuery selectSql, params object[] parameters)
        {
            var sqlQuery = _sqlQueryProvider.GetSql(selectSql);

            return SelectMultiple<T>(sqlQuery, parameters);
        }

        public IList<T> SelectMultiple<T>(string selectRawSql, params object[] parameters)
        {
            _dbCommand = AddSqlParameters(_dbCommand, selectRawSql, parameters);

            try
            {
                _dbConnection.Open();
                _dbCommand.CommandText = selectRawSql;
                _dbCommand.Connection = _dbConnection;

                var reader = _dbCommand.ExecuteReader();

                return reader.ToList<T>();
            }
            catch (Exception e)
            {
                LogHelper.Log("Error ", e.Message, e.StackTrace);
                throw (SqlOperationException)e;
            }
            finally
            {
                _dbConnection.Close();
            }
        }

        protected abstract DbCommand AddSqlParameters(DbCommand dbCommand, string sql, params object[] parameters);

        private int ExecuteNonQuery(string sqlQuery, params object[] parameters)
        {
            _dbCommand = AddSqlParameters(_dbCommand, sqlQuery, parameters);

            var result = -1;
            try
            {
                _dbConnection.Open();
                _dbCommand.CommandText = sqlQuery;
                _dbCommand.Connection = _dbConnection;
                result = _dbCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                LogHelper.Log("Error ", e.Message, e.StackTrace);
                throw (SqlOperationException)e;
            }
            finally
            {
                _dbConnection.Close();
            }
            return result;
        }
    }
}
