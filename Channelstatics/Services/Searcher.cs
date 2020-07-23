using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
using TLSharp.Core;

namespace Channelstatics.Services
{
    /// <summary>
    /// Поисковик телеграм
    /// </summary>
    public class Searcher
    {
        public static async Task<TLChannel> SearchChannelAsync(TelegramClient client, string channelName)
        {
            if (string.IsNullOrEmpty(channelName) || string.IsNullOrWhiteSpace(channelName))
            {
                return null;
            }

            var ChannelList = (await client.SendRequestAsync<TeleSharp.TL.Contacts.TLResolvedPeer>(
              new TeleSharp.TL.Contacts.TLRequestResolveUsername
              {
                  Username = channelName
              }).ConfigureAwait(true)).Chats.ToList();

            if (ChannelList.Count > 0)
            {
                var channel = ChannelList[0] as TLChannel;
                if (channel != null)
                {
                    return channel;
                }
            }
            return null;
        }


        //IN THE FUTURE
        public static async Task<TLChat> SearchChatAsync(string chatName)
        {
            return default;
        }

    }
}
