using IncomeApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IncomeApp.Controllers;

[ApiController]
[Route("[controller]")]
public class SoftwareController(ISoftwareService softwareService) : ControllerBase
{
    [HttpGet("{id:int}")]
    [Authorize(Roles = "user")]
    public async Task<IActionResult> GetSoftware(int id, CancellationToken cancellationToken)
    {
        return Ok(await softwareService.GetSoftwareAsync(id, cancellationToken));
    }

    [HttpGet]
    [Authorize(Roles = "user")]
    public async Task<IActionResult> GetSoftwares(CancellationToken cancellationToken)
    {
        return Ok(await softwareService.GetSoftwaresAsync(cancellationToken));
    }
}