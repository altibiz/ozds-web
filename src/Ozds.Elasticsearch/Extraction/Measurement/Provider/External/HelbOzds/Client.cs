using System.Data;
using System.Data.SqlClient;

namespace Ozds.Elasticsearch.HelbOzds;

public sealed partial class Client : IClient, IDisposable
{
  public const string HelbOzdsSource = "HelbOzds";

  public Client(IConfiguration conf, ILogger<Client> logger)
  {
    var section = conf
      .GetSection("Ozds")
      .GetSection("Elasticsearch")
      .GetSection("External")
      .GetSection("HelbOzds")
      .GetSection("Client");

    Db = new SqlConnection(section.GetNonNullValue<string>("connectionString"));

    Logger = logger;
    // OpenSqlConnection();
  }

  public void Dispose()
  {
    // CloseSqlConnection();
  }

  private void OpenSqlConnection()
  {

    bool retry = false;
    do
    {
      try
      {
        Db.Open();
        retry = false;
      }
      catch (SqlException sqlException)
      {
        Logger.LogWarning(
            $"Failed opening {Source} connection to {Db.ConnectionString}\n" +
                $"Reason: {sqlException.Message}" + "Retrying in 5 seconds...",
            LogLevel.Warning);
        retry = true;
        Thread.Sleep(5000);
      }
    } while (retry);
    Logger.LogInformation(
        $"Opened {Source} connection to {Db.ConnectionString}");
  }

  private void CloseSqlConnection()
  {
    if (Db.State != ConnectionState.Closed)
    {
      bool retry = false;
      do
      {
        try
        {
          Db.Close();
          retry = false;
        }
        catch (SqlException sqlException)
        {
          Logger.LogWarning($"Failed closing connection to {Source}\n" +
                                $"Reason: {sqlException.Message}" +
                                "Retrying in 5 seconds...",
              LogLevel.Warning);
          retry = true;
          Thread.Sleep(5000);
        }
      } while (retry);
    }
    else
    {
      Db.Close();
    }
    Logger.LogInformation(
        $"Closed {Source} connection to {Db.ConnectionString}");
  }

  private IDbConnection Db { get; }
  private ILogger Logger { get; }
}
