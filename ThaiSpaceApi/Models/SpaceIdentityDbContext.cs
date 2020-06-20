using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SpaceServices.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThaiSpaceApi.Models
{
    public class SpaceIdentityDbContext : IdentityDbContext
    {
        public SpaceIdentityDbContext(DbContextOptions<SpaceIdentityDbContext> options) : base(options)
        {
        }

        DbSet<SpaceUser> SpaceUsers { get; set; }
    }
}
