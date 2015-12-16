using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IFT604Projet.Models;
using IFT604Projet.ViewModels;

namespace IFT604Projet.Services
{
    public class GameEventService
    {
        private static readonly Dictionary<int, GameEventHandler> GAME_EVENT_HANDLERS = new Dictionary<int, GameEventHandler>();

        private static readonly ApplicationDbContext DB = new ApplicationDbContext();

        public static void Initialize()
        {
            var db = new ApplicationDbContext();

            var events = db.GameEvents.Where(g => g.State != GameEventState.Completed)
                .Include(g => g.Placers).Include(g => g.Defusers).Include(g => g.Region).ToList();

            foreach (var gameEvent in events)
                GAME_EVENT_HANDLERS.Add(gameEvent.RegionId, new GameEventHandler(gameEvent, db));
        }

        internal class GameEventHandler
        {
            private readonly GameEvent evt;
            private ApplicationDbContext m_db;

            public GameEventHandler(GameEvent gameEvent, ApplicationDbContext db)
            {
                evt = gameEvent;
                m_db = db;
            }

            public GameEventState GetState()
            {
                return evt.State;
            }
        }

        public static GameEventState GetState(int regionId)
        {
            return GAME_EVENT_HANDLERS.ContainsKey(regionId)
                ? GAME_EVENT_HANDLERS[regionId].GetState()
                : GameEventState.Completed;
        }

        public static bool OngoingEvent(int regionId)
        {
            return GAME_EVENT_HANDLERS.ContainsKey(regionId);
        }

        public static void StartEvent(GameEvent gameEvent)
        {
            GAME_EVENT_HANDLERS.Add(gameEvent.RegionId, new GameEventHandler(gameEvent,DB) );
        }
    }
}
