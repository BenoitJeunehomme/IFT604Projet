using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IFT604Projet.Services.States.Contract;

namespace IFT604Projet.Services.States
{
    public class Completed : IState
    {
        public void GoNext(GameEventHandler handler)
        {
            //No other states after
        }
    }
}
