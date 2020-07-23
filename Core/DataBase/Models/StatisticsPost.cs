using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DataBase.Models
{
    public class StatisticsPost
    {
        public long Id { get; set; }
        public long ChannelTelegramId { get; set; }
        public string ChannelUsername { get; set; }
        public int TelegramId { get; set; }
        public int ViewCount { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
