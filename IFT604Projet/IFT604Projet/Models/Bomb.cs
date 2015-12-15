using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IFT604Projet.Models
{
    public class Bomb
    {
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool IsDefused { get; set; }

        public int GameId { get; set; }
        public GameEvent PlantedForGame { get; set; }
    }
}