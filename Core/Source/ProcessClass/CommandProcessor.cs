using System;
using System.Collections.Generic;
using System.Text;
using Core.DataBase.Context;
using Core.DataBase.Models;
using Core.Source.Classes;
using Core.Source.Helpers;

namespace Core.Source.ProcessClass
{
    public partial class CommandProcessor
    {
        List<string> AllCommans = new List<string>()
        {
            "hello",
            "stat",
            "add_channel",
            "remove_channel",
        };

        public bool IsCommand(string commandName)
        {
            if (string.IsNullOrWhiteSpace(commandName)) return false;

            if (AllCommans.Contains(commandName.Trim(' ').ToLower()) == true)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Обрабатываем команду
        /// </summary>
        /// <param name="command"></param>
        public void ProcessCommand(Command command)
        {
            if (IsCommand(command.Name) == false)
            {
                Debug.Error("Не является командой");
            }

            switch (command.Name)
            {
                case "hello":
                    CommandHello(command);
                    break;
                case "stat":
                    CommandShowStat(command);
                    break;
                case "ect...":

                    break;

            }
        }

        /// <summary>
        /// Обработка команды Hello
        /// </summary>
        /// <param name="command"></param>
        private void CommandHello(Command command)
        {
            Debug.Log("Heello tooo!");
        }


        /// <summary>
        /// Обработка команды ShowStat
        /// </summary>
        /// <param name="command"></param>
        private void CommandShowStat(Command command)
        {
            string channelName = command.GetParam("name");
            if(string.IsNullOrWhiteSpace(channelName))
            {
                Debug.Error("Введите параметр [username=ChannelName]!");
                return;
            }

            using (Db db = new Db(HelperDatabase.DB_OPTIONS))
            {
                TelegramChannel channel = DbMethods.GetChannelFromDb(db, channelName);
                
                if (channel == null)
                {
                    Debug.Error($"В базе данных нет канала [{channelName}]");
                    return;
                }


                //Получить статистику по подписчикам
                string subsStat = DbMethods.GetSubscribersStatistics(db, channel);
                string postStat = DbMethods.GetViewsStatistics(db, channel);
                Debug.Log($"\n{subsStat}\n\n{postStat}\n");


            }
        }
    }
}
