using System;

namespace TelegramBot.Resources
{
    class VideoLengthExceededException : Exception
    {
        public VideoLengthExceededException(string message) : base(message) { }
    }
}