using TsttPokemon.Data;
using TsttPokemon.Models;
using TsttPokemon.PokeApi;
using TsttPokemon.Services;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddStackExchangeRedisCache(options => { options.Configuration = configuration["RedisCacheUrl"]; });
builder.Services.AddDbContext<PokemonContext>();

builder.Services.AddScoped<IPokeApi, PokeApiService>();
builder.Services.AddScoped<ICache<Pokemon>, PokemonCache>();
builder.Services.AddScoped<PokemonService>();
builder.Services.AddScoped<TrainerService>();

builder.Services.AddControllers();
builder.Services.AddRazorPages();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseExceptionHandler("/Error");
    // default HSTS value is 30 days. consider changing for production, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

    // app.CreateDbIfNotExists();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.MapRazorPages();

app.Run();
