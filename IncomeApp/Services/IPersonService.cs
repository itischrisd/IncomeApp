using IncomeApp.DTOs.Request;
using IncomeApp.DTOs.Response;

namespace IncomeApp.Services;

public interface IPersonService
{
    Task<IEnumerable<PersonDTO>> GetPersonsAsync(CancellationToken cancellationToken);
    Task<PersonDTO> GetPersonAsync(int id, CancellationToken cancellationToken);
    Task<int> AddPersonAsync(PersonCreateDTO person, CancellationToken cancellationToken);
    Task<int> UpdatePersonAsync(int id, PersonUpdateDTO person, CancellationToken cancellationToken);
    Task<int> DeletePersonAsync(int id, CancellationToken cancellationToken);
}