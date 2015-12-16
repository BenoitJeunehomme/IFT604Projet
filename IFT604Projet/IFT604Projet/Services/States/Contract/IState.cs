namespace IFT604Projet.Services.States.Contract
{
    public interface IState
    {
        void GoNext(GameEventHandler handler);
    }
}
