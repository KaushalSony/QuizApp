using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using QuizApp.Models;

namespace QuizApp.Services
{
    public class HighScoreManager
    {
        private readonly string filePath = "data/highscores.txt";

        public void SaveHighScore(string name, int score)
        {
            var entry = $"{name}|{score}|{DateTime.Now}";
            File.AppendAllLines(filePath, new[] { entry });
        }

        public List<HighScore> LoadHighScores()
        {
            var highScores = new List<HighScore>();

            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    var parts = line.Split('|');
                    if (parts.Length == 3 &&
                        int.TryParse(parts[1], out int score) &&
                        DateTime.TryParse(parts[2], out DateTime date))
                    {
                        highScores.Add(new HighScore
                        {
                            Name = parts[0],
                            Score = score,
                            Date = date
                        });
                    }
                }
            }

            return highScores.OrderByDescending(h => h.Score).Take(5).ToList(); // Top 5 scores
        }

        public void DisplayHighScores()
        {
            var scores = LoadHighScores();

            Console.WriteLine("\nTop 5 High Scores:");
            foreach (var score in scores)
            {
                Console.WriteLine($"{score.Name} - {score.Score} points on {score.Date.ToShortDateString()}");
            }
        }
    }
}
