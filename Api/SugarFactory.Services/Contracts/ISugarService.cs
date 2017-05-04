using System.Collections.Generic;
using SugarFactory.Models.BindingModels.Sugar;
using SugarFactory.Models.ViewModels.Sugar;

namespace SugarFactory.Services
{
    public interface ISugarService
    {
        void CreateSachet(MakeSachetBm makeSachetBm);
     
        IEnumerable<AllSachetsViewModel> GetAllSachet();
       
    }
}