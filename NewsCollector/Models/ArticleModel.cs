using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NewsCollector.Models
{
    public class ArticleModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid AuthorId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string LeadingParagraph { get; set; }        
        [Required]
        public string Body { get; set; }
        
        //! Now its a path to the image, later will probably change
        public string ImagePath { get; set; }

    }
}