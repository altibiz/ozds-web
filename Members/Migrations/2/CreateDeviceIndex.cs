using System;
using YesSql.Sql;
using Members.Devices;

namespace Members.M2;

public static partial class CreateDeviceIndexClass
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
