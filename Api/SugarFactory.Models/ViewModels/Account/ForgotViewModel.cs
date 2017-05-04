using System.ComponentModel.DataAnnotations;

namespace SugarFactory.Web.Models
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}