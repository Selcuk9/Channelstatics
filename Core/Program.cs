using System;
using System.Threading.Tasks;
using Channelstatics;
using Core.Source.Helpers;

namespace Core
{
    class Program
    {
        static void Main(string[] args)
        {

            Authorization auth = new Authorization();
            Task.WaitAll(auth.ConnectAsync());
            Debug.InputData("Нажмите для завершения!");
        }
    }
}
