using RecommendationsMicroservice.Persistance;
using RecommendationsMicroservice.Services;
using RecommendationsMicroservice.Services.Infrastructure;
using Steeltoe.Discovery.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add system services
builder.Services.AddDiscoveryClient(builder.Configuration);

// Add database context
builder.Services.AddDbContext<RecommendationsContext>();

// Add services
builder.Services.AddScoped<IDatabaseService, DatabaseServiceImpl>();
builder.Services.AddSingleton<InteractionConsumerService>();
builder.Services.AddHostedService<InteractionConsumerService>();

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

app.Map("/info", () =>
	new { Value = "Hello from ProductsMicroservice!" }
);

app.MapControllers();
app.Run();
