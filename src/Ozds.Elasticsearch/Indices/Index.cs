namespace Ozds.Elasticsearch;

public readonly record struct Index
(string Name,

 string Base,
 bool IsDev,

 int? Version = null,
 string? Test = null) : IIndex
{ }
