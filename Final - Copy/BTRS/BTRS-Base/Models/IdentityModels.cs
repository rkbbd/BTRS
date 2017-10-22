using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace BTRS_Base.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<BTRS_Server.Models.BTRSModel.Bus> Buses { get; set; }

        public System.Data.Entity.DbSet<BTRS_Server.Models.BTRSModel.BusSchedule> BusSchedules { get; set; }

        public System.Data.Entity.DbSet<BTRS_Server.Models.BTRSModel.Fare> Fares { get; set; }

        public System.Data.Entity.DbSet<BTRS_Server.Models.BTRSModel.Location> Locations { get; set; }

        public System.Data.Entity.DbSet<BTRS_Server.Models.BTRSModel.Payment> Payments { get; set; }


        public System.Data.Entity.DbSet<BTRS_Server.Models.BTRSModel.Transaction> Transactions { get; set; }

        public System.Data.Entity.DbSet<BTRS_Server.Models.BTRSModel.Passenger> Passengers { get; set; }

        public System.Data.Entity.DbSet<BTRS_Base.Models.ContractModel.Contract> Contracts { get; set; }
    }
}