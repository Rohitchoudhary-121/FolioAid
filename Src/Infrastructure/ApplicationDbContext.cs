using Domain.Entity;
using Domain.Entity.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>,
    ApplicationUserRole, IdentityUserLogin<string>,
    IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
             : base(options)
        {

        }

        public DbSet<ApplicationUser> AspNetUsers { get; set; }
        public DbSet<ApplicationRole> AspNetRoles { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<Industry> Industries { get; set; }
        public DbSet<ProjectIndustryMapping> ProjectIndustryMappings { get; set; }
        public DbSet<ProjectTechnologyMapping> ProjectTechnologyMappings { get; set; }
        public DbSet<ProjectImageGallery> ProjectImageGalleries { get; set; }
        public DbSet<ProjectVideoGallery> ProjectVideoGalleries { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().ToTable("AspNetUser").Property(p => p.Id).HasColumnName("Id");
            builder.Entity<ApplicationUserRole>().ToTable("AspNetUserRole");
            builder.Entity<IdentityUserLogin<string>>().ToTable("AspNetUserLogin");
            builder.Entity<IdentityUserClaim<string>>().ToTable("AspNetUserClaim");
            builder.Entity<ApplicationRole>().ToTable("AspNetRole");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("AspNetRoleClaim");


            builder.Entity<ApplicationUserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });
            builder.Entity<Industry>();

            builder.Entity<Technology>();

        }
    }
}
