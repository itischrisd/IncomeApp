using System.Threading;
using System.Threading.Tasks;
using IncomeApp.Models;
using IncomeApp.Repositories;

namespace IncomeApp.Tests.TestDoubles;

public class FakeCustomerRepository(IPersonRepository personRepository, ICompanyRepository companyRepository)
    : ICustomerRepository
{
    public async Task<Customer> GetCustomerAsync(int id, CancellationToken cancellationToken)
    {
        var person = await personRepository.GetPersonAsync(id, cancellationToken);
        var company = await companyRepository.GetCompanyAsync(id, cancellationToken);

        return person != null ? person : company;
    }
}