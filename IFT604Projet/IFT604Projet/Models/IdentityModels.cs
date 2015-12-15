using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IFT604Projet.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public int RegionId { get; set; }
        public Region Region { get; set; }
        public int Score { get; set; }
        public byte[] Avatar { get; set; }

        [InverseProperty("Defusers")]
        public virtual ICollection<GameEvent> DefuserGames { get; set; }
        [InverseProperty("Placers")]
        public virtual ICollection<GameEvent> PlacerGames { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Region> Regions { get; set; }
        public DbSet<Bomb> Bombs { get; set; }
        public DbSet<GameEvent> GameEvents { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.DefuserGames)
                .WithMany(t => t.Defusers)
                .Map(x =>
                {
                    x.MapLeftKey("Id");
                    x.MapRightKey("GameEventId");
                    x.ToTable("DefusersGameEvent");
                });

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.PlacerGames)
                .WithMany(t => t.Placers)
                .Map(x =>
                {
                    x.MapLeftKey("Id");
                    x.MapRightKey("GameEventId");
                    x.ToTable("PlacersGameEvent");
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}