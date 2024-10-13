namespace Zawamil.Web.Utility
{
    public class SD
    {
        public static string CouponAPIBase { get; set; }
        public static string BaseUrl { get; set; }
        public static string ProductAPIBase { get; set; }
        public static string UserRole { get; set; }
        public static string UserId { get; set; }
        public static string UserName { get; set; }
        public const string RoleAdmin = "ADMIN";
        public const string RoleCustomer = "CLIENT";
        public const string TokenCookie = "JWTToken";
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }

        public const string Status_Pending = "Pending";
        public const string Status_Approved = "Approved";
        public const string Status_ReadyForPickup = "ReadyForPickup";
        public const string Status_Completed = "Completed";
        public const string Status_Refunded = "Refunded";
        public const string Status_Cancelled = "Cancelled";

        public enum ContentType
        {
            Json,
            MultipartFormData,
        }
    }
}
