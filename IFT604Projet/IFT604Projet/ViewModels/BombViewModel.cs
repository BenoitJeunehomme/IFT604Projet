using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFT604Projet.ViewModels
{
    public class ClosestBombDistanceViewModel
    {
        public int BombId { get; set; }

        public double Distance { get; set; }
    }

    public class DefuseConfirmationViewModel
    {
        public int BombId { get; set; }
        public bool Defused { get; set; }
    }

    public class PlantConfirmationViewModel
    {
        public bool Planted { get; set; }
        public double Lattitude { get; set; }
        public double Longitude { get; set; }
    }
}
