using System;
using Telegram.Bot.Args;

namespace TelegramBot.Resources.BotSource
{
    class BotController
    {
        #region Singleton
        private static BotController instance;

        public static BotController getInstance()
        {
            if (instance == null)
                instance = new BotController();
            return instance;
        }
        #endregion

        public void BotInitialize()
        {
            var client = Bot.Get();

            var _me = client.GetMeAsync().Result;
            Console.WriteLine(
              $"Hello, I am user {_me.Id} and my name is {_me.FirstName}."
            );

            Console.Title = _me.FirstName;

            client.OnMessage += Bot_OnMessage;

            client.StartReceiving();

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

            client.StopReceiving();
        }

        private async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            var _message = e.Message;

            if (_message == null)
                return;

            Console.WriteLine($"Received a text message in chat {_message.Chat.Id}.\nMessage: {_message.Text}");

            var _commands = Bot.Commands;
            var _client = Bot.Get();

            foreach (var command in _commands)
            {
                if (command.Contains(_message.Text))
                {
                    command.Execute(_message, _client);
                    return;
                }
            }

            await _client.SendTextMessageAsync(chatId: _message.Chat, text: "Invalid command!");
        }
    }
}