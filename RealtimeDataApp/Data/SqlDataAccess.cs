using Dapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.EventArgs;

namespace RealtimeDataApp.Data
{
    public class SqlDataAccess : ISqlDataAccess
    {

        private readonly IConfiguration _config;


        public string ConnectionStringName { get; set; } = "Default";

        public SqlDataAccess(IConfiguration configuration)
        {

            this._config = configuration;

        }



        public async Task<IEnumerable<T>> LoadData<T, U>(string sql, U parameters)
        {
            string connectionString = _config.GetConnectionString(ConnectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var data = await connection.QueryAsync<T>(sql, parameters);

                return data;
            }
        }

        public async Task SaveData<T>(string sql, T parameters)
        {
            string connectionString = _config.GetConnectionString(ConnectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var data = await connection.ExecuteAsync(sql, parameters);
            }
        }
    }
}
