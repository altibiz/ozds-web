﻿using Ozds.Modules.Members.Base;
using Ozds.Modules.Members.Core;
using Ozds.Modules.Members.PartFieldSettings;
using Ozds.Modules.Members.Utils;
using Microsoft.AspNetCore.Http;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Handlers;
using System.Threading.Tasks;

namespace Ozds.Modules.Members.Payments
{
  public class PledgeService : PartService<Pledge>
  {
    private TaxonomyCachedService _taxService;
    private MemberService _memberService;

    public PledgeService(TaxonomyCachedService taxService,
        IHttpContextAccessor htp, MemberService memberService)
        : base(htp)
    {
      _taxService = taxService;
      _memberService = memberService;
    }
    public override async Task UpdatedAsync<TPart>(
        UpdateContentContext context, Pledge model)
    {
      model.InitFields();
      if (!IsAdmin)
      {
        model.ReferenceNr.Text = "11-" + model.Oib.Text;
        var variant = await model.Variant.GetTerm(_taxService);
        model.Amount.Value = variant.As<PledgeVariant>().Price.Value;
        model.Note.Text = variant.DisplayText;
        var member = await _memberService.GetByOib(model.Oib.Text);
        if (member != null)
          model.Person.SetId(member.ContentItemId);
      }
    }
  }
}
