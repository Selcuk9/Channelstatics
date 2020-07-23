using Channelstatics.Extensions;
using Channelstatics.Models;
using System;
using System.Threading.Tasks;
using TeleSharp.TL;
using TeleSharp.TL.Messages;
using TeleSharp.TL.Channels;
using TLSharp.Core;

namespace Channelstatics.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class ChannelMethods
    {
        //public Channel ChannelInfo { get; private set; }
        private readonly TelegramClient client;

        public ChannelMethods(TelegramClient client)
        {
            this.client = client;
        }

        // Получаем основную информацию о канале
        public async Task<Channel> GetAllInfoChannel(string channelName)
        {
            Channel channelInfo = new Channel();
            TLChannel channel = await Searcher.SearchChannelAsync(channelName);
            var fullChannel = await GetChannelFullAsync(channel);
            if (channel != null && fullChannel != null)
            {
                channelInfo.IdChannel = channel.Id;
                channelInfo.ChannelName = channel.Username;
                channelInfo.Title = channel.Title;
                channel.Id = channel.Id;
                channelInfo.Subscribers = ((TeleSharp.TL.TLChannelFull)fullChannel.FullChat).ParticipantsCount;
                channelInfo.Description = ((TeleSharp.TL.TLChannelFull)fullChannel.FullChat).About;
                channelInfo.Link = $"https://web.telegram.org/#/im?p=@{channel.Username}";
            }
            return channelInfo;
        }
        private async Task<TeleSharp.TL.Messages.TLChatFull> GetChannelFullAsync(TLChannel channel)
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

        //public async Task<TLVector<TLAbsMessage>> GetAllPosts()
        //{

        //}

    }
}
