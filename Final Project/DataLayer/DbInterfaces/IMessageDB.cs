using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Models;

namespace DataLayer.DbInterfaces
{
    internal interface IMessageDB
    {
        Task<Message> CreateMessage(Message message);

        Task<Message[]> GetAllMessageToUser(string username);

        Task<Message[]> GetAllMessageSentByUser(string username);
        Task<Message> DeleteMessage(Message message);
    }
}
