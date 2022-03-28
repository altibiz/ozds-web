using System;
using System.Threading;
using System.Data;
using System.Data.SqlClient;

namespace Elasticsearch.HelbOzds {
  public sealed partial class Client : IClient {
    public Client()
        : this(EnvironmentExtensions.AssertEnvironmentVariable(
              "HELB_OZDS_SQL_CONNECTION_STRING")) {}

    public Client(
        string sqlConnectionString, bool shouldRetryOpenClose = true) {
      _sqlConnectionString = sqlConnectionString;
      _shouldRetryOpenClose = shouldRetryOpenClose;
      _connection = new SqlConnection(_sqlConnectionString);

      // Console.WriteLine(
      //     $"Opening {Source} connection to {sqlConnectionString}...");
      // if (_shouldRetryOpenClose) {
      //   bool retry = false;
      //   do {
      //     try {
      //       _connection.Open();
      //       retry = false;
      //     } catch (SqlException sqlException) {
      //       Console.WriteLine($"Failed opening connection to {Source}");
      //       Console.WriteLine($"Reason {sqlException.Message}");
      //       Console.WriteLine("Retrying in 5 seconds...");
      //       retry = true;
      //       Thread.Sleep(5000);
      //     }
      //   } while (retry);
      // } else {
      //   _connection.Open();
      // }
    }

    ~Client() {
      // if (_connection.State != ConnectionState.Closed) {
      //   Console.WriteLine(
      //       $"Closing {Source} connection to {_sqlConnectionString}...");
      //   if (_shouldRetryOpenClose) {
      //     bool retry = false;
      //     do {
      //       try {
      //         _connection.Close();
      //         retry = false;
      //       } catch (SqlException sqlException) {
      //         Console.WriteLine("Failed closing connection to", this.Source);
      //         Console.WriteLine("Reason", sqlException.Message);
      //         Console.WriteLine("Retrying in 5 seconds...");
      //         retry = true;
      //         Thread.Sleep(5000);
      //       }
      //     } while (retry);
      //   } else {
      //     _connection.Close();
      //   }
      // }
    }

    private const string s_source = "HelbOzds";

    private bool _shouldRetryOpenClose { get; init; }
    private string _sqlConnectionString { get; init; }
    private SqlConnection _connection { get; init; }
  }
}
