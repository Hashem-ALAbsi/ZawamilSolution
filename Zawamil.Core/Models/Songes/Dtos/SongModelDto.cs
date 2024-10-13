using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zawamil.Core.Models.Songes.Dtos
{
    public class SongModelDto
    {
        public string? Message { get; set; }
        public bool IsAuthenticated { get; set; }
        public long id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? SongUrl { get; set; }

    }
}
