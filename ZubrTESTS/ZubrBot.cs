using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ZubrTESTS
{
    public static class ZubrBot
    {
        private const string Key = "";
        private static readonly TelegramBotClient Bot = new(Key);
        private static readonly Dictionary<long, BotTerminal> Terminals = new();

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
                    if (message?.Text == null)
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

        public static void BotWriteline(IEnumerable<string> text, Message messageToAnswer)
        {
            BotWriteline(text, messageToAnswer.Chat.Id);
        }

        public static void BotWriteline(IEnumerable<string> text, long id)
        {
            var toSend = new StringBuilder();
            foreach (var str in text)
            {
                toSend.Append(str);
                toSend.Append('\n');
            }

            Bot.SendTextMessageAsync(id, toSend.ToString());
            Bot.AnswerInlineQueryAsync()
        }
    }
}