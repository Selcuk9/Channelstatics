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
        /// <summary>
        /// Получить все каналы из таблицы TelegramChannels
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public static List<TelegramChannel> GetAllChannels(Db db)
        {
            return db.TelegramChannels.ToList();
        }


        /// <summary>
        /// Добавить сущность TelegramPost (Проверка на наличие в базе)
        /// </summary>
        /// <param name="db"></param>
        /// <param name="channel"></param>
        /// <param name="post"></param>
        /// <returns></returns>
        public static TelegramPost AddTelegramPostIfNeed(Db db, TelegramChannel channel, TLMessage post)
        {
            var p = GetTelegramPostByTelegramId(db, channel.TelegramId, post.Id);

            if (Equals(p, null))
            {
                return AddTelegramPostToDb(db, channel, post);
            }

            return p;
        }

        /// <summary>
        /// Добавить сущность TelegramPost в базу без проверки на наличие.
        /// </summary>
        /// <param name="db"></param>
        /// <param name="channel"></param>
        /// <param name="post"></param>
        /// <returns></returns>
        public static TelegramPost AddTelegramPostToDb(Db db, TelegramChannel channel, TLMessage post)
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

            if (post?.EditDate != null)
            {
                newPost.EditTime = Const.StartDate.AddSeconds((long)post.EditDate);
            }
            else
            {
                newPost.EditTime = Const.StartDate.AddSeconds((long)post.Date);
            }

            db.TelegramPosts.Add(newPost);
            db.SaveChanges();
            return newPost;
        }

        /// <summary>
        /// Получить сущность TelegramPost по TelegramId
        /// </summary>
        /// <param name="db"></param>
        /// <param name="channelId"></param>
        /// <param name="telegramId"></param>
        /// <returns></returns>
        public static TelegramPost GetTelegramPostByTelegramId(Db db, long channelId, long telegramId)
        {
            return (from post in db.TelegramPosts
                       where (post.TelegramId == telegramId && post.ChannelTelegramId == channelId)
                       select post)?.FirstOrDefault() ?? null;
        }


        /// <summary>
        /// Добавить сущность TelegramChannel (проверка на наличие в базе)
        /// </summary>
        /// <param name="db"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        public static TelegramChannel AddTelegramChannelIfNeed(Db db, Channel channel)
        {
            TelegramChannel chan = GetTelegramChannelByTelegramId(db, channel.IdChannel);

            if (Equals(chan, null))
            {
                return AddTelegramChannelToDb(db, channel);
            }

            return chan;
        }

        /// <summary>
        /// Добавить сущность TelegramChannel без проверки на наличие
        /// </summary>
        /// <param name="db"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        public static TelegramChannel AddTelegramChannelToDb(Db db, Channel channel)
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

        /// <summary>
        /// Получить сущность TelegramChannel По TelegramId
        /// </summary>
        /// <param name="db"></param>
        /// <param name="telegramId"></param>
        /// <returns></returns>
        public static TelegramChannel GetTelegramChannelByTelegramId(Db db, long telegramId)
        {
            return (from c in db.TelegramChannels
                where c.TelegramId == telegramId
                select c).FirstOrDefault();
        }

        /// <summary>
        /// Добавить сущность StatisticsChannel в базу данных
        /// </summary>
        public static void AddStatisticsChannelToDb(Db db, Channel channelInfo)
        {
            StatisticsChannel chanStat = new StatisticsChannel()
            {
                TelegramId = channelInfo.IdChannel,
                SubscribersCount = channelInfo.Subscribers ?? 0,
                Username = channelInfo.ChannelName,
            };

            db.StatisticsChannels.Add(chanStat);
            db.SaveChanges();
        }

        /// <summary>
        /// Добавить сущность StatisticsPost в базу данных
        /// </summary>
        /// <param name="db"></param>
        /// <param name="channel"></param>
        /// <param name="post"></param>
        public static void AddStatisticsPostToDb(Db db, TelegramChannel channel, TLMessage post)
        {
            StatisticsPost postStat = new StatisticsPost()
            {
                TelegramId = post.Id,
                ChannelUsername = channel.Username,
                ViewCount = post.Views ?? 0,
                ChannelTelegramId = channel.TelegramId,
            };
            db.StatisticsPosts.Add(postStat);
            db.SaveChanges();
        }

    }
}
