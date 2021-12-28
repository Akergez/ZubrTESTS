using System.Collections.Generic;
using System.Linq;

namespace ZubrTESTS
{
    public class Question
    {
        private string Task { get; set; }
        private int IndexOfRightAnswerInAnswerVariants { get; }
        private int UserAnswerIndex { get; set; }
        private string[] AnswerVariants { get; }
        private int QuestionNumber { get; }
        public bool Condition;
        private readonly Test _parentTest;

        public Question(string task, string[] answerVariants, int answerIndex, Test parentTest, int questionNumber)
        {
            AnswerVariants = answerVariants;
            IndexOfRightAnswerInAnswerVariants = answerIndex;
            Task = task;
            _parentTest = parentTest;
            QuestionNumber = questionNumber;
        }

        public void AskQuestion()
        {
            var text = new List<string>();
            text.Add( ($"Вопрос номер {QuestionNumber+1}:"));
            text.Add("");
            text.Add(Task);
            text.Add("");
            text.AddRange(AnswerVariants.Select((t, i) => $"{i + 1}) {t}"));
            text.Add("");
            _parentTest.SendToTerminal(text.ToArray());
            _parentTest.WaitAnswer(this);
            UpdateCondition();
        }

        private void UpdateCondition()
        {
            Condition = UserAnswerIndex == IndexOfRightAnswerInAnswerVariants;
        }

        public void SetAnswer(string answer)
        {
            try
            {
                UserAnswerIndex = int.Parse(answer)-1;
                UpdateCondition();
            }
            catch
            {
                UpdateCondition();
            }
        }
    }
}