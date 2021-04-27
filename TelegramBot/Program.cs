using System.IO;
using TelegramBot.Resources.BotSource;
using TelegramBot.Resources;

namespace TelegramBot.Resources
{
    class Program
    {
        static void Main()
        {
            Directory.CreateDirectory(Constants.FOLDERPATH);
            BotController.getInstance().BotInitialize();
        }
    }
}
