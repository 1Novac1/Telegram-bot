﻿using System.IO;

namespace TelegramBot.Resources
{
    public static class Constants
    {
        public static readonly string YTURL_0 = "https://www.youtube.com/";
        public static readonly string YTURL_1 = "https://youtu.be/";

        public static readonly long MAXLENGHT = 50000000;

        public static readonly string FOLDERPATH = $@"{Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)}\TelegramBot";
    }
}