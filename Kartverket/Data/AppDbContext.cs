using Kartverket.Models.DomainModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Kartverket.Models;

namespace Kartverket.Data
{
    /// <summary>
    /// Represents the database context for the application, extending <see cref="IdentityDbContext{TUser, TRole, TKey"/>
    /// to provide identity and role management.
    /// </summary>
    public class AppDbContext : IdentityDbContext<Users, IdentityRole, string>
    {


        /// <summary>
        /// initializes a new instance of the <see cref="AppDbContext"/> class.
        /// </summary>
        /// <param name="options">options for configuring the database context. </param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }


        // gets or sets the database set for tracking area changes
        public DbSet<AreaChangeModel> AreaChanges { get; set; }


        // gets or sets the database set for tracking submission statuses
        public DbSet<SubmitStatus> StatusState { get; set; }


        /// <summary>
        /// Configures the entity relationships, primary keys, and seeds initial data into the database
        /// </summary>
        /// <param name="modelBuilder">The builder used to construct the model.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Section for identity tables
            base.OnModelCreating(modelBuilder);


            // Unique Role IDs for seeding roles into the database
            var caseWorkerRoleId = "1";
            var privateUserRoleId = "2";


            // Seed roles (Caseworker, PrivateUser)
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Caseworker",
                    NormalizedName = "CASEWORKER",
                    Id = caseWorkerRoleId,
                    ConcurrencyStamp = caseWorkerRoleId
                },
                new IdentityRole
                {
                    Name = "PrivateUser",
                    NormalizedName = "PRIVATEUSER",
                    Id = privateUserRoleId,
                    ConcurrencyStamp = privateUserRoleId
                }
            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);


            // Seed Caseworker
            var caseWorkerId = "1";
            var caseWorker = new Users
            {
                Id = caseWorkerId,
                UserName = "caseworker@test.com",
                NormalizedUserName = "CASEWORKER@TEST.COM",
                Email = "caseworker@test.com",
                NormalizedEmail = "CASEWORKER@TEST.COM",
                FirstName = "Test",
                LastName = "Caseworker"
            };

            caseWorker.PasswordHash = new PasswordHasher<Users>().HashPassword(caseWorker, "caseworker@123");


            modelBuilder.Entity<Users>().HasData(caseWorker);


            // assign the caseworker role to the seeded user.
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = caseWorkerRoleId,
                    UserId = caseWorkerId
                }
            );


            // Seed PrivateUser
            var privateUserId = "2";
            var privateUser = new Users
            {
                Id = privateUserId,
                UserName = "privateUser@test.com",
                NormalizedUserName = "PRIVATEUSER@TEST.COM",
                Email = "privateuser@test.com",
                NormalizedEmail = "PRIVATEUSER@TEST.COM",
                FirstName = "Test",
                LastName = "PrivateUser"
            };

            privateUser.PasswordHash = new PasswordHasher<Users>().HashPassword(privateUser, "privateUser@123");

            modelBuilder.Entity<Users>().HasData(privateUser);


            // assign the PrivateUser role to the seeded user
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = privateUserRoleId,
                    UserId = privateUserId
                }
            );


            // configures the primary key for AreaChangeModel
            modelBuilder.Entity<AreaChangeModel>()
                .HasKey(uniqueThis => uniqueThis.Id);


            // configures the primary key for SubmitStatus
            modelBuilder.Entity<SubmitStatus>()
                .HasKey(uniqueiD => uniqueiD.Id);


            // Seeds predefined submission statues into the database
            modelBuilder.Entity<SubmitStatus>().HasData(

                new SubmitStatus { Id = 1, Status = "Under behandling" },
                 new SubmitStatus { Id = 2, Status = "Ferdig behandlet" },
                  new SubmitStatus { Id = 3, Status = "Avslått" },
                  new SubmitStatus { Id = 4, Status = "Ikke påbegynt" }
              );


            /* configured a foreign key relationship between AreaChangeModel and SubmitStatus.
            // Defines a one-to-many relationship where an area change belongs to submission status. */
            modelBuilder.Entity<AreaChangeModel>()
                .HasOne(s => s.SubmitStatusModel)
                .WithMany()
                .HasForeignKey(i => i.StatusId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
