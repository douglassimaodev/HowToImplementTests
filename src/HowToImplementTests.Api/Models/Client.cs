using System.ComponentModel.DataAnnotations;

namespace HowToImplementTests.Api.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }

        [Required]
        public string Telephone { get; set; }
    }
}
