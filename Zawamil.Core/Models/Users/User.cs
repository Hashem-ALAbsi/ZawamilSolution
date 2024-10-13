using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zawamil.Core.Models.Users
{
    public class User
    {

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual UserRoles? UserRoles { get; set; }
        public virtual UserState? UserState { get; set; }
    }
}
