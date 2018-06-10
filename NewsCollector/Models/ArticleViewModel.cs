﻿using System;

namespace NewsCollector.Models
{
    public class ArticleViewModel
    {
        public Guid Id { get; set; }
        public string Title {get; set;}
        public string LeadParagraph { get; set; }
        public string Content { get; set; }
    }
}