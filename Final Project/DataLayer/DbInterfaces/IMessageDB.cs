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

        Task<Message[]> GetAllMessageToUser(User user);

        Task<Message[]> GetAllMessageSentByUser(User user);
        Task<Message> DeleteMessage(Message message);
    }
}
