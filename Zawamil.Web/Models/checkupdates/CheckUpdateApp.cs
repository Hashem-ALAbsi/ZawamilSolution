using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zawamil.Web.Models.checkupdates
{
    public record CheckUpdateAppResponse(int id, string submassage, string massage,bool IsAuthenticated);
    public record CheckAppVersinResponse(int id, string submassage, string massage,bool IsAuthenticated);
    //public class CheckUpdateApp
    //{

    //}
}
