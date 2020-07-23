using System;
using System.Threading.Tasks;
using Channelstatics;
using Channelstatics.Extensions;
using Core.Source.Helpers;
using Core.Source.ProcessClass;
using Channelstatics.Services;

namespace Core
{
    class Program
    {
        static void Main(string[] args)
        {
            Authorization auth = new Authorization();
            Task.WaitAll(auth.ConnectAsync());
            var channel = GlobalVars.Client.SubscribeAsync("make_your_style").Result;

            var channelInfo = ChannelMethods.GetAllInfoChannel(GlobalVars.Client, "make_your_style").Result;
            var channelAllPosts = ChannelMethods.GetAllPosts(GlobalVars.Client, "make_your_style").Result;
            var channelPost = ChannelMethods.GetPosts(GlobalVars.Client, "make_your_style",80).Result;



            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ConsoleCommandsProcessor procCommands = new ConsoleCommandsProcessor();
            procCommands.StartAwait();
        }
    }
}
