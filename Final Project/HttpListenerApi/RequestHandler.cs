using ApiLogic.Logic;
using ApiLogic.Models;
using Microsoft.Azure.Cosmos.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpListenerApi
{
    public class RequestHandler
    {

        public static void RequestHandle(object obj)
        {
            HttpListenerContext ctx = (HttpListenerContext)obj;
            HttpListenerRequest request = ctx.Request;
            using HttpListenerResponse resp = ctx.Response;

            string HttpMethod = request.HttpMethod;
            string RawUrl = request.RawUrl;


            System.IO.Stream body = request.InputStream;
            System.Text.Encoding encoding = request.ContentEncoding;
            System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
            string stringbody = reader.ReadToEnd();
            Console.WriteLine(RawUrl);
            Console.WriteLine(HttpMethod);
            string[] SplitRawUrl = RawUrl.Split("/");

            if (SplitRawUrl[2] == "user" && SplitRawUrl[3] == "create")
            {
                HandleCreateUser(stringbody, resp);
                body.Close();
                reader.Close();
                return;
            }
            if (  SplitRawUrl[2] == "user" && SplitRawUrl[3] == "login")
            {
                HandlLogin(stringbody, resp);
                body.Close();
                reader.Close();
                return;
            }

            System.Collections.Specialized.NameValueCollection headers = request.Headers;
            var authHeader = request.Headers["Authorization"];
            TokenLogic HandleToken = new TokenLogic(authHeader);
            bool Isvalid = HandleToken.IsValid;
            string username = HandleToken.UserName;
            if (!Isvalid)
            {
                resp.StatusCode = (int)HttpStatusCode.Unauthorized;
                resp.StatusDescription = "Unauthorized";
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes("Unauthorized");
                // Get a response stream and write the response to it.
                resp.ContentLength64 = buffer.Length;
                System.IO.Stream output = resp.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
                body.Close();
                reader.Close();
                return;
            }


            switch (SplitRawUrl[2].ToLower())
            {
                case "userbook":
                    switch (SplitRawUrl[3].ToLower())
                    {
                        case "search":
                            HandleSearchBook( stringbody, resp);
                            break;
                        case "create":
                            HandleCreateUserBook(username, stringbody, resp);
                            break;
                        case "delete":
                            HandleDeleteUserBook(username, stringbody, resp);
                            break;
                        case "get":
                            HandleGetMybooks(username, resp);
                            break;
                    }
                    break;
                    
                case "message":
                    switch (SplitRawUrl[3].ToLower())
                    {
                        case "create":
                            HandleCreateMessage(username, stringbody, resp);
                            break;
                        case "sent":
                            HandleSentMessages(username, resp);
                            break;
                        case "get":
                            HandleMyMessages(username, resp);
                            break;
                        
                    }

                    break;

                default: break;
            }


            body.Close();
            reader.Close();
        }
        public static void HandleCreateUser(string stringbody, HttpListenerResponse resp)
        {
            var response = UserLogic.CreateUser(stringbody).Result;
            if (response != null)
            {
                resp.StatusCode = (int)HttpStatusCode.OK;
                resp.StatusDescription = "Status OK";

                string json = JsonConvert.SerializeObject(response);
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(json);

                // Get a response stream and write the response to it. 
                resp.ContentLength64 = buffer.Length;
                System.IO.Stream output = resp.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
            }
            else
            {
                resp.StatusCode = (int)HttpStatusCode.Conflict;
                resp.StatusDescription = "Conflict";
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes("User already exsists");
                // Get a response stream and write the response to it.
                resp.ContentLength64 = buffer.Length;
                System.IO.Stream output = resp.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
            }
        }
        public static void HandlLogin( string stringbody, HttpListenerResponse resp )
        {
            (bool, string) response = UserLogic.Login(stringbody).Result;
            if (response.Item1)
            {
                resp.StatusCode = (int)HttpStatusCode.OK;
                resp.StatusDescription = "Status OK";
                string str =  TokenLogic.CreateToken(response.Item2);
                
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(str);

                // Get a response stream and write the response to it. 
                resp.ContentLength64 = buffer.Length;
                System.IO.Stream output = resp.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
            }
            else
            {
                resp.StatusCode = (int)HttpStatusCode.Unauthorized;
                resp.StatusDescription = "Unauthorized";
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes("Incorrect Username or Password");
                // Get a response stream and write the response to it.
                resp.ContentLength64 = buffer.Length;
                System.IO.Stream output = resp.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
            }
        }
        public static void HandleSearchBook(string stringbody, HttpListenerResponse resp)
        {
            UserBook[] response = UserBookLogic.SearchBook(stringbody).Result;
            if (response != null)
            {
                resp.StatusCode = (int)HttpStatusCode.OK;
                resp.StatusDescription = "Status OK";

                string json = JsonConvert.SerializeObject(response);
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(json);

                // Get a response stream and write the response to it. 
                resp.ContentLength64 = buffer.Length;
                System.IO.Stream output = resp.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
            }
            else
            {
                resp.StatusCode = (int)HttpStatusCode.NotFound;
                resp.StatusDescription = "NotFoundResult";
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes("User books with your params doesnt exsist");
                // Get a response stream and write the response to it.
                resp.ContentLength64 = buffer.Length;
                System.IO.Stream output = resp.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
            }
        }
        
        public static void HandleCreateUserBook(string useranme, string stringbody, HttpListenerResponse resp)
        {
            UserBook response = UserBookLogic.CreateUserBook(useranme, stringbody).Result;
            if (response != null)
            {
                resp.StatusCode = (int)HttpStatusCode.OK;
                resp.StatusDescription = "Status OK";

                string json = JsonConvert.SerializeObject(response);
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(json);

                // Get a response stream and write the response to it. 
                resp.ContentLength64 = buffer.Length;
                System.IO.Stream output = resp.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
            }
            else
            {
                resp.StatusCode = (int)HttpStatusCode.InternalServerError;
                resp.StatusDescription = "InternalServerError";
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes("Server eror");
                // Get a response stream and write the response to it.
                resp.ContentLength64 = buffer.Length;
                System.IO.Stream output = resp.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
            }
        }
        public static void HandleDeleteUserBook(string useranme, string stringbody, HttpListenerResponse resp)
        {
            UserBook response = UserBookLogic.DeleteUserBook(useranme, stringbody).Result;
            if (response != null)
            {
                resp.StatusCode = (int)HttpStatusCode.OK;
                resp.StatusDescription = "Status OK";

                string json = JsonConvert.SerializeObject(response);
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(json);

                // Get a response stream and write the response to it. 
                resp.ContentLength64 = buffer.Length;
                System.IO.Stream output = resp.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
            }
            else
            {
                resp.StatusCode = (int)HttpStatusCode.NotFound;
                resp.StatusDescription = "NotFoundResult";
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes("User books with your params doesnt exsist");
                // Get a response stream and write the response to it.
                resp.ContentLength64 = buffer.Length;
                System.IO.Stream output = resp.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
            }
        }
        public static void HandleGetMybooks(string useranme, HttpListenerResponse resp)
        {
            UserBook[] response = UserBookLogic.GetMyBooks(useranme).Result;
            if (response != null)
            {
                resp.StatusCode = (int)HttpStatusCode.OK;
                resp.StatusDescription = "Status OK";

                string json = JsonConvert.SerializeObject(response);
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(json);

                // Get a response stream and write the response to it. 
                resp.ContentLength64 = buffer.Length;
                System.IO.Stream output = resp.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
            }
            else
            {
                resp.StatusCode = (int)HttpStatusCode.NotFound;
                resp.StatusDescription = "NotFoundResult";
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes("User books with your params doesnt exsist");
                // Get a response stream and write the response to it.
                resp.ContentLength64 = buffer.Length;
                System.IO.Stream output = resp.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
            }
        }
        public static void HandleCreateMessage(string useranme, string stringbody, HttpListenerResponse resp)
        {
            Message response = MessageLogic.CreateMessage(useranme, stringbody).Result;
            if (response != null)
            {
                resp.StatusCode = (int)HttpStatusCode.OK;
                resp.StatusDescription = "Status OK";

                string json = JsonConvert.SerializeObject(response);
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(json);

                // Get a response stream and write the response to it. 
                resp.ContentLength64 = buffer.Length;
                System.IO.Stream output = resp.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
            }
            else
            {
                resp.StatusCode = (int)HttpStatusCode.InternalServerError;
                resp.StatusDescription = "InternalServerError";
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes("Server eror");
                // Get a response stream and write the response to it.
                resp.ContentLength64 = buffer.Length;
                System.IO.Stream output = resp.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
            }
        }
        public static void HandleSentMessages(string useranme, HttpListenerResponse resp)
        {
            Message[] response = MessageLogic.GetSentMessages(useranme).Result;
            if (response != null)
            {
                resp.StatusCode = (int)HttpStatusCode.OK;
                resp.StatusDescription = "Status OK";

                string json = JsonConvert.SerializeObject(response);
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(json);

                // Get a response stream and write the response to it. 
                resp.ContentLength64 = buffer.Length;
                System.IO.Stream output = resp.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
            }
            else
            {
                resp.StatusCode = (int)HttpStatusCode.NotFound;
                resp.StatusDescription = "NotFoundResult";
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes("you never sent a message");
                // Get a response stream and write the response to it.
                resp.ContentLength64 = buffer.Length;
                System.IO.Stream output = resp.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
            }
        }
        public static void HandleMyMessages(string useranme, HttpListenerResponse resp)
        {
            Message[] response = MessageLogic.GetMyMessages(useranme).Result;
            if (response != null)
            {
                resp.StatusCode = (int)HttpStatusCode.OK;
                resp.StatusDescription = "Status OK";

                string json = JsonConvert.SerializeObject(response);
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(json);

                // Get a response stream and write the response to it. 
                resp.ContentLength64 = buffer.Length;
                System.IO.Stream output = resp.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
            }
            else
            {
                resp.StatusCode = (int)HttpStatusCode.NotFound;
                resp.StatusDescription = "NotFoundResult";
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes("you never sent a message");
                // Get a response stream and write the response to it.
                resp.ContentLength64 = buffer.Length;
                System.IO.Stream output = resp.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
            }
        }
    }
}
