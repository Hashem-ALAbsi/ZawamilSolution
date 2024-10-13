using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using Zawamil.Web.Models;
using Zawamil.Web.Models.Users.DTOs;
using Zawamil.Web.Service.IService;
using Zawamil.Web.Utility;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Zawamil.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
		private readonly IBaseService _baseService;

		public HomeController(ILogger<HomeController> logger, IBaseService baseService)
        {
            _logger = logger;
			_baseService = baseService;
		}

		public async Task<IActionResult> Index()
        {
			string Readcookie = ReadIDcookies();
			ReadNamecookies();
            ReadRolecookies();

            if (Readcookie != "0")
			{
				var result = await _baseService.SendAsync(new RequestDto()
				{
					ApiType = SD.ApiType.GET,
					Url = SD.BaseUrl + "api/v1/User/Users/" +Readcookie,
				});
				//var response = await httpClient.GetAsync("/api/v1.0/User/Users");
				//var result = await response.Content.ReadFromJsonAsync<UserModel>();
				if (result != null && result.IsSuccess)
				{
					UserModel? list = JsonConvert.DeserializeObject<UserModel>(Convert.ToString(result.Result));
					
                    CreateNamecookies(list.UserName);
                    CreateRolecookies(list.UserRole);
                    CreateIDcookies(list.Id.ToString());
					//TempData["usernameinfo"] = list.UserName;
					//TempData["userroleinfo"] = list.UserRole;
                    //               if (list.UserRole != SD.RoleCustomer)
                    //{
                    //	SD.UserRole = list.UserRole;
                    //}
                    //bool userrole = ReadRolecookies();
                    //return View(list);
                }
				//SD.UserName = $"{Request.Cookies["usernameinfo"]}";
				////TempData["username"] = $"{Request.Cookies["usernameinfo"]}";
				//bool userrole = ReadRolecookies();
    //            if (userrole)
    //            {
    //               SD.UserRole = $"{Request.Cookies["userroleinfo"]}";
    //            }
                
                return View();
                //return View(nameof(Login));
                //return RedirectToAction("UserIndex", "User");
            }
			else
			{
				return RedirectToAction(nameof(Login));
			}
			// return View();
		}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

		public IActionResult Login()
		{
			//bool Readcookie = Readcookies();
			//if (Readcookie)
			//{
			//	return RedirectToAction(nameof(Index));
			//}
			//else
			//{
			//	return View();
			//}
			return View();
		}
		
		public IActionResult Logout()
		{
			bool Readcookie = Deletecookies();
			//SD.UserName = null;
			//SD.UserRole = null;
			//SD.UserId = null;
			TempData["success"] = " „  ”ÃÌ· Œ—ÊÃﬂ »‰Ã«Õ";
            bool r1 = ReadNamecookies();
            bool r2 = ReadRolecookies();
            return RedirectToAction(nameof(Index));
            //if (Readcookie)
            //{
            //	return RedirectToAction(nameof(Index));
            //}
            //else
            //{
            //	return View();
            //}
        }
		[HttpPost]
		public async Task<IActionResult> Login(CreateUser user)
		{
			//if (!ModelState.IsValid)
			//	return BadRequest(ModelState);

			try
			{
				//LoginResponseDto loginResponseDto = new();
				if (ModelState.IsValid)
				{
					var result = await _baseService.SendAsync(new RequestDto()
					{
						ApiType = SD.ApiType.POST,
						Data = user,
						Url = SD.BaseUrl + "api/v1/User/Login-User"
					});
					if (result != null && result.IsSuccess)
					{
						UserModel? list = JsonConvert.DeserializeObject<UserModel>(Convert.ToString(result.Result));
						//loginResponseDto.User.Id = list.Id;
						//loginResponseDto.User.Username = list.UserName;
						//loginResponseDto.User.Password = list.Password;
						CreateNamecookies(list.UserName);
						CreateRolecookies(list.UserRole);
                        CreateIDcookies(list.Id.ToString());
                        ReadNamecookies();
                        ReadRolecookies();
                        //await SignInUser(loginResponseDto);
                        //_tokenProvider.SetToken(loginResponseDto.Token);
                        TempData["success"] = result.Message;
						//Url.Action("UserIndex", "User", new { area = "" });
						return RedirectToAction(nameof(Index));
					}
					else
					{
						TempData["error"] = result?.Message;
						return View(user);
					}
				}
			}
			catch (Exception ex)
			{
				TempData["error"] = $"{ex.Message}";
				return NotFound();
			}
			return View(user);
		}
		public bool CreateNamecookies(string username)
		{
			//var cookieOptions = new CookieOptions
			//{
			//    Expires = DateTime.Now.AddDays(1), //  ⁄ÌÌ‰  «—ÌŒ «·«‰ Â«¡
			//    HttpOnly = true, // Ì„‰⁄ «·Ê’Ê· ≈·Ï «·ﬂÊﬂÌ“ „‰ JavaScript
			//    Secure = true // Ì÷„‰ √‰ Ì „ ≈—”«· «·ﬂÊﬂÌ“ ⁄»— HTTPS ›ﬁÿ
			//};
			CookieOptions cookie = new CookieOptions();
			cookie.Expires = DateTime.Now.AddDays(1);
			Response.Cookies.Append("usernameinfo", username, cookie);
			return true;
		}
		public bool CreateRolecookies(string userrole)
		{
			CookieOptions cookie = new CookieOptions();
			cookie.Expires = DateTime.Now.AddDays(1);
			Response.Cookies.Append("userroleinfo", userrole, cookie);
			return true;
		}
		public bool CreateIDcookies(string userid)
		{
			CookieOptions cookie = new CookieOptions();
			cookie.Expires = DateTime.Now.AddDays(1);
			Response.Cookies.Append("useridinfo", userid, cookie);
			return true;
		}
		public bool ReadNamecookies()
		{
			var data = Request.Cookies["usernameinfo"];
			if (data != null)
			{
                TempData["usernameinfo"] = data;
				//ViewBag.UserName = Request.Cookies["usernameinfo"];
				//HttpContext.Session.SetString("usernameinfo", data);
				//ViewBag.UserName = data;

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
					//ViewBag.UserRole = Request.Cookies["userroleinfo"];
					//HttpContext.Session.SetString("userroleinfo", data);
					return true;
				}
				return false;
			}
			else
			{
				return false;
			}
		}
		public string ReadIDcookies()
		{
			var data = Request.Cookies["useridinfo"];
			if (data != null)
			{
            
                return data.ToString();
			}
			else
			{
				return "0";
			}
		}
		public bool Deletecookies()
		{
            Response.Cookies.Delete("usernameinfo");
            Response.Cookies.Delete("userroleinfo");
            Response.Cookies.Delete("useridinfo");
			TempData["usernameinfo"] = null;
			TempData["userroleinfo"] = null;
			//HttpContext.Session.SetString("userroleinfo", null);
			TempData["success"] = " „  ”ÃÌ· «·Œ—ÊÃ »‰Ã«Õ";
			return true;
        }
	}
}
