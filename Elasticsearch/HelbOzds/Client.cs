using System.Data.SqlClient;

namespace Elasticsearch.HelbOzds {
  public sealed partial class Client : IClient {
#if DEBUG
    public const string DefaultSqlConnectionString = "dummy";
#else
    // TODO: to something else
    public const string DefaultSqlConnectionString = "dummy";
#endif

    public Client() : this(DefaultSqlConnectionString) {}

    public Client(string sqlConnectionString) {
      this._connection = new SqlConnection(sqlConnectionString);
      this._connection.Open();
    }

    ~Client() { this._connection.Close(); }

    private SqlConnection _connection { get; init; }
  }
}
