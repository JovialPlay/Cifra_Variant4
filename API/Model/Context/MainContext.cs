using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Numerics;
using System.Reflection.Metadata;

namespace API.Model.Context
{
    public class MainContext(IConfiguration configuration) : IdentityDbContext
    {
        protected readonly IConfiguration Configuration = configuration;

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
        }
        public virtual required DbSet<Data> Data { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Data>(entity =>
            {
                entity.HasKey(e => e.Key);
                entity.Property(e => e.Value).IsRequired();
            });
        }
    }
}
