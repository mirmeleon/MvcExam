using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SugarFactory.Models.EntityModels
{
   public class ClientPrefix 
   {
      
        public int Id { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 2)]
        public string PrefixName { get; set; }

      
        [ForeignKey("SugarUser")]
        public int? SugarUserId { get; set; }
        public virtual SugarUser SugarUser { get; set; }

   
      
   }
}
