using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsCollector.Models
{
    public class ModifyArticleViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string LeadParagraph { get; set; }
        public string Content { get; set; }
    }
}