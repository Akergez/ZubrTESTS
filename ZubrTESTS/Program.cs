using System;
using System.Threading.Tasks;
using Telegram.Bot;

namespace ZubrTESTS
{
    static class Program
    {
        static async Task Main(string[] args)
        {
            await ZubrBot.DoWork();
        }
    }
}