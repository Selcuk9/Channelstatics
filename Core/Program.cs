using System;
using System.Threading.Tasks;
using Channelstatics;
using Channelstatics.Extensions;
using Core.Source.Helpers;
using Core.Source.ProcessClass;
using Channelstatics.Services;
using Core.DataBase.Context;
using Core.Source.Logic;

namespace Core
{
    class Program
    {
        static void Main(string[] args)
        {
            //var channel = GlobalVars.Client.SubscribeAsync("make_your_style").Result;
            //var channelInfo = ChannelMethods.GetAllInfoChannel(GlobalVars.Client, "make_your_style").Result;
            //var channelAllPosts = ChannelMethods.GetAllPosts(GlobalVars.Client, "make_your_style").Result;
            //var channelPost = ChannelMethods.GetPosts(GlobalVars.Client, "make_your_style",80).Result;


            Authorization auth = new Authorization();
            Task.WaitAll(auth.ConnectAsync());

            ConsoleCommandsProcessor procCommands = new ConsoleCommandsProcessor();
            procCommands.StartAwait();

            //Этот метод нужно запустить один раз для инициализации базы данных, потом нужно закомментировать
            //InitDbForTest();

            StatisticsManager manager = new StatisticsManager();
            //Поменяй параметры как удобно
            manager.Start(120, 5);
        }


        static void InitDbForTest()
        {
            var c1 = ChannelMethods.GetAllInfoChannel(GlobalVars.Client, "make_your_style").Result;
            var c2 = ChannelMethods.GetAllInfoChannel(GlobalVars.Client, "fedorinsights").Result;
            var c3 = ChannelMethods.GetAllInfoChannel(GlobalVars.Client, "PumpTheMind").Result;
            var c4 = ChannelMethods.GetAllInfoChannel(GlobalVars.Client, "vot_idea").Result;
            var c5 = ChannelMethods.GetAllInfoChannel(GlobalVars.Client, "TransformatorTV").Result;

            using (Db db = new Db(HelperDatabase.DB_OPTIONS))
            {
                DbMethods.AddTelegramChannelIfNeed(db, c1);
                DbMethods.AddTelegramChannelIfNeed(db, c2);
                DbMethods.AddTelegramChannelIfNeed(db, c3);
                DbMethods.AddTelegramChannelIfNeed(db, c4);
                DbMethods.AddTelegramChannelIfNeed(db, c5);

                var channels = DbMethods.GetAllChannels(db);
                foreach (var c in channels)
                {
                    Debug.Log($"Channel {c.Username} | {c.TelegramId.ToString()} | {c.Description}");
                }
                
            }

           

        }
    }
}
