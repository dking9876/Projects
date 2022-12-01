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
        void CreateUser(User user);
        
        User ReturnUser(string username);
        User ReturnAllUsers();
       


    }
}
