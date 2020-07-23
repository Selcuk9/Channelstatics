using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Core.Source.Classes;
using Core.Source.Helpers;
using TeleSharp.TL;

namespace Core.Source.ProcessClass
{
    public class ConsoleCommandsProcessor
    {
        private static bool enable = false;

        CommandProcessor proc;

        public ConsoleCommandsProcessor()
        {
            this.proc = new CommandProcessor();
        }

        public void StartAwait()
        {
            if(enable) return;
            enable = true;
            Debug.Log("Начинаю считывать команды.");
            Thread t = new Thread(this.Process);
            t.Start();
        }

        private void FinishAwait()
        {
            if (!enable) return;
            enable = false;
            Debug.Log("Прекратил счиывать команды.");
        }

        public void Process()
        {
            while (enable)
            {
                string str = Debug.InputData();

                if (Command.ParseCommand(str, out var command, out var error) == false)
                {
                    Debug.Error(error);
                    continue;
                }

                //обрабатываем комманду
                proc.ProcessCommand(command);
            }
        }

    }
}
