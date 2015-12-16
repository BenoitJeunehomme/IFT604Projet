using IFT604Projet.Models;
using IFT604Projet.Services.States;
using IFT604Projet.Services.States.Contract;

namespace IFT604Projet.Services
{
    public class GameEventHandler
    {
        private readonly GameEvent m_evt;
        private readonly ApplicationDbContext m_db;
        private IState m_currentState;

        public GameEventHandler(GameEvent gameEvent, ApplicationDbContext db)
        {
            m_evt = gameEvent;
            m_db = db;
            m_currentState = GetCurrentGameState(m_evt);
            
        }

        public GameEventState GetState()
        {
            return m_evt.State;
        }

        public void ChangeState(IState state)
        {
            m_currentState = state;

            m_evt.State = state.GetModelState();
            var evt = m_db.GameEvents.Find(m_evt.GameEventId);
            evt.State = m_evt.State;
            m_db.SaveChanges();
        }

        public IState GetCurrentGameState(GameEvent evt)
        {
            switch (evt.State)
            {
                case GameEventState.NotStarted:
                    return new NotStarted();
                case GameEventState.Placing:
                    return new Placing();
                case GameEventState.WaitingForDefuse:
                    return new WaitingForDefuse();
                case GameEventState.Defusing:
                    return new Defusing();
                case GameEventState.Completed:
                default:
                    return new Completed();
            }
        }

        public void CloseEvent()
        {
            // Stop scheduler
            // Assign points to users
        }
    }
}