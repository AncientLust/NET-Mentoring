using ADO_NET_Library.Interfaces;
using Microsoft.Data.SqlClient;

namespace ADO_NET_Library.Database_connections;

public class SqlServerConnector : IDatabaseConnector
{
    private const string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ado_module_db;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
    private SqlConnection? _connection;

    public SqlConnection OpenConnection()
    {
        _connection = new SqlConnection(ConnectionString);
        _connection.Open();
        return _connection;
    }

    public void CloseConnection()
    {
        _connection?.Close();
        _connection?.Dispose();
    }

    public void Dispose()
    {
        CloseConnection();
    }
}