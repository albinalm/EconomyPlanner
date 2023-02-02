using EconomyPlanner.Abstractions.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EconomyPlanner.API.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]
public class HouseholdController : ControllerBase
{
    private readonly ILogger<HouseholdController> _logger;
    private readonly IHouseholdService _householdService;

    public HouseholdController(ILogger<HouseholdController> logger, IHouseholdService householdService)
    {
        _logger = logger;
        _householdService = householdService;
    }
    
    [HttpGet(Name = "GetHouseholdFromGuid")]
    public IActionResult GetHouseholdFromGuid(string guid)
    {
        try
        {
            var householdModel = _householdService.GetHouseholdByGuid(guid);
            
            if (householdModel is null)
                return NotFound();
            
            return Ok(householdModel);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpPost(Name = "CreateHousehold")]
    public IActionResult CreateHousehold(string name)
    {
        try
        {
            _householdService.CreateHousehold(name);
            
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}