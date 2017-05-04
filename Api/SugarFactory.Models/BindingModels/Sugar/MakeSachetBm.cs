
using System.ComponentModel.DataAnnotations;
using System.Web;

using SugarFactory.Models.Enums;

namespace SugarFactory.Models.BindingModels.Sugar
{
  public class MakeSachetBm
    {
        [Required]
       
        public HttpPostedFileBase ImgFile { get; set; }

        [Required]
        public HttpPostedFileBase PdfFile { get; set; }

        [Required]
        public string ClientPrefix { get; set; }

        [Required]
        public TypeOfPaper PaperType { get; set; }

        [Required]
        public SachetForm SachetForm { get; set; }

        [Required]
        public int PaperWidth { get; set; }
        

        [Required]
        [MinLength(3)] 
        public string FirstColor { get; set; }
        public string SecondColor { get; set; }
        public string ThirdColor { get; set; }
        public string FourthColor { get; set; }

        [Required]
        public string ReelType { get; set; }

        [Required]
        public int Pass { get; set; }

        [Required]
        public int SugarSize { get; set; }

        [Required]
        public string PrintingCompany { get; set; }

    }
}
