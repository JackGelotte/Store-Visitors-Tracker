using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
namespace API.Data
{
    public class Seed
    {
        private static ApiContext _context { get; set; }

        public async static Task Seeder(IServiceProvider services)
        {
            _context = services.GetRequiredService<ApiContext>();

            await _context.Database.EnsureDeletedAsync();
            await _context.Database.EnsureCreatedAsync();

            await SeedSections();
        }

        // Adds a sections in our mock-store
        static async Task SeedSections()
        {
            var sections = new StoreSection[]
            {
                new StoreSection{Name="Clothes"},
                new StoreSection{Name="Electronics"},
                new StoreSection{Name="Kitchen"},
                new StoreSection{Name="Bedroom"},
                new StoreSection{Name="Recreation"},
            };

            await _context.StoreSections.AddRangeAsync(sections);
            await _context.SaveChangesAsync();
        }
    }
}
