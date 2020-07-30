using System;
using System.Threading;
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


            //Authorization auth = new Authorization();
            //Task.WaitAll(auth.ConnectAsync());

            ConsoleCommandsProcessor procCommands = new ConsoleCommandsProcessor();
            procCommands.StartAwait();


            StatisticsManager manager = new StatisticsManager();
            //Поменяй параметры как удобно
            //manager.Start(2400, 10);


        }


        static void InitDbForTest()
        {
            var c1 = ChannelMethods.GetAllInfoChannel(GlobalVars.Client, "make_your_style").Result;
            Thread.Sleep(new Random(Convert.ToInt32(DateTime.Now.Ticks % 100000)).Next(10,20) * 1000);
            var c2 = ChannelMethods.GetAllInfoChannel(GlobalVars.Client, "fedorinsights").Result;
            Thread.Sleep(new Random(Convert.ToInt32(DateTime.Now.Ticks % 100000)).Next(10,20) * 1000);
            var c3 = ChannelMethods.GetAllInfoChannel(GlobalVars.Client, "PumpTheMind").Result;
            Thread.Sleep(new Random(Convert.ToInt32(DateTime.Now.Ticks % 100000)).Next(10,20) * 1000);
            var c4 = ChannelMethods.GetAllInfoChannel(GlobalVars.Client, "vot_idea").Result;
            Thread.Sleep(new Random(Convert.ToInt32(DateTime.Now.Ticks % 100000)).Next(10, 20) * 1000);
            var c5 = ChannelMethods.GetAllInfoChannel(GlobalVars.Client, "TransformatorTV").Result;
            Thread.Sleep(new Random(Convert.ToInt32(DateTime.Now.Ticks % 100000)).Next(10, 20) * 1000);
            var c6 = ChannelMethods.GetAllInfoChannel(GlobalVars.Client, "temablog").Result;
            Thread.Sleep(new Random(Convert.ToInt32(DateTime.Now.Ticks % 100000)).Next(10, 20) * 1000);
            var c7 = ChannelMethods.GetAllInfoChannel(GlobalVars.Client, "yurydud").Result;
            Thread.Sleep(new Random(Convert.ToInt32(DateTime.Now.Ticks % 100000)).Next(10, 20) * 1000);
            var c8 = ChannelMethods.GetAllInfoChannel(GlobalVars.Client, "meduzalive").Result;
            Thread.Sleep(new Random(Convert.ToInt32(DateTime.Now.Ticks % 100000)).Next(10, 20) * 1000);
            var c9 = ChannelMethods.GetAllInfoChannel(GlobalVars.Client, "DavydovIn").Result;
            Thread.Sleep(new Random(Convert.ToInt32(DateTime.Now.Ticks % 100000)).Next(10, 20) * 1000);
            var c10 = ChannelMethods.GetAllInfoChannel(GlobalVars.Client, "breakingmash").Result;
            Thread.Sleep(new Random(Convert.ToInt32(DateTime.Now.Ticks % 100000)).Next(10, 20) * 1000);
            var c11 = ChannelMethods.GetAllInfoChannel(GlobalVars.Client, "corona").Result;
            Thread.Sleep(new Random(Convert.ToInt32(DateTime.Now.Ticks % 100000)).Next(10, 20) * 1000);
            var c12 = ChannelMethods.GetAllInfoChannel(GlobalVars.Client, "oldlentach").Result;
            Thread.Sleep(new Random(Convert.ToInt32(DateTime.Now.Ticks % 100000)).Next(10, 20) * 1000);
            var c13 = ChannelMethods.GetAllInfoChannel(GlobalVars.Client, "Lifeyt").Result;
            Thread.Sleep(new Random(Convert.ToInt32(DateTime.Now.Ticks % 100000)).Next(10, 20) * 1000);
            var c14 = ChannelMethods.GetAllInfoChannel(GlobalVars.Client, "dvachannel").Result;
            Thread.Sleep(new Random(Convert.ToInt32(DateTime.Now.Ticks % 100000)).Next(10, 20) * 1000);
            var c15 = ChannelMethods.GetAllInfoChannel(GlobalVars.Client, "Cbpub").Result;
            Thread.Sleep(new Random(Convert.ToInt32(DateTime.Now.Ticks % 100000)).Next(10, 20) * 1000);
            var c16 = ChannelMethods.GetAllInfoChannel(GlobalVars.Client, "exboyfriend").Result;
            Thread.Sleep(new Random(Convert.ToInt32(DateTime.Now.Ticks % 100000)).Next(10, 20) * 1000);
            var c17 = ChannelMethods.GetAllInfoChannel(GlobalVars.Client, "Ovsyanka").Result;
            Thread.Sleep(new Random(Convert.ToInt32(DateTime.Now.Ticks % 100000)).Next(10, 20) * 1000);
            var c18 = ChannelMethods.GetAllInfoChannel(GlobalVars.Client, "hardchannel").Result;
            Thread.Sleep(new Random(Convert.ToInt32(DateTime.Now.Ticks % 100000)).Next(10, 20) * 1000);
            var c19 = ChannelMethods.GetAllInfoChannel(GlobalVars.Client, "paperpublic").Result;
            Thread.Sleep(new Random(Convert.ToInt32(DateTime.Now.Ticks % 100000)).Next(10, 20) * 1000);
            var c20 = ChannelMethods.GetAllInfoChannel(GlobalVars.Client, "memes").Result;
            Thread.Sleep(new Random(Convert.ToInt32(DateTime.Now.Ticks % 100000)).Next(10, 20) * 1000);
            using (Db db = new Db(HelperDatabase.DB_OPTIONS))
            {
                DbMethods.AddTelegramChannelIfNeed(db, c1);
                DbMethods.AddTelegramChannelIfNeed(db, c2);
                DbMethods.AddTelegramChannelIfNeed(db, c3);
                DbMethods.AddTelegramChannelIfNeed(db, c4);
                DbMethods.AddTelegramChannelIfNeed(db, c5);
                DbMethods.AddTelegramChannelIfNeed(db, c6);
                DbMethods.AddTelegramChannelIfNeed(db, c7);
                DbMethods.AddTelegramChannelIfNeed(db, c8);
                DbMethods.AddTelegramChannelIfNeed(db, c9);
                DbMethods.AddTelegramChannelIfNeed(db, c10);
                DbMethods.AddTelegramChannelIfNeed(db, c11);
                DbMethods.AddTelegramChannelIfNeed(db, c12);
                DbMethods.AddTelegramChannelIfNeed(db, c13);
                DbMethods.AddTelegramChannelIfNeed(db, c14);
                DbMethods.AddTelegramChannelIfNeed(db, c15);
                DbMethods.AddTelegramChannelIfNeed(db, c16);
                DbMethods.AddTelegramChannelIfNeed(db, c17);
                DbMethods.AddTelegramChannelIfNeed(db, c18);
                DbMethods.AddTelegramChannelIfNeed(db, c19);
                DbMethods.AddTelegramChannelIfNeed(db, c20);

                var channels = DbMethods.GetAllChannels(db);
                foreach (var c in channels)
                {
                    Debug.Log($"Channel {c.Username} | {c.TelegramId.ToString()} | {c.Description}");
                }
                
            }

           

        }
    }
}
