namespace vuejs_backend.Models
{

    public class UserInfo
    {
        public int id { get; set; }
        public string user_name { get; set; }

        public string user_pass { get; set; }

        public int role_id { get; set; }
    }
    public class UserData
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string? sure_name { get; set; }
        public string email { get; set; }
        public string tel { get; set; }
    }
}