using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;

namespace Core.DataBase.Models
{
    public class TelegramPost
    {
        public long Id { get; set; }
        public string ChannelUsername { get; set; }
        public long TelegramId;
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime WriteTime { get; set; }
        public DateTime EditTime { get; set; }
        public DateTime CreateTime { get; set; }
        
    }
}
