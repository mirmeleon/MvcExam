using System.ComponentModel.DataAnnotations;

namespace SugarFactory.Models.BindingModels.Orders
{
   public class NewOrderBm
    {
        public int Id { get; set; }

        [Required]
        [Range(10, 3000)]
        public int PaperKg { get; set; }
    }
}
