using TeleSharp.TL;
using TeleSharp.TL.Messages;

namespace Channelstatics.Extensions
{
    public static class TelegramClientExtension
    {
        public static TLChannel Subscribe(string channelName)
        {
            var dialogs = (TLDialogsSlice)await client.GetUserDialogsAsync();
      

        }

    }
}
