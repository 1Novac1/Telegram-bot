using System.IO;

namespace TelegramBot.Resources
{
    public static class Constants
    {
        public static readonly string[] YTURLS = { "https://www.youtube.com/", "https://youtu.be/" };

        public static readonly long MAXLENGTH = 50000000;
       
        public static readonly int VIDEOMAXLENGTH = 3600;

        public static readonly string FOLDERPATH = $@"{Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)}\TelegramBot";
    }
}