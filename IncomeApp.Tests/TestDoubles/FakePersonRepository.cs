using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IncomeApp.Models;
using IncomeApp.Repositories;

namespace IncomeApp.Tests.TestDoubles;

public class FakePersonRepository : IPersonRepository
{
    private IEnumerable<Person> _persons = new List<Person>
    {
        new()
        {
            IdCustomer = 1,
            FirstName = "Sample",
            LastName = "Client",
            Address = "Sample Address",
            Email = "client1@mail.com",
            PhoneNumber = "123456789",
            Pesel = "12345678901"
        },
        new()
        {
            IdCustomer = 2,
            FirstName = "Sample",
            LastName = "Client 2",
            Address = "Sample Address 2",
            Email = "client2@mail.com",
            PhoneNumber = "987654321",
            Pesel = "98765432109"
        },
        new()
        {
            IdCustomer = 3,
            FirstName = "Sample",
            LastName = "Client 3",
            Address = "Sample Address 3",
            Email = "client2@mail.com",
            PhoneNumber = "123456789",
            Pesel = "12345678902",
            Contracts = new List<Contract>
            {
                new()
                {
                    IdContract = 1,
                    StartDate = new DateOnly(2022, 1, 1),
                    EndDate = new DateOnly(2022, 1, 15),
                    Cost = 1000,
                    YearsOfUpdates = 1,
                    SoftwareVersion = "1.0",
                    IdCustomer = 3,
                    IdSoftware = 1,
                    IsSigned = true
                }
            }
        }
    };

    public Task<Person> GetPersonAsync(int id, CancellationToken cancellationToken)
    {
        return Task.FromResult(_persons.FirstOrDefault(c => c.IdCustomer == id));
    }

    public Task<IEnumerable<Person>> GetPersonsAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(_persons);
    }

    public Task<int> AddPersonAsync(Person person, CancellationToken cancellationToken)
    {
        var id = _persons.Max(c => c.IdCustomer) + 1;
        _persons = _persons.Append(new Person
        {
            IdCustomer = id,
            FirstName = person.FirstName,
            LastName = person.LastName,
            Address = person.Address,
            Email = person.Email,
            PhoneNumber = person.PhoneNumber,
            Pesel = person.Pesel
        });
        return Task.FromResult(id);
    }

    public Task UpdatePersonAsync(Person person, CancellationToken cancellationToken)
    {
        var personToUpdate = _persons.FirstOrDefault(c => c.IdCustomer == person.IdCustomer);
        if (personToUpdate == null) throw new Exception("Person not found");

        _persons = _persons.Select(c => c.IdCustomer == person.IdCustomer ? person : c);
        return Task.CompletedTask;
    }

    public Task SoftDeletePersonAsync(int id, CancellationToken cancellationToken)
    {
        var personToDelete = _persons.FirstOrDefault(c => c.IdCustomer == id);
        if (personToDelete == null) throw new Exception("Person not found");

        _persons = _persons.Select(c => c.IdCustomer == id
            ? new Person
            {
                IdCustomer = c.IdCustomer,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Address = c.Address,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
                Pesel = c.Pesel,
                IsDeleted = true
            }
            : c);
        return Task.CompletedTask;
    }
}