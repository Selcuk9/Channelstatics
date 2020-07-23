using Channelstatics.Extensions;
using Channelstatics.Models;
using System;
using System.Threading.Tasks;
using TeleSharp.TL;
using TeleSharp.TL.Messages;
using TeleSharp.TL.Channels;
using TLSharp.Core;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading;

namespace Channelstatics.Services
{
    /// <summary>
    /// 
    /// </summary>
    public static class ChannelMethods
    {
        // Получаем основную информацию о канале
        public static async Task<Channel> GetAllInfoChannel(TelegramClient client, string channelName)
        {
            Channel channelInfo = new Channel();
            TLChannel channel = await Searcher.SearchChannelAsync(client,channelName);
            var fullChannel = await GetChannelFullAsync(client,channel);
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
        private static async Task<TeleSharp.TL.Messages.TLChatFull> GetChannelFullAsync(TelegramClient client, TLChannel channel)
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
        public static async Task<TLChannel> SubscribeChannel(TelegramClient client, string channelName)
        {
            if (channelName != null && channelName != "")
            {
                var channel = await client.SubscribeAsync(channelName);
                return channel;
            }
            return null;
        }

        public static async Task<TLVector<TLAbsMessage>> GetAllPosts(TelegramClient client, string channelName)
        {
            TLChannel channel = await Searcher.SearchChannelAsync(client, channelName);
            var allPosts = await CounterPostsAsync(channel, client, true);
            return allPosts;
        }

        public static async Task<TLVector<TLAbsMessage>> GetPosts(TelegramClient client, string channelName, int count)
        {
            TLChannel channel = await Searcher.SearchChannelAsync(client, channelName);
            var posts = await CounterPostsAsync(channel,client,false,count);
            return posts;
        }

        private async static Task<TLVector<TLAbsMessage>> CounterPostsAsync(
            TLChannel channel, 
            TelegramClient client, 
            bool IsAll, 
            int countPost = 100)
        {
            Thread.Sleep(TimeSpan.FromSeconds(1));
            if (IsAll)
            {
                countPost = 100;
            }
            var posts = new TLVector<TLAbsMessage>();
            TLInputPeerChannel inputPeer = new TLInputPeerChannel()
            { ChannelId = channel.Id, AccessHash = (long)channel.AccessHash };
            int offset = 0;
            TLChannelMessages res;
            int target = 0; 
            while (true)
            {
                try
                {
                        res = await client.SendRequestAsync<TLChannelMessages>
                    (new TLRequestGetHistory()
                    {
                        Peer = inputPeer,
                        Limit = countPost,
                        AddOffset = offset,
                        OffsetId = 0
                    });
                }
                catch (Exception)
                {
                    return posts;
                }
                
                var msgs = res.Messages;
                if (posts.Count == 100 && IsAll)
                {
                    break;
                }
                offset += countPost;
                if (msgs.Count >= countPost && (IsAll == false))
                {
                    break;
                }
                foreach (var post in msgs)
                {
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}
