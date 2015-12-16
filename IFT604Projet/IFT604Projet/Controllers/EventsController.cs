using IFT604Projet.Models;
using IFT604Projet.ViewModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using IFT604Projet.Services;

namespace IFT604Projet.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext m_db = new ApplicationDbContext();

        public ActionResult Index()
        {
            List<GameEvent> gameEvents = m_db.GameEvents
                                            .Include(evnt => evnt.Region)
                                            .Include(evnt => evnt.Bombs)
                                            .Where(evnt => evnt.State != GameEventState.Completed)
                                            .ToList();
            return View(gameEvents);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult State(int? regionId)
        {
            if (!regionId.HasValue) return Json(new GameEventStateViewModel { RegionId = -1, State = GameEventState.Completed });

            var state =
                GameEventService.GetState(regionId.Value);

                return Json(new GameEventStateViewModel { RegionId = regionId.Value, State = state }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            ViewBag.RegionId = new SelectList(m_db.Regions, "Id", "Name");

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateGameEventViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            if (GameEventService.OngoingEvent(model.RegionId))
            {
                ModelState.AddModelError("", "Region already as an event scheduled!");
                return View(model);
            }

            var gameEvent = new GameEvent
            {
                State = GameEventState.NotStarted,
                StartPlacing = model.StartPlacing,
                EndPlacing = model.EndPlacing,
                StartDefusing = model.StartDefusing,
                StopDefusing = model.EndDefusing,
                RegionId = model.RegionId
            };

            m_db.GameEvents.Add(gameEvent);
            GameEventService.StartEvent(gameEvent);

            return RedirectToAction("Index", "Events");
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