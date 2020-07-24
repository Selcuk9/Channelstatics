using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Channelstatics.Models;
using Core.DataBase.Context;
using Core.DataBase.Models;
using Core.Source.Constants;
using Microsoft.Extensions.Logging.Abstractions;
using TeleSharp.TL;

namespace Core.Source.Helpers
{
    public class DbMethods
    {
        public static List<TelegramChannel> GetAllChannels(Db db)
        {
            return db.TelegramChannels.ToList();
        }

        public static TelegramPost AddTelegramPostIfNeed(Db db, TelegramChannel channel, TLMessage post)
        {
            var p = GetPostByTelegramId(db, channel.TelegramId, post.Id);

            if (Equals(p, null))
            {
                return AddPostToDb(db, channel, post);
            }

            return p;
        }

        public static TelegramPost AddPostToDb(Db db, TelegramChannel channel, TLMessage post)
        {
            //Добавить в базу сущность TelegramPost
            TelegramPost newPost = new TelegramPost()
            {
                ChannelTelegramId = channel.TelegramId,
                ChannelUsername = channel.Username,
                TelegramId = post.Id,
                WriteTime =  Const.StartDate.AddSeconds(post.Date),
                Content = post.Message ?? (post?.Media as TLMessageMediaPhoto)?.Caption ?? null,
            };

            if (post?.EditDate == null)
            {
                newPost.EditTime = Const.StartDate.AddSeconds(post.Date);
            }
            else
            {
                newPost.EditTime = Const.StartDate.AddSeconds((long)post.EditDate);
            }

            db.TelegramPosts.Add(newPost);
            db.SaveChanges();
            return newPost;
        }

        public static TelegramPost GetPostByTelegramId(Db db, long channelId, long telegramId)
        {
            return (from post in db.TelegramPosts
                       where (post.TelegramId == telegramId && post.ChannelTelegramId == channelId)
                       select post)?.FirstOrDefault() ?? null;
        }

        public static TelegramChannel AddChannelIfNeed(Db db, Channel channel)
        {
            TelegramChannel chan = GetChannelByTelegramId(db, channel.IdChannel);

            if (Equals(chan, null))
            {
                return AddChannelToDb(db, channel);
            }

            return chan;
        }

        public static TelegramChannel AddChannelToDb(Db db, Channel channel)
        {
            TelegramChannel newChannel = new TelegramChannel()
            {
                Description = channel.Description,
                Status = Const.TELEGRAM_CHANNEL_STATUS_ACTIVE,
                TelegramId = channel.IdChannel,
                Username = channel.ChannelName,
            };
            db.TelegramChannels.Add(newChannel);
            db.SaveChanges();
            return newChannel;
        }

        public static TelegramChannel GetChannelByTelegramId(Db db, long telegramId)
        {
            return (from c in db.TelegramChannels
                where c.TelegramId == telegramId
                select c).FirstOrDefault();
        }


    }
}
