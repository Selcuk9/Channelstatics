using System;
using System.Collections.Generic;
using System.Text;
using Core.Source.Classes;
using Core.Source.Helpers;

namespace Core.Source.ProcessClass
{
    public partial class CommandProcessor
    {
        List<string> AllCommans = new List<string>()
        {
            "hello",
            "test",
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
                case "test":

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

    }
}
