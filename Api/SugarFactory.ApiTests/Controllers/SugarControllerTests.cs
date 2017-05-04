using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SugarFactor.WebApi.Controllers;
using SugarFactory.Data;
using SugarFactory.Models.BindingModels.Sugar;
using SugarFactory.Models.EntityModels;
using SugarFactory.Models.ViewModels.Sugar;

namespace SugarFactory.ApiTests
{
    [TestClass]
   public class SugarControllerTests
    {
        private SugarController _controller;
      //  private IEnumerable<SugarSachet> _sachets;
        private HttpConfiguration _config;
     
            [TestInitialize]
        public void Init()
     {
             
            this._controller = new SugarController();
           // this._sachets = GenereteEnumerable();
            _config = new HttpConfiguration();
            this._controller.Configuration = _config;
            ConfigureAutoMapper();
      }

       
        [TestMethod]
        public void SugarSachets_ShouldReturn_OkWithIEnumerableFromAllSachetsViewModel()
        {
            var result = _controller.SugarSachets() as OkNegotiatedContentResult<IEnumerable<AllSachetsViewModel>>;
            Assert.IsNotNull(result);
        }
        

        [TestMethod]
        public void SugarSachets_ShouldReturn_IEnumerableWithSachetPrefixOnFirstPositionEqualToDE()
        {
           var context = new SugarFactoryContext();
           var prefix = context.SugarSachets.OrderBy(p=>p.ClientPrefix.PrefixName).FirstOrDefault();
           var result = _controller.SugarSachets() as OkNegotiatedContentResult<IEnumerable<AllSachetsViewModel>>;
           Assert.AreEqual(prefix.ClientPrefix.PrefixName, result.Content.ElementAt(0).ClientPrefix); 
        }

        [TestMethod]
        public void CreateSachet_ShouldReturn_StatusCodeCreated()
        {
          var sachetBmMock =  MockSachetBm();
          var result = _controller.CreateSachet(sachetBmMock.Object) as StatusCodeResult;
          Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);
        }

        [TestMethod]
        public void CreateSachetWithNotValidModel_ShouldReturn_BadRequestStatusCode()
        {
            
            var sachetBmMock = MockSachetBm();
            sachetBmMock.Object.FirstColor = "re";

            this._controller.Validate(sachetBmMock.Object);
            var result = _controller.CreateSachet(sachetBmMock.Object) as StatusCodeResult;

            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }


        private Mock<MakeSachetBm> MockSachetBm()
        {
            string filePath = Path.GetFullPath(@"testfiles\testimage.jpg");

            Mock<HttpPostedFileBase> uploadedImage = new Mock<HttpPostedFileBase>();
            uploadedImage
            .Setup(f => f.ContentLength)
            .Returns(0);

            uploadedImage
               .Setup(f => f.FileName)
                .Returns("testimage.jpg");


            Mock<HttpPostedFileBase> uploadedPdf = new Mock<HttpPostedFileBase>();
            uploadedPdf
                .Setup(c => c.ContentLength)
                .Returns(0);
            uploadedPdf
                .Setup(f => f.FileName)
                .Returns("testPdf.pdf");

            var sachetBmMock = new Mock<MakeSachetBm>();
            sachetBmMock.SetupAllProperties();
            sachetBmMock.Object.ClientPrefix = "DE";
            sachetBmMock.Object.FirstColor = "red"; 
            sachetBmMock.Object.ImgFile = uploadedImage.Object;
            sachetBmMock.Object.PdfFile = uploadedPdf.Object;
            sachetBmMock.Object.PaperType = 0;
            sachetBmMock.Object.PaperWidth = 234;
            sachetBmMock.Object.Pass = 12;
            sachetBmMock.Object.PrintingCompany = "Sugar print";
            sachetBmMock.Object.SachetForm = 0;
            sachetBmMock.Object.ReelType = "1/2";
            sachetBmMock.Object.SugarSize = 23;

            return sachetBmMock;
        }


        //private IEnumerable<SugarSachet> GenereteEnumerable()
        //{
            

        //    var sachets = new List<SugarSachet>();

        //    sachets.Add(new SugarSachet()
        //    {
        //        Id = 1,
        //        ClientPrefix = new ClientPrefix() { PrefixName = "RE" },
        //        ImageUrl = "javaSux.jpg",
        //        UniqueNumber = "DE/3/12",
        //        CounterValue = 3,
        //        FirstColor = "blue",
        //        PaperType = 0,
        //        PaperWidth = 23,
        //        PdfUrl = "some.pdf",
        //        SachetForm = 0,
        //        ReelType = "1/2",
        //        Pass = 2,
        //        SugarSize = 3,
        //        PrintingCompany = "Sugar print"
        //    });

        //    sachets.Add(new SugarSachet()
        //    {
        //        Id = 2,
        //        ClientPrefix = new ClientPrefix() { PrefixName = "FE" },
        //        ImageUrl = "cCharpRox.jpg",
        //        UniqueNumber = "FE/3/12",
        //        CounterValue = 3,
        //        FirstColor = "green",
        //        PaperType = 0,
        //        PaperWidth = 23,
        //        PdfUrl = "some.pdf",
        //        SachetForm = 0,
        //        ReelType = "1/2",
        //        Pass = 2,
        //        SugarSize = 3,
        //        PrintingCompany = "Cat print"
        //    });

        //    sachets.Add(new SugarSachet()
        //    {
        //        Id = 3,
        //        ClientPrefix = new ClientPrefix() { PrefixName = "DE" },
        //        ImageUrl = "javaSux.jpg",
        //        UniqueNumber = "DE/3/12",
        //        CounterValue = 3,
        //        FirstColor = "blue",
        //        PaperType = 0,
        //        PaperWidth = 23,
        //        PdfUrl = "some.pdf",
        //        SachetForm = 0,
        //        ReelType = "1/2",
        //        Pass = 2,
        //        SugarSize = 3,
        //        PrintingCompany = "Sugar print"
        //    });

        //    return sachets;
        //}


        private void ConfigureAutoMapper()
        {
            Mapper.Initialize(config =>
            {

                config.CreateMap<SugarSachet, AllSachetsViewModel>()
             .ForMember(dest => dest.ClientPrefix,
             opt => opt.MapFrom(
                 src => src.ClientPrefix.PrefixName));
           
                config.CreateMap<MakeSachetBm, SugarSachet>()
                  .ForMember(dest => dest.ImageUrl,
                      opts => opts.Ignore())
                  .ForMember(dest => dest.PdfUrl,
                      op => op.Ignore())
                         .ForMember(dest => dest.ClientPrefix, ost => ost.Ignore());
            });
        }


    }
}
