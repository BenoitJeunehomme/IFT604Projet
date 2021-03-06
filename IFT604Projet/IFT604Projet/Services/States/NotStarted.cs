﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IFT604Projet.Models;
using IFT604Projet.Services.States.Contract;

namespace IFT604Projet.Services.States
{
   public class NotStarted : IState
    {
       public void GoNext(GameEventHandler handler)
       {
           Console.WriteLine("Changing from NotStarted to Placing");
           handler.ChangeState(new Placing());
       }

       public GameEventState GetModelState()
       {
           return GameEventState.NotStarted;
       }

       public DateTime GetTimeToChangeState(GameEvent evt)
       {
           return evt.StartPlacing;
       }
    }
}
