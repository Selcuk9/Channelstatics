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
            client = TLClient;
            if (string.IsNullOrEmpty(channelName) || string.IsNullOrWhiteSpace(channelName))
            {
                return null;
            }

            var ChanneLlist = (await client.SendRequestAsync<TeleSharp.TL.Contacts.TLResolvedPeer>(
               new TeleSharp.TL.Contacts.TLRequestResolveUsername
               {
                   Username = "durov"
               }).ConfigureAwait(true)).Chats.ToList();
            var result = await JoinChannel(ChanneLlist);

            return result;
        }
        /// <summary>
        /// Подписываемся на первый канал по списку поиска
        /// </summary>
        /// <param name="channelList">список найденных каналов</param>
        /// <returns></returns>
        private async static Task<TLChannel> JoinChannel(List<TLAbsChat> channelList)
        {
            if (channelList.Count > 0)
            {
                var ChannelInfo = channelList[0] as TeleSharp.TL.TLChannel;
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
                    return new TLChannel();
                }
                return ChannelInfo;
            }
            return null;
        }
    }
}
