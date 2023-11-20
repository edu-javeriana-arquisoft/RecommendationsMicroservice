using Microsoft.AspNetCore.Mvc;
using RecommendationsMicroservice.Entities;
using RecommendationsMicroservice.Services;

namespace RecommendationsMicroservice.Controllers;

[ApiController]
[Route("[controller]")]
public class RecommendationsController : ControllerBase
{
	private readonly IDatabaseService _dbService;
	public RecommendationsController(IDatabaseService dbService)
	{
		_dbService = dbService;
	}


	[HttpGet("[action]/{userGuid}")]
	public ActionResult<List<CategoryStatistics>> GetTopCategoriesForUser(string userGuid, [FromQuery] int? count)
	{
		try
		{
			var userParsedGuid = Guid.Parse(userGuid);
			return _dbService.TopCategoriesForUser(userParsedGuid, count ?? 5);
		}
		catch (FormatException)
		{
			return BadRequest();
		}
		catch
		{
			return NotFound();
		}
	}
}