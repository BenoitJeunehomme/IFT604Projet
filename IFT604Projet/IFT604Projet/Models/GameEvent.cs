using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IFT604Projet.Models
{
    public class GameEvent
    {
        [Key]
        public int GameEventId { get; set; }

        [Required]
        public DateTime StartPlacing { get; set; }

        [Required]
        public DateTime EndPlacing { get; set; }

        [Required]
        public DateTime StartDefusing { get; set; }

        [Required]
        public DateTime StopDefusing { get; set; }

        public bool  InProgress { get; set; }

        public int RegionId { get; set; }
        public Region Region { get; set; }

        public ICollection<Bomb> Bombs { get; set; }

        [InverseProperty("DefuserGames")]
        public virtual ICollection<ApplicationUser> Defusers { get; set; }
        [InverseProperty("PlacerGames")]
        public virtual ICollection<ApplicationUser> Placers { get; set; }

    }
}