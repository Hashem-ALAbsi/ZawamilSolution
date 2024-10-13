using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zawamil.Web.Models.Users.DTOs
{
    public class UserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string? Password { get; set; }
        public string? CreatedAt { get; set; }
        public string ? UserState { get; set; }
        public string ? UserRole { get; set; }
        public int ? NoUserRole { get; set; }
    }
}
