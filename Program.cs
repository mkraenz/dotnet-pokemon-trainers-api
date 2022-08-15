using dotnettest.Pokemon.Data;
using dotnettest.Pokemon.Models;
using dotnettest.Pokemon.PokeApi;
using dotnettest.Pokemon.Services;

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

builder.Services.AddScoped<IPokeApi, PokeApiService>();
builder.Services.AddScoped<ICache<Species>, SpeciesCacheService>();
builder.Services.AddScoped<SpeciesService>();
builder.Services.AddScoped<TrainerService>();

builder.Services.AddControllers();
builder.Services.AddRazorPages();

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

app.UseAuthorization();

app.MapControllers();

app.MapRazorPages();

app.Run();
