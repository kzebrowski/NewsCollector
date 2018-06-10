using System;
using System.Drawing;

namespace NewsCollector.Models
{
    public class ArticleBrowsingViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Image Image { get; set; }
    }
}