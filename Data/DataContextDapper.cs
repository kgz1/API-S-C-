using System.Data;
using System.Data.Common;
using System.Reflection.Metadata;
using Dapper;
using Microsoft.Data.SqlClient;

namespace BobiApi.Data

{

  class DataContextDapper
  {
    private IConfiguration _config;
    public DataContextDapper(IConfiguration config)
    {
        _config = config;
    }

    public IEnumerable<T> LoadData<T>(string sql)
    {
        IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        return dbConnection.Query<T>(sql);
    }
    
    public T LoadDataSingle<T>(string sql)
    {
        IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        return dbConnection.QuerySingle<T>(sql);
    }

    public bool ExecuteSql(string sql)
    {
          IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
          return dbConnection.Execute(sql) > 0;
    }
     public int ExecuteSqlwithRowCount(string sql)
    {
          IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
          return dbConnection.Execute(sql);
    }
    public bool ExecuteSqlWithParameters(string sql, List<SqlParameter> parameters)
    {
      SqlCommand commandwithParams = new SqlCommand(sql);
      foreach (SqlParameter parameter in parameters)
      {
        commandwithParams.Parameters.Add(parameter);
      }
      SqlConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
      dbConnection.Open();
      commandwithParams.Connection = dbConnection;
      int rowsAffected = commandwithParams.ExecuteNonQuery();
      dbConnection.Close();
      return rowsAffected > 0;
    }
  }
}