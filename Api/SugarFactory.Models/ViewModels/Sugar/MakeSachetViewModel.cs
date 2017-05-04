using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SugarFactory.Models.Enums;


namespace SugarFactory.Models.ViewModels.Sugar
{
   public class MakeSachetViewModel
    {
        public MakeSachetViewModel()
        {
            this.ClientPrefixes = new HashSet<ClientPrefixViewModel>();
        }
        public TypeOfPaper PaperType { get; set; }

        public SachetForm SachetForm { get; set; }

       public IEnumerable<ClientPrefixViewModel> ClientPrefixes { get; set; }

        
        [Required(ErrorMessage = "Image field can't be empty")]
        [Display(Name="Image file")]
        public string ImgFile { get; set; }
        
        [Required(ErrorMessage = "Pdf field can't be empty")]
        [Display(Name = "Pdf file")]
        public string PdfFile { get; set; }

        [Required(ErrorMessage = "Paper width field can't be empty")]
        [Range(10, 400, ErrorMessage = "The value must be between 10 and 400")]
        [Display(Name = "Paper Width")]   
        public int PaperWidth { get; set; }

        [Required(ErrorMessage = "First color field can't be empty")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Minimum color lenght is 3 characters and maximum is 30")]
        [Display(Name="First Color")]
        public string FirstColor { get; set; }

        [Display(Name = "Second Color")]
        public string SecondColor { get; set; }

        [Display(Name = "Third Color")]
        public string ThirdColor { get; set; }

        [Display(Name = "Fourth Color")]
        public string FourthColor { get; set; }

        [Required(ErrorMessage = "Reel type field can't be empty")]
        [Display(Name = "Reel Type")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string ReelType { get; set; }

        [Required(ErrorMessage = "Pass field can't be empty")]
        [Range(1, 1000, ErrorMessage = "Pass has to be between {1} and {2}")]
        public int Pass { get; set; }

        [Required(ErrorMessage = "Sugar Size field can't be empty")]
        [Range(1, 800, ErrorMessage = "Sugar size has to be between {1} and {2}")]
        [Display(Name = "Sugar Size")]
        public int SugarSize { get; set; }

        [Required(ErrorMessage = "Printing Company field can't be empty")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [Display(Name = "Printing Company")]
        public string PrintingCompany { get; set; }



    }
}
