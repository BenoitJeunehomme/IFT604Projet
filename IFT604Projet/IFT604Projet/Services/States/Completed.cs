using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IFT604Projet.Models;
using IFT604Projet.Services.States.Contract;

namespace IFT604Projet.Services.States
{
    public class Completed : IState
    {
        public void GoNext(GameEventHandler handler)
        {
            Console.WriteLine("Changing from Completed to clisig the event and giving points");

            //No other states after
            handler.CloseEvent();
        }

        public GameEventState GetModelState()
        {
            return GameEventState.Completed;
        }

        public DateTime GetTimeToChangeState(GameEvent evt)
        {
            return DateTime.Now.AddYears(-1);
        }
    }
}
