using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IFT604Projet.Models;
using IFT604Projet.Services.States.Contract;
using IFT604Projet.ViewModels;

namespace IFT604Projet.Services
{
    public class GameEventHandler
    {
        private readonly GameEvent m_evt;
        private ApplicationDbContext m_db;
        private IState currentState;

        public GameEventHandler(GameEvent gameEvent, ApplicationDbContext db)
        {
            m_evt = gameEvent;
            m_db = db;
        }

        public GameEventState GetState()
        {
            return m_evt.State;
        }

        public void ChangeState(IState state)
        {
            currentState = state;
        }
    }
}
