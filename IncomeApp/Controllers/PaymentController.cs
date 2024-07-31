using IncomeApp.DTOs.Request;
using IncomeApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IncomeApp.Controllers;

[ApiController]
[Route("payments")]
public class PaymentController(IPaymentService paymentService) : ControllerBase
{
    [HttpPost]
    [Authorize(Roles = "user")]
    public async Task<IActionResult> AddPayment(ContractPaymentCreateDTO command, CancellationToken cancellationToken)
    {
        return Ok(await paymentService.AddContractPaymentAsync(command, cancellationToken));
    }
}