

namespace ApiLogic.Models
{
    public class UserCreateModel
    {
        public UserCreateModel() { }
        public UserCreateModel(DataLayer.Models.User DBUser)
        {

            UserName = DBUser.UserName;
            Password = DBUser.Password;
            City = DBUser.City;
            
        }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string City { get; set; }
        public DataLayer.Models.User GetUserDB()
        {
            return new DataLayer.Models.User() { id = UserName, City = City, UserName = UserName, Password = Password,};
        }

    }

    public class UserUpdateModel
    {
        public string UserName { get; set; }
        public string City { get; set; }
    }
}
