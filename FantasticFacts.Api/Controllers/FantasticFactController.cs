using FantasticFacts.Entities.Contents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FantasticFacts.Api.Controllers;

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