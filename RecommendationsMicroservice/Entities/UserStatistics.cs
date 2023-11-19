using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RecommendationsMicroservice.Entities;

public record UserStatistics
{
	[Key, Required]
	public Guid UserId { get; set; }

	public ICollection<CategoryStatistics> Categories { get; init; } = new List<CategoryStatistics>();

	[DefaultValue(0)]
	public ulong Interactions { get; set; }
}