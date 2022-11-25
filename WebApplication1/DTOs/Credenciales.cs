using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs
{
    public class Credenciales
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Contra { get; set; }
    }
}
