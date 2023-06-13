using ApiLogic.Models;
using DataLayer.DbInterfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLogic.Logic
{
    public class MessageLogic
    {
        public static async Task<Message> CreateMessage(string username, string body)
        {
            var APIMessage = JsonConvert.DeserializeObject<Models.Message>(body);
            APIMessage.source = username;
            var DBMessage = APIMessage.GetMessageDB();

            MessageDB message = new MessageDB();
            try
            {
                var CreatedDBmessage = await message.CreateMessage(DBMessage);
                var MessageAPIMOdel = new Models.Message(CreatedDBmessage);
                return MessageAPIMOdel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static async Task<Message[]> GetSentMessages(string userName)
        {

            MessageDB message = new MessageDB();
            try
            {
                var SentMessagesArrayDb = await message.GetAllMessageSentByUser(userName);
                var MessagesArrayAPI = new Models.Message[SentMessagesArrayDb.Length];

                if (SentMessagesArrayDb.Length == 0)
                {
                    return null;
                    //return null;
                }
                for (int i = 0; i < SentMessagesArrayDb.Length; i++)
                {
                    MessagesArrayAPI[i] = new ApiLogic.Models.Message(SentMessagesArrayDb[i]);
                }

                return MessagesArrayAPI;

            }
            catch (Exception ex)
            {
                return null;

            }

        }
        public static async Task<Message[]> GetMyMessages( string userName)
        {
            MessageDB message = new MessageDB();
            try
            {
                var SentMessagesArrayDb = await message.GetAllMessageToUser(userName);
                var MessagesArrayAPI = new Models.Message[SentMessagesArrayDb.Length];

                if (SentMessagesArrayDb.Length == 0)
                {
                    return null;
                    //return null;
                }
                for (int i = 0; i < SentMessagesArrayDb.Length; i++)
                {
                    MessagesArrayAPI[i] = new ApiLogic.Models.Message(SentMessagesArrayDb[i]);
                }

                return MessagesArrayAPI;

            }
            catch (Exception ex)
            {
                return null;

            }
        }
    }
}
