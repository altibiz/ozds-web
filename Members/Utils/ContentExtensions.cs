﻿using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Metadata;
using System.Linq;

namespace Members.Utils
{
    public static class ContentExtensions
    {
        public static TSetting GetSettings<TSetting>(this IContentDefinitionManager _cdm, ContentPart part)
            where TSetting : new()
        {
            var contentTypeDefinition = _cdm.GetTypeDefinition(part.ContentItem.ContentType);
            var contentTypePartDefinition = contentTypeDefinition.Parts.FirstOrDefault(x => string.Equals(x.PartDefinition.Name, part.GetType().Name));
            return contentTypePartDefinition.GetSettings<TSetting>();
        }
    }
}
