using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using SugarFactory.Models.BindingModels.Sugar;
using SugarFactory.Models.EntityModels;
using SugarFactory.Models.ViewModels.Sugar;



namespace SugarFactory.Services
{
   public class SugarService : Service, ISugarService
   {
        [HandleError(ExceptionType = typeof(ArgumentNullException), View = "CustomError")]
        public void CreateSachet(MakeSachetBm makeSachetBm) 
        {

           
          Counter lastCounter;
          ClientPrefix prefix = this.Context.ClientPrefixes.FirstOrDefault(pr=>pr.PrefixName == makeSachetBm.ClientPrefix);

            if ((lastCounter = this.Context.Counters.OrderByDescending(c => c.Id).FirstOrDefault()) == null)
            {
                lastCounter = new Counter();
                lastCounter.Value = 1;
                
            }

            SugarSachet sugarModel = new SugarSachet();
        
            sugarModel = Mapper.Map<MakeSachetBm, SugarSachet>(makeSachetBm);
          
            sugarModel.ClientPrefix = prefix;

            
            HttpPostedFileBase fileImg = makeSachetBm.ImgFile;
            HttpPostedFileBase filePdf = makeSachetBm.PdfFile;
       
            if (fileImg.ContentLength > 0)
            {
                string imgName = Path.GetFileName(fileImg.FileName);

                if (!imgName.EndsWith(".jpg") && !imgName.EndsWith(".png"))
                {
                    throw new ArgumentNullException();
                }

                string pathImg = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), imgName);
                fileImg.SaveAs(pathImg);
            }

            if (filePdf.ContentLength > 0)
            {
                string pdfName = Path.GetFileName(filePdf.FileName);

                if (!pdfName.EndsWith(".pdf"))
                {
                    throw new ArgumentNullException();
                }

                string pathPdf = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), pdfName);

                filePdf.SaveAs(pathPdf);
            }

            sugarModel.ImageUrl = makeSachetBm.ImgFile.FileName; 
          
            sugarModel.PdfUrl = makeSachetBm.PdfFile.FileName;
       
            sugarModel.UniqueNumber = makeSachetBm.ClientPrefix +"/" + makeSachetBm.PaperWidth.ToString() + "/" +
                                      lastCounter.Value.ToString();

            lastCounter.Value ++;
            this.Context.Counters.Add(lastCounter);
            this.Context.SaveChanges(); 
            this.Context.SugarSachets.Add(sugarModel);
            this.Context.SaveChanges();
         
        }

        public IEnumerable<AllSachetsViewModel> GetAllSachet()
        {
          
            IEnumerable<SugarSachet>  sachetEntity = this.Context.SugarSachets.OrderBy(s=>s.ClientPrefix.PrefixName);
                IEnumerable<AllSachetsViewModel> mapped =
                    Mapper.Map<IEnumerable<SugarSachet>, IEnumerable<AllSachetsViewModel>>(sachetEntity);
                return mapped;
          
        }
      
   }
}
