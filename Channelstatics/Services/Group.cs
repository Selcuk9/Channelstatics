using Channelstatics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TLSharp.Core;

namespace Channelstatics.Services
{
    public class Group
    {
        public Channel ChannelInfo { get; private set; }
        private readonly TelegramClient client;

        public Group(TelegramClient client)
        {
            this.client = client;
        }

        public async Task<Channel> GetAllInfoChannel()
        {
           
        }
    }
}
