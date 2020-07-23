using Channelstatics.Extensions;
using Channelstatics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
using TeleSharp.TL.Channels;
using TLSharp.Core;

namespace Channelstatics.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class Group
    {
        //public Channel ChannelInfo { get; private set; }
        private readonly TelegramClient client;

        public Group(TelegramClient client)
        {
            this.client = client;
        }

        // Получаем основную информацию о канале
        public async Task<Channel> GetAllInfoChannel(string channelName)
        {
            Channel channelInfo = new Channel();
            TLChannel channel = await Founder.SearchChannelAsync(channelName);
            if (channel != null)
            {
                channelInfo.IdChannel = channel.Id;
                channelInfo.ChannelName = channel.Username;
                channelInfo.Title = channel.Title;
                


            }
        }
        private async TLChatFull GetChannelFull(TLChannel channel)
        {
            try
            {
                var chan = await client.SendRequestAsync<TeleSharp.TL.Messages.TLChatFull>(new TLRequestGetFullChannel()
                {
                    Channel = new TLInputChannel()
                    { ChannelId = channel.Id, AccessHash = (long)channel.AccessHash }
                });
                return chan;
            }
            catch (Exception)
            {
                throw new Exception("Не удачно получили все данные канала");
            }
           
        }
        public async Task<TLChannel> SubscribeChannel(string channelName)
        {
            if (channelName != null && channelName != "")
            {
                var channel = await client.SubscribeAsync(channelName);
                return channel;
            }
            return null;
        }
    }
}
