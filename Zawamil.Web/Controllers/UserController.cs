using Azure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using Zawamil.Web.Models;
using Zawamil.Web.Models.Users.DTOs;
using Zawamil.Web.Service.IService;
using Zawamil.Web.Utility;

namespace Zawamil.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IBaseService _baseService;
        private readonly ITokenProvider _tokenProvider;
		private readonly HttpClient httpClient;
		public UserController(IBaseService baseService, ITokenProvider tokenProvider, HttpClient httpClient)
        {
            _baseService = baseService;
            _tokenProvider = tokenProvider;
			this.httpClient = httpClient;
		}
       
        [HttpGet]
        public async Task<IActionResult> UserIndex()
        {
			if (!ModelState.IsValid)
					return BadRequest(ModelState);
            bool r1 = ReadNamecookies();
            bool r2 = ReadRolecookies();

            if (r1 == false || r2 == false)
                return RedirectToAction("Index", "Home");
			List<UserModel>? list = new();
			try
			{
				var result = await _baseService.SendAsync(new RequestDto()
				{
					ApiType = SD.ApiType.GET,
					Url = SD.BaseUrl + "api/v1/User/Users"
				});
				//var response = await httpClient.GetAsync("/api/v1.0/User/Users");
				//var result = await response.Content.ReadFromJsonAsync<UserModel>();
				if (result != null && result.IsSuccess)
				{
					list = JsonConvert.DeserializeObject<List<UserModel>>(Convert.ToString(result.Result));
				}
				else
				{
					TempData["error"] = result?.Message;
				}
				//HttpClient client = new HttpClient();
				//client.BaseAddress = new Uri($"{SD.BaseUrl}");
				//client.DefaultRequestHeaders.Accept.Add(
				//	new MediaTypeWithQualityHeaderValue("application/json"));
				//HttpResponseMessage response = client.GetAsync("/api/v1/User/Users").Result;
				//if (response.IsSuccessStatusCode)
				//{
				//	ViewBag.result = response.Content.ReadFromJsonAsync<IEnumerable<UserModel>>().Result;
				//}
				//else
				//{
				//	ViewBag.result = "Error";
				//}
			}
			catch (Exception ex) {
                TempData["error"] = $"{ex.Message}";
                return NotFound();
            }
			//if (!ModelState.IsValid)
			//	return BadRequest(ModelState);
			//var result = await _baseService.SendAsync.();
			//return Ok(result);
			

			//Url = "https://localhost:7219/api/v1/User/Users"

			//UserModel r = (UserModel)result.Result;
			return View(list);
		}
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
            bool r1 = ReadNamecookies();
            bool r2 = ReadRolecookies();

            if (r1 == false || r2 == false)
                return RedirectToAction("Index", "Home");
			List<UserModel>? list = new();
			try
			{
				var result = await _baseService.SendAsync(new RequestDto()
				{
					ApiType = SD.ApiType.GET,
					Url = SD.BaseUrl + "api/v1/User/Users"
				});
				if (result != null && result.IsSuccess)
				{
					list = JsonConvert.DeserializeObject<List<UserModel>>(Convert.ToString(result.Result));
				}
				else
				{
					TempData["error"] = result?.Message;
				}
			}
			catch (Exception ex) { }
			return View(list);
		}
        public IActionResult CreateUsers()
        {
            bool r1 = ReadNamecookies();
            bool r2 = ReadRolecookies();
            return View();
        }
        [HttpPost]
		public async Task<IActionResult> CreateUsers(CreateUser user)
		{
            //if (!ModelState.IsValid)
            //	return BadRequest(ModelState);
            bool r1 = ReadNamecookies();
            bool r2 = ReadRolecookies();

            if (r1 == false || r2 == false)
                return RedirectToAction("Index", "Home");
			try
			{
                if (ModelState.IsValid)
                {
                    var result = await _baseService.SendAsync(new RequestDto()
                    {
                        ApiType = SD.ApiType.POST,
                        Data = user,
                        Url = SD.BaseUrl + "api/v1/User/Create-User"
                    });
                    if (result != null && result.IsSuccess)
                    {
                        TempData["success"] = result.Message;
                        return RedirectToAction(nameof(UserIndex));
                    }
                    else
                    {
                        TempData["error"] = result?.Message;
                    }
                }
            }
			catch (Exception ex) {
                TempData["error"] = $"{ex.Message}";
                return NotFound();
            }
			return View(user);
		}
		public async Task<IActionResult> UpdateUser(int userId)
		{
            bool r1 = ReadNamecookies();
            bool r2 = ReadRolecookies();

            if (r1 == false || r2 == false)
                return RedirectToAction("Index", "Home");
			var result = await _baseService.SendAsync(new RequestDto()
			{
				ApiType = SD.ApiType.GET,
				Url = SD.BaseUrl + "api/v1/User/Users/" + userId,
			});
			//var response = await httpClient.GetAsync("/api/v1.0/User/Users");
			//var result = await response.Content.ReadFromJsonAsync<UserModel>();
			if (result != null && result.IsSuccess)
			{
				UserModel? list = JsonConvert.DeserializeObject<UserModel>(Convert.ToString(result.Result));
				CreateUser? user = new() ;
                user.Id = list.Id;
                user.Username = list.UserName;
                user.Password = list.Password;
				return View("CreateUsers", user);
			}
			else
			{
				TempData["error"] = result?.Message;

			}
			return NotFound();
			//return View("CreateUser",);
		}
		[HttpPost]
		public async Task<IActionResult> UpdateUser( CreateUser user)
		{
            //if (!ModelState.IsValid)
            //	return BadRequest(ModelState);
            bool r1 = ReadNamecookies();
            bool r2 = ReadRolecookies();

            if (r1 == false || r2 == false)
                return RedirectToAction("Index", "Home");
			try
			{
                if (ModelState.IsValid)
                {
                    var result = await _baseService.SendAsync(new RequestDto()
                    {
                        ApiType = SD.ApiType.POST,
                        Data = user,
                        Url = SD.BaseUrl + "api/v1/User/Update-User/"+ user.Id,
					});
                    if (result != null && result.IsSuccess)
                    {
                        TempData["success"] = result.Message;
                        //TempData["success"] = "User created successfully";
                        return RedirectToAction(nameof(UserIndex));
                    }
                    else
                    {
                        TempData["error"] = result?.Message;
                    }
                }
            }
			catch (Exception ex) {
                TempData["error"] = $"{ex.Message}";
                return NotFound();
            }
			return View(user);
		}
        public async Task<IActionResult> DetelsUser(int userId)
        {
            bool r1 = ReadNamecookies();
            bool r2 = ReadRolecookies();

            if (r1 == false || r2 == false)
                return RedirectToAction("Index", "Home");
			var result = await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.BaseUrl + "api/v1/User/Users/"+userId,
            });
            //var response = await httpClient.GetAsync("/api/v1.0/User/Users");
            //var result = await response.Content.ReadFromJsonAsync<UserModel>();
            if (result != null && result.IsSuccess)
            {
                UserModel? list = JsonConvert.DeserializeObject<UserModel>(Convert.ToString(result.Result));
                return View(list);
            }
            else
            {
                TempData["error"] = result?.Message;
            }
            return NotFound();
        }
        //[HttpPost]
        //public async Task<IActionResult> DetelsUser(UserModel model)
        //{
        //    try
        //    {
        //        var result = await _baseService.SendAsync(new RequestDto()
        //        {
        //            ApiType = SD.ApiType.GET,
        //            Url = SD.BaseUrl + "/api/v1/User/UpdateActive-User/"+model.Id 
        //        });
        //        if (result != null && result.IsSuccess)
        //        {
        //            TempData["success"] = "User created successfully";
        //            return RedirectToAction(nameof(UserIndex));
        //        }
        //        else
        //        {
        //            TempData["error"] = result?.Message;
        //        }
        //    }
        //    catch (Exception ex) { }
            
        //    return View(model);
        //}
        public async Task<IActionResult> ActiveUser(int id)
        {
            bool r1 = ReadNamecookies();
            bool r2 = ReadRolecookies();

            if (r1 == false || r2 == false)
                return RedirectToAction("Index", "Home");
			try
            {
                var result = await _baseService.SendAsync(new RequestDto()
                {
                    ApiType = SD.ApiType.GET,
                    Url = SD.BaseUrl + "api/v1/User/UpdateActive-User/" +id
                });
                if (result != null && result.IsSuccess)
                {
                    TempData["success"] = result.Message;
                    return RedirectToAction(nameof(UserIndex));
                }
                else
                {
                    TempData["error"] = result?.Message;
                    return View();
                }
                
            }
            catch (Exception ex) {
                TempData["error"] = $"{ex.Message}";
                return NotFound();
            }
        }
        public async Task<IActionResult> AdminUser(int id)
        {
            bool r1 = ReadNamecookies();
            bool r2 = ReadRolecookies();

            if (r1 == false || r2 == false)
                return RedirectToAction("Index", "Home");
			//if (SD.UserRole == null)
			//	return RedirectToAction("Index", "Home");
			try
            {
                var result = await _baseService.SendAsync(new RequestDto()
                {
                    ApiType = SD.ApiType.GET,
                    Url = SD.BaseUrl + "api/v1/User/UpdateAdmin-User/" + id
                });
                if (result != null && result.IsSuccess)
                {
                    TempData["success"] = result.Message;
                    return RedirectToAction(nameof(UserIndex));
                }
                else
                {
                    TempData["error"] = result?.Message;
                    return View();
                }
            }
            catch (Exception ex) {
                TempData["error"] = $"{ex.Message}";
                return NotFound();
            }

        }

        //private async Task SignInUser(LoginResponseDto model)
        //{
        //    var handler = new JwtSecurityTokenHandler();

        //    var jwt = handler.ReadJwtToken(model.Token);

        //    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
        //    identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email,
        //        jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
        //    identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub,
        //        jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));
        //    identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name,
        //        jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));


        //    identity.AddClaim(new Claim(ClaimTypes.Name,
        //        jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
        //    identity.AddClaim(new Claim(ClaimTypes.Role,
        //        jwt.Claims.FirstOrDefault(u => u.Type == "role").Value));



        //    var principal = new ClaimsPrincipal(identity);
        //    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        //}
        public bool ReadNamecookies()
        {
            var data = Request.Cookies["usernameinfo"];
            if (data != null)
            {
                TempData["usernameinfo"] = data;

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ReadRolecookies()
        {
            var data = Request.Cookies["userroleinfo"];
            if (data != null)
            {
                //return data;

                if (data == SD.RoleAdmin)
                {
                    TempData["userroleinfo"] = data;
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }
    }
}
