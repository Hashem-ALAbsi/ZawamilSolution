using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zawamil.Core.Models.Songes.Dtos
{
    public class CreateSong
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class SowaggerCreateSong
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile? SongFile { get; set; }
    }
    
    public class SowaggerUpdateSong
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public IFormFile? SongFile { get; set; }
    }
    public class UpdateSong
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
