using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace ZubrTESTS
{
    public class Test
    {
        private readonly List<Question> _questions = new List<Question>();
        private int _rightAnswerCount;
        private int _waitingTestIndex;
        private readonly BotTerminal _parentTerminal;
        public bool IsTestCompleted;

        public Test(BotTerminal parentTerminal)
        {
            _parentTerminal = parentTerminal;
        }
        public void AddNewQuestion(string task, string[] answerVariants, int answerIndex)
        {
            var question = new Question(task, answerVariants, answerIndex, this,_questions.Count);
            _questions.Add(question);
        }

        public void DoTest(string answer)
        {
            var startIndex = 0;
            if (_waitingTestIndex > 0)
            {
                _questions[_waitingTestIndex-1].SetAnswer(answer);
                startIndex = _waitingTestIndex;
                if (_questions[_waitingTestIndex-1].Condition)
                    _rightAnswerCount++;
            }

            for (var i = startIndex; i < _questions.Count; i++)
            {
                _questions[i].AskQuestion();
                return;
            }

            WriteResult();
            IsTestCompleted = true;
        }

        private void WriteResult()
        {
            _parentTerminal.ChatWithUser($"You answered {_rightAnswerCount} of {_questions.Count}");
        }

        public void SendToTerminal(string[] text)
        {
            _parentTerminal.ChatWithUser(text);
        }
        public void SendToTerminal(string text)
        {
            _parentTerminal.ChatWithUser(text);
        }
        public void WaitAnswer(Question waitingQuestion)
        {
            _waitingTestIndex = _questions.IndexOf(waitingQuestion)+1;
        }
    }
}