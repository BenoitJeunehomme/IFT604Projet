using System;
using IFT604Projet.Models;

namespace IFT604Projet.Services.States.Contract
{
    public interface IState
    {
        void GoNext(GameEventHandler handler);
        GameEventState GetModelState();
        DateTime GetTimeToChangeState(GameEvent evt);
    }
}
