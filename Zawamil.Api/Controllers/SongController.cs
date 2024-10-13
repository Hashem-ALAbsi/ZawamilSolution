using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Zawamil.Core.Interface.ISongServices;
using Zawamil.Core.Models.Songes.Dtos;

namespace Zawamil.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0", Deprecated = true)]
    [ApiController]
    public class SongController : ControllerBase
    {

        private readonly ISongService _songService;
        private IWebHostEnvironment hostingEnv;
        public SongController(
        ISongService songService,
        IWebHostEnvironment environment)
        {
            _songService = songService;
            hostingEnv = environment;

        }

        [HttpGet("allsong")]
        public async Task<IActionResult> GetSongAsync()
        {
            var result = await _songService.GetSongAsync();
            return Ok(result);
        }
        
        [HttpGet("GetSong-ByID/{id}")]
        public async Task<IActionResult> GetSongByidAsync(int id)
        {
            var result = await _songService.GetSongByidAsync(id);

            return Ok(result);
        }
        
        [HttpGet("IsDeleted-Song")]
        public async Task<IActionResult> GetSongIsDeletedAsync()
        {
            var result = await _songService.GetSongIsDeletedAsync();
            return Ok(result);
        }

        [HttpPost("Create-Song")]
        public async Task<IActionResult> SwgCreateSongAsync([FromForm] SowaggerCreateSong model)
        {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var SongName = "";
            try
            {
                if (model.SongFile != null)
                {
                    SongName = Guid.NewGuid().ToString() + Path.GetExtension(model.SongFile.FileName);
                    string imagefile = GetFilePath();
                    using var filStrem = new FileStream(Path.Combine(imagefile + SongName),
                        FileMode.Create);
                    //model.SongFile.CopyTo(filStrem);
                    await model.SongFile.CopyToAsync(filStrem);
                }
                else
                {
                    return BadRequest("يرجى اضافة موسيقى");
                }
            }
            catch (Exception ex) { return BadRequest(ex.Message); }

                var result = await _songService.SwgCreateSongAsync(model,SongName);
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);
            return Ok(result);


        }
        
        [HttpPost("Update-Song/{id}")]
        public async Task<IActionResult> SwgUpdateSongAsync(int id,[FromForm] SowaggerCreateSong model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var SongName = "";
            if(model.SongFile != null)
            {
                SongName = Guid.NewGuid().ToString() + Path.GetExtension(model.SongFile.FileName);
                string imagefile = GetFilePath();
                using var filStrem = new FileStream(Path.Combine(imagefile + SongName),
                    FileMode.Create);
                await model.SongFile.CopyToAsync(filStrem);
            }
            else
            {
                SongName = null;
            }
            
            var result = await _songService.SwgUpdateSongAsync(id,model,SongName);
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);
            return Ok(result);


        }
        
        [HttpPost("Test-Create-Song")]
        public async Task<IActionResult> TestCreateSongAsync(IFormCollection formCollection, IFormFile file)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var SongName = "";
            if(file != null)
            {
                SongName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string imagefile = GetFilePath();
                using var filStrem = new FileStream(Path.Combine(imagefile + SongName),
                    FileMode.Create);
                await file.CopyToAsync(filStrem);
            }
            else
            {
                return BadRequest("يرجى اضافة موسيقى");
            }
            var model = JsonConvert.DeserializeObject<CreateSong>(
                formCollection
                ["createsong"]
                );
            var result = await _songService.CreateSongAsync(model,SongName);
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);
            return Ok(result);


        }
        
        
        [HttpPost("Test-Update-Song/{id}")]
        public async Task<IActionResult> TestCreateSongAsync(int id,IFormCollection formCollection, IFormFile? file)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var SongName = "";
            if(file != null)
            {
                SongName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string imagefile = GetFilePath();
                using var filStrem = new FileStream(Path.Combine(imagefile + SongName),
                    FileMode.Create);
                await file.CopyToAsync(filStrem);
            }
            else
            {
                SongName = null;
            }
            var model = JsonConvert.DeserializeObject<UpdateSong>(
                formCollection
                ["updatesong"]
                );
            var result = await _songService.UpdateSongAsync(id,model,SongName);
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);
            return Ok(result);


        }

        [HttpDelete("Active-song/{id}")]
        public async Task<IActionResult> ActiveSongAsync(int id)
        {
            var result = await _songService.ActiveSongAsync(id);
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);
            return Ok(result);
        }
        
        [HttpDelete("Deleted-song/{id}")]
        public async Task<IActionResult> DeleteSongAsync(int id)
        {
            var result = await _songService.DeleteSongAsync(id);
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);
            return Ok(result);
        }

        [HttpGet("GetAudioFile/{id}")]
        public async Task<IActionResult> GetAudioFile(int id)
        {
            var result = await _songService.GetSongByidAsync(id);
            // Define the path to the audio file
            var filePath = result.SongUrl;
                //Path.Combine("Path/To/Your/AudioFiles", filename);

            // Check if the file exists
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            // Get the content type based on the file extension
            var contentType = "audio/mpeg"; // Default content type. Adjust as necessary.

            // Return the audio file
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return File(fileStream, contentType, result.Name);
        }

        [NonAction]
        private string GetFilePath()
        {
            return hostingEnv.WebRootPath + "\\SongesMP3\\";
        }


    }
}
