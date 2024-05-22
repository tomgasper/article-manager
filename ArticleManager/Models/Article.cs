﻿using System;
using System.ComponentModel.DataAnnotations;

namespace ArticleManager.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Author { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Upvotes { get; set; }

        [DataType(DataType.MultilineText)]
        public string? ContentMarkdown { get; set; }
    }
}