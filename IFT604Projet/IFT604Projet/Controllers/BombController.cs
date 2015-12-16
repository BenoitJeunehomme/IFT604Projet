using System;
using System.Linq;
using System.Web.Mvc;
using IFT604Projet.Models;
using IFT604Projet.ViewModels;

namespace IFT604Projet.Controllers
{
    public class BombController : Controller
    {
        private readonly ApplicationDbContext m_db = new ApplicationDbContext();

        // GET: Distance to closest bomb
        [HttpGet]
        [AllowAnonymous]
        public ActionResult ClosestDistance(double? lattitude, double? longitude)
        {
            double minDist = double.MaxValue;
            int bombId = -1;

            if (lattitude.HasValue && longitude.HasValue)
            {
                Point p = new Point(lattitude.Value, longitude.Value);
                //TODO Get currently running game event with its bombs
                var bombs = m_db.Bombs.ToList();

                foreach (var bomb in bombs)
                {
                    double distance = Distance(p, new Point(bomb.Latitude, bomb.Longitude));
                    if (!(minDist > distance)) continue;
                    bombId = bomb.Id;
                    minDist = distance;
                }
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
            if (!bombId.HasValue || string.IsNullOrWhiteSpace(username)) return Json(new DefuseConfirmationViewModel { BombId = -1, Defused = false }, JsonRequestBehavior.AllowGet);

            var bomb = m_db.Bombs.Find(bombId);
            var user = m_db.Users.FirstOrDefault(u => u.UserName.Equals(username));
            if (bomb == null || user == null || bomb.IsDefused) return Json(new DefuseConfirmationViewModel { BombId = bombId.Value, Defused = false }, JsonRequestBehavior.AllowGet);

            bomb.IsDefused = true;
            //TODO: Adjust points for users
            user.Score += 100;
            m_db.SaveChanges();
            return Json(new DefuseConfirmationViewModel { BombId = bombId.Value, Defused = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Plant(double? lattitude, double? longitude)
        {
            if (!lattitude.HasValue || !longitude.HasValue)
                return Json(new PlantConfirmationViewModel
                {
                    Lattitude = -1,
                    Longitude = -1,
                    Planted = false
                }, JsonRequestBehavior.AllowGet);

            var bomb = new Bomb
            {
                //TODO Get GameId
                IsDefused = false,
                Latitude = lattitude.Value,
                Longitude = longitude.Value
            };
            bomb.IsDefused = true;
            m_db.SaveChanges();

            return Json(new PlantConfirmationViewModel
            {
                //TODO Get gameId
                Lattitude = lattitude.Value,
                Longitude = longitude.Value,
                Planted = true
            }, JsonRequestBehavior.AllowGet);
        }

        public double Distance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow((p2.Y - p1.Y), 2) + Math.Pow((p2.X - p1.X), 2));
        }


    }

    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}