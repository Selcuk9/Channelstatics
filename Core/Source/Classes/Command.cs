using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Core.Source.Classes
{
    public class Command
    {
        
        public string Name;
        private Dictionary<string, string> Params;
        
        public Command(string name)
        {
            this.Name = name;
            this.Params = new Dictionary<string, string>();
        }

        public void AddParam(string paramName, string data)
        {
            this.Params.Add(paramName, data);
        }

        public string GetParam(string paramName)
        {
            bool res = this.Params.TryGetValue(paramName, out var data);
            if (res)
            {
                return data;
            }
            else
            {
                return null;
            }
        }


        public static bool ParseCommand(string strCommand, out Command command, out string error)
        {
            error = null;

            if (string.IsNullOrWhiteSpace(strCommand))
            {
                command = null;
                error = "Пустая строка!";
                return false;
            }

            strCommand = strCommand.ToLower();
            
            List<string> words = strCommand.Split(' ').ToList();
            
            command = new Command(words[0]);
            strCommand = strCommand.Replace(words[0], "");

            //Ищем параметры.
            Regex regParam = new Regex(@"(?<name>\S+)\s*=\s*(?<data>\S+)");

            var res = regParam.Matches(strCommand);

            foreach (Match match in res)
            {
                command.AddParam(match.Groups["name"].Value, match.Groups["data"].Value);
            }

            return true;
        }


    }
}
