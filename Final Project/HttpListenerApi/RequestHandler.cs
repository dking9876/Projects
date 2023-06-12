using ApiLogic.Logic;
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


            if (HttpMethod == "POST" && SplitRawUrl[1] == "api" && SplitRawUrl[2] == "user" && SplitRawUrl[4] == "login")
            {
                bool response = UserLogic.Login(stringbody).Result;
                if (response)
                {
                    resp.StatusCode = (int)HttpStatusCode.OK;
                    resp.StatusDescription = "Status OK";
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes("Welcome");
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
                body.Close();
                reader.Close();

            }
        }

    }
}
