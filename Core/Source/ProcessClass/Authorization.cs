using System;
using System.IO;
using System.Threading.Tasks;
using Core.Source.Helpers;
using TeleSharp.TL;
using TLSharp.Core;

namespace Channelstatics
{
    public class Authorization
    {
        public Authorization()
        {

        }

        public async Task<TLUser> ConnectAsync()
        {
            DeleteAuthSession();

            try
            {
                await GlobalVars.Client.ConnectAsync();
            }
            catch
            {
                throw new Exception("Ошибка подключения!");
            }
           
            string hash = "";
            if (GlobalVars.Client.IsConnected)
            {
                Debug.Log("Ждем код...");
                try
                {
                    hash = await GlobalVars.Client.SendCodeRequestAsync(GlobalVars.PhoneNumber);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                var code = Debug.InputData("Введите код авторизации!");

                TLUser user = null;
                try
                {
                    user = await GlobalVars.Client.MakeAuthAsync(GlobalVars.PhoneNumber, hash, code);
                    Debug.Success($"Вход с аккаунта [{user.FirstName} {user.LastName}]");
                    return user;
                }
                catch (Exception)
                {
                    return new TLUser() { };
                }
            }
            else
            {
                return new TLUser() { };
            }
        }


        private void DeleteAuthSession()
        {
            var sessionPath = Directory.GetCurrentDirectory() + "\\session.dat";

            if (File.Exists(sessionPath))
            {
                File.Delete(sessionPath);
            }
        }
    }
}
