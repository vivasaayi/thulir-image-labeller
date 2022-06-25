using System.Data;
using Dapper;
using ImageLabeller.Dals;
using Npgsql;
using NpgsqlTypes;
using System.Text.Json;

namespace ImageLabeller.DbModels;

public class GenericTypeHandler<T> : SqlMapper.TypeHandler<T>
{
    public GenericTypeHandler() { }
    public static JObjectHandler Instance { get; } = new JObjectHandler();
    public override T Parse(object value)
    {
        var type = typeof(T);
        var json = value.ToString();
        return json == null ? JsonSerializer.Deserialize<T>("") : JsonSerializer.Deserialize<T>(value?.ToString());
    }
        
    public override void SetValue(IDbDataParameter parameter, T value)
    {	
        parameter.Value = JsonSerializer.Serialize(value);
        ((NpgsqlParameter)parameter).NpgsqlDbType = NpgsqlDbType.Jsonb;
    }
}