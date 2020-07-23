using System;
using System.Threading.Tasks;
using Channelstatics;
using Core.Source.Helpers;
using Core.Source.ProcessClass;

namespace Core
{
    class Program
    {
        static void Main(string[] args)
        {

            Authorization auth = new Authorization();
            Task.WaitAll(auth.ConnectAsync());
            
            ConsoleCommandsProcessor procCommands = new ConsoleCommandsProcessor();
            procCommands.StartAwait();
        }
    }
}
