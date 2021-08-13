﻿using Microsoft.Extensions.Localization;
using Newtonsoft.Json.Linq;
using OrchardCore.ContentFields.Drivers;
using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentFields.ViewModels;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Members.PartFieldSettings
{
    public class PartTextFieldDriver : TextFieldDisplayDriver
    {
        private static IEnumerable<Type> _implementingTypes;
        public static IEnumerable<Type> ImplementingTypes = _implementingTypes ??= AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(x => x.GetTypes())
                    .Where(t => typeof(IFieldEditorSettings).IsAssignableFrom(t) && t.IsClass).ToList();

        public PartTextFieldDriver(IStringLocalizer<TextFieldDisplayDriver> localizer) : base(localizer) { }

        private bool CheckSettings(BuildFieldEditorContext context)
        {
            IFieldEditorSettings partSettings = null;
            foreach (var typ in ImplementingTypes)
            {
                context.TypePartDefinition.Settings.TryGetValue(typ.Name, out JToken val);
                if (val == null) continue;
                partSettings = val.ToObject(typ) as IFieldEditorSettings;
            }
            if (partSettings != null)
            {
                if (partSettings.IsFieldHidden(context.PartFieldDefinition.Name, context))
                    return false;
                var textset = context.PartFieldDefinition.GetSettings<ContentPartFieldSettings>();
                textset.DisplayName = partSettings.GetFieldLabel(context.PartFieldDefinition.Name, textset.DisplayName);
                textset.Editor = partSettings.GetFieldEditor(context.PartFieldDefinition.Name, textset.DisplayMode, context);
            }
            return true;
        }

        public override IDisplayResult Edit(TextField field, BuildFieldEditorContext context)
        {
            if (!CheckSettings(context)) return null;
            var shapeType = GetEditorShapeType(context);
            return Initialize<EditTextFieldViewModel>(shapeType, model =>
            {
                model.Text = field.Text;
                model.Field = field;
                model.Part = context.ContentPart;
                model.PartFieldDefinition = context.PartFieldDefinition;
            });
        }

        public override async Task<IDisplayResult> UpdateAsync(TextField field, IUpdateModel updater, UpdateFieldEditorContext context)
        {
            if (!CheckSettings(context)) return null;
            if (context.PartFieldDefinition.Editor() == "Disabled") return Edit(field, context);
            return await base.UpdateAsync(field, updater, context);
        }
    }
}
