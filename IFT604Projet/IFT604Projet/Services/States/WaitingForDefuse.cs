﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IFT604Projet.Models;
using IFT604Projet.Services.States.Contract;

namespace IFT604Projet.Services.States
{
    public class WaitingForDefuse : IState
    {
        public void GoNext(GameEventHandler handler)
        {
            handler.ChangeState(new Defusing());
        }

        public GameEventState GetModelState()
        {
            return GameEventState.WaitingForDefuse;
        }
    }
}
