
using ApiLogic.Models;
using DataLayer.DbInterfaces;
using Newtonsoft.Json;



namespace ApiLogic.Logic
{
    public class UserLogic
    {
        public static async Task<bool> Login(string body)
        {
           
            var APIuser = JsonConvert.DeserializeObject<UserCreateModel>(body);
            
            UserDB userDb = new UserDB();
            try
            {
                var Checkuser = await userDb.CheckUser(APIuser.UserName, APIuser.Password, APIuser.City);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
