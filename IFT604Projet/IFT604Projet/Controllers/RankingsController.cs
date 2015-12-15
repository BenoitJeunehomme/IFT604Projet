using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IFT604Projet.Models;
using IFT604Projet.ViewModels;

namespace IFT604Projet.Controllers
{
    public class RankingsController : Controller
    {
        // GET: Rankings
        public ActionResult Index()
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

            return View(rankings);
        }
    }
}