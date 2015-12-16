using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IFT604Projet.Models;
using IFT604Projet.Services.States.Contract;

namespace IFT604Projet.Services.States
{
    public class Defusing : IState
    {
        public void GoNext(GameEventHandler handler)
        {
            Console.WriteLine("Changing from Defusing to Completed");

            handler.ChangeState(new Completed());
        }

        public GameEventState GetModelState()
        {
            return GameEventState.Defusing;
        }

        public DateTime GetTimeToChangeState(GameEvent evt)
        {
            return evt.StopDefusing;
        }
    }
}
