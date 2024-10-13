namespace Zawamil.Web.Models.Users
{
    public class ApplicationUser
    {

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual UserRoles? UserRoles { get; set; }
        public virtual UserState? UserState { get; set; }
    }
}
