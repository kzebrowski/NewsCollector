using System;
using System.Drawing;

namespace NewsCollector.Models
{
    public class ModifyArticleViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string LeadParagraph { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
    }
}