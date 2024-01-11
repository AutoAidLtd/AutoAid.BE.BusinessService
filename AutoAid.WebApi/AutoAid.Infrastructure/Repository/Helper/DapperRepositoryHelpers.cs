using Dapper;
using Npgsql;
using System.Data;

namespace AutoAid.Infrastructure.Repository.Helper
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2200:Rethrow to preserve stack details", Justification = "<Pending>")]
    public static class DapperRepositoryHelpers
    {
        private static string ToPostgresStoredStatement(this string storedName, DynamicParameters param, string[] resultParams)
        {
            //DiagnosticListener diagnosticListener = Engine.ContainerManager.Resolve<DiagnosticListener>("Npgsql_Listener");
            //diagnosticListener.Write("parameters", param);
            string empty = string.Empty;
            if (param != null)
            {
                IEnumerable<string> values = param.ParameterNames.Select((string x) => "@" + x);
                empty = "(" + string.Join(",", values) + ")";
            }
            else
            {
                empty = "()";
            }

            string fetchQuery = string.Empty;
            if (resultParams?.Any() ?? false)
            {
                resultParams.ToList().ForEach(delegate (string x)
                {
                    fetchQuery = fetchQuery + " FETCH ALL IN " + x + ";";
                });
            }

            return "CALL " + storedName + empty + "; " + fetchQuery;
        }

        private static void Reconnect(this IDbConnection connection)
        {
            NpgsqlConnection npgsqlConnection = connection as NpgsqlConnection;
            if (npgsqlConnection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
            {
                npgsqlConnection.Open();
            }
        }

        public static async Task<IEnumerable<T>> QueryStoredProcPgSql<T>(this IDbConnection connection, 
            string procName, DynamicParameters parameters, string resultParam, IDbTransaction? tran = null)
        {
            connection.Reconnect();
            IDbTransaction transaction = ((tran == null) ? connection.BeginTransaction() : tran);
            try
            {
                string query = procName.ToPostgresStoredStatement(parameters, new string[1] { resultParam });
                SqlMapper.GridReader multi = await connection.QueryMultipleAsync(query, parameters, commandType: CommandType.Text, transaction: transaction, commandTimeout: 300);
                await multi.ReadAsync<object>();
                IEnumerable<T> result = await multi.ReadAsync<T>();
                if (tran == null)
                {
                    transaction.Commit();
                }

                return result;
            }
            catch (NpgsqlException ex)
            {
                if (ex.Message.Contains("terminating connection due to administrator command"))
                {
                    return await connection.QueryStoredProcPgSql<T>(procName, parameters, resultParam, tran);
                }

                transaction?.Rollback();
                throw ex;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public static async Task<T?> QueryFirstStoredProcPgSql<T>(this IDbConnection connection, 
            string procName, DynamicParameters parameters, string resultParam, IDbTransaction? tran = null)
        {
            connection.Reconnect();
            IDbTransaction transaction = ((tran == null) ? connection.BeginTransaction() : tran);
            try
            {
                string query = procName.ToPostgresStoredStatement(parameters, new string[1] { resultParam });
                SqlMapper.GridReader multi = await connection.QueryMultipleAsync(query, parameters, commandType: CommandType.Text, transaction: transaction, commandTimeout: 300);
                await multi.ReadAsync<object>();
                IEnumerable<T> result = await multi.ReadAsync<T>();
                if (tran == null)
                {
                    transaction.Commit();
                }

                return result.FirstOrDefault();
            }
            catch (NpgsqlException ex)
            {
                if (ex.Message.Contains("terminating connection due to administrator command"))
                {
                    return await connection.QueryFirstStoredProcPgSql<T>(procName, parameters, resultParam, tran);
                }

                transaction?.Rollback();
                throw ex;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public static async Task<SqlMapper.GridReader> QueryMultiStoredProcPgSql(this IDbConnection connection,
            string procName, DynamicParameters parameters, params string[] resultParams)
        {
            connection.Reconnect();
            try
            {
                string query = procName.ToPostgresStoredStatement(parameters, resultParams);
                CommandType? commandType = CommandType.Text;
                SqlMapper.GridReader result = await connection.QueryMultipleAsync(query, parameters, null, null, commandType);
                await result.ReadAsync<object>();
                return result;
            }
            catch (NpgsqlException ex)
            {
                if (ex.Message.Contains("terminating connection due to administrator command"))
                {
                    return await connection.QueryMultiStoredProcPgSql(procName, parameters, resultParams);
                }

                throw ex;
            }
            catch
            {
                throw;
            }
        }

        public static async Task<int> ExecuteStoredProcPgSql(this IDbConnection connection, 
            string procName, DynamicParameters parameters, string resultParam, IDbTransaction? tran = null)
        {
            connection.Reconnect();
            IDbTransaction transaction = ((tran == null) ? connection.BeginTransaction() : tran);
            try
            {
                parameters.Add(resultParam, 0);
                string query = procName.ToPostgresStoredStatement(parameters, null);
                CommandType? commandType = CommandType.Text;
                IEnumerable<int> result = await (await connection.QueryMultipleAsync(query, parameters, transaction, null, commandType)).ReadAsync<int>();
                if (tran == null)
                {
                    transaction.Commit();
                }

                return result.First();
            }
            catch (NpgsqlException ex)
            {
                if (ex.Message.Contains("terminating connection due to administrator command"))
                {
                    return await connection.ExecuteStoredProcPgSql(procName, parameters, resultParam, tran);
                }

                transaction?.Rollback();
                throw ex;
            }
            catch
            {
                transaction?.Rollback();
                throw;
            }
        }

        public static async Task<T> ExecuteStoredProcPgSql<T>(this IDbConnection connection, 
            string procName, DynamicParameters parameters, string resultParam, IDbTransaction? tran = null)
        {
            connection.Reconnect();
            IDbTransaction transaction = ((tran == null) ? connection.BeginTransaction() : tran);
            try
            {
                parameters.Add(resultParam, default(T));
                string query = procName.ToPostgresStoredStatement(parameters, null);
                CommandType? commandType = CommandType.Text;
                IEnumerable<T> result = await (await connection.QueryMultipleAsync(query, parameters, transaction, null, commandType)).ReadAsync<T>();
                if (tran == null)
                {
                    transaction.Commit();
                }

                return result.First();
            }
            catch (NpgsqlException ex)
            {
                if (ex.Message.Contains("terminating connection due to administrator command"))
                {
                    return await connection.ExecuteStoredProcPgSql<T>(procName, parameters, resultParam, tran);
                }

                transaction?.Rollback();
                throw ex;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
