using System;
using System.Collections.Generic;

namespace ZubrTESTS
{
    public class Test
    {
        private readonly List<Question> _questions = new();
        private int _rightAnswerCount;
        private int _waitingTestIndex;
        private readonly Action<string[]> _sendString;
        public bool IsTestCompleted;

        public Test(Action<string[]> sendString)
        {
            _sendString = sendString;
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
            _sendString(new string[]{$"You answered {_rightAnswerCount} of {_questions.Count}"});
        }

        public void SendToTerminal(string[] text)
        {
            _sendString(text);
        }
        public void WaitAnswer(Question waitingQuestion)
        {
            _waitingTestIndex = _questions.IndexOf(waitingQuestion)+1;
        }
    }
}