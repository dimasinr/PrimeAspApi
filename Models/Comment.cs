using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeAspApi.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public required string Content { get; set; }
        
        [Required]
        public int AuthorId { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("AuthorId")]
        public User? Author { get; set; }
    }

    
}
