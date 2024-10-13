using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zawamil.Core.Models.Users.DTOs
{
    public class UserModelDto
    {
        public string? Message { get; set; }
        public bool IsAuthenticated { get; set; }
        public int? id { get; set; }
        public string? UserName { get; set; }
        public string? UserState { get; set; }
        public string? UserRole { get; set; }
        public int? NoUserRole { get; set; }
    }
}
