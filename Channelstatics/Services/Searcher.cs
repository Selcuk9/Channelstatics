using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;

namespace Channelstatics.Services
{
    /// <summary>
    /// Поисковик телеграм
    /// </summary>
    public class Searcher
    {
        public static async Task<TLChannel> SearchChannelAsync(string channelName)
        {
            if (string.IsNullOrEmpty(channelName) || string.IsNullOrWhiteSpace(channelName))
            {
                return null;
            }

            var ChanneLlist = (await GlobalVars.Client.SendRequestAsync<TeleSharp.TL.Contacts.TLResolvedPeer>(
              new TeleSharp.TL.Contacts.TLRequestResolveUsername
              {
                  Username = channelName
              }).ConfigureAwait(true)).Chats.ToList();

            if (ChanneLlist.Count > 0)
            {
                var channel = ChanneLlist[0] as TLChannel;
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
