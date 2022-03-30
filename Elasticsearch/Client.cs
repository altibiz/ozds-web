using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Nest;
using Elasticsearch.Net;

namespace Elasticsearch;

public interface IClientPrototype {
  public IClient ClonePrototype(string? indexSuffix = null);
}

public sealed partial class Client : IClientPrototype, IClient {
#region Constructors
  public Client(
      IHostEnvironment env, ILogger<Client> logger, IConfiguration conf) {
    Env = env;
    Logger = logger;

    var section = conf.GetSection("Elasticsearch").GetSection("Client");

    var settings =
        new ConnectionSettings(
            new Uri(section.GetNonNullValue<string>("serverUri")))
#if DEBUG
            .PrettyJson(true)
            .DisableDirectStreaming()
#else
            .PrettyJson(false)
#endif
            .ServerCertificateValidationCallback(
                CertificateValidations.AuthorityIsRoot(new X509Certificate(
                    section.GetNonNullValue<string>("caPath"))))
            .BasicAuthentication(section.GetNonNullValue<string>("user"),
                section.GetNonNullValue<string>("password"));

    Elasticsearch = new ElasticClient(settings);
    var pingResponse = Elasticsearch.Ping();
    if (!pingResponse.IsValid) {
      if (Env.IsDevelopment()) {
        throw new WebException(
            $"Could not connect to {Source}\n" +
            $"Ping response information: {pingResponse.DebugInformation}");
      } else {
        throw new WebException($"Could not connect to {Source}\n" +
                               $"Ping response: {pingResponse}");
      }
    }

    Logger.LogInformation(
        $"Successfully connected {Source} to {Elasticsearch}");
  }
#endregion // Constructors

#region Prototype
  public IClient ClonePrototype(string? indexSuffix = null) {
    return new Client(this, indexSuffix);
  }

  private Client(Client other, string? indexSuffix) {
    Env = other.Env;
    Logger = other.Logger;

    Elasticsearch = other.Elasticsearch;

    IndexSuffix = indexSuffix ?? "";
  }
#endregion // Prototype

  private IHostEnvironment Env { get; }
  private ILogger Logger { get; }

  private IElasticClient Elasticsearch { get; init; }

#region Index Suffix
  private string IndexSuffix {
    get => _indexSuffix;
    init {
      _indexSuffix = String.IsNullOrWhiteSpace(value) ? ""
                     : value.StartsWith('.')          ? value
                                                      : $".{value}";

      if (Env.IsDevelopment()) {
        TryDeleteIndices();
      }

      TryCreateIndices();
    }
  }

  private void TryDeleteIndices() {
    Elasticsearch.TryDeleteIndex(MeasurementIndexName);
    Elasticsearch.TryDeleteIndex(DeviceIndexName);
    Elasticsearch.TryDeleteIndex(LogIndexName);
    Logger.LogInformation($"Deleted Elasticsearch indices{ConsoleIndexSuffix}");
  }

  private void TryCreateIndices() {
    Elasticsearch.TryCreateIndex<Measurement>(MeasurementIndexName);
    Elasticsearch.TryCreateIndex<Device>(DeviceIndexName);
    Elasticsearch.TryCreateIndex<Log>(LogIndexName);
    Logger.LogInformation($"Created Elasticsearch indices{ConsoleIndexSuffix}");
  }

  private string ConsoleIndexSuffix {
    get => String.IsNullOrWhiteSpace(IndexSuffix) ? "" : $" '{IndexSuffix}'";
  }

  private string _indexSuffix = "";
#endregion // Index Suffix

#region Index Names
  private string MeasurementIndexName {
    get => s_measurementIndexPrefix + IndexSuffix;
  }

  private string DeviceIndexName { get => s_deviceIndexPrefix + IndexSuffix; }

  private string LogIndexName { get => s_logIndexPrefix + IndexSuffix; }

#if DEBUG
  private const string s_measurementIndexPrefix = "ozds.debug.measurements";
  private const string s_deviceIndexPrefix = "ozds.debug.devices";
  private const string s_logIndexPrefix = "ozds.debug.log";
#else
  private const string s_measurementIndexPrefix = "ozds.measurements";
  private const string s_deviceIndexPrefix = "ozds.devices";
  private const string s_logIndexPrefix = "ozds.log";
#endif
#endregion // Index Names
}
