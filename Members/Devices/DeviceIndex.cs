using System;
using YesSql.Indexes;
using YesSql.Sql;

namespace Members.Devices;

public class DeviceIndex : MapIndex
{
  public string MemberId { get; init; }

  public string Source { get; init; }

  public string State { get; init; }
  public DateTime DateAdded { get; init; }
  public DateTime DateDiscontinued { get; init; }
}

public class DeviceIndexProvider : IndexProvider<Device>
{
  public override void Describe(DescribeContext<Device> context) =>
      context.For<DeviceIndex>().Map(device =>
      {
        return new DeviceIndex
        {
          MemberId = device.MemberId,
          Source = device.Source,
          State = device.State,
          DateAdded = device.DateAdded,
          DateDiscontinued = device.DateDiscontinued
        };
      });
}

public static class DeviceIndexExtensions
{
  public static void CreateDeviceIndex(this ISchemaBuilder SchemaBuilder) =>
      SchemaBuilder.CreateMapIndexTable<DeviceIndex>(
          table => table.Column<string>(nameof(DeviceIndex.MemberId))
                       .Column<string>(nameof(DeviceIndex.Source))
                       .Column<string>(
                           nameof(DeviceIndex.State), c => c.WithLength(20))
                       .Column<DateTime>(nameof(DeviceIndex.DateAdded))
                       .Column<DateTime>(nameof(DeviceIndex.DateDiscontinued)));
}
