using IncomeApp.DTOs.Request;
using IncomeApp.DTOs.Response;
using IncomeApp.Exceptions;
using IncomeApp.Models;
using IncomeApp.Repositories;

namespace IncomeApp.Services;

public class PersonService(IPersonRepository personRepository, IUnitOfWork unitOfWork) : IPersonService
{
    public async Task<IEnumerable<PersonDTO>> GetPersonsAsync(CancellationToken cancellationToken)
    {
        return (await personRepository.GetPersonsAsync(cancellationToken))
            .Select(c => new PersonDTO
            {
                IdPerson = c.IdCustomer,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Address = c.Address,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
                Pesel = c.Pesel
            });
    }

    public async Task<PersonDTO> GetPersonAsync(int id, CancellationToken cancellationToken)
    {
        var client = await personRepository.GetPersonAsync(id, cancellationToken);
        if (client == null) throw new RecordNotFoundException("Person not found");
        return new PersonDTO
        {
            IdPerson = client.IdCustomer,
            FirstName = client.FirstName,
            LastName = client.LastName,
            Address = client.Address,
            Email = client.Email,
            PhoneNumber = client.PhoneNumber,
            Pesel = client.Pesel
        };
    }

    public async Task<int> AddPersonAsync(PersonCreateDTO person, CancellationToken cancellationToken)
    {
        await unitOfWork.InitializeAsync(cancellationToken);
        var id = await personRepository.AddPersonAsync(new Person
        {
            FirstName = person.FirstName,
            LastName = person.LastName,
            Address = person.Address,
            Email = person.Email,
            PhoneNumber = person.PhoneNumber,
            Pesel = person.Pesel
        }, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
        return id;
    }

    public async Task<int> UpdatePersonAsync(int id, PersonUpdateDTO person, CancellationToken cancellationToken)
    {
        await unitOfWork.InitializeAsync(cancellationToken);
        await personRepository.UpdatePersonAsync(new Person
        {
            IdCustomer = id,
            FirstName = person.FirstName,
            LastName = person.LastName,
            Address = person.Address,
            Email = person.Email,
            PhoneNumber = person.PhoneNumber
        }, cancellationToken);
        return await unitOfWork.CommitAsync(cancellationToken);
    }

    public async Task<int> DeletePersonAsync(int id, CancellationToken cancellationToken)
    {
        await unitOfWork.InitializeAsync(cancellationToken);
        await personRepository.SoftDeletePersonAsync(id, cancellationToken);
        return await unitOfWork.CommitAsync(cancellationToken);
    }
}