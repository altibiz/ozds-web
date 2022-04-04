using OrchardCore.ContentManagement;
using System;

namespace Members.Devices;

public class Device : ContentPart
{
  public string Id { get; init; }
  public string MemberId { get; init; }

  public string Source { get; init; }

  public string State { get; init; }
  public DateTime DateAdded { get; init; }
  public DateTime DateDiscontinued { get; init; }
}
