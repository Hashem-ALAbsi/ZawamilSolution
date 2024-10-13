using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Zawamil.Core.Interface.IUserServices;
using Zawamil.Core.Models;
using Zawamil.Core.Models.checkupdates;
using Zawamil.Core.Models.Users;
using Zawamil.Core.Models.Users.DTOs;

namespace Zawamil.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0", Deprecated = true)]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private IWebHostEnvironment _hostingEnv;
		private ResponseDto _response;
		public UserController(
        IUserService userService,
        IWebHostEnvironment hostingEnv)
        {
            _userService = userService;
            _hostingEnv = hostingEnv;
			_response = new ResponseDto();

		}

        [HttpGet("Users")]
        public async Task<IActionResult> GetUserAsync()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

			var users = await _userService.GetUserAsync();
   //         try
   //         {
   //             _response.Result = await _userService.GetUserAsync();

   //         }
			//catch (Exception ex)
   //         {
			//	_response.IsSuccess = false;
			//	_response.Message = ex.Message;
			//}
			//var users = await _userService.GetUserAsync();
            

			return Ok(users);
        }
        
        [HttpGet("Users/{id}")]
        public async Task<IActionResult> GetUserByidAsync(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
			var user = await _userService.GetUserByidAsync(id);
			if (user.Id == 0)
				return BadRequest(user.UserName);
			//try
   //         {
			//	var user = await _userService.GetUserByidAsync(id);
			//	if (user.Id == 0)
   //             {
			//		_response.IsSuccess = false;
			//		_response.Message = user.UserName;
			//	}
			//		//return BadRequest(user.UserName);
			//	_response.Result = user;
			//}
   //         catch (Exception ex)
   //         {
			//	_response.IsSuccess = false;
			//	_response.Message = ex.Message;
			//}

            return Ok(user);
        }

        [HttpPost("Create-User")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateUser model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
			var user = await _userService.RegisterAsync(model);
			if (!user.IsAuthenticated)
				return BadRequest(user.Message);
			//try
   //         {
			//	var user = await _userService.RegisterAsync(model);
			//	if (!user.IsAuthenticated)
   //             {
   //                 _response.IsSuccess = false;
   //                 _response.Message = user.Message;
   //             }
			//		//return BadRequest(result.Message);
			//}
			//catch (Exception ex)
   //         {
			//	_response.IsSuccess = false;
			//	_response.Message = ex.Message;
			//}
			

            return Ok(user);
        }

        [HttpPost("Login-User")]
        public async Task<IActionResult> LoginAsync([FromBody] CreateUser model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _userService.LoginAsync(model);
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }
        
        [HttpPost("Update-User/{id}")]
        public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] CreateUser model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _userService.UpdateUserAsync(id,model);
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }
        
        [HttpGet("UpdateActive-User/{id}")]
        public async Task<IActionResult> UpdateUserActiveAsync(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _userService.UpdateUserActiveAsync(id);
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }
        
        [HttpGet("UpdateAdmin-User/{id}")]
        public async Task<IActionResult> UpdateUserAdminAsync(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _userService.UpdateUserAdminAsync(id);
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }
        [HttpGet("CheckUpdate-App/{versionapp}")]
        public async Task<IActionResult> CheckUpdateApp(string versionapp)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var result = await _userService.CheckListAppVersion(versionapp.Trim());
                if (!result.IsAuthenticated)
                    return BadRequest(result);
                return Ok(result);
            }
            catch (Exception ex) { throw new NotImplementedException(); }
        }

        [HttpGet("Create-AppVersion/{appversion}")] 
        public async Task<IActionResult> CreateAppVersionAsync(string appversion)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _userService.CreateAppVersionAsync( appversion);
            if (!result.IsAuthenticated)
                return BadRequest(result.massage);
            var thelistversion = await _userService.GetlistAppVersionsAsync(result.id);
            if (thelistversion.Id != 0)
            {
                await _userService.DeleteAppVersionAsync(thelistversion.Id);
            }
            return Ok(result);
        }
        
        [HttpGet("Update-AppVersion/{id}/{appversion}")] 
        public async Task<IActionResult> UpdateAppVersionAsync(int id,string appversion)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _userService.UpdateAppVersionAsync(id, appversion);
            if (!result.IsAuthenticated)
                return BadRequest(result.massage);
           
            return Ok(result);
        }
        
        [HttpGet("Get-AppVersion")] 
        public async Task<IActionResult> GetAppVersionsAsync()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _userService.GetAppVersionsAsync();
           
           
            return Ok(result);
        }
        
        [HttpGet("Get-AppVersionById/{id}")] 
        public async Task<IActionResult> GetAppVersionsByIdAsync(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _userService.GetAppVersionsByIdAsync(id);
            if (result.Id == 0)
                return BadRequest(result.Name);

            return Ok(result);
        }
        [HttpGet("Deleted-AppVersionById/{id}")] 
        public async Task<IActionResult> DeletedAppVersionsByIdAsync(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _userService.DeleteAppVersionAsync(id);
            if (!result)
                return BadRequest("لم تتم العملية");

            return Ok(result);
        }


    }
}
