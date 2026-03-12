using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog_Application.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "The UserName is Required")]
        [MaxLength(100, ErrorMessage = "The title cannot exceed 100 characters")]
        public int UserName { get; set; }

        [DataType(DataType.Date)]
        public DateTime CommentDate { get; set; } = DateTime.Now;

        [Required]
        public string Content { get; set; }

        [ForeignKey("Post")]
        public int PostId { get; set; }

        // Navigation property to the Post
        public Post Post { get; set; }

    }
}