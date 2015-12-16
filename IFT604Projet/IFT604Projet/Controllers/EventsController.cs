using IFT604Projet.Models;
using IFT604Projet.ViewModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace IFT604Projet.Controllers
{
    public class EventsController : Controller
    {
        private static readonly Dictionary<int, GameEventHandler> GAME_EVENT_HANDLERS = new Dictionary<int, GameEventHandler>();
        private readonly ApplicationDbContext m_db = new ApplicationDbContext();

        public static void Initialize()
        {
            var db = new ApplicationDbContext();

            var events = db.GameEvents.Where(g => g.State != GameEventState.Completed)
                .Include(g => g.Placers).Include(g => g.Defusers).Include(g => g.Region).ToList();

            foreach (var gameEvent in events)
                GAME_EVENT_HANDLERS.Add(gameEvent.RegionId, new GameEventHandler(gameEvent));
        }

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

            var evt =
                m_db.GameEvents.FirstOrDefault(e => e.RegionId == regionId.Value && e.State != GameEventState.Completed);
            return evt == null
                ? Json(new GameEventStateViewModel { RegionId = -1, State = GameEventState.Completed })
                : Json(new GameEventStateViewModel { RegionId = evt.RegionId, State = evt.State }, JsonRequestBehavior.AllowGet);
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

            if (GAME_EVENT_HANDLERS.ContainsKey(model.RegionId))
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
            GAME_EVENT_HANDLERS.Add(gameEvent.RegionId, new GameEventHandler(gameEvent));

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

    internal class GameEventHandler
    {
        private GameEvent evt;

        public GameEventHandler(GameEvent gameEvent)
        {
            evt = gameEvent;
        }
    }
}