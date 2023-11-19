using Microsoft.EntityFrameworkCore;
using RecommendationsMicroservice.Persistance;

namespace RecommendationsMicroservice.Services;

public class RecommendationsDatabaseService : IDatabaseService
{
	private readonly RecommendationsContext _dbContext;

	public RecommendationsDatabaseService(RecommendationsContext dbContext)
	{
		_dbContext = dbContext;
	}

	public void AddInteractionToUser(Guid userId, string category)
	{
		// Get or create the user
		var userStatistics = _dbContext.Statistics.Include(u => u.Categories).SingleOrDefault(u => u.UserId == userId);
		if (userStatistics is null)
		{
			userStatistics = new Entities.UserStatistics { UserId = userId };
			_dbContext.Statistics.Add(userStatistics);
		}

		// Get or create the category
		var categoryStatistics = userStatistics.Categories.SingleOrDefault(c => c.Category == category);
		if (categoryStatistics is null)
		{
			categoryStatistics = new Entities.CategoryStatistics
			{
				UserId = userId,
				Category = category
			};

			userStatistics.Categories.Add(categoryStatistics);
			_dbContext.Categories.Add(categoryStatistics);
		}

		// Increment interactions
		userStatistics.Interactions++;
		categoryStatistics.Value++;

		// Update changes on database
		_dbContext.SaveChanges();
	}

	public List<Entities.CategoryStatistics> TopCategoriesForUser(Guid userId, int amount)
	{
		var userStatistics = _dbContext.Statistics.Include(u => u.Categories).Single(u => u.UserId == userId);
		var categoryStatistics = userStatistics.Categories.OrderByDescending(e => e.Value);

		return categoryStatistics.Take(amount).ToList();
	}
}