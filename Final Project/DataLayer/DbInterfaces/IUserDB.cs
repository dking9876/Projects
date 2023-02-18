using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DbInterfaces
{
    internal interface IUserDB
    {
        Task<User> CreateUser(User user);

        Task<User> GetUser(string username);

        Task<User> UpdateUsername(User user, string newUsername);
        Task<User> DeleteUser(User user);

    }
}
