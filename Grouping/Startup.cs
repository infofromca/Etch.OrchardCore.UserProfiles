﻿using Etch.OrchardCore.UserProfiles.Grouping.Drivers;
using Etch.OrchardCore.UserProfiles.Grouping.Indexes;
using Etch.OrchardCore.UserProfiles.Grouping.Models;
using Etch.OrchardCore.UserProfiles.Grouping.Services;
using Etch.OrchardCore.UserProfiles.Grouping.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.Data.Migration;
using OrchardCore.Modules;
using System;
using YesSql.Indexes;

namespace Etch.OrchardCore.UserProfiles.Grouping
{
    [Feature(Constants.Features.Grouping)]
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IIndexProvider, ProfileGroupPartIndexProvider>();
            services.AddSingleton<IIndexProvider, ProfileGroupedPartIndexProvider>();

            services.AddContentPart<ProfileGroupPart>()
                .UseDisplayDriver<ProfileGroupPartDisplay>();

            services.AddContentPart<ProfileGroupedPart>();

            services.AddScoped<IContentTypePartDefinitionDisplayDriver, ProfileGroupPartSettingsDisplayDriver>();
            services.AddScoped<IProfileGroupsService, ProfileGroupsService>();

            services.AddScoped<IContentPickerResultProvider, ProfilePickerResultProvider>();

            services.AddScoped<IDataMigration, Migrations>();
        }

        public override void Configure(IApplicationBuilder app, IEndpointRouteBuilder routes, IServiceProvider serviceProvider) {
            routes.MapAreaControllerRoute(
                name: "ProfilePicker",
                areaName: "Etch.OrchardCore.UserProfiles",
                pattern: "ProfilePicker",
                defaults: new { controller = "ProfilePicker", action = "List" }
            );
        }
    }
}
