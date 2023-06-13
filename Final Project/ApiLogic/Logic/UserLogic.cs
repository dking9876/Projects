
using ApiLogic.Models;
using DataLayer.DbInterfaces;
using Newtonsoft.Json;



namespace ApiLogic.Logic
{
    public class UserLogic
    {
        public static async Task<(bool, string)> Login(string body)
        {
           
            var APIuser = JsonConvert.DeserializeObject<UserCreateModel>(body);
            
            UserDB userDb = new UserDB();
            try
            {
                var Checkuser = await userDb.CheckUser(APIuser.UserName, APIuser.Password, APIuser.City);
                return (true, APIuser.UserName);

            }
            catch (Exception ex)
            {
                return (false, "");
            }

        }
        public static async Task<UserUpdateModel> CreateUser(string body)
        {
            var APIuser = JsonConvert.DeserializeObject<UserCreateModel>(body);
            var DBuser = new DataLayer.Models.User() { id = APIuser.UserName, UserName = APIuser.UserName, Password = APIuser.Password, City = APIuser.City };

            UserDB userDb = new UserDB();
            try
            {
                var CreatedDBuser = await userDb.CreateUser(DBuser);
                var UserAPIMOdel = new UserUpdateModel() { UserName = CreatedDBuser.UserName, City = CreatedDBuser.City };
                return UserAPIMOdel;
            }
            catch (UserExistsException ex)
            {
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
 }


