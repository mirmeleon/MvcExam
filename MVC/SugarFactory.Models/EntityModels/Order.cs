using System;
using System.ComponentModel.DataAnnotations;
using SugarFactory.Models.Enums;

namespace SugarFactory.Models.EntityModels
{
   public class Order
    {
        public int Id { get; set; }

        [Required]
        public string ClientPrefix { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        [Range(10, 3000)]
        public int PaperKg { get; set; }

        [Required]
        public string SachetUniqueNumber { get; set; }

        [Required]
        public OrderStatus OrderStatus { get; set; }
    }
}
