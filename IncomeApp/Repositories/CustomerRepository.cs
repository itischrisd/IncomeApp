using IncomeApp.Models;
using Microsoft.EntityFrameworkCore;

namespace IncomeApp.Repositories;

public class CustomerRepository(Context context) : ICustomerRepository
{
    public async Task<Customer> GetCustomerAsync(int id, CancellationToken cancellationToken)
    {
        return (await context.Customers
            .Include(c => c.Contracts)
            .FirstOrDefaultAsync(c => c.IdCustomer == id, cancellationToken))!;
    }
}