using IncomeApp.Models;
using Microsoft.EntityFrameworkCore;

namespace IncomeApp.Repositories;

public class SoftwareRepository(Context context) : ISoftwareRepository
{
    public async Task<Software> GetSoftwareAsync(int id, CancellationToken cancellationToken)
    {
        return (await context.Softwares
            .Include(s => s.Discounts)
            .FirstOrDefaultAsync(s => s.IdSoftware == id, cancellationToken))!;
    }

    public async Task<IEnumerable<Software>> GetSoftwaresAsync(CancellationToken cancellationToken)
    {
        return await context.Softwares
            .Include(s => s.Discounts)
            .ToListAsync(cancellationToken);
    }
}