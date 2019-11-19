using System;

namespace Kata.Code.Chat.API.Services
{
    public class utc: IUtc
    {
        public DateTime Now() => DateTime.UtcNow;
    }
}