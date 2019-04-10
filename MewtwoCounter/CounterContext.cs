using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MewtwoCounter
{
    public class CounterContext : DbContext
    {
        public CounterContext(DbContextOptions<CounterContext> options)
            : base(options)
        {
        }

        public DbSet<Counter> Counters { get; set; }

    }
}
