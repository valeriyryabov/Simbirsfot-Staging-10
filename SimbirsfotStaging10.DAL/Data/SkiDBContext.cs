using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SimbirsfotStaging10.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SimbirsfotStaging10.DAL.Data
{
	public class SkiDBContext : IdentityDbContext<User,CustomRole,int>
    {
        public SkiDBContext(DbContextOptions options) : base(options) { }


        public DbSet<Card> Cards { get; set; }
        public DbSet<CardPlatformItem> CardPlatformItemSet { get; set; }
        public DbSet<EventLog> EventLogSet { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<UserEquipmentItem> UserEquipmentItemSet { get; set; }
        public DbSet<Equipment> EquipmentSet { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .HasAlternateKey(u => u.Email);
        }


        public class EFDBContextFactory : IDesignTimeDbContextFactory<SkiDBContext>
        {
            SkiDBContext IDesignTimeDbContextFactory<SkiDBContext>.CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder();
                optionsBuilder.UseSqlServer(@"Server = localhost\SQLEXPRESS; Database = test; Trusted_Connection = True;");
                return new SkiDBContext(optionsBuilder.Options);
            }
        }
    }
}
