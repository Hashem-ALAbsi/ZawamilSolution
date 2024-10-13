namespace Zawamil.Web.Models.Users
{
    public class AppVersion
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateAt { get; set; }
        public bool IsList { get; set; }
    }

    public class CreateAppVersionModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
       // public bool IsList { get; set; }
    }
}
