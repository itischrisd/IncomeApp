using IncomeApp.DTOs.Request;
using IncomeApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IncomeApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubscriptionController(ISubscriptionService subscriptionService) : ControllerBase
{
    [HttpPost]
    [Authorize(Roles = "user")]
    public async Task<IActionResult> CreateSubscriptionAsync(SubscriptionCreateDTO subscription,
        CancellationToken cancellationToken)
    {
        return Ok(await subscriptionService.CreateSubscriptionAsync(subscription, cancellationToken));
    }
}