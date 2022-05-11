using System.IO;
using TelegramBot.Resources.BotSource;
using TelegramBot.Resources;

namespace TelegramBot.Resources
{
    public class Program
    {
        public static void Main()
        {
            Directory.CreateDirectory(Constants.FOLDERPATH);
            BotController.getInstance().BotInitialize();
        }
    }
}
