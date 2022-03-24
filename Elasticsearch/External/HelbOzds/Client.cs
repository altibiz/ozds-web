using System;
using System.Threading;
using System.Data;
using System.Data.SqlClient;

namespace Elasticsearch.HelbOzds {
  public sealed partial class Client : IClient {
    public Client()
        : this(EnvironmentExtensions.GetEnvironmentVariable(
              "HELB_OZDS_CONNECTION_STRING")) {}

    public Client(
        string sqlConnectionString, bool shouldRetryOpenClose = true) {
      this._connection = new SqlConnection(sqlConnectionString);
      this._shouldRetryOpenClose = shouldRetryOpenClose;

      Console.WriteLine($"Connecting {Source} to {sqlConnectionString}");
      if (this._shouldRetryOpenClose) {
        bool retry = false;
        do {
          try {
            this._connection.Open();
            retry = false;
          } catch (SqlException sqlException) {
            Console.WriteLine($"Failed opening connection to {Source}");
            Console.WriteLine("Reason", sqlException.Message);
            Console.WriteLine("Retrying in 5 seconds...");
            retry = true;
            Thread.Sleep(5000);
          }
        } while (retry);
      } else {
        this._connection.Open();
      }
    }

    ~Client() {
      if (_connection.State != ConnectionState.Closed) {
        if (this._shouldRetryOpenClose) {
          bool retry = false;
          do {
            try {
              this._connection.Close();
              retry = false;
            } catch (SqlException sqlException) {
              Console.WriteLine("Failed closing connection to", this.Source);
              Console.WriteLine("Reason", sqlException.Message);
              Console.WriteLine("Retrying in 5 seconds...");
              retry = true;
              Thread.Sleep(5000);
            }
          } while (retry);
        } else {
          this._connection.Close();
        }
      }
    }

    private const string s_source = "HelbOzds";

    private bool _shouldRetryOpenClose { get; init; }
    private SqlConnection _connection { get; init; }
  }
}
