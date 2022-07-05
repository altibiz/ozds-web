using Nest;

namespace Ozds.Elasticsearch;

public class IndexMapper : IIndexMapper
{
  public
  IReadOnlyDictionary<string, Func<
      IElasticClient, IIndex, Task<CreateIndexResponse>>>
  AsyncCreatorFor { get; } =
    new Dictionary<string, Func<
      IElasticClient, IIndex, Task<CreateIndexResponse>>>
    {
      {
        IndexNamer.Measurements,
        (client, index) => client.Indices
          .CreateAsync(index.Name, i => i
            .Map<Measurement>(m => m
              .AutoMap<Measurement>()))
      },
      {
        IndexNamer.Devices,
        (client, index) => client.Indices
          .CreateAsync(index.Name, i => i
            .Map<Device>(m => m
              .AutoMap<Device>()))
      },
      {
        IndexNamer.Log,
        (client, index) => client.Indices
          .CreateAsync(index.Name, i => i
            .Map<MissingDataLog>(m => m
              .AutoMap<MissingDataLog>())
            .Map<LoadLog>(m => m
              .AutoMap<LoadLog>()))
      }
    };

  public
  IReadOnlyDictionary<string, Func<
      IElasticClient, IIndex, CreateIndexResponse>>
  CreatorFor { get; } =
    new Dictionary<string, Func<
      IElasticClient, IIndex, CreateIndexResponse>>
    {
      {
        IndexNamer.Measurements,
        (client, index) => client.Indices
          .Create(index.Name, i => i
            .Map<Measurement>(m => m
              .AutoMap<Measurement>()))
      },
      {
        IndexNamer.Devices,
        (client, index) => client.Indices
          .Create(index.Name, i => i
            .Map<Device>(m => m
              .AutoMap<Device>()))
      },
      {
        IndexNamer.Log,
        (client, index) => client.Indices
          .Create(index.Name, i => i
            .Map<MissingDataLog>(m => m
              .AutoMap<MissingDataLog>())
            .Map<LoadLog>(m => m
              .AutoMap<LoadLog>()))
      }
    };

  public
  IReadOnlyDictionary<string, Func<
      IElasticClient, IIndex, Task<PutMappingResponse>>>
  AsyncMapperFor { get; } =
    new Dictionary<string, Func<
      IElasticClient, IIndex, Task<PutMappingResponse>>>
    {
      {
        IndexNamer.Measurements,
        async (client, index) =>
          await client.Indices
            .PutMappingAsync<Measurement>(m => m
              .Index(index.Name)
              .AutoMap())
      },
      {
        IndexNamer.Devices,
        async (client, index) =>
          await client.Indices
            .PutMappingAsync<Device>(m => m
              .Index(index.Name)
              .AutoMap())
      },
      {
        IndexNamer.Log,
        async (client, index) =>
          await client.Indices
            .PutMappingAsync<LoadLog>(m => m
              .Index(index.Name)
              .AutoMap()) is { IsValid : false } response ?
          response
        : await client.Indices
            .PutMappingAsync<MissingDataLog>(m => m
              .Index(index.Name)
              .AutoMap())
      }
    };

  public
  IReadOnlyDictionary<string, Func<
      IElasticClient, IIndex, PutMappingResponse>>
  MapperFor { get; } =
    new Dictionary<string, Func<
      IElasticClient, IIndex, PutMappingResponse>>
    {
      {
        IndexNamer.Measurements,
        (client, index) =>
          client.Indices
            .PutMapping<Measurement>(m => m
              .Index(index.Name)
              .AutoMap())
      },
      {
        IndexNamer.Devices,
        (client, index) =>
          client.Indices
            .PutMapping<Device>(m => m
              .Index(index.Name)
              .AutoMap())
      },
      {
        IndexNamer.Log,
        (client, index) =>
          client.Indices
            .PutMapping<LoadLog>(m => m
              .Index(index.Name)
              .AutoMap()) is { IsValid : false } response ?
          response
        : client.Indices
            .PutMapping<MissingDataLog>(m => m
              .Index(index.Name)
              .AutoMap())
      }
    };

  public
  IReadOnlyDictionary<string, Func<
      IElasticClient, IIndex, DeleteIndexResponse>>
  DeleterFor { get; } =
    new Dictionary<string, Func<
      IElasticClient, IIndex, DeleteIndexResponse>>
    {
      {
        IndexNamer.Measurements,
        (client, index) =>
          client.Indices.Delete(index.Name)
      },
      {
        IndexNamer.Devices,
        (client, index) =>
          client.Indices.Delete(index.Name)
      },
      {
        IndexNamer.Log,
        (client, index) =>
          client.Indices.Delete(index.Name)
      }
    };

  public
  IReadOnlyDictionary<string, Func<
      IElasticClient, IIndex, Task<DeleteIndexResponse>>>
  AsyncDeleterFor { get; } =
    new Dictionary<string, Func<
      IElasticClient, IIndex, Task<DeleteIndexResponse>>>
    {
      {
        IndexNamer.Measurements,
        (client, index) =>
          client.Indices.DeleteAsync(index.Name)
      },
      {
        IndexNamer.Devices,
        (client, index) =>
          client.Indices.DeleteAsync(index.Name)
      },
      {
        IndexNamer.Log,
        (client, index) =>
          client.Indices.DeleteAsync(index.Name)
      }
    };
}
