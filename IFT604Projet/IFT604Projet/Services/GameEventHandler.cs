using System;
using System.Data.Entity;
using System.Linq;
using IFT604Projet.Models;
using IFT604Projet.Services.Jobs;
using IFT604Projet.Services.States;
using IFT604Projet.Services.States.Contract;
using Quartz;
using Quartz.Impl;

namespace IFT604Projet.Services
{
    public class GameEventHandler
    {
        private readonly GameEvent m_evt;
        private readonly ApplicationDbContext m_db;
        private IState m_currentState;
        private readonly IScheduler m_scheduler;

        public GameEventHandler(GameEvent gameEvent, ApplicationDbContext db)
        {
            m_evt = gameEvent;
            m_db = db;
            m_currentState = InitializeGameState(m_evt);

            m_scheduler = StdSchedulerFactory.GetDefaultScheduler();
            m_scheduler.JobFactory = new GameEventJobFactory(this);
            m_scheduler.Start();

            IJobDetail job = JobBuilder.Create<GameEventJob>().Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithSimpleSchedule
                  (s =>
                     s.WithIntervalInSeconds(30).RepeatForever()
                  )
                .Build();

            m_scheduler.ScheduleJob(job, trigger);
        }

        public GameEventState GetState()
        {
            return m_evt.State;
        }

        public void ChangeState(IState state)
        {
            m_currentState = state;

            m_evt.State = state.GetModelState();
            var evt = m_db.GameEvents.Find(m_evt.GameEventId);
            evt.State = m_evt.State;
            m_db.SaveChanges();
        }

        public IState InitializeGameState(GameEvent evt)
        {
            switch (evt.State)
            {
                case GameEventState.NotStarted:
                    return new NotStarted();
                case GameEventState.Placing:
                    return new Placing();
                case GameEventState.WaitingForDefuse:
                    return new WaitingForDefuse();
                case GameEventState.Defusing:
                    return new Defusing();
                case GameEventState.Completed:
                default:
                    return new Completed();
            }
        }

        public void CloseEvent()
        {
            // Stop scheduler
            m_scheduler.Clear();

            // Assign points to users that planted
            var evt = m_db.GameEvents.Include(g => g.Placers).Include(g => g.Bombs).First(g => g.GameEventId == m_evt.GameEventId);
            int points = evt.Bombs.Count(b => !b.IsDefused)*20;

            foreach (var placer in evt.Placers)
                placer.Score += points;

            m_db.SaveChanges();

            //Stop event from service
            GameEventService.StopEvent(m_evt);
        }

        public DateTime GetNextScheduledChange()
        {
            return m_currentState.GetTimeToChangeState(m_evt);
        }

        public void GoNext()
        {
            m_currentState.GoNext(this);
        }

        public int GetId()
        {
            return m_evt.GameEventId;
        }
    }
}