using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SugarFactor.WebApi.Controllers;
using SugarFactory.Data;
using SugarFactory.Models.BindingModels.Sugar;
using SugarFactory.Models.EntityModels;
using SugarFactory.Models.ViewModels.Sugar;
using SugarFactory.Services;

namespace SugarFactory.ApiTests
{
    [TestClass]
   public class SugarServiceTests
    {
     
        private SugarController _controller;
        private IEnumerable<SugarSachet> _sachets;
        private HttpConfiguration _config;
        private SugarFactoryContext _context;

        [TestInitialize]
        public void Init()
        {
            this._controller = new SugarController();
            this._sachets = GenereteEnumerable();
            _config = new HttpConfiguration();
            this._controller.Configuration = _config;
            ConfigureAutoMapper();
            this._context = new SugarFactoryContext();
        }

      
        [TestMethod]
        public void CreateSachets_ShouldReturn_OneMoreSachetsIfSuccessfullyAddedToDb()
        {
            
            var sachets = _context.SugarSachets.ToList();
            int sachetsNum = sachets.Count();

            var sachetBmMock = MockSachetBm();
            var service = new Mock<SugarService>();
            service.Object.CreateSachet(sachetBmMock);

            int sachetsAfterInsert = _context.SugarSachets.ToList().Count();
           
            Assert.AreEqual(sachetsAfterInsert, sachetsNum+1);
        }

        [TestMethod]
        public void GetAllSachetsMethod_ShouldReturn_3Sachets()
        {
            
            var sachetsQueryable = _sachets.AsQueryable();
            var mockSet = new Mock<DbSet<SugarSachet>>();
            var mockContext = new Mock<SugarFactoryContext>();
            mockContext.Setup(m => m.SugarSachets).Returns(mockSet.Object);

            var serviceMock = new Mock<ISugarService>();
            var mappedSachets = Mapper.Map<IEnumerable<SugarSachet>, IEnumerable<AllSachetsViewModel>>(sachetsQueryable);

            serviceMock.Setup(x => x.GetAllSachet()).Returns(mappedSachets);

         
            var sachets = serviceMock.Object.GetAllSachet();

            
            Assert.AreEqual(3, sachets.Count());
           
        }

       
        private MakeSachetBm MockSachetBm()
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

            return sachetBmMock.Object;
        }


      


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
        private IEnumerable<SugarSachet> GenereteEnumerable()
        {
            var sachets = new List<SugarSachet>();

            sachets.Add(new SugarSachet()
            {
                Id = 1,
                ClientPrefix = new ClientPrefix() { PrefixName = "RE" },
                ImageUrl = "javaSux.jpg",
                UniqueNumber = "DE/3/12",
                CounterValue = 3,
                FirstColor = "blue",
                PaperType = 0,
                PaperWidth = 23,
                PdfUrl = "some.pdf",
                SachetForm = 0,
                ReelType = "1/2",
                Pass = 2,
                SugarSize = 3,
                PrintingCompany = "Sugar print"
            });

            sachets.Add(new SugarSachet()
            {
                Id = 2,
                ClientPrefix = new ClientPrefix() { PrefixName = "FE" },
                ImageUrl = "cCharpRox.jpg",
                UniqueNumber = "FE/3/12",
                CounterValue = 3,
                FirstColor = "green",
                PaperType = 0,
                PaperWidth = 23,
                PdfUrl = "some.pdf",
                SachetForm = 0,
                ReelType = "1/2",
                Pass = 2,
                SugarSize = 3,
                PrintingCompany = "Cat print"
            });

            sachets.Add(new SugarSachet()
            {
                Id = 3,
                ClientPrefix = new ClientPrefix() { PrefixName = "DE" },
                ImageUrl = "javaSux.jpg",
                UniqueNumber = "DE/3/12",
                CounterValue = 3,
                FirstColor = "blue",
                PaperType = 0,
                PaperWidth = 23,
                PdfUrl = "some.pdf",
                SachetForm = 0,
                ReelType = "1/2",
                Pass = 2,
                SugarSize = 3,
                PrintingCompany = "Sugar print"
            });

            return sachets;
        }

    }
}
