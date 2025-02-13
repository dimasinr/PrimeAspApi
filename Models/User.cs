using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PrimeAspApi.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Email { get; set; }

        [JsonIgnore]
        public string? Password { get; set; } 

        // public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
