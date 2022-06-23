

using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Newtonsoft.Json.Linq;
using Npgsql;
using NpgsqlTypes;

namespace ImageLabeller.Dals
{
    
    public class JObjectHandler : SqlMapper.TypeHandler<JObject>
    {
        public JObjectHandler() { }
        public static JObjectHandler Instance { get; } = new JObjectHandler();
        public override JObject Parse(object value)
        {
            var json = value.ToString();
            return json == null ? null : JObject.Parse(json);
        }
        public override void SetValue(IDbDataParameter parameter, JObject value)
        {	
            parameter.Value = value?.ToString(Newtonsoft.Json.Formatting.None);
            ((NpgsqlParameter)parameter).NpgsqlDbType = NpgsqlDbType.Jsonb;
        }
    }
    
    public class PostgresDal
    {
        private static PostgresDal Instance;
        private static PostgresConfig _postgresConfig;
        private static IDbConnection Connection; 
        
        private readonly static object _lock = new object();

        public static void Init(PostgresConfig config)
        {
            _postgresConfig = config;
            NpgsqlConnection.GlobalTypeMapper.UseJsonNet();
            SqlMapper.AddTypeHandler(new JObjectHandler());
        }
        
        public static PostgresDal GetInstance()
        {
            lock (_lock)
            {
                if (Instance == null)
                {
                    Instance = new PostgresDal();
                }
            }

            return Instance;
        }

        public static IDbConnection GetConnection()
        {
            lock (_lock)
            {
                if (Connection == null)
                {
                    Connection = new NpgsqlConnection(_postgresConfig.GetConnectionString());
                    Connection.Open();
                }
            }

            return Connection;
            
        }

        public async Task<IEnumerable<T>> ExecuteQuery<T>(string command, Object parameters)
        {
            var result   = await GetConnection().QueryAsync<T>(command, parameters);
            return result;
        }
        
        public async Task InsertRecord(string command, Object parameters)
        {
            await GetConnection().QueryAsync(command, parameters);
        }
    }
}