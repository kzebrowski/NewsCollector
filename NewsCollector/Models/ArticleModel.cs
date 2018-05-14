using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewsCollector.Models
{
    public class ArticleModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int AuthorId { get; set; }
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