using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;
using SugarFactory.Models.BindingModels.AdminArea;
using SugarFactory.Models.EntityModels;
using SugarFactory.Models.ViewModels.AdminArea;
using SugarFactory.Services.Contracts;

namespace SugarFactory.Services
{
    [HandleError(ExceptionType = typeof(ArgumentNullException), View = "Error")]
    public class AdminService : Service, IAdminService
    {
        public IEnumerable<UsersViewModel> GetUsersAndRoles()
        {
            IEnumerable<ApplicationUser> users = this.Context.Users;

            IEnumerable<UsersViewModel> usersVm =
                Mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<UsersViewModel>>(users);

            return usersVm;
        }

        public IPagedList<UsersViewModel> MakePagedList(IEnumerable<UsersViewModel> vm, int? page)
        {
            int pageNumber = page ?? 1;
            IPagedList<UsersViewModel> paged = vm.ToPagedList(pageNumber, 5);
            return paged;
        }

        public AllUsersWithAllPrefixesVm GetNotAssignedPrefixesAndUsers()
        {
            IEnumerable<SugarUser> users = this.Context.SugarUsers.Where(u => u.ClientPrefix == null).ToList();
            IEnumerable<ClientPrefix> prefixes = this.Context.ClientPrefixes.Where(p => p.SugarUser == null).ToList();

            AllUsersWithAllPrefixesVm allUsersAndPrefixes = new AllUsersWithAllPrefixesVm();
            allUsersAndPrefixes.Prefixes = prefixes;
            allUsersAndPrefixes.UsersWithoutPrefix = users;

            return allUsersAndPrefixes;
        }


        public void AssignPrefixes(AssignPrefixBm assignPrefixBm)
        {
            SugarUser user = this.Context.SugarUsers.FirstOrDefault(u => u.Id == assignPrefixBm.Id);

            if (user == null)
            {
                user = this.Context.SugarUsers.FirstOrDefault();
            }

            user.ClientPrefix = assignPrefixBm.ClientPrefix;
            this.Context.SugarUsers.AddOrUpdate(user);
            this.Context.SaveChanges();

            var prefForUpdate =
                this.Context.ClientPrefixes.FirstOrDefault(p => p.PrefixName == assignPrefixBm.ClientPrefix);
            if (prefForUpdate == null)
            {
                prefForUpdate = new ClientPrefix();
                prefForUpdate.PrefixName = "DEF";
            }

            prefForUpdate.SugarUser = user;
            this.Context.ClientPrefixes.AddOrUpdate(prefForUpdate);
            this.Context.SaveChanges();

        }

        public UserAndRoleViewModel MakeUserChanges(string id)
        {
            ApplicationUser user = this.Context.Users.Find(id);

            UserAndRoleViewModel userVm = new UserAndRoleViewModel();
            userVm.UserId = id;
            userVm.Email = user.Email;

            userVm.Roles = this.Context.Roles;

            return userVm;
        }

        public UserAndRoleViewModel MakeUserChangesWithoutAdminRole(string id)
        {
            ApplicationUser user = this.Context.Users.Find(id);

            UserAndRoleViewModel userVm = new UserAndRoleViewModel();
            userVm.UserId = id;
            userVm.Email = user.Email;

            userVm.Roles = this.Context.Roles.Where(r => r.Name != "Admin");
            return userVm;
        }

        public void SetUserRole(EditRoleBm editRoleBm)
        {
            ApplicationUser user = this.Context.Users.Find(editRoleBm.UserId);
            IdentityRole role = this.Context.Roles.Find(editRoleBm.RoleId);

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.Context));
            userManager.AddToRole(user.Id, role.Name);
            this.Context.SaveChanges();

        }

        public UserWithAssignedRoleViewModel ViewAssignedRoles(string id)
        {
            IEnumerable<IdentityRole> roles =
                this.Context.Roles.Where(u => u.Users.FirstOrDefault(us => us.UserId == id).UserId == id);
            ApplicationUser user = this.Context.Users.Find(id);

            UserWithAssignedRoleViewModel userAndRolesVm = new UserWithAssignedRoleViewModel();
            userAndRolesVm.Roles = roles;
            userAndRolesVm.UserEmail = user.Email;
            return userAndRolesVm;
        }

        [HandleError(ExceptionType = typeof(ArgumentNullException), View = "Error")]
        public string GetUserId(string userEmail)
        {
            ApplicationUser user = this.Context.Users.FirstOrDefault(u => u.Email == userEmail);
            return user.Id;
        }

        [HandleError(ExceptionType = typeof(ArgumentNullException), View = "Error")]
        public void DeleteRole(DeleteRoleBm deleteRoleBm)
        {
          
                var userId =  Context.Users.FirstOrDefault(u => u.Email == deleteRoleBm.UserEmail).Id;
                var role = this.Context.Roles.FirstOrDefault(r => r.Name == deleteRoleBm.RolesName);

                UserManager<ApplicationUser> userManager =
                    new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.Context));
                userManager.RemoveFromRole(userId, role.Name);
                this.Context.SaveChanges();
         }

        public IEnumerable<AllUsersPrefixViewModel> GetAllUsers()
        {
            IEnumerable<SugarUser> users = this.Context.SugarUsers.Where(u => u.ClientPrefix == null);
            IEnumerable<AllUsersPrefixViewModel> usersMapped =
                Mapper.Map<IEnumerable<SugarUser>, IEnumerable<AllUsersPrefixViewModel>>(users);

            return usersMapped;

        }


        public IEnumerable<CreatePrefixViewModel> GetAllPrefixes()
        {
            IEnumerable<ClientPrefix> prefixes = this.Context.ClientPrefixes;
            IEnumerable<CreatePrefixViewModel> mappedPrefixes =
                Mapper.Map<IEnumerable<ClientPrefix>, IEnumerable<CreatePrefixViewModel>>(prefixes);
            
           
            return mappedPrefixes;
        }

        public void CreatePrefix(CreatePrefixBm createPrefixBm)
        {
            ClientPrefix prefix = new ClientPrefix();
          
            prefix.PrefixName = createPrefixBm.PrefixName.ToUpper();
         
            this.Context.ClientPrefixes.Add(prefix);
           
            this.Context.SaveChanges();
        }
    }
}
