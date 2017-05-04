using System.ComponentModel;
using SugarFactory.Models.Enums;

namespace SugarFactory.Models.ViewModels.Sugar
{
   public class PreviewSachetViewModel
    {
        //todo
        //moje bi id-to
        [DisplayName("Unique number")]
        public string UniqueNumber { get; set; }

        [DisplayName("Paper Width")]
        public int PaperWidth { get; set; }

        [DisplayName("Image Url")]
        public string ImageUrl { get; set; }

        [DisplayName("Pdf")]
        public string PdfUrl { get; set; }

        [DisplayName("First Color")]
        public string FirstColor { get; set; }

        [DisplayName("Second Color")]
        public string SecondColor { get; set; }

        [DisplayName("Third Color")]
        public string ThirdColor { get; set; }

        [DisplayName("Fourth Color")]
        public string FourthColor { get; set; }

        [DisplayName("Paper Type")]
        public TypeOfPaper PaperType { get; set; }

        [DisplayName("Sachet Form")]
        public SachetForm SachetForm { get; set; }
       
        [DisplayName("Reel Type")]
        public string ReelType { get; set; }

        public int Pass { get; set; }

        [DisplayName("Sugar Size")]
        public int SugarSize { get; set; }
      
    }
}
