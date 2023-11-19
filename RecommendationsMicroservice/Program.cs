using RecommendationsMicroservice.Persistance;
using RecommendationsMicroservice.Services;

var builder = WebApplication.CreateBuilder(args);

// Add database context
builder.Services.AddDbContext<RecommendationsContext>();

// Add services via DI
builder.Services.AddScoped<IDatabaseService, RecommendationsDatabaseService>();

// Add web controllers
builder.Services.AddControllers();

// Configure endpoints
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();
app.Run();