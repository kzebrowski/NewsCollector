using System.Drawing;

namespace NewsCollector.Models
{
    public class CreateArticleViewModel
    {
        public string Title {get; set;}
        public string LeadParagraph { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; }
    }
}