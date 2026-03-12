using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog_Application.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "The Title is Required")]
        [MaxLength(400, ErrorMessage = "The title cannot exceed 200 characters")]
        public string Title { get; set; }


        [Required(ErrorMessage = "The Content is Required")]
        public string Content { get; set; }

        [Required(ErrorMessage = "The Author is Required")]
        [MaxLength(100, ErrorMessage = "The Name cannot exceed 100 characters")]

        public string FeatureImagePath { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublishedDate { get; set; } = DateTime.Now;

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        // Navigation property to the Category
        public Category Category { get; set; }

        public ICollection<Comment> Comments { get; set; }



    }
}



