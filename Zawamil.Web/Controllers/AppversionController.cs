using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Zawamil.Web.Models;
using Zawamil.Web.Models.Users;
using Zawamil.Web.Service.IService;
using Zawamil.Web.Utility;

namespace Zawamil.Web.Controllers
{
	public class AppversionController : Controller
	{
		private readonly IBaseService _baseService;
		private readonly ITokenProvider _tokenProvider;
		private readonly HttpClient httpClient;

		public AppversionController(IBaseService baseService, ITokenProvider tokenProvider, HttpClient httpClient)
		{
			_baseService = baseService;
			_tokenProvider = tokenProvider;
			this.httpClient = httpClient;
		}
		public async Task<IActionResult> Index()
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
            bool r1 = ReadNamecookies();
            bool r2 = ReadRolecookies();

            if (r1 == false || r2 == false)
				return RedirectToAction("Index", "Home");
			List<AppVersion>? list = new();
			try
			{
				var result = await _baseService.SendAsync(new RequestDto()
				{
					ApiType = SD.ApiType.GET,
					Url = SD.BaseUrl + "api/v1/User/Get-AppVersion"
				});
				//var response = await httpClient.GetAsync("/api/v1.0/User/Users");
				//var result = await response.Content.ReadFromJsonAsync<UserModel>();
				if (result != null && result.IsSuccess)
				{
					list = JsonConvert.DeserializeObject<List<AppVersion>>(Convert.ToString(result.Result));
				}
				else
				{
					TempData["error"] = result?.Message;
				}
			}
			catch (Exception ex)
			{
				TempData["error"] = $"{ex.Message}";
				return NotFound();
			}
			return View(list);
		}
		public IActionResult CreateAppVersion()
		{
            bool r1 = ReadNamecookies();
            bool r2 = ReadRolecookies();
            return View();
		}
		[HttpPost]
		public async Task<IActionResult> CreateAppVersion(CreateAppVersionModel appversion)
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
						ApiType = SD.ApiType.GET,
						Url = SD.BaseUrl + "api/v1/User/Create-AppVersion/"+appversion.Name
					});
					if (result != null && result.IsSuccess)
					{
						TempData["success"] = result.Message;
						return RedirectToAction(nameof(Index));
					}
					else
					{
						TempData["error"] = result?.Message;
					}
				}
			}
			catch (Exception ex)
			{
				TempData["error"] = $"{ex.Message}";
				return NotFound();
			}
			return View(appversion);
		}
        public async Task<IActionResult> UpdateAppVersion(int Id)
        {
            bool r1 = ReadNamecookies();
            bool r2 = ReadRolecookies();

            if (r1 == false || r2 == false)
                return RedirectToAction("Index", "Home");
            var result = await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.BaseUrl + "api/v1/User/Get-AppVersionById/" + Id,
            });
            //var response = await httpClient.GetAsync("/api/v1.0/User/Users");
            //var result = await response.Content.ReadFromJsonAsync<UserModel>();
            if (result != null && result.IsSuccess)
            {
                AppVersion? list = JsonConvert.DeserializeObject<AppVersion>(Convert.ToString(result.Result));
                CreateAppVersionModel? appversion = new();
				appversion.Id = list.Id;
				appversion.Name = list.Name;
                return View("CreateAppVersion", appversion);
            }
            else
            {
                TempData["error"] = result?.Message;

            }
            return NotFound();
            //return View("CreateUser",);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateAppVersion(CreateAppVersionModel appversion)
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
                        ApiType = SD.ApiType.GET,
                        Url = SD.BaseUrl + "api/v1/User/Update-AppVersion/" + appversion.Id +"/"+appversion.Name,
                    });
                    if (result != null && result.IsSuccess)
                    {
                        TempData["success"] = result.Message;
                        //TempData["success"] = "User created successfully";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["error"] = result?.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = $"{ex.Message}";
                return NotFound();
            }
            return View(appversion);
        }
		public async Task<IActionResult> DetelsAppVerstion(int Id)
		{
            bool r1 = ReadNamecookies();
            bool r2 = ReadRolecookies();

            if (r1 == false || r2 == false)
                return RedirectToAction("Index", "Home");
			var result = await _baseService.SendAsync(new RequestDto()
			{
				ApiType = SD.ApiType.GET,
				Url = SD.BaseUrl + "api/v1/User/Get-AppVersionById/" + Id,
			});
			//var response = await httpClient.GetAsync("/api/v1.0/User/Users");
			//var result = await response.Content.ReadFromJsonAsync<UserModel>();
			if (result != null && result.IsSuccess)
			{
				AppVersion? list = JsonConvert.DeserializeObject<AppVersion>(Convert.ToString(result.Result));
				return View(list);
			}
			else
			{
				TempData["error"] = result?.Message;
			}
			return NotFound();
		}
		public async Task<IActionResult> DeletedAppverstion(int id)
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
                    Url = SD.BaseUrl + "api/v1/User/Deleted-AppVersionById/" + id
                });
                if (result != null && result.IsSuccess)
                {
                    TempData["success"] = result.Message;
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["error"] = result?.Message;
                    return View();
                }

            }
            catch (Exception ex)
            {
                TempData["error"] = $"{ex.Message}";
                return NotFound();
            }
        }
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
