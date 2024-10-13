using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zawamil.Core.Interface.IUserServices;
using Zawamil.Core.Models.Songes.Dtos;
using Zawamil.Core.Models.Songes;
using Zawamil.Core.Models.Users;
using Zawamil.Core.Models.Users.DTOs;
using Zawamil.Core.Models.checkupdates;
using Microsoft.AspNetCore.Mvc;

namespace Zawamil.EF.UserServices
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserService(
           ApplicationDbContext context,
           IWebHostEnvironment webHostEnvironment)

        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;

        }


        public async Task<UserModelDto> RegisterAsync(CreateUser model)
        {
            try
            {
                var user = await _context.Users
                .Where(m => m.UserName == model.Username.Trim())
                .FirstOrDefaultAsync();
                if (user != null)
                    return new UserModelDto { Message = "هذا الحساب موجود مسبقا", IsAuthenticated = false };

                var createuser = new User
                {
                    UserName = model.Username.Trim(),
                    Password = model.Password.Trim(),
                    CreatedAt = DateTime.Now,
                    UserState = UserState.Active,
                    UserRoles =UserRoles.CLIENT,
                    
                };
                await _context.AddAsync(createuser);
                _context.SaveChanges();
                return new UserModelDto
                {
                    id = createuser.Id,
                    Message = "تمت العملية بنجاح",
                    IsAuthenticated = true,
                    UserName = model.Username.Trim(),
                    UserState = UserState.Active.ToString(),
                    UserRole = UserRoles.CLIENT.ToString(),
                    NoUserRole = (int?)UserRoles.CLIENT,
                };
            }catch (Exception ex)
            {
                throw new NotImplementedException();
            }

        }
        public async Task<UserModelDto> LoginAsync(CreateUser model)
        {
            try
            {
                var user = await _context.Users
                    .Where(m => m.UserName == model.Username.Trim())
                    .FirstOrDefaultAsync();
                if (user == null || user.Password != model.Password.Trim())
                    return new UserModelDto { Message = "الايميل او كلمة المرور خطأ", IsAuthenticated = false };

                if (user.UserState == UserState.Locked)
                    return new UserModelDto { Message = "هذا الحساب موقوف", IsAuthenticated = false };
                return new UserModelDto
                {
                    id = user.Id,
                    Message = "تمت العملية بنجاح",
                    IsAuthenticated = true,
                    UserName = model.Username.Trim(),
                    UserState = UserState.Active.ToString(),
                    UserRole = user.UserRoles.ToString(),
                    NoUserRole = (int?)user.UserRoles.Value,
                };
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }

            //throw new NotImplementedException();
        }
        public async Task<UserModelDto> UpdateUserAsync(int id, CreateUser model)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                    return new UserModelDto { Message = "لايوجد هذا الحساب", IsAuthenticated = false };
                user.UserName = model.Username.Trim();
                user.Password = model.Password.Trim();
                //user.UserRoles = model.Roles == 1 ? UserRoles.Admin: UserRoles.Client;
                _context.SaveChanges();
                return new UserModelDto
                {
                    id = user.Id,
                    Message = "تمت العملية بنجاح",
                    IsAuthenticated = true,
                    UserName = model.Username.Trim(),
                    UserState = UserState.Active.ToString(),
                    UserRole = user.UserRoles.ToString(),
                    NoUserRole = (int?)user.UserRoles.Value,
                };
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }
        public async Task<UserModelDto> UpdateUserActiveAsync(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                    return new UserModelDto { Message = "لايوجد هذا الحساب", IsAuthenticated = false };
                if (user.UserState ==  UserState.Active)
                {
                    user.UserState = UserState.Locked;
                }
                else
                {
                    user.UserState = UserState.Active;
                }
                _context.SaveChanges();
                return new UserModelDto
                {
                    id = user.Id,
                    Message = "تمت العملية بنجاح",
                    IsAuthenticated = true,
                    UserName = user.UserName.Trim(),
                    UserState = user.UserState.ToString(),
                    UserRole= user.UserRoles.ToString(),
                    NoUserRole = (int?)user.UserRoles.Value,
                };
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }

        }
        
        public async Task<UserModelDto> UpdateUserAdminAsync(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                    return new UserModelDto { Message = "لايوجد هذا الحساب", IsAuthenticated = false };
                if (user.UserRoles == UserRoles.ADMIN)
                {
                    user.UserRoles = UserRoles.CLIENT;
                }
                else
                {
                    user.UserRoles = UserRoles.ADMIN;
                }
                _context.SaveChanges();
                return new UserModelDto
                {
                    id = user.Id,
                    Message = "تمت العملية بنجاح",
                    IsAuthenticated = true,
                    UserName = user.UserName.Trim(),
                    UserState = user.UserState.ToString(),
                    UserRole= user.UserRoles.ToString(),
                    NoUserRole = (int?)user.UserRoles.Value,
                };
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }

        }

        public async Task<UserModel> GetUserByidAsync(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                    return new UserModel { UserName = "لايوجد هذا الحساب", Id = 0 };
                return new UserModel
                {
                    Id = user.Id,
                    UserName = user.UserName.Trim(),
                    UserState = user.UserState.ToString(),
                    Password = user.Password,
                    CreatedAt = user.CreatedAt.ToShortDateString(),
                    UserRole = user.UserRoles.ToString(),
                    NoUserRole = (int?)user.UserRoles.Value,
                };
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }

        public async Task<IEnumerable<UserModel>> GetUserAsync()
        {
            try
            {
                var user = await _context.Users
                .Select(m => new UserModel
                {
                    Id = m.Id,
                    UserName = m.UserName,
                    Password = m.Password,
                    CreatedAt = m.CreatedAt.ToShortDateString(),
                    UserState = m.UserState.ToString(),
                    UserRole = m.UserRoles.ToString(),
                    NoUserRole = (int?)m.UserRoles.Value,
                }).ToListAsync();
                if (user == null)
                    return Enumerable.Empty<UserModel>();
                return user;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }


        public Task<UserModelDto> DeletUserAsync(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<CheckAppVersinResponse> CreateAppVersionAsync(string model)
        {
            try
            {
                var appversion = new AppVersion
                {
                    Name = model,
                    CreateAt = DateTime.Now,
                    IsList = true,

                };
                await _context.AppVersions.AddAsync(appversion);
                _context.SaveChanges();
               
                return new CheckAppVersinResponse(appversion.Id,"ملاحظه","تمت العملية بنجاح",true);
            }
            catch (Exception ex)
            {
                //throw new NotImplementedException();
                return new CheckAppVersinResponse(0, "ملاحظه", "خطأ لم تتم العملية", false); 
            }
        }
        public async Task<CheckAppVersinResponse> UpdateAppVersionAsync(int id, string model)
        {
            try
            {
                var appversion = await _context.AppVersions
                .SingleOrDefaultAsync(m => m.Id == id);
                if (appversion == null)
                    return new CheckAppVersinResponse(0, "ملاحظه", "خطأ لم تتم العملية", false); 
                appversion.Name = model;
                _context.SaveChanges();
                return new CheckAppVersinResponse(appversion.Id, "ملاحظه", "تمت العملية بنجاح", true); 
            }
            catch (Exception ex) 
            {
                return new CheckAppVersinResponse(0, "ملاحظه", "خطأ لم تتم العملية", false);
            }
        }
        public async Task<bool> DeleteAppVersionAsync(int id)
        {
            try
            {
                var appVersion = await _context.AppVersions
                .SingleOrDefaultAsync(m => m.Id == id);
                if (appVersion == null)
                    return false;
                appVersion.IsList = false;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex) { return false; }
        } 
        public async Task<CheckUpdateAppResponse> CheckListAppVersion(string name)
        {
            try
            {
                var appVersion = await _context.AppVersions
                    .Where(m => m.Name == name.Trim() && m.IsList == true)
                    .FirstOrDefaultAsync();
                if (appVersion == null)
                    return new CheckUpdateAppResponse(1, "يوجد تحديث جديد", "هل تريد تحديث التطبيق لتتمكن من الحصول على المزيد من الأناشيد", true); 
                //appVersion.IsList = false;
                //_context.SaveChanges();
                return new CheckUpdateAppResponse(0, "000", "000", false) ;
            }
            catch (Exception ex) { return new CheckUpdateAppResponse(0, "000", "000", false); }
        }
        
        public async Task<IEnumerable<AppVersion>> GetAppVersionsAsync()
        {
            try
            {
                var appversion = await _context.AppVersions
                   .Select(m => new AppVersion
                   {
                       Id = m.Id,
                       Name = m.Name,
                       IsList = m.IsList,
                       CreateAt =  m.CreateAt,
                   }).ToListAsync();
                if (appversion == null)
                    return Enumerable.Empty<AppVersion>();
                return appversion;
                //var appVersion = await _context.AppVersion
                //    .FirstOrDefaultAsync();
                //return appVersion.to;
            }
            catch (Exception ex) {
                throw new NotImplementedException();
            }
        }
        
        public async Task<AppVersion> GetAppVersionsByIdAsync(int id)
        {
            try
            {
                var appVersion = await _context.AppVersions
                    .Where(m => m.Id == id)
                    .FirstOrDefaultAsync();
                if (appVersion == null)
                    return new AppVersion { Id = 0, Name = "لاتوجد هذه الاصدار" ,CreateAt = DateTime.Now,IsList=false};
                return new AppVersion
                {
                    Id = appVersion.Id,
                    Name = appVersion.Name,
                    CreateAt = appVersion.CreateAt,
                    IsList =  appVersion.IsList,
                };
            }
            catch (Exception ex) {
                throw new NotImplementedException();
            }
        }
        public async Task<AppVersion> GetlistAppVersionsAsync(int id)
        {
            try
            {
                var appVersion = await _context.AppVersions
                    .Where(m => m.IsList == true && m.Id != id)
                    .FirstOrDefaultAsync();
                if (appVersion == null)
                    return new AppVersion { Id = 0, Name = "لاتوجد هذه الاصدار" ,CreateAt = DateTime.Now,IsList=false};
                return new AppVersion
                {
                    Id = appVersion.Id,
                    Name = appVersion.Name,
                    CreateAt = appVersion.CreateAt,
                    IsList =  appVersion.IsList,
                };
            }
            catch (Exception ex) {
                throw new NotImplementedException();
            }
        }
    }
}
