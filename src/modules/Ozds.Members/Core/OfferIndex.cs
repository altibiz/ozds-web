﻿using OrchardCore.ContentManagement;
using YesSql.Indexes;
using System.Linq;
using Ozds.Members.Core;
using Ozds.Members.Utils;

namespace Ozds.Members.Indexes
{
  public class OfferIndex : MapIndex
  {
    public string ContentItemId { get; set; }
    public string CompanyContentItemId { get; set; }
    public string Title { get; set; }
    public bool Published { get; set; }
    public bool Latest { get; set; }
    public string Owner { get; set; }
  }
  public class OfferIndexProvider : IndexProvider<ContentItem>
  {
    public override void Describe(DescribeContext<ContentItem> context)
    {
      context.For<OfferIndex>().Map(contentItem =>
      {
        var offer = contentItem.AsReal<Offer>();
        if (offer == null)
          return null;
        var offerIndex = new OfferIndex
        {
          ContentItemId =
                                              contentItem.ContentItemId,
          CompanyContentItemId = offer.Company?.ContentItemIds.FirstOrDefault(),
          Title = contentItem.DisplayText,
          Published = contentItem.Published,
          Latest = contentItem.Latest,
          Owner = contentItem.Owner
        };
        return offerIndex;
      });
    }
  }
}
