using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsCollector.Models
{
    public class ArticleModel
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("AuthorId")]
        public ApplicationUser Author { get; set; }

        [Required]
        public string AuthorId { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string LeadingParagraph { get; set; }        
        [Required]
        public string Body { get; set; }

        //! This byte array will be converted into an image when retrieving the article
        public byte[] Image { get; set; }

    }
}