using Microsoft.Data.SqlClient;

namespace ADO_NET_Library.Interfaces;
public interface IDatabaseConnector : IDisposable
{
    public SqlConnection OpenConnection();
    public void CloseConnection();
}