using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zawamil.Core.Models.Users
{
    public class AppVersion
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public bool IsList { get; set; } = false;
    }

    //public class CreateAppVersionModel
    //{
    //    public string Name { get; set; }
    //    public bool IsList { get; set; }
    //}
}
