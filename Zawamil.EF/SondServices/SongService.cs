using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zawamil.Core.Interface.ISongServices;
using Zawamil.Core.Models.Songes;
using Zawamil.Core.Models.Songes.Dtos;

namespace Zawamil.EF.SondServices
{
    public class SongService : ISongService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string Pathurl;

        public SongService(
           ApplicationDbContext context,
           IWebHostEnvironment webHostEnvironment)

        {
            //2Logo of the technical center
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            Pathurl = Path.Combine(@"wwwroot", "SongesMP3/");
            //Pathurl = Path.Combine(webHostEnvironment.WebRootPath, "SongesMP3/");

        }


        public async Task<SongModelDto> SwgCreateSongAsync(SowaggerCreateSong model, string url)
        {
            try
            {
                var song = new Song
                {
                    Name = model.Name,
                    Description = model.Description,
                    SongUrl = url,
                    CtreateAt = DateTime.Now,
                    IsDeleted = false,
                    
                };
                await _context.Songs.AddAsync(song);
                _context.SaveChanges();
                return new SongModelDto
                {
                    Message = "تمت العملية بنجاح",
                    id = song.Id,
                    IsAuthenticated = true,
                    SongUrl = song.SongUrl,
                    Name = song.Name,
                    Description = song.Description
                };
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
            //throw new NotImplementedException();
        }
        
        
        public async Task<SongModelDto> CreateSongAsync(CreateSong model, string url)
        {
            try
            {
                var song = new Song
                {
                    Name = model.Name,
                    Description = model.Description,
                    SongUrl = url,
                    CtreateAt = DateTime.Now,
                    IsDeleted = false,
                    
                };
                await _context.Songs.AddAsync(song);
                _context.SaveChanges();
                return new SongModelDto
                {
                    Message = "تمت العملية بنجاح",
                    id = song.Id,
                    IsAuthenticated = true,
                    SongUrl = song.SongUrl,
                    Name = song.Name,
                    Description = song.Description
                };
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
            //throw new NotImplementedException();
        }
        public async Task<SongModelDto> SwgUpdateSongAsync(int id, SowaggerCreateSong model, string? url)
        {
            try
            {
                var song = await _context.Songs
                .SingleOrDefaultAsync(m => m.Id == id);
                if (song == null)
                    return new SongModelDto { Message = "لايوجد هذه الاغنية", IsAuthenticated = false };
                if(url != null)
                    File.Delete(Path.Combine(_webHostEnvironment.WebRootPath + "\\SongesMP3\\", song.SongUrl));
                song.Name = model.Name != null ? model.Name : song.Name;
                song.Description = model.Description != null ? model.Description : song.Description;
                song.SongUrl = url != null ? url : song.SongUrl;
                _context.SaveChanges();
                return new SongModelDto
                {
                    Message = "تمت العملية بنجاح",
                    id = song.Id,
                    IsAuthenticated = true,
                    SongUrl = song.SongUrl,
                    Name = song.Name,
                    Description = song.Description
                };
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }

        }
        
        public async Task<SongModelDto> UpdateSongAsync(int id, UpdateSong model, string? url)
        {
            try
            {
                var song = await _context.Songs
                .SingleOrDefaultAsync(m => m.Id == id);
                if (song == null)
                    return new SongModelDto { Message = "لايوجد هذه الاغنية", IsAuthenticated = false };
                if (url != null)
                    File.Delete(Path.Combine(_webHostEnvironment.WebRootPath + "\\SongesMP3\\", song.SongUrl));
                song.Name = model.Name != null ? model.Name : song.Name;
                song.Description = model.Description != null ? model.Description : song.Description;
                song.SongUrl = url != null ? url : song.SongUrl;
                _context.SaveChanges();
                return new SongModelDto
                {
                    Message = "تمت العملية بنجاح",
                    id = song.Id,
                    IsAuthenticated = true,
                    SongUrl = song.SongUrl,
                    Name = song.Name,
                    Description = song.Description
                };
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }

        }

        public async Task<SongModelDto> ActiveSongAsync(int id)
        {
            try
            {
                var song = await _context.Songs
               .SingleOrDefaultAsync(m => m.Id == id);
                if (song == null)
                    return new SongModelDto { Message = "لايوجد هذه الاغنية", IsAuthenticated = false };
                song.IsDeleted = false;
                _context.SaveChanges();
                return new SongModelDto
                {
                    Message = "تمت العملية بنجاح",
                    id = song.Id,
                    IsAuthenticated = true,
                    SongUrl = song.SongUrl,
                    Name = song.Name,
                    Description = song.Description
                };
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }


        public async Task<SongModelDto> DeleteSongAsync(int id)
        {
            try
            {
                var song = await _context.Songs
               .SingleOrDefaultAsync(m => m.Id == id);
                if (song == null)
                    return new SongModelDto { Message = "لايوجد هذه الاغنية", IsAuthenticated = false };
				File.Delete(Path.Combine(_webHostEnvironment.WebRootPath + "\\SongesMP3\\", song.SongUrl));
			
                 _context.Songs.Remove(song);

                _context.SaveChanges();
                return new SongModelDto
                {
                    Message = "تمت العملية بنجاح",
                    id = 0,
                    IsAuthenticated = true,
                    SongUrl = "",
                    Name = "",
                    Description = ""
                };
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }

        public async Task<SongModel> GetSongByidAsync(int id)
        {
            try
            {
                var song = await _context.Songs
                    .Where(m => m.Id == id)
                    .FirstOrDefaultAsync();
                if (song == null)
                    return new SongModel { id = 0, Name = "لاتوجد هذه الاغنية" };
                return new SongModel
                {
                    id = song.Id,
                    Name = song.Name,
                    Description = song.Description,
                    CreatedAt = song.CtreateAt.ToShortDateString(),
					SongUrl = Pathurl + song.SongUrl,
                };
            }catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }
        public async Task<IEnumerable<SongModel>> GetSongAsync()
        {
            try
            {
                var song = await _context.Songs
                    .Where(m => m.IsDeleted == false)
                    .OrderByDescending(m =>m.Id)
                    .Select(m => new SongModel
                    {
                        id = m.Id,
                        Name = m.Name,
                        Description = m.Description,
                        CreatedAt = m.CtreateAt.ToShortDateString(),
                        SongUrl = Pathurl + m.SongUrl,
                    })
                    .ToListAsync();
                if(song == null)
                    return Enumerable.Empty<SongModel>();
                return song;

            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }


        public async Task<IEnumerable<SongModel>> GetSongIsDeletedAsync()
        {
            try
            {
                var song = await _context.Songs
                    .Where(m => m.IsDeleted == true)
                    .Select(m => new SongModel
                    {
                        id = m.Id,
                        Name = m.Name,
                        Description = m.Description,
                        CreatedAt = m.CtreateAt.ToShortDateString(),
                        SongUrl = Pathurl + m.SongUrl,
                    }).ToListAsync();
                if (song == null)
                    return Enumerable.Empty<SongModel>();
                return song;

            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }

        
    }
}
