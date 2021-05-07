﻿using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext>options) : base(options) { }

        DbSet<SensorLog> SensorLogs { get; set; }
        DbSet<StoreSection> StoreSections { get; set; }
    }
}
