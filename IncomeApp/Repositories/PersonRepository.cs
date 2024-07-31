using IncomeApp.Models;
using Microsoft.EntityFrameworkCore;

namespace IncomeApp.Repositories;

public class PersonRepository(Context context) : IPersonRepository
{
    public async Task<Person> GetPersonAsync(int id, CancellationToken cancellationToken)
    {
        return (await context.Persons.Where(c => !c.IsDeleted)
            .FirstOrDefaultAsync(c => c.IdCustomer == id, cancellationToken))!;
    }

    public async Task<IEnumerable<Person>> GetPersonsAsync(CancellationToken cancellationToken)
    {
        return await context.Persons.Where(c => !c.IsDeleted)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> AddPersonAsync(Person person, CancellationToken cancellationToken)
    {
        await context.Persons.AddAsync(person, cancellationToken);
        return person.IdCustomer;
    }

    public async Task UpdatePersonAsync(Person person, CancellationToken cancellationToken)
    {
        if (!await context.Persons.AnyAsync(c => c.IdCustomer == person.IdCustomer, cancellationToken))
            context.Persons.Attach(person);

        context.Entry(person)
            .State = EntityState.Modified;

        context.Entry(person)
            .Property(e => e.Pesel)
            .IsModified = false;
    }

    public async Task SoftDeletePersonAsync(int id, CancellationToken cancellationToken)
    {
        var client = await context.Persons.FindAsync([id], cancellationToken);
        if (client == null) throw new Exception("Client not found");
        client.IsDeleted = true;
    }
}