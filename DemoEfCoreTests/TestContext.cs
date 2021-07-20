using DemoEfCore.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoEfCoreTests
{
    public class TestContext : DbContext
    {
        public DbSet<Pessoa> Pessoas { get; set; }

        public TestContext(DbContextOptions<TestContext> options) : base(options)
        {

        }
    }
}
