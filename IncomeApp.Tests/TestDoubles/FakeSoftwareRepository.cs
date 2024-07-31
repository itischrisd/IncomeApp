using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IncomeApp.Models;
using IncomeApp.Repositories;

namespace IncomeApp.Tests.TestDoubles;

public class FakeSoftwareRepository : ISoftwareRepository
{
    private readonly IEnumerable<Software> _softwares = new List<Software>
    {
        new()
        {
            IdSoftware = 1,
            Name = "Visual Studio",
            Description = "Integrated development environment",
            CurrentVersion = "16.11.7",
            Category = "IDE",
            YearlyCost = 100,
            Discounts = new List<Discount>
            {
                new()
                {
                    IdDiscount = 1,
                    Name = "Back to school",
                    Offer = "Buy 1 get 1 free",
                    Percentage = 50,
                    StartDate = new DateOnly(2021, 9, 1),
                    EndDate = new DateOnly(2021, 9, 30)
                },
                new()
                {
                    IdDiscount = 2,
                    Name = "Black Friday",
                    Offer = "Buy 2 get 1 free",
                    Percentage = 33,
                    StartDate = new DateOnly(2021, 11, 26),
                    EndDate = new DateOnly(2021, 11, 26)
                }
            }
        },
        new()
        {
            IdSoftware = 2,
            Name = "Microsoft Office",
            Description = "Office suite",
            CurrentVersion = "16.0.14326.20404",
            Category = "Office",
            YearlyCost = 200
        },
        new()
        {
            IdSoftware = 3,
            Name = "Adobe Photoshop",
            Description = "Raster graphics editor",
            CurrentVersion = "22.5.1",
            Category = "Graphics",
            YearlyCost = 300,
            Discounts = new List<Discount>
            {
                new()
                {
                    IdDiscount = 3,
                    Name = "Christmas",
                    Offer = "Buy 3 get 1 free",
                    Percentage = 25,
                    StartDate = new DateOnly(2021, 12, 24),
                    EndDate = new DateOnly(2025, 12, 24)
                }
            }
        }
    };

    public Task<Software> GetSoftwareAsync(int id, CancellationToken cancellationToken)
    {
        return Task.FromResult(_softwares.FirstOrDefault(c => c.IdSoftware == id));
    }

    public Task<IEnumerable<Software>> GetSoftwaresAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(_softwares);
    }
}