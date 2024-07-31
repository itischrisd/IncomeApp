using IncomeApp.DTOs.Request;
using IncomeApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IncomeApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PersonController(IPersonService personService) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = "admin, user")]
    public async Task<IActionResult> GetPersons(CancellationToken cancellationToken)
    {
        return Ok(await personService.GetPersonsAsync(cancellationToken));
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = "admin, user")]
    public async Task<IActionResult> GetPerson(int id, CancellationToken cancellationToken)
    {
        return Ok(await personService.GetPersonAsync(id, cancellationToken));
    }

    [HttpPost]
    [Authorize(Roles = "admin, user")]
    public async Task<IActionResult> AddPerson(PersonCreateDTO person, CancellationToken cancellationToken)
    {
        return Ok(await personService.AddPersonAsync(person, cancellationToken));
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "admin, user")]
    public async Task<IActionResult> UpdatePerson(int id, PersonUpdateDTO person, CancellationToken cancellationToken)
    {
        return Ok(await personService.UpdatePersonAsync(id, person, cancellationToken));
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "admin, user")]
    public async Task<IActionResult> DeletePerson(int id, CancellationToken cancellationToken)
    {
        return Ok(await personService.DeletePersonAsync(id, cancellationToken));
    }
}