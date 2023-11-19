using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RecommendationsMicroservice.Entities;

[PrimaryKey(nameof(UserId), nameof(Category))]
public class CategoryStatistics
{
	[Required, ForeignKey(nameof(UserStatistics))]
	public Guid UserId { get; set; }

	public UserStatistics? User { get; set; }

	[Required]
	public string Category { get; set; } = "";

	[DefaultValue(0)]
	public ulong Value { get; set; }
}