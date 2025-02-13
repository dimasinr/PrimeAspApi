using System;
using System.ComponentModel.DataAnnotations;

namespace PrimeAspApi.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public required string Content { get; set; }
        
        [Required]
        public required string Author { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
