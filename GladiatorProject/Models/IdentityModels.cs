using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GladiatorProject.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        // Adding  need to be added in all locations it used.
        public List<Gladiator> Gladiators { get; set; }

        public List<SupportRequests> Supports { get; set; }

        public int AccountScore { get; set; } // will become the score for the account and the high score for players.

        public int AccountHighScore { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
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

        public DbSet<Gladiator> Gladiators { get; set; } // the database for gladiators 

        public DbSet<Opponent> Opponents { get; set; } // database for opponents

        public DbSet<BattleStart> Battles { get; set; } // database for each battle.

        public DbSet<SupportRequests> Supports { get; set; } 
        

    }
}