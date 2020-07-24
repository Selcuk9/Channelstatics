using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Channelstatics;
using Channelstatics.Services;
using Core.DataBase.Context;
using Core.DataBase.Models;
using Core.Source.Helpers;
using TeleSharp.TL;

namespace Core.Source.Logic
{
    /// <summary>
    /// Отвечаает за непосредственный сбор данных
    /// </summary>
    public class StatisticsProcessor
    {
        /// <summary>
        /// Запускаем ежечасно, желательно в отдельном потоке, поставить приоритет потока на высокий, а потом выполнять параллельно.
        /// </summary>
        public void Process(object intChannelDelaySeconds)
        {
            int channelDelaySeconds = (int) intChannelDelaySeconds;


            using (Db db = new Db(HelperDatabase.DB_OPTIONS))
            {
                //1) Взять из базы список всех каналов.
                List<TelegramChannel> channels = DbMethods.GetAllChannels(db);

                if (channels == null || channels.Count == 0)
                {
                    Debug.Error("Нет каналов в базе для сбора статистики!");
                    return;
                }
                
                //Запускаем цикл по каналам
                //Получить запросом канал Добавить в базу кол-во подписчиков канала на текущий час.

                foreach(var channel in channels)
                {
                    Debug.Log($"Берем статистику канала [{channel.Username}]");

                    var channelInfo = ChannelMethods.GetAllInfoChannel(GlobalVars.Client, channel.Username).Result;
                    StatisticsChannel chanStat = new StatisticsChannel()
                    {
                        TelegramId = channelInfo.IdChannel,
                        SubscribersCount = channelInfo.Subscribers ?? 0,
                        Username = channelInfo.ChannelName,
                    };

                    db.StatisticsChannels.Add(chanStat);


                    //Взять все посты у канала, через запрос.
                    var posts = ChannelMethods.GetAllPosts(GlobalVars.Client, channel.Username).Result;
                    foreach (var post in posts)
                    {
                        var p = post as TLMessage;

                        if(Equals(p, null))
                        {
                            continue;
                        }

                        //Если пост новый и еще не добавлен в базу, то добавить в базу.
                        TelegramPost telePost = DbMethods.AddTelegramPostIfNeed(db, channel, p);

                        StatisticsPost postStat = new StatisticsPost()
                        {
                            TelegramId = p.Id,
                            ChannelUsername = channel.Username,
                            ViewCount = p.Views ?? 0,
                            ChannelTelegramId = channel.TelegramId,
                        };
                        db.StatisticsPosts.Add(postStat);

                    }

                    db.SaveChanges();
                    Thread.Sleep(channelDelaySeconds * 1000);
                }

                Debug.Log("Сбор статистики завершен!");
                db.SaveChanges();

            }



        }

    }
}
