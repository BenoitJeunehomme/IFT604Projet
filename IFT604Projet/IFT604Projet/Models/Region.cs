using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using IFT604Projet.ViewModels;

namespace IFT604Projet.Models
{
    public class Region
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Radius { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public ICollection<ApplicationUser> Members { get; set; }
    }
}