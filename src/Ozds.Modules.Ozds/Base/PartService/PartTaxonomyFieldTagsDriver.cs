using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OrchardCore.Admin;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Taxonomies.Drivers;
using OrchardCore.Taxonomies.Fields;
using OrchardCore.Taxonomies.Models;
using OrchardCore.Taxonomies.Settings;
using OrchardCore.Taxonomies.ViewModels;
using Ozds.Extensions;

namespace Ozds.Modules.Ozds.PartFieldSettings
{
  public class PartTaxonomyFieldTagsDriver : TaxonomyFieldTagsDisplayDriver
  {
    public override IDisplayResult? Edit(
        TaxonomyField field, BuildFieldEditorContext context) =>
      context
        .GetFieldDefinition(AdminAttribute.IsApplied(HttpContext.HttpContext))
        .WhenNonNullable(fieldDefinition =>
            Initialize<EditTagTaxonomyFieldViewModel>(
              GetEditorShapeType(fieldDefinition),
              async model =>
              {
                var settings = fieldDefinition
                  .GetSettings<TaxonomyFieldSettings>();

                model.Taxonomy = await Content.GetAsync(
                  settings.TaxonomyContentItemId,
                  VersionOptions.Latest);
                if (model.Taxonomy != null)
                {
                  var termEntries = new List<TermEntry>();
                  TaxonomyFieldDriverHelper.PopulateTermEntries(
                      termEntries,
                      field,
                      ContentItemExtensions
                        .As<TaxonomyPart>(model.Taxonomy)
                        .Terms,
                      0);

                  model.TagTermEntries = JsonConvert.SerializeObject(
                      termEntries.Select(
                      termEntry => new TagTermEntry
                      {
                        ContentItemId = termEntry.ContentItemId,
                        Selected = termEntry.Selected,
                        DisplayText = termEntry.Term.DisplayText,
                        IsLeaf = termEntry.IsLeaf
                      }), SerializerSettings);
                }

                model.Field = field;
                model.Part = context.ContentPart;
                model.PartFieldDefinition = fieldDefinition;
              }));

    public override Task<IDisplayResult?> UpdateAsync(TaxonomyField field,
        IUpdateModel updater, UpdateFieldEditorContext context) =>
    context
      .GetFieldDefinition(AdminAttribute.IsApplied(HttpContext.HttpContext))
      .WhenTask(fieldDefinition => fieldDefinition.Editor() != "Disabled",
        _ => base.UpdateAsync(field, updater, context),
        () => Edit(field, context));

    public PartTaxonomyFieldTagsDriver(
        IStringLocalizer<TaxonomyFieldTagsDisplayDriver> localizer,
        IContentManager content,
        IHttpContextAccessor httpContext)
        : base(content, localizer)
    {
      HttpContext = httpContext;
      Content = content;
    }

    private IHttpContextAccessor HttpContext { get; }
    private IContentManager Content { get; }

    private static JsonSerializerSettings SerializerSettings { get; } =
      new JsonSerializerSettings
      {
        ContractResolver = new CamelCasePropertyNamesContractResolver()
      };
  }
}
