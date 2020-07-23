using Channelstatics.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeleSharp.TL;
using TLSharp.Core;

namespace Channelstatics.Extensions
{
    /// <summary>
    /// Расширяем клинта для подписки на канал
    /// </summary>
    public static class TelegramClientExtension
    {
        private static TelegramClient client;
        public static async Task<TLChannel> SubscribeAsync(this TelegramClient TLClient ,string channelName)
        {
            var foundedChannel = await Searcher.SearchChannelAsync(channelName);
            if (foundedChannel != null)
            {
                bool success = await JoinChannel(foundedChannel);
                if (success)
                {
                    return foundedChannel;
                }
            }
            return null;
        }
        /// <summary>
        /// Подписываемся на первый канал по списку поиска
        /// </summary>
        /// <param name="channelList">список найденных каналов</param>
        /// <returns></returns>
        private async static Task<bool> JoinChannel(TLChannel channel)
        {
            var ChannelInfo = channel;
            var RequestJoin = new TeleSharp.TL.Channels.TLRequestJoinChannel()
            {
                Channel = new TLInputChannel
                {
                    ChannelId = ChannelInfo.Id,
                    AccessHash = (long)ChannelInfo.AccessHash
                }
            };
            try
            {
                var JoinResponse = await client.SendRequestAsync<TLUpdates>(RequestJoin);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
