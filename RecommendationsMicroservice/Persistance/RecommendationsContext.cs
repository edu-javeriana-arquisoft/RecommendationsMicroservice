using Microsoft.EntityFrameworkCore;
using RecommendationsMicroservice.Entities;

namespace RecommendationsMicroservice.Persistance;

public class RecommendationsContext : DbContext
{
	public DbSet<UserStatistics> Statistics { get; set; }
	public DbSet<CategoryStatistics> Categories { get; set; }

	private readonly string _dbConnectionString;

	public RecommendationsContext(IConfiguration configuration)
	{
		_dbConnectionString = configuration["ConnectionStrings:DatabaseConnection"]!;
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
		optionsBuilder.UseMySql(_dbConnectionString, ServerVersion.Parse("8.1.0-mysql"));

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<UserStatistics>()
			.HasMany(e => e.Categories)
			.WithOne(e => e.User)
			.HasForeignKey(e => e.UserId)
			.IsRequired();
	}
}