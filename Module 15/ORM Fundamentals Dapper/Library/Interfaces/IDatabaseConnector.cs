using Microsoft.Data.SqlClient;

namespace Library.Interfaces;
public interface IDatabaseConnector : IDisposable
{
    public SqlConnection OpenConnection();
    public void CloseConnection();
}