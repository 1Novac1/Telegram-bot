# Telegram bot (Youtube audio converter)

## Introduction

The following NuGet packages are included in this project:

- MediaToolkit - https://www.nuget.org/packages/MediaToolkit
- VideoLibrary - https://www.nuget.org/packages/VideoLibrary
- Telegram.Bot - https://www.nuget.org/packages/Telegram.Bot

This is a telegram bot based on a console application. It can extract the audio file by linking to a video hosted on YouTube video hosting.

At the moment, only 3 commands are implemented in this bot, 2 of which:

- /help - commands list
- /get - get a music

## How it works

`You must create a bot in the telegram application before using this code!` More about it here - [link](https://telegrambots.github.io/book)

In a file called [`BotAppSettings`](https://github.com/1Novac1/Telegram-bot-C-sharp/blob/main/TelegramBot/Resources/BotSource/BotAppSettings.cs), you must enter your token issued to you by BotFather!

```cs
namespace TelegramBot.Resources.BotSource
{
    public static class BotAppSettings
    {
        public static string Token { get; set; } = "your token";
    }
}
 ```
 
 ### After these steps you can use the bot as follows:
 
 Write the get command with reference to the video, as shown in this example:
 
 ![](https://github.com/1Novac1/Telegram-bot-C-sharp/blob/main/docs/scr1.png)
 
 And after a certain amount of time the bot will send you an audio file
 
 ![](https://github.com/1Novac1/Telegram-bot-C-sharp/blob/main/docs/scr2.png)
 
 > The author of the song in the example: [Marshmello - Alone](https://www.youtube.com/watch?v=ALZHF5UqnU4)
 
 
 > `!IMPORTANTLY! This bot will work as long as the program is running on your computer and with an internet connection` 

## That's all ;)

★ If you found it useful, do not be lazy to rate this post as an star ★
