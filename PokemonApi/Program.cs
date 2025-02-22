using PokemonApi;
using PokemonApi.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddSqlServer<AppDbContext>(connectionString, sql => sql.EnableRetryOnFailure());
builder.Services.AddControllers();
builder.Services.AddScoped<ImageService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var port = Environment.GetEnvironmentVariable("PORT") ?? "8000";
builder.WebHost.UseUrls($"http://+:{port}");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || Environment.GetEnvironmentVariable("RENDER") != null)
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pokemon API V1");
        c.RoutePrefix = ""; // Makes Swagger available at the root URL
    });
}


//app.UseHttpsRedirection();


app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapGet("/", () => "Welcome to the Pokémon API! Your API is running.");

app.Run();
