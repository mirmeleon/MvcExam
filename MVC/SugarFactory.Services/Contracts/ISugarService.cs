using System.Collections.Generic;
using System.Security.Principal;
using PagedList;
using SugarFactory.Models.BindingModels.Sugar;
using SugarFactory.Models.ViewModels.Sugar;

namespace SugarFactory.Services.Contracts
{
    public interface ISugarService
    {
        void CreateSachet(MakeSachetBm makeSachetBm);
        MakeSachetViewModel GenereteSacheteForm();
        IEnumerable<AllSachetsViewModel> GetAllSachet(IPrincipal user);
        IPagedList<AllSachetsViewModel> MakePagedList(IEnumerable<AllSachetsViewModel> sachetsVm, int? page);
    }
}