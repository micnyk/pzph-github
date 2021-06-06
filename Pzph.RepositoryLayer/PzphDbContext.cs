using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pzph.ServiceLayer.Bookings.Domain;
using Pzph.ServiceLayer.Categories.Domain;
using Pzph.ServiceLayer.Contractors.Domain;
using Pzph.ServiceLayer.Customers.Domain;
using Pzph.ServiceLayer.Users.Domain;

namespace Pzph.RepositoryLayer
{
    public class PzphDbContext : IdentityDbContext<User>
    {
        public PzphDbContext(DbContextOptions<PzphDbContext> options) : base(options)
        {
        }

        public DbSet<Contractor> Contractors { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Category> Category { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Contractor>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.CreatedAt);
                b.HasMany(x => x.OfferedServices).WithOne(x => x.Contractor);
                b.HasOne(x => x.User).WithOne(x => x.Contractor).HasForeignKey<Contractor>(x => x.UserId);
            });

            builder.Entity<Service>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.CreatedAt);
                b.Property(x => x.Name);
                b.Property(x => x.Description);
                b.HasOne(x => x.Contractor).WithMany(x => x.OfferedServices);
            });

            builder.Entity<Customer>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.CreatedAt);
                b.HasOne(x => x.User).WithOne(x => x.Customer).HasForeignKey<Customer>(x => x.UserId);
                b.HasMany(x => x.Bookings).WithOne(x => x.Customer);
            });

            builder.Entity<Booking>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.CreatedAt);
                b.HasOne(x => x.Customer).WithMany(x => x.Bookings).HasForeignKey(x => x.CustomerId);
            });

            builder.Entity<Category>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.CreatedAt);
                b.Property(x => x.Name);
                b.HasMany(x => x.Services).WithOne(x => x.Category);
            });
        }
    }
}