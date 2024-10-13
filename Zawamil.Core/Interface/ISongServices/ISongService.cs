using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zawamil.Core.Models.Songes.Dtos;

namespace Zawamil.Core.Interface.ISongServices
{
    public interface ISongService
    {
        Task<SongModelDto> CreateSongAsync(CreateSong model,string url);
        Task<SongModelDto> SwgCreateSongAsync(SowaggerCreateSong model, string url);
        Task<SongModelDto> UpdateSongAsync(int id, UpdateSong model,string? url);
        Task<SongModelDto> SwgUpdateSongAsync(int id, SowaggerCreateSong model,string? url);
        Task<SongModelDto> DeleteSongAsync(int id);
        Task<SongModelDto> ActiveSongAsync(int id);
        Task<IEnumerable<SongModel>> GetSongAsync();
        Task<IEnumerable<SongModel>> GetSongIsDeletedAsync();
        Task<SongModel> GetSongByidAsync(int id);
    }
}
