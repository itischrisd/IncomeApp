using IncomeApp.DTOs.Request;
using IncomeApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IncomeApp.Controllers;

[ApiController]
[Route("[controller]")]
public class CompanyController(ICompanyService companyService) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = "admin,user")]
    public async Task<IActionResult> GetCompanies(CancellationToken cancellationToken)
    {
        return Ok(await companyService.GetCompaniesAsync(cancellationToken));
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = "admin,user")]
    public async Task<IActionResult> GetCompany(int id, CancellationToken cancellationToken)
    {
        return Ok(await companyService.GetCompanyAsync(id, cancellationToken));
    }

    [HttpPost]
    [Authorize(Roles = "admin, user")]
    public async Task<IActionResult> AddCompany(CompanyCreateDTO company, CancellationToken cancellationToken)
    {
        return Ok(await companyService.AddCompanyAsync(company, cancellationToken));
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "admin, user")]
    public async Task<IActionResult> UpdateCompany(int id, CompanyUpdateDTO company,
        CancellationToken cancellationToken)
    {
        return Ok(await companyService.UpdateCompanyAsync(id, company, cancellationToken));
    }
}