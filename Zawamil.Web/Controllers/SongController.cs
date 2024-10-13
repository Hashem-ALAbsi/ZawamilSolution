using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Zawamil.Web.Models;
using Zawamil.Web.Models.Songes.Dtos;
using Zawamil.Web.Service.IService;
using Zawamil.Web.Utility;

namespace Zawamil.Web.Controllers
{
    public class SongController : Controller
    {
        private readonly IBaseService _baseService;
        private readonly ITokenProvider _tokenProvider;
        private readonly HttpClient httpClient;

        public SongController(IBaseService baseService, ITokenProvider tokenProvider, HttpClient httpClient)
        {
            _baseService = baseService;
            _tokenProvider = tokenProvider;
            this.httpClient = httpClient;
        }

        public async Task<IActionResult> SongIndex()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            bool r1 = ReadNamecookies();
            bool r2 = ReadRolecookies();
            if (r1 == false )
                return RedirectToAction("Index", "Home");
            List<SongModel>? list = new();
            try
            {
                var result = await _baseService.SendAsync(new RequestDto()
                {
                    ApiType = SD.ApiType.GET,
                    Url = SD.BaseUrl + "api/v1/Song/allsong"
                });
                //var response = await httpClient.GetAsync("/api/v1.0/User/Users");
                //var result = await response.Content.ReadFromJsonAsync<UserModel>();
                if (result != null && result.IsSuccess)
                {
                    list = JsonConvert.DeserializeObject<List<SongModel>>(Convert.ToString(result.Result));
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

  //      [HttpGet]
  //      public async Task<IActionResult> IsDeletedSong()
  //      {
  //          if (!ModelState.IsValid)
  //              return BadRequest(ModelState);
  //          if (SD.UserName == null)
  //              return RedirectToAction("Index", "Home");
  //          List<SongModel>? list = new();
  //          try
  //          {
  //              var result = await _baseService.SendAsync(new RequestDto()
  //              {
  //                  ApiType = SD.ApiType.GET,
  //                  Url = SD.BaseUrl + "api/v1/Song/IsDeleted-Song"
		//		});
  //              //var response = await httpClient.GetAsync("/api/v1.0/User/Users");
  //              //var result = await response.Content.ReadFromJsonAsync<UserModel>();
  //              if (result != null && result.IsSuccess)
  //              {
  //                  list = JsonConvert.DeserializeObject<List<SongModel>>(Convert.ToString(result.Result));
  //              }
  //              else
  //              {
  //                  TempData["error"] = result?.Message;
  //              }
  //          }
  //          catch (Exception ex)
  //          {
  //              TempData["error"] = $"{ex.Message}";
  //              return NotFound();
  //          }
		//	//return RedirectToAction("IsDeletedSong", "Song");
		//	return View("IsDeletedSong",list);
		//}

        public IActionResult CreateSong()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateSong(SowaggerCreateSong song)
        {
            //if (!ModelState.IsValid)
            //	return BadRequest(ModelState);
            bool r1 = ReadNamecookies();
            bool r2 = ReadRolecookies();
            if (r1 == false)
                return RedirectToAction("Index", "Home");
            //var file = HttpContext.Request.Form.Files;
            //var file = HttpContext.Request.Form.Files;
            //if (file.Count() > 0)
            //{
            //	song.SongFile = file[0];
            //	//string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
            //	//var filStrem = new FileStream(Path.Combine(@"wwwroot/", "Images", ImageName), FileMode.Create);
            //	//file[0].CopyTo(filStrem);
            //	//cls_Studenty.StuImage = ImageName;
            //	////E:\Visual Studio 2022\projects\BokarRare\wwwroot\Images\
            //}
            //else
            //{
            //	TempData["error"] = "يرجى اختيار موسيقى";
            //	return View();
            //}
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _baseService.SendAsync(new RequestDto()
                    {
                        ApiType = SD.ApiType.POST,
                        Data = song,
                        ContentType = SD.ContentType.MultipartFormData,
                        Url = SD.BaseUrl + "api/v1/Song/Create-Song"
                    });
                    if (result != null && result.IsSuccess)
                    {
                        TempData["success"] = result.Message;
                        return RedirectToAction(nameof(SongIndex));
                        //return View(song);
                    }
                    else
                    {
                        TempData["error"] = result?.Message;
                        return View();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = $"{ex.Message}";
                return NotFound();
            }
            return View(song);

        }

        public async Task<IActionResult> UpdateSong(int songId)
        {
            bool r1 = ReadNamecookies();
            bool r2 = ReadRolecookies();
            if (r1 == false)
                return RedirectToAction("Index", "Home");
            var result = await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.BaseUrl + "api/v1/Song/GetSong-ByID/" + songId,
            });
            //var response = await httpClient.GetAsync("/api/v1.0/User/Users");
            //var result = await response.Content.ReadFromJsonAsync<UserModel>();
            if (result != null && result.IsSuccess)
            {
                SongModel? list = JsonConvert.DeserializeObject<SongModel>(Convert.ToString(result.Result));
                SowaggerCreateSong? song = new();
                song.Id = list!.id;
                song.Name = list.Name;
                song.Description = list.Description;
                //song.SongFile = null;
                ViewData["SongUrl"] = list.SongUrl;

                return View("CreateSong", song);
            }
            else
            {
                TempData["error"] = result?.Message;

            }
            return NotFound();
            //return View("CreateUser",);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateSong(SowaggerCreateSong song)
        {
            //if (!ModelState.IsValid)
            //	return BadRequest(ModelState);
            bool r1 = ReadNamecookies();
            bool r2 = ReadRolecookies();
            if (r1 == false)
                return RedirectToAction("Index", "Home");
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _baseService.SendAsync(new RequestDto()
                    {
                        ApiType = SD.ApiType.POST,
                        Data = song,
                        Url = SD.BaseUrl + "api/v1/Song/Update-Song/" + song.Id,
                        ContentType = SD.ContentType.MultipartFormData,
                    });
                    if (result != null && result.IsSuccess)
                    {
                        TempData["success"] = result.Message;
                        //TempData["success"] = "User created successfully";
                        return RedirectToAction(nameof(SongIndex));
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
            return View(song);
        }

        public async Task<IActionResult> DetelsSong(int songId)
        {
            bool r1 = ReadNamecookies();
            bool r2 = ReadRolecookies();
            if (r1 == false)
                return RedirectToAction("Index", "Home");
            var result = await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.BaseUrl + "api/v1/Song/GetSong-ByID/" + songId,
            });
            //var response = await httpClient.GetAsync("/api/v1.0/User/Users");
            //var result = await response.Content.ReadFromJsonAsync<UserModel>();
            if (result != null && result.IsSuccess)
            {
                SongModel? list = JsonConvert.DeserializeObject<SongModel>(Convert.ToString(result.Result));
                return View(list);
            }
            else
            {
                TempData["error"] = result?.Message;
            }
            return NotFound();
        }

        public async Task<IActionResult> ActiveSong(int id)
        {
            bool r1 = ReadNamecookies();
            bool r2 = ReadRolecookies();
            if (r1 == false)
                return RedirectToAction("Index", "Home");
            try
            {
                var result = await _baseService.SendAsync(new RequestDto()
                {
                    ApiType = SD.ApiType.GET,
                    Url = SD.BaseUrl + "api/v1/Song/Active-song/" + id
                });
                if (result != null && result.IsSuccess)
                {
                    TempData["success"] = result.Message;
                    return RedirectToAction(nameof(SongIndex));
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
        
        public async Task<IActionResult> DeleteSong(int id)
        {
            bool r1 = ReadNamecookies();
            bool r2 = ReadRolecookies();
            if (r1 == false)
                return RedirectToAction("Index", "Home");
            try
            {
                var result = await _baseService.SendAsync(new RequestDto()
                {
                    ApiType = SD.ApiType.DELETE,
                    Url = SD.BaseUrl + "api/v1/Song/Deleted-song/"+id
                });
                if (result != null && result.IsSuccess)
                {
                    TempData["success"] = result.Message;
                    return RedirectToAction(nameof(SongIndex));
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