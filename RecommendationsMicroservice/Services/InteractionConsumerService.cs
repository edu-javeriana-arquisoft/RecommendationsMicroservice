
using Confluent.Kafka;

namespace RecommendationsMicroservice.Services;

public class InteractionConsumerService : BackgroundService, IDisposable
{
	private readonly string _topic;
	private readonly IConsumer<Ignore, string> _consumer;
	private readonly ILogger<InteractionConsumerService> _logger;
	private readonly IServiceScopeFactory _scopeFactory;

	public InteractionConsumerService(
		IConfiguration configuration,
		ILogger<InteractionConsumerService> logger,
		IServiceScopeFactory scopeFactory)
	{
		// Create the Kafka consumer
		_consumer = new ConsumerBuilder<Ignore, string>(new ConsumerConfig
		{
			GroupId = "DefaultConsumerService",
			BootstrapServers = configuration["KafkaBroker:Server"]!
		}).Build();

		_topic = configuration["KafkaBroker:Topic"]!;
		_scopeFactory = scopeFactory;
		_logger = logger;

		_logger.LogInformation($"Connecting to kafka broker `{configuration["KafkaBroker:Server"]!}` with topic `{_topic}`");
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		_logger.LogInformation($"Consuming topic `{_topic}` from Kafka");
		using var scope = _scopeFactory.CreateScope();

		// Create the database service
		var dbService = scope.ServiceProvider.GetRequiredService<IDatabaseService>();

		// Connect to kafka as listener
		_consumer.Subscribe(_topic);

		while (!stoppingToken.IsCancellationRequested)
		{
			try
			{
				// Await for the resource
				var consume = await Task.Run(() => _consumer.Consume(stoppingToken));

				// Consume the result
				var consumeResult = consume.Message.Value!;
				_logger.LogInformation($"Parsing interaction: {consumeResult}");

				// Tokenize with ':'
				var tokens = consumeResult.Split(":");

				// Get parameters
				Guid userId = Guid.Parse(tokens[0]);
				string categoryName = tokens[1];

				// Update in database
				dbService.AddInteractionToUser(userId, categoryName);
				_logger.LogInformation($"Interacted with `{categoryName}` for user with ID `{userId}`");
			}
			catch (Exception e)
			{
				_logger.LogError($"Failed to parse interaction request: {e.Message}\n{e.StackTrace}");
			}
		}
	}

	override public void Dispose()
	{
		Dispose(true);
		base.Dispose();

		GC.SuppressFinalize(this);
	}

	protected void Dispose(bool disposing)
	{
		if (disposing)
		{
			_consumer.Close();
			_consumer.Dispose();
		}
	}
}