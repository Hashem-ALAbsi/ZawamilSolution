using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zawamil.Core.Models.Songes
{
    public class Song
    {
        [Key]
        [Required]
        public long Id { get; set; }
        public string Name { get; set; }

        //public string Name { get; set; } = string.Empty;
        public string Description { get; set; } 
        public bool IsDeleted { get; set; }

        public DateTime? DeletedAt { get; set; }

        public DateTime CtreateAt { get; set; }
        public string? SongUrl { get; set; }
    }
}
