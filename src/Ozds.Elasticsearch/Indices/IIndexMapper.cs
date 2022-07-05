using Nest;

namespace Ozds.Elasticsearch;

public interface IIndexMapper
{
  public
  IReadOnlyDictionary<string, Func<
      IElasticClient, IIndex, CreateIndexResponse>>
  CreatorFor { get; }

  public
  IReadOnlyDictionary<string, Func<
      IElasticClient, IIndex, Task<CreateIndexResponse>>>
  AsyncCreatorFor { get; }

  public
  IReadOnlyDictionary<string, Func<
      IElasticClient, IIndex, PutMappingResponse>>
  MapperFor { get; }

  public
  IReadOnlyDictionary<string, Func<
      IElasticClient, IIndex, Task<PutMappingResponse>>>
  AsyncMapperFor { get; }

  public
  IReadOnlyDictionary<string, Func<
      IElasticClient, IIndex, DeleteIndexResponse>>
  DeleterFor { get; }

  public
  IReadOnlyDictionary<string, Func<
      IElasticClient, IIndex, Task<DeleteIndexResponse>>>
  AsyncDeleterFor { get; }
}
