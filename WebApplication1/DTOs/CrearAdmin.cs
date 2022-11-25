using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs
{
    public class CrearAdmin
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
