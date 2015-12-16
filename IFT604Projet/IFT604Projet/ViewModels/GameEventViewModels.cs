using IFT604Projet.Models;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IFT604Projet.Controllers
{
    public class GameEventStateViewModel
    {
        public GameEventState State { get; set; }
        public string Username { get; set; }
    }

    public class CreateGameEventViewModel
    {
        public int RegionId { get; set; }

        [Required]
        [DisplayName("Placing Start Date/Time")]
        public DateTime StartPlacing { get; set; }

        [Required]
        [DisplayName("Placing End Date/Time")]
        public DateTime EndPlacing { get; set; }

        [Required]
        [DisplayName("Defusing Start Date/Time")]
        public DateTime StartDefusing { get; set; }

        [Required]
        [DisplayName("Defusing End Date/Time")]
        public DateTime EndDefusing { get; set; }
    }
}