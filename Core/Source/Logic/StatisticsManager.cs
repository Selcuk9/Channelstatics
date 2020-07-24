using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Core.Source.Helpers;
using Core.Source.Logic;

namespace Core.Source.Logic
{
    /// <summary>
    /// Управляет процессом сбора статистики, устанавливает интервал, запускает и приостанавливает процесс.
    /// </summary>
    public class StatisticsManager
    {
        public StatisticsProcessor StatisticsProcessor;
        private System.Threading.Timer timer;

        public int _statisticsDelaySeconds = 3600;
        public int StatisticsDelaySeconds
        {
            get { return this._statisticsDelaySeconds; }
            set
            {
                this._statisticsDelaySeconds = value;
                if(Equals(this.timer, null) == false)
                {
                    this.timer.Change(TimeSpan.Zero, TimeSpan.FromSeconds(this._statisticsDelaySeconds));
                }
            }
        }

        public int RequestsDelaySeconds
        {
            get
            {
                return this.StatisticsProcessor.RequestsDelaySeconds;
            }
            set { this.StatisticsProcessor.RequestsDelaySeconds = value; }
        }
    

        public StatisticsManager()
        {
            StatisticsProcessor = new StatisticsProcessor();
        }

        /// <summary>
        /// Начать сбор статистики
        /// </summary>
        /// <param name="statisticsDelaySeconds">Задержка сбора статистики</param>
        /// <param name="delayBetweenChanelSeconds">задержка между запросами для каждого канала (чтобы телега не банила запросы по превышению лимита)</param>
        public void Start(int statisticsDelaySeconds = 3600, int requestsDelaySeconds = 5)
        {
            this.StatisticsDelaySeconds = statisticsDelaySeconds;
            this.RequestsDelaySeconds = requestsDelaySeconds;

            this.timer = new System.Threading.Timer((e) =>
            {
                StartStatisticsProcessor(this.RequestsDelaySeconds);
            }, null, TimeSpan.Zero, TimeSpan.FromSeconds(this.StatisticsDelaySeconds));
        }

        public void Stop()
        {
            Debug.Log("Сбор статистики остановлен!");
            this.timer.Dispose();
        }

        private void StartStatisticsProcessor(int delayBetweenChanelSecond)
        {
            Debug.Log("Запускаем сбор статистики");

            Thread th = new Thread(new ParameterizedThreadStart(StatisticsProcessor.Process));
            th.Priority = ThreadPriority.Highest;
            th.Name = "StatisticsThread";
            th.Start(delayBetweenChanelSecond);
        }
    }
}
