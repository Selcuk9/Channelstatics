using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Channelstatics;
using Core.DataBase.Context;
using Microsoft.EntityFrameworkCore;

namespace Core.Source.Helpers
{
    public class HelperDatabase
    {
        private static string default_connection = @"Server=(localdb)\mssqllocaldb;Database=TelegramStatistics;Trusted_Connection=True;";
        private static string connection_string;
        public static string CONNECTION_STRING
        {
            get
            {
                if (string.IsNullOrEmpty(connection_string))
                {
                    try
                    {
                        return File.ReadAllText(GlobalVars.CONNECTION_STRING_FILEPATH);
                    }
                    catch
                    {
                        return default_connection;
                    }
                }

                return connection_string;
            }
        }

        public static DbContextOptions<Db> DB_OPTIONS
        {
            get
            {
                var optionBuilder = new DbContextOptionsBuilder<Db>();
                return optionBuilder.UseSqlServer(CONNECTION_STRING).Options;
            }
        }
    }
}
