using IncomeApp.Models;

namespace IncomeApp.Repositories;

public interface ICustomerRepository
{
    Task<Customer> GetCustomerAsync(int id, CancellationToken cancellationToken);
}