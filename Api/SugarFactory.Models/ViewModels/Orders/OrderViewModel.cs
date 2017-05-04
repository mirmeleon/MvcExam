using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SugarFactory.Models.Enums;


namespace SugarFactory.Models.ViewModels.Orders
{
  public class OrderViewModel
    {
        public int Id { get; set; }

        public string ClientPrefix { get; set; }
  

        [DisplayName("Order Date:")]
        public string OrderDate { get; set; }


        [DisplayName("Kgs of paper:")]
        [Range(10, 3000, ErrorMessage = "The value must be between 10 and 3000")]
        public int PaperKg { get; set; }

        [DisplayName("Sachet Unique Number:")]
        public string SachetUniqueNumber { get; set; }

       
        [DisplayName("Status:")]
        public OrderStatus OrderStatus { get; set; }

        
    }
}
