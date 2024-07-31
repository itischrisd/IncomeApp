using System;
using System.Linq;
using System.Threading.Tasks;
using IncomeApp.DTOs.Request;
using IncomeApp.Exceptions;
using IncomeApp.Services;
using IncomeApp.Tests.TestDoubles;
using JetBrains.Annotations;
using Xunit;

namespace IncomeApp.Tests.Services;

[TestSubject(typeof(PersonService))]
public class PersonServiceTest
{
    [Fact]
    public async Task GetPersonAsync_Should_Return_Person_With_Correct_Id()
    {
        var personRepository = new FakePersonRepository();
        var unitOfWork = new FakeUnitOfWork();
        var personService = new PersonService(personRepository, unitOfWork);
        const int id = 1;

        var person = await personService.GetPersonAsync(id, default);

        Assert.Equal("Sample", person.FirstName);
        Assert.Equal("Client", person.LastName);
        Assert.Equal("Sample Address", person.Address);
        Assert.Equal("client1@mail.com", person.Email);
        Assert.Equal("123456789", person.PhoneNumber);
        Assert.Equal("12345678901", person.Pesel);
    }

    [Fact]
    public async Task GetPersonAsync_Should_Throw_RecordNotFoundException_When_Person_Not_Found()
    {
        var personRepository = new FakePersonRepository();
        var unitOfWork = new FakeUnitOfWork();
        var personService = new PersonService(personRepository, unitOfWork);
        const int id = 4;

        await Assert.ThrowsAsync<RecordNotFoundException>(() => personService.GetPersonAsync(id, default));
    }

    [Fact]
    public async Task GetPersonsAsync_Should_Return_All_Persons()
    {
        var personRepository = new FakePersonRepository();
        var unitOfWork = new FakeUnitOfWork();
        var personService = new PersonService(personRepository, unitOfWork);

        var persons = await personService.GetPersonsAsync(default);

        Assert.Equal(3, persons.Count());
    }

    [Fact]
    public async Task AddPersonAsync_Should_Add_Person()
    {
        var personRepository = new FakePersonRepository();
        var unitOfWork = new FakeUnitOfWork();
        var personService = new PersonService(personRepository, unitOfWork);
        var person = new PersonCreateDTO
        {
            FirstName = "Sample",
            LastName = "Client 4",
            Address = "Sample Address 4",
            Email = "test@mail.com",
            PhoneNumber = "123456789",
            Pesel = "12345678903"
        };

        var id = await personService.AddPersonAsync(person, default);

        Assert.Equal(4, id);

        var persons = await personRepository.GetPersonsAsync(default);

        var enumerable = persons.ToList();
        Assert.Equal(4, enumerable.Count);

        var addedPerson = enumerable.Last();

        Assert.Equal("Sample", addedPerson.FirstName);
        Assert.Equal("Client 4", addedPerson.LastName);
        Assert.Equal("Sample Address 4", addedPerson.Address);
        Assert.Equal("test@mail.com", addedPerson.Email);
        Assert.Equal("123456789", addedPerson.PhoneNumber);
        Assert.Equal("12345678903", addedPerson.Pesel);
    }

    [Fact]
    public async Task UpdatePersonAsync_Should_Update_Person()
    {
        var personRepository = new FakePersonRepository();
        var unitOfWork = new FakeUnitOfWork();
        var personService = new PersonService(personRepository, unitOfWork);
        var person = new PersonUpdateDTO
        {
            FirstName = "Sample",
            LastName = "Client 4",
            Address = "Sample Address 4",
            Email = "test@mail.com",
            PhoneNumber = "123456789"
        };
        const int id = 1;

        await personService.UpdatePersonAsync(id, person, default);

        var updatedPerson = await personRepository.GetPersonAsync(id, default);

        Assert.Equal("Sample", updatedPerson.FirstName);
        Assert.Equal("Client 4", updatedPerson.LastName);
        Assert.Equal("Sample Address 4", updatedPerson.Address);
        Assert.Equal("test@mail.com", updatedPerson.Email);
        Assert.Equal("123456789", updatedPerson.PhoneNumber);
    }

    [Fact]
    public async Task UpdatePersonAsync_Should_Throw_Exception_When_Person_Not_Found()
    {
        var personRepository = new FakePersonRepository();
        var unitOfWork = new FakeUnitOfWork();
        var personService = new PersonService(personRepository, unitOfWork);
        var person = new PersonUpdateDTO
        {
            FirstName = "Sample",
            LastName = "Client 4",
            Address = "Sample Address 4",
            Email = "test@mail.com",
            PhoneNumber = "123456789"
        };
        const int id = 4;

        await Assert.ThrowsAsync<Exception>(() => personService.UpdatePersonAsync(id, person, default));
    }

    [Fact]
    public async Task DeletePersonAsync_Should_Mark_Person_As_Deleted()
    {
        var personRepository = new FakePersonRepository();
        var unitOfWork = new FakeUnitOfWork();
        var personService = new PersonService(personRepository, unitOfWork);
        const int id = 1;

        await personService.DeletePersonAsync(id, default);

        var deletedPerson = await personRepository.GetPersonAsync(id, default);

        Assert.True(deletedPerson.IsDeleted);
    }

    [Fact]
    public async Task DeletePersonAsync_Should_Throw_Exception_When_Person_Not_Found()
    {
        var personRepository = new FakePersonRepository();
        var unitOfWork = new FakeUnitOfWork();
        var personService = new PersonService(personRepository, unitOfWork);
        const int id = 4;

        await Assert.ThrowsAsync<Exception>(() => personService.DeletePersonAsync(id, default));
    }
}