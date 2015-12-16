using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Spi;

namespace IFT604Projet.Services.Jobs
{
    public class GameEventJobFactory : IJobFactory
    {
        private GameEventHandler gameEventHandler;

        public GameEventJobFactory(GameEventHandler gameEventHandler)
        {
            this.gameEventHandler = gameEventHandler;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return new GameEventJob(gameEventHandler);
        }

        public void ReturnJob(IJob job)
        {
        }
    }
}
