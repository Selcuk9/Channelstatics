using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.DataBase.Context;
using Core.DataBase.Models;

namespace Core.Source.Helpers
{
    public partial class DbMethods
    {
        /// <summary>
        /// Вернуть канал из базы данных
        /// </summary>
        /// <param name="db"></param>
        /// <param name="channelName"></param>
        /// <returns></returns>
        public static TelegramChannel GetChannelFromDb(Db db, string channelName)
        {
            return (from c in db.TelegramChannels
                where c.Username == channelName.Trim(' ')
                select c)?.FirstOrDefault() ?? null;
        }


        public static string GetSubscribersStatistics(Db db, TelegramChannel channel)
        {
            List<StatisticsChannel> statChanList =
                (from s in db.StatisticsChannels
                    where s.TelegramId == channel.TelegramId
                    select s)?.ToList() ?? new List<StatisticsChannel>();

            if (statChanList.Count == 0)
            {
                return "Нет данных!";
            }

            string res = $"Представлены данные на {statChanList.Last().CreateTime}\n\n" +
                         $"Cтатистика подписок на канал:\n";
            res += $"|Дата                     |Подписки\n";

            for(var i = 0; i<statChanList.Count; i++)
            {
                var stat = statChanList[i];

                int subChange = i > 0 ? stat.SubscribersCount - statChanList[i - 1].SubscribersCount : stat.SubscribersCount;
                string change = subChange > -1 ? $"+{subChange}" : subChange.ToString();
                res += $"|{stat.CreateTime.ToString(),-25}|{change}\n";
            }

            return res;
        }

        public static string GetViewsStatistics(Db db, TelegramChannel channel)
        {
            string res = null;

            StatisticsChannel lastChannelStat =
                (from c in db.StatisticsChannels
                    where c.TelegramId == channel.TelegramId
                    select c)?.ToList().LastOrDefault() ?? null;

            if(lastChannelStat == null)
            {
                return "Нет статистики по каналу";
            }

            List<TelegramPost> postList =
                (from p in db.TelegramPosts
                    where p.ChannelTelegramId == channel.TelegramId
                    select p)?.ToList() ?? new List<TelegramPost>();

            List<StatisticsPost> postStatList =
                (from s in db.StatisticsPosts
                    where s.ChannelTelegramId == channel.TelegramId
                    select s)?.ToList() ?? new List<StatisticsPost>();


            if (postList.Count == 0 || postStatList.Count == 0)
            {
                return "Нет статистики о постах!";
            }

            Dictionary<long, TelegramPost> allPosts = new Dictionary<long, TelegramPost>();
            foreach (var item in postList)
            {
                allPosts.Add(item.TelegramId, item);
            }

            Dictionary<long, StatisticsPost> lastStat = new Dictionary<long, StatisticsPost>();
            foreach (var item in postStatList)
            {
                if (lastStat.ContainsKey(item.TelegramId))
                {
                    lastStat[item.TelegramId] = item;
                }
                else
                {
                    lastStat.Add(item.TelegramId, item);
                }
            }

            long averagePostViewAllTime = GetAveragePostViewOnLastDays(lastStat, allPosts);
            long averagePostView30 = GetAveragePostViewOnLastDays(lastStat, allPosts, 30);
            long averagePostView7 = GetAveragePostViewOnLastDays(lastStat, allPosts, 7);
            long averagePostView3 = GetAveragePostViewOnLastDays(lastStat, allPosts, 3);


            res += $"Статистика постов:\n\n" +
                   $"В базе есть статистика по последним {postList.Count} постам\n\n" +
                   $"Среднее количество просмотров на каждый пост за последние:\n" +
                   $"По всем данным : {averagePostViewAllTime} просмотр/пост.\n" +
                   $"        30 дн. : {averagePostView30} просмотр/пост.\n" +
                   $"         7 дн. : {averagePostView7} просмотр/пост.\n"+
                   $"         3 дн. : {averagePostView3} просмотр/пост.\n\n" +
                   $"    На данный момент ER (за 30 дн.) = {((float)averagePostView30 / (float) lastChannelStat.SubscribersCount) * 100}%";

            return res;
        }


        private static long GetAveragePostViewOnLastDays(Dictionary<long, StatisticsPost> lastStat, Dictionary<long, TelegramPost> allPosts, int days = 30000)
        {
            DateTime finishDate = DateTime.Now;
            DateTime startDate = finishDate.Date.AddDays(-1 * days).Date;

            long sumView = 0;
            long count = 0;
            foreach (var item in lastStat)
            {
                var post = allPosts[item.Key];
                var postStat = item.Value;

                if(post.WriteTime.Date >= startDate)
                {
                    count++;
                    sumView += postStat.ViewCount;
                }
            }

            return sumView / count;
        }

    }
}
