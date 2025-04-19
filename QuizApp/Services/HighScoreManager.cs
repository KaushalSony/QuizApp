using QuizApp.Models;

namespace QuizApp.Services;

public class HighScoreManager
{
    private const string FilePath = "data/highscores.txt";

    public void SaveHighScore(HighScore score)
    {
        var line = $"{score.Username}|{score.Score}|{score.Date}";
        File.AppendAllText(FilePath, line + Environment.NewLine);
    }

    public List<HighScore> GetHighScores()
    {
        if (!File.Exists(FilePath)) return new();

        return File.ReadAllLines(FilePath)
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .Select(line =>
            {
                var parts = line.Split('|');
                return new HighScore
                {
                    Username = parts[0],
                    Score = int.Parse(parts[1]),
                    Date = DateTime.Parse(parts[2])
                };
            })
            .OrderByDescending(h => h.Score)
            .Take(5)
            .ToList();
    }
}