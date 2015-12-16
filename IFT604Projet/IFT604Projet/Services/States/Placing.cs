using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IFT604Projet.Models;
using IFT604Projet.Services.States.Contract;

namespace IFT604Projet.Services.States
{
    public class Placing : IState
    {
        public void GoNext(GameEventHandler handler)
        {
            Console.WriteLine("Changing from Placing to WaitingForDefuse");

            handler.ChangeState(new WaitingForDefuse());
        }

        public GameEventState GetModelState()
        {
            return GameEventState.Placing;
        }

        public DateTime GetTimeToChangeState(GameEvent evt)
        {
            return evt.EndPlacing;
        }
    }
}
