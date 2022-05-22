using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDev2ndCW_2022.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppDev2ndCW_2022.Models
{
    public class DataBaseContext : IdentityDbContext<User>
    {
        // as per the entity framework this code insures if the database had been created or not, plus if not created it creates automatically
        // and DB context has also been implemented in sartup.cs
        // all public DBSet are for creating tables in the database

        public DbSet<User> User { get; set; }
        public DbSet<Actor> Actor { get; set; }
        public DbSet<Studio> Studio { get; set; }
        public DbSet<DvdCategory> DvdCategory { get; set; }
        public DbSet<Producer> Producer { get; set; }
        public DbSet<LoanTypes> LoanTypes { get; set; }
        public DbSet<MembershipCategory> MembershipCategory { get; set; }
        public DbSet<Member> Member { get; set; }
        public DbSet<DvdTitle> DvdTitle { get; set; }
        public DbSet<CastMember> CastMember { get; set; }
        public DbSet<DvdCopy> DvdCopy { get; set; }
        public DbSet<Loan> Loan { get; set; }


        

        public DataBaseContext(DbContextOptions<DataBaseContext> options)
            : base(options)
        {
            Database.EnsureCreated();

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CastMember>().HasKey(table => new {
                table.DvdNumber,
                table.ActorNumber
            });
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new RoleConfiguration());
        }
    }
   
}

