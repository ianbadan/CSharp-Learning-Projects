using System;
using System.Collections.Generic;
using System.Text;

namespace CommentPostProject.Entities
{
    class Post
    {
        public DateTime Moment { get; set; }
        public String Title { get; set; }
        public String Content { get; set; }
        public int Likes { get; set; }
        public List<Comment> Comments { get; private set; } = new List<Comment>();

        public Post()
        {
        }

        public Post(DateTime moment, string title, string content, int likes)
        {
            Moment = moment;
            Title = title;
            Content = content;
            Likes = likes;
        }

        public void AddComment(Comment comment)
        {
            Comments.Add(comment);
        }

        public void RemoveComment(Comment comment)
        {
            Comments.Remove(comment);
        }

        public override string ToString()
        {
            string text = Title + "\n" + Likes + " likes - " + Moment.ToString("dd/MM/yyyy HH:mm:ss") + "\n" + Content + "\n" + "Comments:\n";
            foreach(Comment c in Comments)
            {
                text += c.Text + "\n";
            }
            return text;
        }

    }
}
