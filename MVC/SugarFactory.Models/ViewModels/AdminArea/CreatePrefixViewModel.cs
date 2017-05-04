using System.ComponentModel.DataAnnotations;

namespace SugarFactory.Models.ViewModels.AdminArea
{
 public class CreatePrefixViewModel
    {
        [Required]
        [Display(Name="Prefix Name")]
        [StringLength(3, ErrorMessage = "Client prefix has to be between 2 and 3 chars", MinimumLength = 2)]
        public string PrefixName { get; set; }
    }
}
