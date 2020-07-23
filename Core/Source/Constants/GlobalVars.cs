using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleSharp.TL;
using TLSharp.Core;

namespace Channelstatics
{
    /// <summary>
    /// Содержит все константы и глобальные переменные проекта Core
    /// </summary>
    public static class GlobalVars
    {
       /// <summary>
       /// Константа из DeveloperAccount
       /// </summary>
       private static string _apiHash = "0b5294d57d4424bb5aabcfe8e180051f";
       public static string ApiHash
       {
           get { return _apiHash; }
       }

       /// <summary>
       /// Константа из DeveloperAccount
       /// </summary>
       private static int _apiId = 1534969;
       public static int ApiId
       {
            get { return _apiId; }
       }

       /// <summary>
       /// Номер телефона клиента
       /// </summary>
       private static string _phoneNumber = "+79888642460";
       public static string PhoneNumber
       {
           get { return _phoneNumber; }
       }

       /// <summary>
       /// Объект клиента
       /// </summary>
       private static TelegramClient _client;
       public static TelegramClient Client
       {
           get
           {
               if (Equals(_client, null)) 
               {
                    _client = new TelegramClient(ApiId, ApiHash);
               }

               return _client;
           }
       }


       private static string _connection_string_filepath;
       public static string CONNECTION_STRING_FILEPATH
       {
           get
           {
                if (string.IsNullOrEmpty(_connection_string_filepath))
                {
                    _connection_string_filepath = Directory.GetCurrentDirectory() + "\\database_connection.txt";
                }

                return _connection_string_filepath;
           }
       }


    }
}
