using System.Collections.Generic;
using System.Web.Mvc;
using IFT604Projet.Models;
using IFT604Projet.ViewModels;

namespace IFT604Projet.Controllers
{
    public class EventController : Controller
    {
        private static Dictionary<int, GameEvent> GameEvents = new Dictionary<int,GameEvent>();
        private readonly ApplicationDbContext m_db = new ApplicationDbContext();

        // GET: Test
        public ActionResult Index()
        {
            //TODO: Prepare list of game events from dictionary
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                m_db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}