using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SugarFactory.Models.Enums;

namespace SugarFactory.Models.ViewModels.Orders
{
    public class NewOrderFromExistingSachetViewModel
    {
        public int Id { get; set; }

        public string ClientPrefix { get; set; }

        [DisplayName("Image:")]
        public string ImageUrl { get; set; }


        [DisplayName("Order Date:")]
        public string OrderDate { get; set; }

        [Required]
        [DisplayName("Kgs of paper:")]
        [Range(10, 3000, ErrorMessage = "Minimum value for order is 10 kgs, maximum is 3000")]
        public int PaperKg { get; set; }

        [DisplayName("Sachet Unique Number:")]
        public string UniqueNumber { get; set; }

        [DisplayName("Status:")]
        public OrderStatus OrderStatus { get; set; }
    }
}
