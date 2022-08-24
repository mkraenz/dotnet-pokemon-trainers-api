using dotnettest.Pokemon.Controllers;
using dotnettest.Pokemon.Data;
using dotnettest.Pokemon.Models;
using dotnettest.Pokemon.PokeApi;
using dotnettest.Pokemon.Services;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
    options.LogoutPath = "/signout";
})
.AddOpenIdConnect(options =>
{
    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.Authority = "http://localhost:4344/realms/teatime/";
    options.SignedOutRedirectUri = "/"; // https://stackoverflow.com/a/60205731/3963260
    options.ClientSecret = "UZH5JVmo6iVpK3sjCPSGHbUYz0ueqmr2";
    options.ClientId = "aspnet";
    options.ResponseType = "code";
    options.SaveTokens = true;
    options.GetClaimsFromUserInfoEndpoint = false;
    options.RequireHttpsMetadata = false; // TODO: WARNING: use in dev only
    options.Scope.Add("openid");
    options.Scope.Add("email");
    options.Scope.Add("aspnet_roles");
    options.Scope.Add("roles");
    options.Scope.Add("profile");
    options.DisableTelemetry = true;

    options.Events.OnTokenResponseReceived = ctx =>
    {
        // TODO remove. This is useful for debugging because here we can see that the token retrieval actually worked
        List<AuthenticationToken> tokens = ctx.Properties.GetTokens().ToList();
        return Task.CompletedTask;
    };
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
