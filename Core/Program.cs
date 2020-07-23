using System;
using System.Threading.Tasks;
using Channelstatics;

namespace Core
{
    class Program
    {
        static void Main(string[] args)
        {

            Authorization auth = new Authorization();
            Task.WaitAll(auth.ConnectAsync());

        }
    }
}
