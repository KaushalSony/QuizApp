using QuizApp.Models;
using QuizApp.Services;

class Program
{
    static void Main()
    {
        var quiz = new QuizManager();
        var highScoreManager = new HighScoreManager();

        quiz.LoadQuestions("data/questions.json");

        Console.Write("Enter your name: ");
        string username = Console.ReadLine() ?? "Guest";

        Console.WriteLine("\nChoose a category or press Enter for all:");
        Console.WriteLine(" - Geography\n - Science\n - Literature");
        Console.Write("Your choice: ");
        string category = Console.ReadLine() ?? "";

        quiz.StartQuiz(category, timePerQuestion: 10);

        int finalScore = quiz.GetScore();
        highScoreManager.SaveHighScore(new HighScore
        {
            Name = username,
            Score = finalScore,
            Date = DateTime.Now
        });

        Console.WriteLine("\n🏆 High Scores:");
        foreach (var hs in highScoreManager.DisplayHighScores())
        {
            Console.WriteLine($"{hs.Username} - {hs.Score} ({hs.Date.ToShortDateString()})");
        }

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}
