using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zawamil.Core.Models.Users.DTOs
{
    public class CreateUser
    {
        
        [StringLength(50)]
        public string Username { get; set; }

        [StringLength(256)]
        public string Password { get; set; }
        //public int Roles { get; set; }
    }
}
