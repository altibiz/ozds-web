using System;
using YesSql.Indexes;

namespace Ozds.Modules.Members.Devices;

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
