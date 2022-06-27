using Nest;

namespace Ozds.Elasticsearch;

public static class RefreshExtensions
{
  public static IndexDescriptor<T> RefreshInDevelopment<T>(
      this IndexDescriptor<T> descriptor,
      IHostEnvironment env) where T : class =>
    descriptor.Refresh(
      env.IsDevelopment() ?
        global::Elasticsearch.Net.Refresh.WaitFor
      : global::Elasticsearch.Net.Refresh.False);

  public static UpdateDescriptor<T, P> RefreshInDevelopment<T, P>(
      this UpdateDescriptor<T, P> descriptor,
      IHostEnvironment env)
        where T : class
        where P : class =>
    descriptor.Refresh(
      env.IsDevelopment() ?
        global::Elasticsearch.Net.Refresh.WaitFor
      : global::Elasticsearch.Net.Refresh.False);

  public static DeleteDescriptor<T> RefreshInDevelopment<T>(
      this DeleteDescriptor<T> descriptor,
      IHostEnvironment env) where T : class =>
    descriptor.Refresh(
      env.IsDevelopment() ?
        global::Elasticsearch.Net.Refresh.WaitFor
      : global::Elasticsearch.Net.Refresh.False);

  public static BulkDescriptor RefreshInDevelopment(
      this BulkDescriptor descriptor,
      IHostEnvironment env) =>
    descriptor.Refresh(
      env.IsDevelopment() ?
        global::Elasticsearch.Net.Refresh.WaitFor
      : global::Elasticsearch.Net.Refresh.False);
}
