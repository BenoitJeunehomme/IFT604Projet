using IFT604Projet.Models;
using IFT604Projet.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace IFT604Projet.Controllers
{
    public class RankingsController : Controller
    {
        // GET: Rankings
        public ActionResult Index(int? regionId)
        {
            var db = new ApplicationDbContext();
            var rankings = from u in db.Users
                           orderby u.Score descending
                           select new RankingViewModel
                           {
                               RegionId = u.RegionId,
                               Score = u.Score,
                               Name = u.UserName
                           };

            // ViewBag.RegionId = new SelectList(db.Regions.ToList(), "Id", "Name");

            return View(rankings);
        }

        public ActionResult List(string username)
        {

            if (string.IsNullOrWhiteSpace(username))
                return Json(new RankingViewModel
                {
                    RegionId = -1,
                    Score = 0,
                    Name = ""
                }, JsonRequestBehavior.AllowGet);

            var db = new ApplicationDbContext();

            var user = db.Users.FirstOrDefault(u => u.UserName.Equals(username));
            if(user == null)
                return Json(new RankingViewModel
                {
                    RegionId = -1,
                    Score = 0,
                    Name = ""
                }, JsonRequestBehavior.AllowGet);

            var rankings = from u in db.Users.Where(u => u.RegionId == user.RegionId)
                           orderby u.Score descending
                           select new RankingViewModel
                           {
                               RegionId = u.RegionId,
                               Score = u.Score,
                               Name = u.UserName
                           };

            // ViewBag.RegionId = new SelectList(db.Regions.ToList(), "Id", "Name");

            return Json(rankings, JsonRequestBehavior.AllowGet);
        }
    }
}