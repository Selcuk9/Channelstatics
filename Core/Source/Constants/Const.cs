using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Source.Constants
{
    public class Const
    {
        public const string TELEGRAM_CHANNEL_STATUS_ACTIVE = "active";
        public const string TELEGRAM_CHANNEL_STATUS_NOT_ACTIVE = "not_active";
        public const string TELEGRAM_CHANNEL_STATUS_DELETED = "deleted";

        public static DateTime StartDate = new DateTime(1970,1,1,0,0,0);
    }
}
