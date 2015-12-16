using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;

namespace IFT604Projet.Services.Jobs
{
    public class GameEventJob : IJob
    {
        private GameEventHandler gameEventHandler;

        public GameEventJob(GameEventHandler gameEventHandler)
        {
            this.gameEventHandler = gameEventHandler;
        }

        public void Execute(IJobExecutionContext context)
        {
            DateTime nextTransitionDateTime = gameEventHandler.GetNextScheduledChange();
            if (DateTime.Now < nextTransitionDateTime)
            {
                Console.WriteLine($"{nextTransitionDateTime} not yet reached. Waiting");
                return;
            }
            gameEventHandler.GoNext();
        }
    }
}
