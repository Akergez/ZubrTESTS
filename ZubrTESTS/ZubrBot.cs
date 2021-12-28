using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ZubrTESTS
{
    public static class ZubrBot
    {
        public static string key = "";
        public static TelegramBotClient Bot = new Telegram.Bot.TelegramBotClient(key);
        public static Dictionary<long, BotTerminal> Terminals = new Dictionary<long, BotTerminal>();

        public static async Task DoWork()
        {
            await Bot.SetWebhookAsync("");
            var offset = 0;
            while (true)
            {
                var updates = await Bot.GetUpdatesAsync(offset);
                foreach (var t in updates)
                {
                    var message = t.Message;
                    if(message==null||message.Text==null)
                        continue;
                    if (message is {Text: "/start"})
                        Terminals[message.Chat.Id] = new BotTerminal(message.Chat.Id);

                    if (Terminals.ContainsKey(message.Chat.Id))
                        Terminals[message.Chat.Id].HandleMessage(message);
                    Console.WriteLine(t.Type);
                    Console.WriteLine(message.Chat.Id);
                    if (message.From != null) Console.WriteLine(message.From.Username);

                    offset = t.Id + 1;
                }
            }
        }

        public static void BotWriteline(string[] text, Message messageToAnswer)
        {
            var toSend = new StringBuilder();
            foreach (var str in text)
            {
                toSend.Append(str);
                toSend.Append("\n");
            }

            Bot.SendTextMessageAsync(messageToAnswer.Chat.Id, toSend.ToString());
        }

        public static void BotWriteline(string[] text, long id)
        {
            var toSend = new StringBuilder();
            foreach (var str in text)
            {
                toSend.Append(str);
                toSend.Append("\n");
            }

            Bot.SendTextMessageAsync(id, toSend.ToString());
        }
    }
}