using System;
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
    }
}
