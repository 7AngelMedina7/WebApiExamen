using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs
{
    public class HacerInquilino
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
