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
        /// Задержка между запросами, чтобы не превышать лимит запросов.
        /// </summary>
        public int RequestsDelaySeconds = 5;

        /// <summary>
        /// Запускаем ежечасно, желательно в отдельном потоке, поставить приоритет потока на высокий, а потом выполнять параллельно.
        /// </summary>
        public void Process(object intChannelDelaySeconds)
        {
            this.RequestsDelaySeconds = (int) intChannelDelaySeconds;


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
                    DbMethods.AddStatisticsChannelToDb(db, channelInfo);


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
                        DbMethods.AddStatisticsPostToDb(db, channel, p);
                    }

                    //Делаем задержку по времени, чтобы не банили запросы за превыщение лимита
                    //Берем данные по каналу, по его постам и делаем задержку.
                    Thread.Sleep(this.RequestsDelaySeconds * 1000);
                }

                Debug.Log("Сбор статистики завершен!");

            }



        }

    }
}
