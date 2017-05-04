using System.ComponentModel;

namespace SugarFactory.Models.ViewModels.Sugar
{
   public class AllSachetsViewModel
    {
        public int Id { get; set; }

        [DisplayName("Client Prefix")]
        public string ClientPrefix { get; set; }

        [DisplayName("Unique Number")]
        public string UniqueNumber { get; set; }

        [DisplayName("Image")]
        public string ImageUrl { get; set; }
    }
}
