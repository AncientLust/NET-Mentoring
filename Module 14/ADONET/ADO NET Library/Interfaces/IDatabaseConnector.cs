using Microsoft.Data.SqlClient;

namespace ADO_NET_Library.Interfaces;
internal interface IDatabaseConnector : IDisposable
{
    public SqlConnection OpenConnection();
    public void CloseConnection();
}