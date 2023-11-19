using Microsoft.AspNetCore.Mvc;
using RecommendationsMicroservice.Persistance;
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
}