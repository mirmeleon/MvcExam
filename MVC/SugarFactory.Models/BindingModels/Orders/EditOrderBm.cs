using System;
using System.ComponentModel.DataAnnotations;
using SugarFactory.Models.Enums;

namespace SugarFactory.Models.BindingModels.Orders
{
   public class EditOrderBm
    {
        public int Id { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Range(10, 3000)]
        public int PaperKg { get; set; }

        public OrderStatus OrderStatus { get; set; }
    }
}
