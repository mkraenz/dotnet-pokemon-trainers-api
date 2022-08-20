using dotnettest.Pokemon.Controllers;
using dotnettest.Pokemon.Data;
using dotnettest.Pokemon.Models;
using dotnettest.Pokemon.PokeApi;
using dotnettest.Pokemon.Services;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

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

// following https://dev.to/kayesislam/integrating-openid-connect-to-your-application-stack-25ch
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
        options =>
        {
            // http://localhost:4344/realms/teatime
            options.MetadataAddress = "http://localhost:4344/realms/teatime/.well-known/openid-configuration";
            options.RequireHttpsMetadata = false; // TODO only for dev
            options.IncludeErrorDetails = true;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidIssuer = "http://localhost:4344/realms/teatime",
                ValidAudience = "test",
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
            };
        });
// builder.Services.AddAuthorization(o => o.DefaultPolicy = new AuthorizationPolicyBuilder()
//         .RequireAuthenticatedUser()
//         .RequireClaim("email_verified", "true")
//         .Build());

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
