using System;
using System.IO;
using Telegram.Bot.Args;

namespace TelegramBot.Resources.BotSource
{
    public class BotController
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
            var _client = Bot.Get();

            if (_message.Text == null)
            {
                Console.WriteLine($"Received a text message in chat {_message.Chat.Id}.\nMessage type: {_message.Type} | Error");
                await _client.SendTextMessageAsync(chatId: e.Message.Chat, text: "Invalid command!");
                return;
            }

            if (_message == null)
                return;

            if(!Directory.Exists($@"{Constants.FOLDERPATH}\{_message.Chat.Id}"))
                Directory.CreateDirectory($@"{Constants.FOLDERPATH}\{_message.Chat.Id}");

            Console.WriteLine($"Received a text message in chat {_message.Chat.Id}.\nMessage: {_message.Text}");

            var _commands = Bot.Commands;

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