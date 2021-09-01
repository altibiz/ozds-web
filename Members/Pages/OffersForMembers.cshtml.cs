using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Members.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrchardCore.ContentFields.Indexing.SQL;
using OrchardCore.ContentManagement;
using OrchardCore.DisplayManagement.Notify;
using YesSql;

namespace Members.Pages
{
    public class OffersForMembersModel : PageModel
    {
        private readonly IHtmlLocalizer H;
        private readonly MemberService _memberService;
        private readonly INotifier _notifier;
        private readonly ISession _session;

        public List<ContentItem> OfferContentItems { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public OffersForMembersModel(ISession session, MemberService mService, IHtmlLocalizer<CreateMemberModel> htmlLocalizer, INotifier notifier)
        {
            _notifier = notifier;
            H = htmlLocalizer;
            _memberService = mService;
            _session = session;

        }

        public async Task OnGetAsync(string catId = null)
        {

            if (catId!=null)
            {
                OfferContentItems = await _memberService.GetOffersForUserByTag(catId);
            }
            else
            {
                OfferContentItems = await _memberService.GetOffersForUser();
            }

            if (SearchString != null)
            {

                List<ContentItem>contentItems = GetTextFieldIndexRecords("Offer", "DisplayText").Result.ToList();

                OfferContentItems = contentItems.Where(x => x.DisplayText.Contains(SearchString)).ToList();

                //OfferContentItems = OfferContentItems.Where(x => x.Content.Offer.DisplayText.Text.Contains(SearchString)).ToList();
            }
        }
        public async Task<IEnumerable<ContentItem>> GetTextFieldIndexRecords(string contentType, string contentField)
        {
            return await _session.Query<ContentItem, TextFieldIndex>(x => x.ContentType == contentType && x.ContentField == contentField).ListAsync();
        }
    }
}
