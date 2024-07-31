using System.Linq;
using System.Threading.Tasks;
using IncomeApp.Exceptions;
using IncomeApp.Services;
using IncomeApp.Tests.TestDoubles;
using JetBrains.Annotations;
using Xunit;

namespace IncomeApp.Tests.Services;

[TestSubject(typeof(SoftwareService))]
public class SoftwareServiceTest
{
    [Fact]
    public async Task GetSoftwareAsync_Should_Return_Software_With_Correct_Id()
    {
        var softwareRepository = new FakeSoftwareRepository();
        var softwareService = new SoftwareService(softwareRepository);
        const int id = 1;

        var software = await softwareService.GetSoftwareAsync(id, default);

        Assert.Equal("Visual Studio", software.Name);
        Assert.Equal("Integrated development environment", software.Description);
        Assert.Equal("16.11.7", software.CurrentVersion);
        Assert.Equal("IDE", software.Category);
        Assert.Equal(100, software.YearlyCost);
        Assert.Equal(2, software.Discounts.Count);
    }

    [Fact]
    public async Task GetSoftwareAsync_Should_Throw_RecordNotFoundException_When_Software_Not_Found()
    {
        var softwareRepository = new FakeSoftwareRepository();
        var softwareService = new SoftwareService(softwareRepository);
        const int id = 4;

        await Assert.ThrowsAsync<RecordNotFoundException>(() => softwareService.GetSoftwareAsync(id, default));
    }

    [Fact]
    public async Task GetSoftwaresAsync_Should_Return_All_Softwares()
    {
        var softwareRepository = new FakeSoftwareRepository();
        var softwareService = new SoftwareService(softwareRepository);

        var softwares = await softwareService.GetSoftwaresAsync(default);

        Assert.Equal(3, softwares.Count());
    }
}