using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SugarFactory.Models.Enums;

namespace SugarFactory.Models.EntityModels
{
   public class SugarSachet
    {
       
        public int Id { get; set; }

        public virtual ClientPrefix ClientPrefix { get; set; }

        
        [ForeignKey("ClientPrefix")]
        public int? ClientPrefixId { get; set; }

        [Required]
        [Range(10, 400)]
        public int PaperWidth { get; set; }

        [Required]
        public int CounterValue { get; set; }

        [Required]
        public string UniqueNumber { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string PdfUrl { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string FirstColor { get; set; }
        public string SecondColor { get; set; }
        public string ThirdColor { get; set; }
        public string FourthColor { get; set; }
        public TypeOfPaper PaperType { get; set; }

        public SachetForm SachetForm { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string ReelType { get; set; }

        [Required]
        [Range(1, 1000)]
        public int Pass { get; set; }

        [Required]
        [Range(1, 800)]
        public int SugarSize { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string PrintingCompany { get; set; }

    }
}
