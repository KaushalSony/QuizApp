using QuizApp.Models;
using System.Text.Json;

namespace QuizApp.Services;

public class QuizManager
{
    private List<Question> questions = new();
    private int score = 0;

    public void LoadQuestions(string filePath)
    {
        string json = File.ReadAllText(filePath);
        questions = JsonSerializer.Deserialize<List<Question>>(json)!;
    }

    public void StartQuiz(string categoryFilter = "", int timePerQuestion = 10)
    {
        Console.Clear();
        var filtered = string.IsNullOrWhiteSpace(categoryFilter)
            ? questions
            : questions.Where(q => q.Category.Equals(categoryFilter, StringComparison.OrdinalIgnoreCase)).ToList();

        if (filtered.Count == 0)
        {
            Console.WriteLine("No questions available for this category.");
            return;
        }

        filtered = filtered.OrderBy(q => Guid.NewGuid()).ToList(); // shuffle

        for (int i = 0; i < filtered.Count; i++)
        {
            var q = filtered[i];
            Console.WriteLine($"\nQ{i + 1}: {q.Text}");
            for (int j = 0; j < q.Options.Count; j++)
                Console.WriteLine($"  {j + 1}. {q.Options[j]}");

            Console.Write($"You have {timePerQuestion}s to answer... ");

            string? input = WaitForInput(timePerQuestion);
            if (int.TryParse(input, out int choice) &&
                choice >= 1 && choice <= q.Options.Count)
            {
                if (choice - 1 == q.AnswerIndex)
                {
                    Console.WriteLine("✅ Correct!");
                    score++;
                }
                else
                {
                    Console.WriteLine($"❌ Wrong! Correct: {q.Options[q.AnswerIndex]}");
                }
            }
            else
            {
                Console.WriteLine("⏱ Time up or invalid input! Skipping.");
            }
        }

        Console.WriteLine($"\n🎉 Quiz Over! Score: {score}/{filtered.Count}");
    }

    private string? WaitForInput(int seconds)
    {
        DateTime endTime = DateTime.Now.AddSeconds(seconds);
        string input = "";
        while (DateTime.Now < endTime)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter) break;
                input += key.KeyChar;
                Console.Write(key.KeyChar);
            }
        }
        return input;
    }

    public int GetScore() => score;
}