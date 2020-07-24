using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Core.Source.Helpers;

namespace Core.Source.Logic
{
    /// <summary>
    /// Управляет процессом сбора статистики, устанавливает интервал, запускает и приостанавливает процесс.
    /// </summary>
    public class StatisticsManager
    {
        private StatisticsProcessor proc;
        private System.Threading.Timer timer;

        public StatisticsManager()
        {
            proc = new StatisticsProcessor();
        }

        /// <summary>
        /// Начать сбор статистики
        /// </summary>
        /// <param name="statisticsDelaySeconds">Задержка сбора статистики</param>
        /// <param name="delayBetweenChanelSeconds">задержка между запросами для каждого канала (чтобы телега не банила запросы по превышению лимита)</param>
        public void Start(int statisticsDelaySeconds = 3600, int delayBetweenChanelSeconds = 5)
        {
            this.timer = new System.Threading.Timer((e) =>
            {
                StartStatistics(delayBetweenChanelSeconds);
            }, null, TimeSpan.Zero, TimeSpan.FromSeconds(statisticsDelaySeconds));
        }

        public void Stop()
        {
            Debug.Log("Сбор статистики остановлен!");
            this.timer.Dispose();
        }

        private void StartStatistics(int delayBetweenChanelSecond)
        {
            Debug.Log("Запускаем сбор статистики");

            Thread th = new Thread(new ParameterizedThreadStart(proc.Process));
            th.Priority = ThreadPriority.Highest;
            th.Name = "StatisticsThread";
            th.Start(delayBetweenChanelSecond);
        }
    }
}
