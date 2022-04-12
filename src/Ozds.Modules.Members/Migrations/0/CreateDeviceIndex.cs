using YesSql.Sql;
using Ozds.Modules.Members.Devices;

namespace Ozds.Modules.Members.M0;

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
