using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DataBase.Models
{
    public class StatisticsChannel
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public int SubscribersCount { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
