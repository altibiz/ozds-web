using System;
using System.Linq;
using System.Collections.Generic;
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
  public Client(IHostEnvironment env, ILogger<Client> logger,
      IConfiguration conf, IEnumerable<IMeasurementProvider> providers) {
    Env = env;
    Logger = logger;

    var section = conf.GetSection("Elasticsearch").GetSection("Client");
    var serverUri = section.GetNonNullValue<string>("serverUri");
    var caPath = section.GetNonNullValue<string>("caPath");
    var user = section.GetNonNullValue<string>("user");
    var password = section.GetNonNullValue<string>("password");

    var settings = new ConnectionSettings(new Uri(serverUri))
                       .ServerCertificateValidationCallback(
                           CertificateValidations.AuthorityIsRoot(
                               new X509Certificate(caPath)))
                       .BasicAuthentication(user, password);
    if (Env.IsDevelopment()) {
      settings = settings.PrettyJson(true).DisableDirectStreaming();
    }

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

    Logger.LogInformation($"Successfully connected {Source} to {serverUri}");

    Providers = providers.ToList();
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

    Providers = other.Providers;

    IndexSuffix = indexSuffix ?? "";
  }
#endregion // Prototype

  private IHostEnvironment Env { get; }
  private ILogger Logger { get; }

  private IElasticClient Elasticsearch { get; init; }

  private List<IMeasurementProvider> Providers { get; }

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
    get => s_measurementIndexDebugPrefix + IndexSuffix;
  }

  private string DeviceIndexName {
    get => s_deviceIndexDebugPrefix + IndexSuffix;
  }

  private string LogIndexName { get => s_logIndexDebugPrefix + IndexSuffix; }

#if DEBUG
  private const string s_measurementIndexDebugPrefix =
      "ozds.debug.measurements";
  private const string s_deviceIndexDebugPrefix = "ozds.debug.devices";
  private const string s_logIndexDebugPrefix = "ozds.debug.log";
#else
  private const string s_measurementIndexDebugPrefix = "ozds.measurements";
  private const string s_deviceIndexDebugPrefix = "ozds.devices";
  private const string s_logIndexDebugPrefix = "ozds.log";
#endif
#endregion // Index Names
}
