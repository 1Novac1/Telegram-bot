using System.Collections.Generic;
using Telegram.Bot;

namespace TelegramBot.Resources.BotSource
{
    public static class Bot
    {
        private static TelegramBotClient client;

        private static List<Commands.Command> commandList;

        public static IReadOnlyList<Commands.Command> Commands { get => commandList.AsReadOnly(); }

        public static List<long> UserIdRequestExist = new List<long>();

        public static TelegramBotClient Get()
        {
            if (client != null)
                return client;

            //---Commands---
            commandList = new List<Commands.Command>();
            commandList.Add(new Commands.StartCommand());
            commandList.Add(new Commands.GetCommand());
            commandList.Add(new Commands.HelpCommand());
            //--------------

            client = new TelegramBotClient(BotAppSettings.Token);

            return client;
        }
    }
}