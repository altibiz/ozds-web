using OrchardCore.Modules.Manifest;

[assembly:Module(Name = "Ozds.Members", Author = "AltiBiz",
              Website = "https://altibiz.com", Version = "0.0.1",
              Description = "Members Module", Category = "Content Management",
              Dependencies = new[] {
                  "OrchardCore.Taxonomies", "OrchardCore.ContentFields" })]
