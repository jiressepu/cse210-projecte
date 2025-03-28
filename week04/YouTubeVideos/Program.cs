using System;
using System.Collections.Generic;

class Comment
{
    public string Author { get; set; }
    public string Text { get; set; }
    
    public Comment(string author, string text)
    {
        Author = author;
        Text = text;
    }
}

class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Duration { get; set; } // Duration in seconds
    private List<Comment> comments;
    
    public Video(string title, string author, int duration)
    {
        Title = title;
        Author = author;
        Duration = duration;
        comments = new List<Comment>();
    }
    
    public void AddComment(Comment comment)
    {
        comments.Add(comment);
    }
    
    public int GetCommentCount()
    {
        return comments.Count;
    }
    
    public void DisplayVideoInfo()
    {
        Console.WriteLine($"Title: {Title}");
        Console.WriteLine($"Author: {Author}");
        Console.WriteLine($"Duration: {Duration} seconds");
        Console.WriteLine($"Number of comments: {GetCommentCount()}");
        Console.WriteLine("Comments:");
        foreach (var comment in comments)
        {
            Console.WriteLine($"- {comment.Author}: {comment.Text}");
        }
        Console.WriteLine("----------------------------");
    }
}

class Program
{
    static void Main()
    {
        List<Video> videos = new List<Video>();
        
        Video video1 = new Video("Learn web development in 10 Minutes", "webAcademy", 800);
        video1.AddComment(new Comment("lusa", "Great explanation!"));
        video1.AddComment(new Comment("mario", "Very helpful, thanks!"));
        video1.AddComment(new Comment("Charly", "Could you cover LINQ next?"));
        
        Video video2 = new Video("OOP Principles in C#", "Tech Guru", 900);
        video2.AddComment(new Comment("Dave", "Encapsulation is my favorite!"));
        video2.AddComment(new Comment("Eve", "I finally understand inheritance!"));
        video2.AddComment(new Comment("Frank", "Polymorphism was tricky, but this helped."));
        
        Video video3 = new Video("How to Build a Web App with ASP.NET", "WebDev", 1200);
        video3.AddComment(new Comment("Grace", "This is exactly what I needed!"));
        video3.AddComment(new Comment("Hank", "Could you make a tutorial on authentication?"));
        video3.AddComment(new Comment("Ivy", "Love your content, keep it up!"));
        
        videos.Add(video1);
        videos.Add(video2);
        videos.Add(video3);
        
        foreach (var video in videos)
        {
            video.DisplayVideoInfo();
        }
    }
}
