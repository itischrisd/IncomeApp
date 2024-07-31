using IncomeApp.DTOs.Request;
using IncomeApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IncomeApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContractController(IContractService contractService) : ControllerBase
{
    [HttpPost]
    [Authorize(Roles = "user")]
    public async Task<IActionResult> AddCompany(ContractCreateDTO contract, CancellationToken cancellationToken)
    {
        return Ok(await contractService.AddContractAsync(contract, cancellationToken));
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "user")]
    public async Task<IActionResult> DeleteCompany(int id, CancellationToken cancellationToken)
    {
        return Ok(await contractService.DeleteContractAsync(id, cancellationToken));
    }
}