using dotnettest.Pokemon.Controllers;
using dotnettest.Pokemon.Data;
using dotnettest.Pokemon.Models;
using dotnettest.Pokemon.PokeApi;
using dotnettest.Pokemon.Services;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Logging
builder.Logging.ClearProviders();
// TODO figure out how to log the object argument
builder.Logging.AddConsole();


// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddStackExchangeRedisCache(options => options.Configuration = configuration["RedisCacheUrl"]);
builder.Services.AddDbContext<PokemonContext>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    // maybe overwritten below by options.UseTokenLifetime = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
    options.LogoutPath = "/signout";
})
.AddOpenIdConnect(options =>
{
    //  TODO move to config
    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.Authority = "http://localhost:4344/realms/teatime/";
    options.SignedOutRedirectUri = "/"; // https://stackoverflow.com/a/60205731/3963260
    options.ClientSecret = "UZH5JVmo6iVpK3sjCPSGHbUYz0ueqmr2";
    options.ClientId = "aspnet";
    options.ResponseType = OpenIdConnectResponseType.Code;
    options.SaveTokens = true;
    options.GetClaimsFromUserInfoEndpoint = false;
    options.UseTokenLifetime = true;
    options.RequireHttpsMetadata = false; // TODO: WARNING: use in dev only
    options.Scope.Add("openid");
    options.Scope.Add("email");
    options.Scope.Add("aspnet_roles");
    options.Scope.Add("roles");
    options.Scope.Add("profile");
    options.DisableTelemetry = true;
    // Note: .NET uses claim.Type == ClaimTypes.NameIdentifier for the subject id 
});

builder.Services
    .AddScoped<IPokeApi, PokeApiService>()
    .AddScoped<ICache<Species>, SpeciesCacheService>()
    .AddScoped<SpeciesService>()
    .AddScoped<TeamService>()
    .AddScoped<TrainerService>()
    .AddScoped<TeamController>();

builder.Services.AddControllers();
builder.Services.AddRazorPages();
// enable this when editing form views or _layout. Then reload browser manually with F5.
// .AddRazorRuntimeCompilation();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
    _ = app.UseExceptionHandler("/Error");
    // default HSTS value is 30 days. consider changing for production, see https://aka.ms/aspnetcore-hsts.
    _ = app.UseHsts();

    // app.CreateDbIfNotExists();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapRazorPages();

app.Run();
