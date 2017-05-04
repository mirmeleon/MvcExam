using System.ComponentModel;

namespace SugarFactory.Models.ViewModels.AdminArea
{
  public class AllUsersPrefixViewModel
    {
        public int Id { get; set; }

        [DisplayName("Client Prefix")]
        public string ClientPrefix { get; set; }

        public string Email { get; set; }
    }
}
