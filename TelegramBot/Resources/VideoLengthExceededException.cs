using System;

namespace TelegramBot.Resources
{
    public class VideoLengthExceededException : Exception
    {
        public VideoLengthExceededException(string message) : base(message) { }
    }
}