using HonorFlightScreening;
using HonorFlightScreening.Components;
using HonorFlightScreening.Components.Account;
using HonorFlightScreening.Data;
using HonorFlightScreening.Helper;
using HonorFlightScreening.Services;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
var configuration = builder.Configuration;

if (Environment.GetEnvironmentVariable("3cAzureAppConfig") is null)
{
    //Throw error
    throw new Exception(
        "The AzureAppConfig env variable is missing that is required to read variables from Azure App Configuration");
}
string? hostingEnvironmentName = null;
builder.Logging.AddConfiguration(configuration.GetSection("Logging"));
builder.Logging.AddConsole();
builder.Logging.AddDebug();

hostingEnvironmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

configuration.AddAzureAppConfiguration(options =>
    options
        .Connect(Environment.GetEnvironmentVariable("3cAzureAppConfig"))
        .Select("Global.*", LabelFilter.Null)
        .Select(Constants.ConfigAppServiceName + ".*", LabelFilter.Null)
        .Select("Global.*", hostingEnvironmentName)
        .Select(Constants.ConfigAppServiceName + ".*",
            hostingEnvironmentName)


);
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(configuration["HonorFlightScreening.ConnectionString"],
            providerOptions =>
            {
                providerOptions.EnableRetryOnFailure(
                    maxRetryCount: 30, // Maximum number of retry attempts
                    maxRetryDelay: TimeSpan.FromSeconds(30), // Maximum delay between retries
                    errorNumbersToAdd: null); // Additional error numbers to consider for retry); 
            }),
    ServiceLifetime.Transient);



builder.Services.AddApplicationInsightsTelemetry(new ApplicationInsightsServiceOptions()
    { ConnectionString = $"{configuration["HonorFlightScreening.ApplicationInsights"]}" });

builder.Services.AddSingleton<ITelemetryInitializer, MyTelemetryInitializer>();


builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

// Add application services
builder.Services.AddScoped<VeteranScreeningService>();
builder.Services.AddScoped<HonorFlightService>();
builder.Services.AddCascadingValue(sp => new HonorFlightSession() );


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();
