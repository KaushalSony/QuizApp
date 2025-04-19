namespace QuizApp.Models;

public class Question
{
    public string Text { get; set; }
    public List<string> Options { get; set; }
    public int AnswerIndex { get; set; }
    public string Category { get; set; }
}