using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Snippets
{
    public static class QuestionsDemo
    {
        #region Q_A
        public static async IAsyncEnumerable<string> AnswerQuestionsAsync(IAsyncEnumerable<string> questions, [EnumeratorCancellation] CancellationToken token = default)
        {
            await foreach (string question in questions)
            {
                string answer = await GetAnswerAsync(question);
                yield return answer;
            }
        }
        #endregion

        public static async ValueTask<string> GetAnswerAsync(string question)
        {
            await Task.Yield();

            return string.Empty;
        }
    }
}
