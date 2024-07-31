using IncomeApp.DTOs.Response;
using IncomeApp.Exceptions;
using IncomeApp.Repositories;

namespace IncomeApp.Services;

public class SoftwareService(ISoftwareRepository softwareRepository) : ISoftwareService
{
    public async Task<SoftwareDTO> GetSoftwareAsync(int id, CancellationToken cancellationToken)
    {
        var software = await softwareRepository.GetSoftwareAsync(id, cancellationToken);
        if (software == null) throw new RecordNotFoundException("Software not found");
        return new SoftwareDTO
        {
            IdSoftware = software.IdSoftware,
            Name = software.Name,
            Description = software.Description,
            CurrentVersion = software.CurrentVersion,
            Category = software.Category,
            YearlyCost = software.YearlyCost,
            Discounts = software.Discounts.Select(d => new DiscountDTO
                {
                    IdDiscount = d.IdDiscount,
                    Name = d.Name,
                    Offer = d.Offer,
                    Percentage = d.Percentage,
                    StartDate = d.StartDate,
                    EndDate = d.EndDate
                })
                .ToList()
        };
    }

    public async Task<IEnumerable<SoftwareDTO>> GetSoftwaresAsync(CancellationToken cancellationToken)
    {
        var softwares = await softwareRepository.GetSoftwaresAsync(cancellationToken);
        return softwares.Select(s => new SoftwareDTO
        {
            IdSoftware = s.IdSoftware,
            Name = s.Name,
            Description = s.Description,
            CurrentVersion = s.CurrentVersion,
            Category = s.Category,
            YearlyCost = s.YearlyCost,
            Discounts = s.Discounts.Select(d => new DiscountDTO
                {
                    IdDiscount = d.IdDiscount,
                    Name = d.Name,
                    Offer = d.Offer,
                    Percentage = d.Percentage,
                    StartDate = d.StartDate,
                    EndDate = d.EndDate
                })
                .ToList()
        });
    }
}