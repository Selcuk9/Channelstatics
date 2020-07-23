using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DataBase.Models
{
    public class TelegramChannel
    {
        public long Id { get; set; }
        public long TelegramId { get; set; }
        public string Username { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }//active|not_active|deleted
        public DateTime CreateTime { get; set; }
    }

   
}
