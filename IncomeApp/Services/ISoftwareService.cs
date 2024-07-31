using IncomeApp.DTOs.Response;

namespace IncomeApp.Services;

public interface ISoftwareService
{
    Task<SoftwareDTO> GetSoftwareAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<SoftwareDTO>> GetSoftwaresAsync(CancellationToken cancellationToken);
}