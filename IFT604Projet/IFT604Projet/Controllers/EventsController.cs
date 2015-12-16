using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using IFT604Projet.Models;
using IFT604Projet.ViewModels;
using Newtonsoft.Json;

namespace IFT604Projet.Controllers
{
    public class EventsController : Controller
    {
        public ActionResult Index()
        {
            var eventContext = new ApplicationDbContext();
            
            List<GameEvent> gameEvents = eventContext.GameEvents
                                            .Include(evnt => evnt.Region)
                                            .Include(evnt => evnt.Bombs)
                                            .Where(evnt => evnt.InProgress)
                                            .ToList();
            return View(gameEvents);
        }
    }
}