using System;
using System.ComponentModel.DataAnnotations;
using SugarFactory.Models.Enums;

namespace SugarFactory.Models.BindingModels.Sugar
{
   public class EditOrderBm
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        [Required]
        [Range(10, 3000)]
        public int PaperKg { get; set; }

        public OrderStatus OrderStatus { get; set; }
    }
}
