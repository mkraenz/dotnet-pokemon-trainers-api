using TsttPokemon.Data;
using TsttPokemon.Services;

var builder = WebApplication.CreateBuilder(args);

// Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddDbContext<PokemonContext>();
builder.Services.AddScoped<PokemonService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // app.CreateDbIfNotExists();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
