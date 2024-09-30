using System;
using System.Collections.Generic;

// Comment class to represent a comment on a video
public class Comment
{
    public string CommenterName { get; private set; }
    public string CommentText { get; private set; }

    public Comment(string commenterName, string commentText)
    {
        CommenterName = commenterName;
        CommentText = commentText;
    }
}

// Video class to represent a YouTube video
public class Video
{
    public string Title { get; private set; }
    public string Author { get; private set; }
    public int LengthInSeconds { get; private set; }
    private List<Comment> _comments;

    public Video(string title, string author, int lengthInSeconds)
    {
        Title = title;
        Author = author;
        LengthInSeconds = lengthInSeconds;
        _comments = new List<Comment>();
    }

    public void AddComment(Comment comment)
    {
        _comments.Add(comment);
    }

    public int GetNumberOfComments()
    {
        return _comments.Count;
    }

    public List<Comment> GetComments()
    {
        return _comments;
    }
}

// Program class that drives the application
class Program
{
    static void Main(string[] args)
    {
        // Create a list to store videos
        List<Video> videos = new List<Video>();

        // Create first video and its comments
        Video video1 = new Video("How to Cook Pasta", "John Doe", 300);
        video1.AddComment(new Comment("Alice", "Great recipe!"));
        video1.AddComment(new Comment("Bob", "Tried it, turned out amazing."));
        video1.AddComment(new Comment("Charlie", "Very helpful, thanks!"));

        // Create second video and its comments
        Video video2 = new Video("Learn C# in 10 Minutes", "Jane Smith", 600);
        video2.AddComment(new Comment("Dave", "Nice introduction."));
        video2.AddComment(new Comment("Eve", "Very clear and concise."));
        video2.AddComment(new Comment("Frank", "Helped me a lot!"));

        // Create third video and its comments
        Video video3 = new Video("Top 10 Travel Destinations", "Traveler's Guide", 1200);
        video3.AddComment(new Comment("Grace", "Can't wait to visit these places."));
        video3.AddComment(new Comment("Hank", "I want to go to Italy!"));
        video3.AddComment(new Comment("Ivy", "Great video, very informative."));

        // Add the videos to the list
        videos.Add(video1);
        videos.Add(video2);
        videos.Add(video3);

        // Iterate through the list of videos and display their information
        foreach (Video video in videos)
        {
            Console.WriteLine($"Title: {video.Title}");
            Console.WriteLine($"Author: {video.Author}");
            Console.WriteLine($"Length: {video.LengthInSeconds} seconds");
            Console.WriteLine($"Number of Comments: {video.GetNumberOfComments()}");

            Console.WriteLine("Comments:");
            foreach (Comment comment in video.GetComments())
            {
                Console.WriteLine($"- {comment.CommenterName}: {comment.CommentText}");
            }

            Console.WriteLine(); // Blank line for spacing between videos
        }
    }
}