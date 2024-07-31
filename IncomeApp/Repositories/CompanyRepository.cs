using IncomeApp.Models;
using Microsoft.EntityFrameworkCore;

namespace IncomeApp.Repositories;

public class CompanyRepository(Context context) : ICompanyRepository
{
    public async Task<Company> GetCompanyAsync(int id, CancellationToken cancellationToken)
    {
        return (await context.Companies.FindAsync([id], cancellationToken))!;
    }

    public async Task<IEnumerable<Company>> GetCompaniesAsync(CancellationToken cancellationToken)
    {
        return await context.Companies.ToListAsync(cancellationToken);
    }

    public async Task<int> AddCompanyAsync(Company company, CancellationToken cancellationToken)
    {
        await context.Companies.AddAsync(company, cancellationToken);
        return company.IdCustomer;
    }

    public async Task UpdateCompanyAsync(Company company, CancellationToken cancellationToken)
    {
        if (!await context.Companies.AnyAsync(c => c.IdCustomer == company.IdCustomer, cancellationToken))
            context.Companies.Attach(company);

        context.Entry(company)
            .State = EntityState.Modified;

        context.Entry(company)
            .Property(e => e.KRS)
            .IsModified = false;
    }

    public async Task DeleteCompanyAsync(int id, CancellationToken cancellationToken)
    {
        var company = await context.Companies.FindAsync([id], cancellationToken);
        if (company == null) throw new Exception("Company not found");
        context.Companies.Remove(company);
    }
}