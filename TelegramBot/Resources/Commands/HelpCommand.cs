using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Resources.Commands
{
    public class HelpCommand : Command
    {
        public override string Name => "/help";

        public override async void Execute(Message message, TelegramBotClient client)
        {
            await client.SendTextMessageAsync(message.Chat, "/get - get an audio file from a YouTube video\n\nExample:\n/get https://www.youtube.com/...", replyToMessageId: message.MessageId);
        }
    }
}
