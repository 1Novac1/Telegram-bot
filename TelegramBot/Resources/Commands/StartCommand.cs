﻿using System.IO;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Resources.Commands
{
    class StartCommand : Command
    {
        public override string Name => "/start";

        public override async void Execute(Message message, TelegramBotClient client)
        {
            await client.SendTextMessageAsync(message.Chat, "Enter /help to see all commands", replyToMessageId: message.MessageId);
            Directory.CreateDirectory($@"{Constants.FOLDERPATH}\{message.Chat.Id}");
        }
    }
}