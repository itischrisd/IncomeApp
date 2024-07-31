using IncomeApp.Models;

namespace IncomeApp.Repositories;

public interface IPersonRepository
{
    Task<Person> GetPersonAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<Person>> GetPersonsAsync(CancellationToken cancellationToken);
    Task<int> AddPersonAsync(Person person, CancellationToken cancellationToken);
    Task UpdatePersonAsync(Person person, CancellationToken cancellationToken);
    Task SoftDeletePersonAsync(int id, CancellationToken cancellationToken);
}