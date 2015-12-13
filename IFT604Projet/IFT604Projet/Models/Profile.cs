using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace IFT604Projet.Models
{
    public class Profile
    {
        [Key]
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Region { get; set; }
        public DateTime CreatedOn { get; set; }

        public Profile()
        {
            CreatedOn = DateTime.Now;
        }

    }

    public class ProfileDBContext : DbContext
    {
        public DbSet<Profile> Profiles { get; set; }
    }
}
