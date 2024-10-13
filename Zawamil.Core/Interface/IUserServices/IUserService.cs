using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zawamil.Core.Models.checkupdates;
using Zawamil.Core.Models.Users;
using Zawamil.Core.Models.Users.DTOs;

namespace Zawamil.Core.Interface.IUserServices
{
    public interface IUserService
    {
        Task<UserModelDto> RegisterAsync(CreateUser model);
        Task<UserModelDto> LoginAsync(CreateUser model);
        Task<UserModelDto> UpdateUserAsync(int id,CreateUser model);
        Task<UserModelDto> UpdateUserActiveAsync(int id);
        Task<UserModelDto> UpdateUserAdminAsync(int id);
        Task<UserModelDto> DeletUserAsync(int id);
        Task<UserModel> GetUserByidAsync(int id);
        Task<IEnumerable<UserModel>> GetUserAsync();
        Task<CheckAppVersinResponse> CreateAppVersionAsync(string model);
        Task<CheckAppVersinResponse> UpdateAppVersionAsync(int id, string model);
        Task<bool> DeleteAppVersionAsync(int id);
        Task<CheckUpdateAppResponse> CheckListAppVersion(string appVersion);
        Task<IEnumerable<AppVersion>> GetAppVersionsAsync();
        Task<AppVersion> GetlistAppVersionsAsync(int id);
        Task<AppVersion> GetAppVersionsByIdAsync(int id);
    }
}
