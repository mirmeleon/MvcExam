using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;
using SugarFactory.Models.BindingModels.AdminArea;
using SugarFactory.Models.EntityModels;
using SugarFactory.Models.ViewModels.AdminArea;

namespace SugarFactory.Services
{
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

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.Context));
            UserManager.AddToRole(user.Id, role.Name);
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

        public string GetUserId(string userEmail)
        {
            ApplicationUser user = this.Context.Users.FirstOrDefault(u => u.Email == userEmail);
            return user.Id;
        }

        public void DeleteRole(DeleteRoleBm deleteRoleBm)
        {
            var userId = Context.Users.FirstOrDefault(u => u.Email == deleteRoleBm.UserEmail).Id;
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

        public void AssignPrefixes(AssignPrefixBm assignPrefixBm)
        {
            SugarUser user = this.Context.SugarUsers.FirstOrDefault(u => u.Id == assignPrefixBm.Id);
            user.ClientPrefix = assignPrefixBm.ClientPrefix;
            this.Context.SugarUsers.AddOrUpdate(user);
            this.Context.SaveChanges();

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
            SugarUser admin = this.Context.SugarUsers.Find(1);
            prefix.PrefixName = createPrefixBm.PrefixName.ToUpper();

            this.Context.ClientPrefixes.Add(prefix);
            try
            {
                this.Context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Debug.WriteLine("Entity of type {0}  in state {1} has the following validation errors: ",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            catch (DbUpdateException exe)
            {
                var sb = new StringBuilder();
                sb.AppendLine($"DbUpdateException error details - {exe?.InnerException?.InnerException?.Message}");

                foreach (var eve in exe.Entries)
                {
                    sb.AppendLine($"Entity of type {eve.Entity.GetType().Name} in state {eve.State} could not be updated");
                }

                var result = sb.ToString();

                throw;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }

           

        }


    }
}
