using System;
using System.Collections.Generic;
using Telegram.Bot.Types;

namespace ZubrTESTS
{
    public class BotTerminal
    {
        private Test _test;
        private readonly long _chatId;
        private readonly string[] _testList = {"distrotest", "algem"};

        public BotTerminal(long id)
        {
            _chatId = id;
        }

        public void HandleMessage(Message message)
        {
            if (message?.Text == null)
            {
                Console.WriteLine("null");
                return;
            }

            if (message.Text.ToLower() == "/start")
                ChatWithUser("Commands:\n/list");
            switch (message.Text.ToLower())
            {
                case "/list":
                    ZubrBot.BotWriteline(_testList, message);
                    break;
                case "distrotest":
                    _test = new Test(ChatWithUser);
                    _test.AddNewQuestion("Choose best linux distro", new[] {"manjaro", "fedora", "arch"}, 1);
                    _test.DoTest("");
                    break;
                case "algem":
                    _test = new Test(ChatWithUser);
                    _test.AddNewQuestion("2+2", new[] {"4", "1", "австралия"}, 0);
                    _test.DoTest("");
                    break;
                default:
                {
                    if (_test is {IsTestCompleted: false})
                    {
                        _test.DoTest(message.Text);
                    }

                    break;
                }
            }
        }

        private void ChatWithUser(string toWrite)
        {
            ZubrBot.BotWriteline(new[] {toWrite}, _chatId);
        }

        private void ChatWithUser(string[] toWrite)
        {
            ZubrBot.BotWriteline(toWrite, _chatId);
        }
    }
}