using IncomeApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IncomeApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IncomeController(IIncomeService incomeService) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = "user")]
    public async Task<IActionResult> GetIncome([FromQuery] string? currency, [FromQuery] int? productId,
        CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(currency))
            return Ok(await incomeService.GetIncomeWithCurrencyAsync(currency, cancellationToken));
        return productId.HasValue
            ? Ok(await incomeService.GetIncomeAsync(productId.Value, cancellationToken))
            : Ok(await incomeService.GetIncomeAsync(cancellationToken));
    }

    [HttpGet("expected")]
    [Authorize(Roles = "user")]
    public async Task<IActionResult> GetExpectedIncome([FromQuery] string? currency, [FromQuery] int? productId,
        CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(currency))
            return Ok(await incomeService.GetExpectedIncomeWithCurrencyAsync(currency, cancellationToken));
        return productId.HasValue
            ? Ok(await incomeService.GetExpectedIncomeAsync(productId.Value, cancellationToken))
            : Ok(await incomeService.GetExpectedIncomesAsync(cancellationToken));
    }
}