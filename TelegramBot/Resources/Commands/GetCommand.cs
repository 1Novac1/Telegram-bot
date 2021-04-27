using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Resources.BotSource;
using TelegramBot.Resources.Converter;

namespace TelegramBot.Resources.Commands
{
    class GetCommand : Command
    {
        public override string Name => "/get";

        public override async void Execute(Message message, TelegramBotClient client)
        {
            if(message.Text.Contains($"/get {Constants.YTURL_0}") || message.Text.Contains($"/get {Constants.YTURL_1}"))
            {
                var _audioConverter = new AudioConverter();
                if (Bot.UserIdRequestExist.Contains(message.Chat.Id))
                {
                    await client.SendTextMessageAsync(chatId: message.Chat, text: "Wait for the audio conversion to finish!");
                    return;
                }

                Bot.UserIdRequestExist.Add(message.Chat.Id);

                string _url = message.Text.Remove(0, 5);
                if (!await _audioConverter.CheckUrlAsync(_url))
                {
                    await client.SendTextMessageAsync(chatId: message.Chat, text: "Invalid URL\nTry again!:");
                    return;
                }
                await client.SendTextMessageAsync(chatId: message.Chat, text: "Attempt to convert video to audio\nPlease wait :)", replyToMessageId: message.MessageId);

                if (await _audioConverter.TryGetVideoAsync(_url))
                {
                    await _audioConverter.CreateMP3Async($@"{Constants.FOLDERPATH}\{message.Chat.Id}", _url, "file_" + message.Chat.Id);
                    if (_audioConverter.SizeOfAudioFile() >= Constants.MAXLENGHT)
                    {
                        await client.SendTextMessageAsync(chatId: message.Chat, text: "Maximum file size exceeded (50 megabytes)!");
                        await _audioConverter.DeleteAudioFileAsync(_audioConverter.AudioPath);
                        Bot.UserIdRequestExist.Remove(message.Chat.Id);
                        return;
                    }
                }
                else
                {
                    await client.SendTextMessageAsync(chatId: message.Chat, text: "Stream can not be converted!");
                    Bot.UserIdRequestExist.Remove(message.Chat.Id);
                    return;
                }

                if (await _audioConverter.CheckMP3PathAsync() == null)
                {
                    await client.SendTextMessageAsync(chatId: message.Chat, text: "Audio converting error");
                    Bot.UserIdRequestExist.Remove(message.Chat.Id);
                    return;
                }            

                Console.WriteLine("Sending");

                await client.SendTextMessageAsync(chatId: message.Chat, text: "Sending...");

                using (var stream = System.IO.File.OpenRead(_audioConverter.AudioPath))
                {
                    await client.SendAudioAsync(chatId: message.Chat, stream, title: _audioConverter.AudioName);
                    await _audioConverter.DeleteAudioFileAsync(_audioConverter.AudioPath);
                    Bot.UserIdRequestExist.Remove(message.Chat.Id);
                }

                Console.WriteLine($"Sending to user: {message.Chat.Id} Completed");

                await client.SendTextMessageAsync(chatId: message.Chat, text: "Don't forget to support the author by watching on YouTube!)");

                System.GC.Collect();
            }
            else
            {
                await client.SendTextMessageAsync(message.Chat, "Invalid URL!", replyToMessageId: message.MessageId);
            }
        }
    }
}