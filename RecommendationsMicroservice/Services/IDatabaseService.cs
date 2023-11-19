using RecommendationsMicroservice.Entities;

namespace RecommendationsMicroservice.Services;

public interface IDatabaseService
{
	void AddInteractionToUser(Guid userId, string category);

	List<CategoryStatistics> TopCategoriesForUser(Guid userId, int amount);
}