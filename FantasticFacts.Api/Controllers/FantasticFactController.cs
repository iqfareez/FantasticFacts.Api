using FantasticFacts.Entities.Contents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FantasticFacts.Api.Controllers;

/// <summary>
/// Provides endpoints to read fantastic facts.
/// </summary>
[ApiController]
[Route("[controller]")]
public class FantasticFactController : ControllerBase
{
    private readonly ILogger<FantasticFactController> _logger;
    private readonly AppDbContext _appDbContext;

    public FantasticFactController(ILogger<FantasticFactController> logger, AppDbContext appDbContext)
    {
        _logger = logger;
        _appDbContext = appDbContext;
    }

    /// <summary>
    /// Gets a single fantastic fact by its identifier.
    /// </summary>
    /// <param name="id">The fact identifier.</param>
    /// <returns>The requested fact when found, otherwise 404.</returns>
    [HttpGet("{id:int}", Name = "GetFantasticFact")]
    public async Task<IActionResult> Get(int id)
    {
        _logger.LogInformation("Fetching fantastic fact with ID: {Id}", id);
        var fact = await _appDbContext.FantasticFacts.FirstOrDefaultAsync(f => f.Id == id);
        if (fact == null)
        {
            return NotFound();
        }
        return Ok(fact);
    }
    
    /// <summary>
    /// Searches and paginates fantastic facts.
    /// </summary>
    /// <param name="keyword">Optional text to filter by content.</param>
    /// <param name="page">1-based page number.</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <returns>A paged result containing facts and total count.</returns>
    [HttpGet(Name = "SearchFantasticFacts")]
    public async Task<IActionResult> GetAll(string? keyword = null, int page = 1, int pageSize = 10)
    {
        // Validate and clamp inputs
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 1;

        _logger.LogInformation("Fetching fantastic facts (page {Page}, pageSize {PageSize}, keyword={Keyword})", page, pageSize, keyword);

        var query = _appDbContext.FantasticFacts
            .Where(f => string.IsNullOrEmpty(keyword) || f.Content.Contains(keyword));

        var totalCount = await query.CountAsync();

        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        var result = new
        {
            Results = items,
            Count = totalCount,
            Page = page,
            PageSize = pageSize
        };

        return Ok(result);
    }
}