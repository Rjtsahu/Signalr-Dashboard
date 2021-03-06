﻿using System;
using System.Data.SQLite;
using System.Linq;
using System.Text.RegularExpressions;

namespace SignalrDashboard.Extensions
{
    internal static class SqlParameterExtensions
    {
        public static void MapSQLiteParameters(this SQLiteParameterCollection sqliteParameter, string sql, params object[] parameters)
        {
            var paramsInSql = Regex.Matches(sql, "@[a-zA-Z0-9_]*").Cast<Match>().Select(match => match.Value).ToList();
            if (paramsInSql.Count != parameters.Length)
            {
                throw new ArgumentException($"No of argument in sql({paramsInSql.Count}) doesn't match with arguments in parameter list ({parameters?.Length})." +
                    $" Require parameters : {string.Join(" , ", paramsInSql)}");
            }

            for (int i = 0; i < paramsInSql.Count; i++)
            {
                sqliteParameter.AddWithValue(paramsInSql[i], parameters[i]);
            }
        }
    }
}
