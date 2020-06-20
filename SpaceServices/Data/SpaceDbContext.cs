using Microsoft.EntityFrameworkCore;
using SpaceServices.Data.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceServices.Data
{
    public class SpaceDbContext : DbContext
    {
        public SpaceDbContext(DbContextOptions<SpaceDbContext> options) : base(options)
        {

        }

        DbSet<Link> Links { get; set; }
    }
}
