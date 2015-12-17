using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using IFT604Projet.Models;
using IFT604Projet.Services;
using IFT604Projet.ViewModels;

namespace IFT604Projet.Controllers
{
    public class BombController : Controller
    {
        private readonly ApplicationDbContext m_db = new ApplicationDbContext();

        // GET: Distance to closest bomb
        [HttpGet]
        [AllowAnonymous]
        public ActionResult ClosestDistance(double? lattitude, double? longitude, string username)
        {
            double minDist = double.MaxValue;
            int bombId = -1;

            if (!lattitude.HasValue || !longitude.HasValue || string.IsNullOrWhiteSpace(username))
                return Json(new ClosestBombDistanceViewModel()
                {
                    Distance = minDist,
                    BombId = bombId
                }, JsonRequestBehavior.AllowGet);

            var user = m_db.Users.FirstOrDefault(u => u.UserName.Equals(username));
            if(user == null)
                return Json(new ClosestBombDistanceViewModel()
                {
                    Distance = minDist,
                    BombId = bombId
                }, JsonRequestBehavior.AllowGet);

            GPSPostition p = new GPSPostition(lattitude.Value, longitude.Value);
            int gameId = GameEventService.GetGameId(user.RegionId);
            var bombs = m_db.Bombs.Where(b => b.GameId == gameId && !b.IsDefused).ToList();

            foreach (var bomb in bombs)
            {
                double distance = Distance(p, new GPSPostition(bomb.Latitude, bomb.Longitude));
                if (!(minDist > distance)) continue;
                bombId = bomb.Id;
                minDist = distance;
            }

            return Json(new ClosestBombDistanceViewModel()
            {
                Distance = minDist,
                BombId = bombId
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Defuse(int? bombId, string username)
        {
            if (!bombId.HasValue || string.IsNullOrWhiteSpace(username))
                return Json(new DefuseConfirmationViewModel { BombId = -1, Defused = false }, JsonRequestBehavior.AllowGet);

            var bomb = m_db.Bombs.Include(b => b.PlantedForGame).FirstOrDefault(b => b.Id == bombId.Value);
            var user = m_db.Users.FirstOrDefault(u => u.UserName.Equals(username));

            if (bomb == null || user == null || bomb.IsDefused || bomb.PlantedForGame.State != GameEventState.Defusing)
                return Json(new DefuseConfirmationViewModel { BombId = bombId.Value, Defused = false }, JsonRequestBehavior.AllowGet);

            bomb.IsDefused = true;
            user.Score += 100;

            m_db.SaveChanges();
            return Json(new DefuseConfirmationViewModel { BombId = bombId.Value, Defused = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Plant(double? lattitude, double? longitude, string username)
        {
            if (!lattitude.HasValue || !longitude.HasValue || string.IsNullOrWhiteSpace(username))
                return Json(new PlantConfirmationViewModel
                {
                    Lattitude = -1,
                    Longitude = -1,
                    Planted = false
                }, JsonRequestBehavior.AllowGet);

            var user = m_db.Users.FirstOrDefault(u => u.UserName.Equals(username));
            if (user == null)
                return Json(new PlantConfirmationViewModel
                {
                    Lattitude = -1,
                    Longitude = -1,
                    Planted = false
                }, JsonRequestBehavior.AllowGet);

            if(GameEventService.GetState(user.RegionId) != GameEventState.Placing)
                return Json(new PlantConfirmationViewModel
                {
                    Lattitude = -1,
                    Longitude = -1,
                    Planted = false
                }, JsonRequestBehavior.AllowGet);


            var bomb = new Bomb
            {
                GameId = GameEventService.GetGameId(user.RegionId),
                IsDefused = false,
                Latitude = lattitude.Value,
                Longitude = longitude.Value
            };
            bomb.IsDefused = false;
            m_db.Bombs.Add(bomb);
            m_db.SaveChanges();

            return Json(new PlantConfirmationViewModel
            {
                Lattitude = lattitude.Value,
                Longitude = longitude.Value,
                Planted = true
            }, JsonRequestBehavior.AllowGet);
        }

        public double Distance(GPSPostition p1, GPSPostition p2)
        {
            var latMid = (p1.Latitude + p2.Latitude) / 2.0;

            var m_per_deg_lat = 111132.954 - 559.822 * Math.Cos(2.0 * latMid) + 1.175 * Math.Cos(4.0 * latMid);
            var m_per_deg_lon = (3.14159265359 / 180) * 6367449 * Math.Cos(latMid);

            var deltaLat = Math.Abs(p1.Latitude - p2.Latitude);
            var deltaLon = Math.Abs(p1.Longitude - p2.Longitude);

            return Math.Sqrt(Math.Pow(deltaLat * m_per_deg_lat, 2) + Math.Pow(deltaLon * m_per_deg_lon, 2));
        }


    }

    public class GPSPostition
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public GPSPostition(double lat, double lg)
        {
            Latitude = lat;
            Longitude = lg;
        }
    }
}