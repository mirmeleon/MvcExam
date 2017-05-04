using System.Collections.Generic;
using PagedList;
using SugarFactory.Models.BindingModels.AdminArea;
using SugarFactory.Models.ViewModels.AdminArea;

namespace SugarFactory.Services
{
    public interface IAdminService
    {
        UserAndRoleViewModel MakeUserChanges(string id);
        UserAndRoleViewModel MakeUserChangesWithoutAdminRole(string id);
        void SetUserRole(EditRoleBm editRoleBm);
        UserWithAssignedRoleViewModel ViewAssignedRoles(string id);

        string GetUserId(string userEmail);

        void DeleteRole(DeleteRoleBm deleteRoleBm);

        IEnumerable<AllUsersPrefixViewModel> GetAllUsers();

        void AssignPrefixes(AssignPrefixBm assignPrefixBm);

        IEnumerable<CreatePrefixViewModel> GetAllPrefixes();

        void CreatePrefix(CreatePrefixBm createPrefixBm);

        IEnumerable<UsersViewModel> GetUsersAndRoles();
        IPagedList<UsersViewModel> MakePagedList(IEnumerable<UsersViewModel> vm, int? page);

    }
}