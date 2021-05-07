using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        }
    }
}
